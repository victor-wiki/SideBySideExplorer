using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using System.Collections;
using System.IO;
using SideBySideExplorer.Helper;
using SideBySideExplorer.Model;
using System.Threading;

namespace SideBySideExplorer.Controls
{
    public delegate void ClipboardChangeHandler(object sender, FileClipBoard clipBoard);

    public partial class UC_Explorer : UserControl
    {
        private CancellationTokenSource cancellationTokenSource;
        private bool isMoving = false;
        private bool isCanceled = false;
        public FileClipBoard ClipBoard { get; set; }

        public ClipboardChangeHandler ClipboardChange;
        public FeedbackHandler OnFeedback;

        public UC_Explorer()
        {
            InitializeComponent();
        }

        private void UC_Explorer_Load(object sender, EventArgs e)
        {
        }

        public void Init()
        {
            this.LoadDrives();
            this.InitTree();
            this.InitDragDrop(this.navigator);
            this.InitDragDrop(this.lvFiles);
            this.InitFileList();
        }

        private void InitFileList()
        {
            this.lvFiles.SelectedBackColor = ColorTranslator.FromHtml("#CCE8FF");

            this.olvSize.AspectGetter = delegate (object x)
            {
                MyFileSystemInfo info = x as MyFileSystemInfo;

                if (info.IsDirectory)
                    return (long)-1;

                try
                {
                    return info.AsFile.Length;
                }
                catch (System.IO.FileNotFoundException)
                {
                    return (long)-2;
                }
            };

            this.olvSize.AspectToStringConverter = delegate (object x)
            {
                long sizeInBytes = (long)x;

                if (sizeInBytes < 0)
                {
                    return "";
                }
                return FileHelper.FormatFileSize(sizeInBytes);
            };

            this.olvSize.MakeGroupies(new long[] { 0, 1024 * 1024, 512 * 1024 * 1024 },
                new string[] { "Folders", "Small", "Big", "Disk space chewer" });

            this.olvCreated.GroupKeyGetter = delegate (object x)
            {
                DateTime dt = ((MyFileSystemInfo)x).CreationTime;
                return new DateTime(dt.Year, dt.Month, 1);
            };
            this.olvCreated.GroupKeyToTitleConverter = delegate (object x)
            {
                return ((DateTime)x).ToString("yyyy MM");
            };

            this.olvModified.GroupKeyGetter = delegate (object x)
            {
                DateTime dt = ((MyFileSystemInfo)x).LastWriteTime;
                return new DateTime(dt.Year, dt.Month, 1);
            };

            this.olvModified.GroupKeyToTitleConverter = delegate (object x)
            {
                return ((DateTime)x).ToString("yyyy MM");
            };
        }

        private void InitDragDrop(ObjectListView listView)
        {
            listView.IsSimpleDragSource = true;
            listView.IsSimpleDropSink = true;

            SimpleDropSink dropSink = (SimpleDropSink)listView.DropSink;
            dropSink.AcceptExternal = true;
            dropSink.CanDropBetween = true;
            dropSink.CanDropOnBackground = true;

            listView.ModelCanDrop += delegate (object sender, ModelDropEventArgs e)
            {
                bool isNavigator = this.IsNavigator(e.ListView);

                e.Handled = true;
                e.Effect = DragDropEffects.None;

                if (e.SourceModels.Contains(e.TargetModel))
                {
                    e.InfoMessage = "Cannot drop on self";
                    return;
                }
                else if (e.TargetModel is MyFileSystemInfo info)
                {
                    string targetFolder = info.AsDirectory.FullName;

                    if (info.IsDirectory)
                    {
                        if (e.SourceModels.OfType<MyFileSystemInfo>().Any(item => item.IsDirectory && targetFolder.StartsWith(item.AsDirectory.FullName)))
                        {
                            e.InfoMessage = "Cannot drop on descendant";
                            return;
                        }
                    }
                }
                else
                {
                    if (e.SourceModels.OfType<MyFileSystemInfo>().Any(item => item.IsDrive))
                    {
                        return;
                    }

                    if (isNavigator)
                    {
                        if (e.TargetModel == null)
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (e.ListView.Tag == null)
                        {
                            return;
                        }
                    }                    
                }

                e.Effect = DragDropEffects.Move;
            };

            listView.ModelDropped += delegate (object sender, ModelDropEventArgs e)
            {
                bool isNavigator = this.IsNavigator(e.ListView);

                MyFileSystemInfo targetModel = null;

                if (isNavigator)
                {
                    targetModel = e.TargetModel as MyFileSystemInfo;
                }
                else
                {
                    targetModel = e.ListView.Tag as MyFileSystemInfo;
                }

                if (targetModel == null)
                {
                    return;
                }

                this.MoveFiles(e.SourceListView, e.ListView, e.SourceModels.OfType<MyFileSystemInfo>(), targetModel, FileMoveMode.Drag);

                e.RefreshObjects();

                e.Effect = DragDropEffects.None;
            };
        }

