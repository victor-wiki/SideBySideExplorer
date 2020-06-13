using BrightIdeasSoftware;
using SideBySideExplorer.Controls;
using SideBySideExplorer.Helper;
using SideBySideExplorer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideBySideExplorer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.InitControls();            
        }

        private void InitControls()
        {
            this.ucSource.Init();
            this.ucTarget.Init();

            this.ucSource.ClipboardChange += this.ClipboardChange;
            this.ucTarget.ClipboardChange += this.ClipboardChange;          
            this.ucSource.OnFeedback += this.Feedback;
            this.ucTarget.OnFeedback += this.Feedback;

            this.splitContainer1.SplitterDistance = (int)((this.Width - this.splitContainer1.SplitterWidth) * 1.0 / 2);
        }

        private void ClipboardChange(object sender, FileClipBoard clipBoard)
        {
            UC_Explorer uc = sender as UC_Explorer;

            if(uc.Name == this.ucSource.Name)
            {
                this.ucTarget.ClipBoard = clipBoard;
            }
            else
            {
                this.ucSource.ClipBoard = clipBoard;
            }
        }     
        
        private void Feedback(FeedbackInfo info)
        {
            this.txtMessage.Invoke(new Action(() =>
            {
                this.txtMessage.Text = " " + info.Message;
            }));
        }
    }
}
