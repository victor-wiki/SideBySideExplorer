using SideBySideExplorer.Model;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SideBySideExplorer.Helper
{
    public class FileHelper
    {
        public static bool IsSameDrive(string path1, string path2)
        {
            return Path.GetPathRoot(path1) == Path.GetPathRoot(path2);
        }

        public static async Task MoveFolder(string source, string target, Action<TransferProgress> progress, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(IsSameDrive(source, target))
            {
                await FileTransferManager.MoveWithProgressAsync(source, target, progress, cancellationToken);
            }
            else
            {
                await CopyFolder(source, target, true, progress, cancellationToken);

                Directory.Delete(source, true);
            }
        }       

        public static async Task MoveFile(string source, string target, Action<TransferProgress> progress, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (IsSameDrive(source, target))
            {
                await FileTransferManager.MoveWithProgressAsync(source, target, progress, cancellationToken);
            }
            else
            {
                await CopyFile(source, target, progress, cancellationToken);

                File.Delete(source);
            }
        }

        public static Task CopyFile(string source, string target, Action<TransferProgress> progress, CancellationToken cancellationToken= default(CancellationToken))
        {
            if(File.Exists(target))
            {
                return Task.Run(() => { });
            }

            return FileTransferManager.CopyWithProgressAsync(source, target, progress, false, false, cancellationToken);
        }

        public static async Task CopyFolder(string source, string target, bool copySub, Action<TransferProgress> progress, CancellationToken cancellationToken = default(CancellationToken))
        {           
            DirectoryInfo dir = new DirectoryInfo(source);    

            DirectoryInfo[] dirs = dir.GetDirectories();
         
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
           
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                string path = Path.Combine(target, file.Name);

                await CopyFile(file.FullName, path, progress, cancellationToken);
            }
       
            if (copySub)
            {
                foreach (DirectoryInfo sub in dirs)
                {
                    string targetSub = Path.Combine(target, sub.Name);

                    await CopyFolder(sub.FullName, targetSub, copySub, progress, cancellationToken);
                }
            }
        }

        public static string FormatFileSize(long size)
        {
            int[] limits = new int[] { 1024 * 1024 * 1024, 1024 * 1024, 1024 };
            string[] units = new string[] { "GB", "MB", "KB" };

            for (int i = 0; i < limits.Length; i++)
            {
                if (size >= limits[i])
                    return String.Format("{0:#,##0.##} " + units[i], ((double)size / limits[i]));
            }

            return String.Format("{0} bytes", size);
        }

        public static long GetFolderSize(DirectoryInfo folder)
        {
            return folder.GetFiles().Sum(fi => fi.Length) +
                   folder.GetDirectories().Sum(di => GetFolderSize(di));
        }

        public static int GetFolderFileCount(DirectoryInfo folder)
        {
            return folder.GetFiles().Length +
                   folder.GetDirectories().Sum(di => GetFolderFileCount(di));
        }

        public static bool IsFolder(string path)
        {
            if (Directory.Exists(path) || File.Exists(path))
            {               
                var fileAttr = File.GetAttributes(path);

                if (fileAttr.HasFlag(FileAttributes.Directory))
                {
                    return true;
                }               
            }

            return false;
        }

        public static string CorrectFileDestinationPath(string source, string destination)
        {
            var destinationFile = destination;

            if (IsFolder(source))
            {
                destinationFile = Path.Combine(destination, Path.GetFileName(source));
            }

            return destinationFile;
        }
    }
}