        private async void MoveFiles(ObjectListView sourceListView, ObjectListView targetListView, IEnumerable<MyFileSystemInfo> sources, MyFileSystemInfo target, FileMoveMode mode = FileMoveMode.None)
        {
            if (mode == FileMoveMode.None)
            {
                return;
            }

            long totalSize = 0;
            int fileCount = 0;
            long totalTransferred = 0;

            foreach (MyFileSystemInfo source in sources)
            {
                if (source.IsDirectory)
                {
                    fileCount += FileHelper.GetFolderFileCount(source.AsDirectory);
                    totalSize += FileHelper.GetFolderSize(source.AsDirectory);
                }
                else
                {
                    totalSize += source.AsFile.Length;
                    fileCount++;
                }
            }

            this.cancellationTokenSource = new CancellationTokenSource();

            FileCopyInfo info = new FileCopyInfo() { TotalSize = totalSize, FileCount = fileCount };

            Dictionary<string, TransferProgress> fileTransferred = new Dictionary<string, TransferProgress>();

            Action<TransferProgress> fileCopyProgress = (progress) =>
            {
                if (this.isCanceled)
                {
                    return;
                }

                if (!fileTransferred.ContainsKey(progress.SourcePath))
                {
                    fileTransferred.Add(progress.SourcePath, progress);
                }
                else
                {
                    fileTransferred[progress.SourcePath] = progress;
                }

                totalTransferred = fileTransferred.Sum(item => item.Value.Transferred);
                info.FinishedCount = fileTransferred.Where(item => item.Value.Transferred == item.Value.Total).Count();

                info.TotalTransferred = totalTransferred;
                info.CurrentSize = progress.StreamSize;
                info.CurrentTransferred = progress.Transferred;
                info.CurrentPercent = progress.Fraction;
                info.TotalPercent = totalTransferred / (totalSize * 1.0);

                this.Feedback($"Progress:({info.FinishedCount}/{info.FileCount}) {FileHelper.FormatFileSize(info.TotalTransferred)}/{FileHelper.FormatFileSize(info.TotalSize)}, {info.TotalPercent.ToString("P")}");
            };

            try
            {
                this.isCanceled = false;
                this.isMoving = true;

                foreach (MyFileSystemInfo source in sources)
                {
                    if (this.isCanceled)
                    {
                        break;
                    }

                    if (source.IsDirectory)
                    {
                        DirectoryInfo sourceFolder = source.AsDirectory;

                        string targetFolder = Path.Combine(target.FullName, sourceFolder.Name);

                        if (FileHelper.IsSameDrive(source.AsDirectory.FullName, targetFolder) && (mode == FileMoveMode.Cut || mode == FileMoveMode.Drag))
                        {
                            await FileTransferManager.MoveWithProgressAsync(source.AsDirectory.FullName, targetFolder, fileCopyProgress, this.cancellationTokenSource.Token);
                        }
                        else
                        {
                            await FileHelper.CopyFolder(source.AsDirectory.FullName, targetFolder, true, fileCopyProgress, this.cancellationTokenSource.Token);

                            if (mode == FileMoveMode.Cut)
                            {
                                source.AsDirectory.Delete(true);
                            }
                        }
                    }
                    else
                    {
                        FileInfo sourceFile = source.AsFile;

                        string targetFilePath = Path.Combine(target.FullName, sourceFile.Name);

                        if (FileHelper.IsSameDrive(sourceFile.DirectoryName, target.FullName) && (mode == FileMoveMode.Cut || mode == FileMoveMode.Drag))
                        {
                            await FileHelper.MoveFile(sourceFile.FullName, targetFilePath, fileCopyProgress, this.cancellationTokenSource.Token);
                        }
                        else
                        {
                            await FileHelper.CopyFile(sourceFile.FullName, targetFilePath, fileCopyProgress, this.cancellationTokenSource.Token);

                            if (mode == FileMoveMode.Cut)
                            {
                                sourceFile.Delete();
                            }
                        }
                    }
                }

                this.MoveObjects(sourceListView, targetListView, sources, target, mode);
            }
            catch (Exception ex)
            {
                this.Feedback(ex.Message, true);
            }
            finally
            {
                this.isMoving = false;

                if(mode != FileMoveMode.Copy)
                {
                    this.RefreshListView(this.navigator);
                }               

                this.RefreshListView(this.lvFiles);
            }
        }

