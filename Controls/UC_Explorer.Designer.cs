namespace SideBySideExplorer.Controls
{
    partial class UC_Explorer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Explorer));
            this.lvFiles = new BrightIdeasSoftware.ObjectListView();
            this.olvFileName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvModified = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCreated = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.navigator = new BrightIdeasSoftware.TreeListView();
            this.olvName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDrive = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.lvFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFiles
            // 
            this.lvFiles.AllColumns.Add(this.olvFileName);
            this.lvFiles.AllColumns.Add(this.olvSize);
            this.lvFiles.AllColumns.Add(this.olvModified);
            this.lvFiles.AllColumns.Add(this.olvCreated);
            this.lvFiles.CellEditUseWholeCell = false;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvFileName,
            this.olvSize,
            this.olvModified,
            this.olvCreated});
            this.lvFiles.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(0, 0);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.SelectedForeColor = System.Drawing.Color.Black;
            this.lvFiles.ShowGroups = false;
            this.lvFiles.Size = new System.Drawing.Size(264, 416);
            this.lvFiles.SmallImageList = this.imageList1;
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.SelectionChanged += new System.EventHandler(this.lvFiles_SelectionChanged);
            this.lvFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvFiles_KeyDown);
            this.lvFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFiles_MouseDoubleClick);
            this.lvFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvFiles_MouseUp);
            // 
            // olvFileName
            // 
            this.olvFileName.AspectName = "Name";
            this.olvFileName.Text = "Name";
            this.olvFileName.Width = 180;
            // 
            // olvSize
            // 
            this.olvSize.AspectName = "Extension";
            this.olvSize.Text = "Size";
            // 
            // olvModified
            // 
            this.olvModified.AspectName = "LastWriteTime";
            this.olvModified.Text = "Modified";
            this.olvModified.Width = 130;
            // 
            // olvCreated
            // 
            this.olvCreated.AspectName = "CreationTime";
            this.olvCreated.Text = "Created";
            this.olvCreated.Width = 130;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            // 
            // navigator
            // 
            this.navigator.AllColumns.Add(this.olvName);
            this.navigator.CellEditUseWholeCell = false;
            this.navigator.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvName});
            this.navigator.Cursor = System.Windows.Forms.Cursors.Default;
            this.navigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigator.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.navigator.HideSelection = false;
            this.navigator.Location = new System.Drawing.Point(0, 0);
            this.navigator.Name = "navigator";
            this.navigator.ShowGroups = false;
            this.navigator.ShowHeaderInAllViews = false;
            this.navigator.ShowImagesOnSubItems = true;
            this.navigator.ShowItemToolTips = true;
            this.navigator.Size = new System.Drawing.Size(132, 416);
            this.navigator.SmallImageList = this.imageList1;
            this.navigator.TabIndex = 0;
            this.navigator.UseCompatibleStateImageBehavior = false;
            this.navigator.View = System.Windows.Forms.View.Details;
            this.navigator.VirtualMode = true;
            this.navigator.SelectedIndexChanged += new System.EventHandler(this.navigator_SelectedIndexChanged);
            this.navigator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.navigator_KeyDown);
            this.navigator.MouseUp += new System.Windows.Forms.MouseEventHandler(this.navigator_MouseUp);
            // 
            // olvName
            // 
            this.olvName.AspectName = "Name";
            this.olvName.Sortable = false;
            this.olvName.Text = "Name";
            this.olvName.Width = 100;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.navigator);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvFiles);
            this.splitContainer1.Size = new System.Drawing.Size(398, 416);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Drive:";
            // 
            // cboDrive
            // 
            this.cboDrive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDrive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDrive.FormattingEnabled = true;
            this.cboDrive.Location = new System.Drawing.Point(55, 3);
            this.cboDrive.Name = "cboDrive";
            this.cboDrive.Size = new System.Drawing.Size(315, 20);
            this.cboDrive.TabIndex = 4;
            this.cboDrive.SelectedIndexChanged += new System.EventHandler(this.cboDrive_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::SideBySideExplorer.Properties.Resources.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(376, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBack,
            this.tsmiCopy,
            this.tsmiCut,
            this.tsmiPaste,
            this.tsmiDelete,
            this.tsmiRefresh,
            this.tsmiCancel});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(121, 158);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(120, 22);
            this.tsmiCopy.Text = "Copy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiCut
            // 
            this.tsmiCut.Name = "tsmiCut";
            this.tsmiCut.Size = new System.Drawing.Size(120, 22);
            this.tsmiCut.Text = "Cut";
            this.tsmiCut.Click += new System.EventHandler(this.tsmiCut_Click);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(120, 22);
            this.tsmiPaste.Text = "Paste";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(120, 22);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(120, 22);
            this.tsmiRefresh.Text = "Refresh";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tsmiCancel
            // 
            this.tsmiCancel.Name = "tsmiCancel";
            this.tsmiCancel.Size = new System.Drawing.Size(120, 22);
            this.tsmiCancel.Text = "Cancel";
            this.tsmiCancel.Click += new System.EventHandler(this.tsmiCancel_Click);
            // 
            // tsmiBack
            // 
            this.tsmiBack.Name = "tsmiBack";
            this.tsmiBack.Size = new System.Drawing.Size(180, 22);
            this.tsmiBack.Text = "Back";
            this.tsmiBack.Click += new System.EventHandler(this.tsmiBack_Click);
            // 
            // UC_Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboDrive);
            this.Controls.Add(this.splitContainer1);
            this.Name = "UC_Explorer";
            this.Size = new System.Drawing.Size(404, 452);
            this.Load += new System.EventHandler(this.UC_Explorer_Load);
            this.SizeChanged += new System.EventHandler(this.UC_Explorer_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.lvFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigator)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView lvFiles;
        private BrightIdeasSoftware.TreeListView navigator;
        private BrightIdeasSoftware.OLVColumn olvName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDrive;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnRefresh;
        private BrightIdeasSoftware.OLVColumn olvFileName;
        private BrightIdeasSoftware.OLVColumn olvSize;
        private BrightIdeasSoftware.OLVColumn olvCreated;
        private BrightIdeasSoftware.OLVColumn olvModified;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCut;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiCancel;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
    }
}
