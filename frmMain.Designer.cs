namespace SideBySideExplorer
{
    partial class frmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucSource = new SideBySideExplorer.Controls.UC_Explorer();
            this.ucTarget = new SideBySideExplorer.Controls.UC_Explorer();
            this.txtMessage = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucTarget);
            this.splitContainer1.Size = new System.Drawing.Size(800, 432);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 0;
            // 
            // ucSource
            // 
            this.ucSource.ClipBoard = null;
            this.ucSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSource.Location = new System.Drawing.Point(0, 0);
            this.ucSource.Name = "ucSource";
            this.ucSource.Size = new System.Drawing.Size(400, 432);
            this.ucSource.TabIndex = 0;
            // 
            // ucTarget
            // 
            this.ucTarget.ClipBoard = null;
            this.ucTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTarget.Location = new System.Drawing.Point(0, 0);
            this.ucTarget.Name = "ucTarget";
            this.ucTarget.Size = new System.Drawing.Size(398, 432);
            this.ucTarget.TabIndex = 0;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.SystemColors.Control;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMessage.Location = new System.Drawing.Point(0, 436);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(800, 14);
            this.txtMessage.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Side by Side Explorer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.UC_Explorer ucSource;
        private Controls.UC_Explorer ucTarget;
        private System.Windows.Forms.TextBox txtMessage;
    }
}