        private void MoveObjects(ObjectListView sourceListView, ObjectListView targetListView, IEnumerable<MyFileSystemInfo> sources, MyFileSystemInfo target, FileMoveMode mode = FileMoveMode.None)
        {
            foreach (MyFileSystemInfo x in sources)
            {
                bool isCopy = mode == FileMoveMode.Copy || (mode == FileMoveMode.Drag && !FileHelper.IsSameDrive(x.FullName, target.FullName));

                if (!isCopy)
                {
                    if (!(sourceListView is TreeListView))
                    {
                        sourceListView.RemoveObject(x);
                    }

                    x.Parent.Children.Remove(x);
                }

                if (!(targetListView is TreeListView))
                {
                    if (!targetListView.Objects.OfType<MyFileSystemInfo>().Any(item => item.Name == x.Name))
                    {
                        targetListView.AddObject(x);
                    }
                }

                x.Parent = target;

                if (!target.Children.Any(item => item.Name == x.Name))
                {
                    target.Children.Add(x);
                }
            }
        }

        private void InitTree()
        {
            SysImageListHelper helper = new SysImageListHelper(this.navigator);

            this.olvFileName.ImageGetter = this.olvName.ImageGetter = delegate (object x)
            {
                var info = (MyFileSystemInfo)x;

                return helper.GetImageIndex(info.FullName);
            };

            this.navigator.CanExpandGetter = delegate (object x)
            {
                return ((MyFileSystemInfo)x).IsDirectory;
            };

            this.navigator.ChildrenGetter = delegate (object x)
            {
                return this.GetChildren(x as MyFileSystemInfo, true);
            };

            this.LoadRoots();
        }

        private void LoadDrives()
        {
            var drivers = this.GetDrives();

            this.cboDrive.Items.AddRange(drivers.ToArray());

            this.cboDrive.DisplayMember = nameof(MyFileSystemInfo.Name);
            this.cboDrive.ValueMember = nameof(MyFileSystemInfo.FullName);

            this.cboDrive.Items.Insert(0, "All");
        }

        private List<MyFileSystemInfo> GetDrives()
        {
            List<MyFileSystemInfo> drives = new List<MyFileSystemInfo>();

            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.IsReady)
                {
                    drives.Add(new MyFileSystemInfo(new DirectoryInfo(di.Name)));
                }
            }

