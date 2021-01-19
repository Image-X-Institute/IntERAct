
using System.Windows.Forms;

namespace PhantomControl
{
    partial class Log_tab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textbox_Log = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textbox_Log
            // 
            this.textbox_Log.BackColor = System.Drawing.Color.Gray;
            this.textbox_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textbox_Log.Location = new System.Drawing.Point(0, 0);
            this.textbox_Log.Margin = new System.Windows.Forms.Padding(4);
            this.textbox_Log.Multiline = true;
            this.textbox_Log.Name = "textbox_Log";
            this.textbox_Log.ReadOnly = true;
            this.textbox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_Log.Size = new System.Drawing.Size(2741, 1705);
            this.textbox_Log.TabIndex = 0;
            // 
            // Log_tab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.textbox_Log);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Log_tab";
            this.Size = new System.Drawing.Size(2741, 1705);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox textbox_Log;

        //private WindowsFormsControlLibrary1.BunifuCustomTextbox textbox_Log;

    }
}