            return drives;
        }

        private void LoadRoots()
        {
            var drives = this.GetDrives();
            this.navigator.Roots = drives;
        }

        private void LoadByDriver(MyFileSystemInfo driver)
        {
            this.navigator.Roots = this.GetChildren(driver, true);
        }

        private IEnumerable<MyFileSystemInfo> GetChildren(MyFileSystemInfo folder, bool isNavigator)
        {
            try
            {
                if (isNavigator)
                {
                    return folder.GetFileSystemInfos().Where(item => item.IsDirectory || item.Extension.ToLower() == ".zip");
                }
                else
                {
                    return folder.GetFileSystemInfos();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                this.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.navigator.Collapse(folder);
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                });

                return Enumerable.Empty<MyFileSystemInfo>();
            }
        }

        private void UC_Explorer_SizeChanged(object sender, EventArgs e)
        {
            this.cboDrive.Refresh();
            this.olvName.Width = this.navigator.Width - 10;
        }

        private void cboDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            this.navigator.Items.Clear();

            if (this.cboDrive.SelectedItem is MyFileSystemInfo driver)
            {
                this.LoadByDriver(driver);
                this.lvFiles.SetObjects(driver.GetFileSystemInfos());
            }
            else
            {
                this.LoadRoots();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cboDrive.Items.Clear();
            this.LoadDrives();
            this.LoadData();
        }

        private void navigator_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyFileSystemInfo info = this.navigator.SelectedObject as MyFileSystemInfo;

            if (info != null && info.IsDirectory)
            {
                this.lvFiles.SetObjects(info.GetFileSystemInfos());
                this.lvFiles.Tag = info;

                this.ShowItemCount();
            }
            else
            {
                this.lvFiles.Tag = null;
                this.lvFiles.Items.Clear();
            }
        }

        private void lvFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.EnterSubFolder();
        }

        private void lvFiles_KeyDown(object sender, KeyEventArgs e)
        {
            this.HandleKeyEvent(sender, e);
        }

        private void HandleKeyEvent(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    this.SetClipboard(FileMoveMode.Copy);
                }
                else if (e.KeyCode == Keys.X)
                {
                    this.SetClipboard(FileMoveMode.Cut);
                }
                else if (e.KeyCode == Keys.V)
                {
                    this.Paste();
                }
            }
            else if (e.Shift)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    this.DeleteItems(true);
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.EnterSubFolder();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    this.BackToParent();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    this.DeleteItems(false);
                }
            }
        }

        private void EnterSubFolder()
        {
            MyFileSystemInfo info = this.lvFiles.SelectedObject as MyFileSystemInfo;

            if (info != null && info.IsDirectory)
            {
                this.lvFiles.SetObjects(this.GetChildren(info, false));
                this.lvFiles.Tag = info;

                this.ShowItemCount();
            }
        }       

        private void BackToParent()
        {
            MyFileSystemInfo info = this.lvFiles.Tag as MyFileSystemInfo;

            if (info != null)
            {
                if (info.Parent != null)
                {
                    this.lvFiles.SetObjects(info.Parent.GetFileSystemInfos());
                    this.lvFiles.Tag = info.Parent;

                    this.ShowItemCount();
                }
                else
                {
                    //this.lvFiles.SetObjects(this.GetDrives());
                }
            }
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            ObjectListView listView = sender as ObjectListView;
            bool isNavigator = this.IsNavigator(listView);

            if (e.Button == MouseButtons.Right)
            {
                int count = listView.SelectedObjects.Count;
                bool containsDrive = count > 0 && listView.SelectedObjects.OfType<MyFileSystemInfo>().Any(item => item.IsDrive);
                bool isSelectFolder = count > 0 && listView.SelectedObjects.OfType<MyFileSystemInfo>().First().IsDirectory;

                this.tsmiBack.Visible = !isNavigator && listView.Tag != null && !(listView.Tag as MyFileSystemInfo).IsDrive;
                this.tsmiCopy.Visible = count > 0;
                this.tsmiCut.Visible = count > 0 && !containsDrive;
                this.tsmiDelete.Visible = count > 0 && !containsDrive;
                this.tsmiPaste.Visible = this.ClipBoard != null && (isNavigator ? isSelectFolder : listView.Tag != null);
                this.tsmiRefresh.Visible = (!isNavigator && count == 0) || (isNavigator && count == 1 && isSelectFolder);
                this.tsmiCancel.Visible = this.isMoving;

                this.contextMenuStrip1.Show(Cursor.Position);
                this.contextMenuStrip1.Tag = sender as ObjectListView;
            }
        }

        private void lvFiles_MouseUp(object sender, MouseEventArgs e)
        {
            this.HandleMouseUp(sender, e);
        }

        private void navigator_MouseUp(object sender, MouseEventArgs e)
        {
            this.HandleMouseUp(sender, e);
        }

        private void navigator_KeyDown(object sender, KeyEventArgs e)
        {
            this.HandleKeyEvent(sender, e);
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            this.SetClipboard(FileMoveMode.Copy);
        }

        private ObjectListView GetCurrentListView()
        {
            return (this.contextMenuStrip1.Tag as ObjectListView).Name == this.navigator.Name ? this.navigator : this.lvFiles;
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            this.DeleteItems(false);
        }

        private void tsmiCut_Click(object sender, EventArgs e)
        {
            this.SetClipboard(FileMoveMode.Cut);
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            this.Paste();
        }

        private void SetClipboard(FileMoveMode mode)
        {
            ObjectListView listView = this.GetCurrentListView();

            this.ClipBoard = new FileClipBoard() { Mode = mode, SourceListView = listView, SelectedItems = listView.SelectedObjects.OfType<MyFileSystemInfo>() };

            if (this.ClipboardChange != null)
            {
                this.ClipboardChange(this, this.ClipBoard);
            }
        }

        private void Paste()
        {
            if (this.ClipBoard == null)
            {
                return;
            }

            ObjectListView listView = this.GetCurrentListView();

            MyFileSystemInfo target = null;

            if (this.IsNavigator(listView))
            {
                target = listView.SelectedObject as MyFileSystemInfo;
            }
            else
            {
                target = listView.Tag as MyFileSystemInfo;
            }

            if (target == null || !target.IsDirectory)
            {
                return;
            }

            this.MoveFiles(this.ClipBoard.SourceListView, listView, this.ClipBoard.SelectedItems, target, this.ClipBoard.Mode);

            this.RefreshListView(this.ClipBoard.SourceListView);
            this.RefreshListView(listView);

            this.ClipBoard = null;
        }

        private void DeleteItems(bool permanently)
        {
            ObjectListView listView = this.GetCurrentListView();

            foreach (MyFileSystemInfo info in listView.SelectedObjects)
            {
                if (info.IsDirectory)
                {
                    if (!info.IsDrive)
                    {
                        info.AsDirectory.Delete(true);
                    }
                }
                else
                {
                    info.AsFile.Delete();
                }

                listView.RemoveObject(info);
            }

            listView.Refresh();

            if (this.IsNavigator(listView))
            {
                this.lvFiles.Items.Clear();
            }
            else
            {
                this.navigator.Refresh();
            }
        }

        private bool IsNavigator(ObjectListView listView)
        {
            return listView.Name == this.navigator.Name;
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            ObjectListView listView = this.GetCurrentListView();

            this.RefreshListView(listView);
        }

        private void RefreshListView(ObjectListView listView)
        {
            if (this.IsNavigator(listView))
            {
                if (listView.SelectedObject != null)
                {
                    listView.RefreshObject(listView.SelectedObject);
                }
                else
                {
                    listView.Refresh();
                }
            }
            else
            {
                MyFileSystemInfo info = listView.Tag as MyFileSystemInfo;

                if (info != null)
                {
                    var children = this.GetChildren(info, false);

                    listView.SetObjects(children);

                    listView.Refresh();
                }
            }
        }

        private void tsmiCancel_Click(object sender, EventArgs e)
        {
            this.cancellationTokenSource.Cancel();
            this.isCanceled = true;
            this.Feedback("Cancel requested");           
        }

        private void Feedback(string message, bool isError = false)
        {
            if (this.OnFeedback != null)
            {
                this.OnFeedback(new FeedbackInfo() { InfoType = isError ? FeedbackInfoType.Error : FeedbackInfoType.Info, Message = message });
            }
        }

        private void tsmiBack_Click(object sender, EventArgs e)
        {
            this.BackToParent();
        }

        private void lvFiles_SelectionChanged(object sender, EventArgs e)
        {
            this.ShowItemCount();
        }

        private void ShowItemCount()
        {
            int? selectedCount = this.lvFiles.SelectedItems?.Count;

            if (selectedCount > 0)
            {
                this.Feedback($"{this.lvFiles.Items.Count} item(s), selected {selectedCount} item(s)");
            }
            else
            {
                this.Feedback($"{this.lvFiles.Items.Count} item(s)");
            }
        }
    }
}
