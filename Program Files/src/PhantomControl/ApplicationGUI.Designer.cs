using System.Windows.Forms;

namespace PhantomControl
{
    partial class ApplicationGUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationGUI));
            this.process1 = new System.Diagnostics.Process();
            this.sidepanel = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.flatButton_log = new System.Windows.Forms.Button();
            this.flatButton_settings = new System.Windows.Forms.Button();
            this.flatButton_motionControl = new System.Windows.Forms.Button();
            this.logopanel = new System.Windows.Forms.Panel();
            this.label_BrandName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.headerpanel = new System.Windows.Forms.Panel();
            this.picContainer = new System.Windows.Forms.PictureBox();
            this.label_heading = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.log_tab = new PhantomControl.Log_tab();
            this.settings_tab = new PhantomControl.Settings_tab();

            this.motionControl_tab = new PhantomControl.MotionControl_tab(ref this.settings_tab);
            this.sidepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.logopanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.headerpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // sidepanel
            // 
            this.sidepanel.BackColor = System.Drawing.Color.DimGray;
            this.sidepanel.Controls.Add(this.linkLabel1);
            this.sidepanel.Controls.Add(this.pictureBox4);
            this.sidepanel.Controls.Add(this.pictureBox3);
            this.sidepanel.Controls.Add(this.pictureBox2);
            this.sidepanel.Controls.Add(this.flatButton_log);
            this.sidepanel.Controls.Add(this.flatButton_settings);
            this.sidepanel.Controls.Add(this.flatButton_motionControl);
            this.sidepanel.Controls.Add(this.logopanel);
            this.sidepanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidepanel.Location = new System.Drawing.Point(0, 0);
            this.sidepanel.Margin = new System.Windows.Forms.Padding(1);
            this.sidepanel.Name = "sidepanel";
            this.sidepanel.Size = new System.Drawing.Size(252, 821);
            this.sidepanel.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.linkLabel1.Location = new System.Drawing.Point(7, 799);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(130, 15);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Help -  Documentation";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(10, 233);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 8;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(10, 170);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 107);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // flatButton_log
            // 
            this.flatButton_log.BackColor = System.Drawing.Color.DimGray;
            this.flatButton_log.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flatButton_log.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flatButton_log.Dock = System.Windows.Forms.DockStyle.Top;
            this.flatButton_log.FlatAppearance.BorderSize = 0;
            this.flatButton_log.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.flatButton_log.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatButton_log.ForeColor = System.Drawing.Color.White;
            this.flatButton_log.Location = new System.Drawing.Point(0, 215);
            this.flatButton_log.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flatButton_log.Name = "flatButton_log";
            this.flatButton_log.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flatButton_log.Size = new System.Drawing.Size(252, 63);
            this.flatButton_log.TabIndex = 5;
            this.flatButton_log.Text = "               Log";
            this.flatButton_log.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.flatButton_log.UseVisualStyleBackColor = false;
            this.flatButton_log.Click += new System.EventHandler(this.flatButton_log_Click);
            // 
            // flatButton_settings
            // 
            this.flatButton_settings.BackColor = System.Drawing.Color.DimGray;
            this.flatButton_settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flatButton_settings.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flatButton_settings.Dock = System.Windows.Forms.DockStyle.Top;
            this.flatButton_settings.FlatAppearance.BorderSize = 0;
            this.flatButton_settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.flatButton_settings.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatButton_settings.ForeColor = System.Drawing.Color.White;
            this.flatButton_settings.Location = new System.Drawing.Point(0, 152);
            this.flatButton_settings.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flatButton_settings.Name = "flatButton_settings";
            this.flatButton_settings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flatButton_settings.Size = new System.Drawing.Size(252, 63);
            this.flatButton_settings.TabIndex = 4;
            this.flatButton_settings.Text = "               Settings";
            this.flatButton_settings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.flatButton_settings.UseVisualStyleBackColor = false;
            this.flatButton_settings.Click += new System.EventHandler(this.flatButton_settings_Click);
            // 
            // flatButton_motionControl
            // 
            this.flatButton_motionControl.BackColor = System.Drawing.Color.DimGray;
            this.flatButton_motionControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flatButton_motionControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flatButton_motionControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.flatButton_motionControl.FlatAppearance.BorderSize = 0;
            this.flatButton_motionControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.flatButton_motionControl.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatButton_motionControl.ForeColor = System.Drawing.Color.White;
            this.flatButton_motionControl.Location = new System.Drawing.Point(0, 89);
            this.flatButton_motionControl.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flatButton_motionControl.Name = "flatButton_motionControl";
            this.flatButton_motionControl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flatButton_motionControl.Size = new System.Drawing.Size(252, 63);
            this.flatButton_motionControl.TabIndex = 3;
            this.flatButton_motionControl.Text = "               Motion Control";
            this.flatButton_motionControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.flatButton_motionControl.UseVisualStyleBackColor = false;
            this.flatButton_motionControl.Click += new System.EventHandler(this.flatButton_motionControl_Click);
            // 
            // logopanel
            // 
            this.logopanel.BackColor = System.Drawing.Color.SandyBrown;
            this.logopanel.Controls.Add(this.label_BrandName);
            this.logopanel.Controls.Add(this.pictureBox1);
            this.logopanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.logopanel.Location = new System.Drawing.Point(0, 0);
            this.logopanel.Margin = new System.Windows.Forms.Padding(1);
            this.logopanel.Name = "logopanel";
            this.logopanel.Size = new System.Drawing.Size(252, 89);
            this.logopanel.TabIndex = 2;
            // 
            // label_BrandName
            // 
            this.label_BrandName.AutoSize = true;
            this.label_BrandName.BackColor = System.Drawing.Color.Transparent;
            this.label_BrandName.Font = new System.Drawing.Font("Century Gothic", 20F);
            this.label_BrandName.ForeColor = System.Drawing.Color.White;
            this.label_BrandName.Location = new System.Drawing.Point(51, 31);
            this.label_BrandName.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_BrandName.Name = "label_BrandName";
            this.label_BrandName.Size = new System.Drawing.Size(165, 33);
            this.label_BrandName.TabIndex = 0;
            this.label_BrandName.Text = "6D MOTION";
            this.label_BrandName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 7);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // headerpanel
            // 
            this.headerpanel.BackColor = System.Drawing.Color.Gainsboro;
            this.headerpanel.Controls.Add(this.picContainer);
            this.headerpanel.Controls.Add(this.label_heading);
            this.headerpanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerpanel.Location = new System.Drawing.Point(252, 0);
            this.headerpanel.Margin = new System.Windows.Forms.Padding(10);
            this.headerpanel.Name = "headerpanel";
            this.headerpanel.Size = new System.Drawing.Size(1218, 89);
            this.headerpanel.TabIndex = 1;
            // 
            // picContainer
            // 
            this.picContainer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picContainer.Image = ((System.Drawing.Image)(resources.GetObject("picContainer.Image")));
            this.picContainer.Location = new System.Drawing.Point(423, 25);
            this.picContainer.Margin = new System.Windows.Forms.Padding(1);
            this.picContainer.Name = "picContainer";
            this.picContainer.Size = new System.Drawing.Size(52, 53);
            this.picContainer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picContainer.TabIndex = 6;
            this.picContainer.TabStop = false;
            this.picContainer.Click += new System.EventHandler(this.picContainer_Click);
            // 
            // label_heading
            // 
            this.label_heading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_heading.AutoSize = true;
            this.label_heading.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_heading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(63)))), ((int)(((byte)(55)))));
            this.label_heading.Location = new System.Drawing.Point(495, 34);
            this.label_heading.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_heading.Name = "label_heading";
            this.label_heading.Size = new System.Drawing.Size(237, 30);
            this.label_heading.TabIndex = 1;
            this.label_heading.Text = "MOTION CONTROL";
            this.label_heading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_heading.Click += new System.EventHandler(this.label_heading_Click);
            // 
            // log_tab
            // 
            this.log_tab.AutoSize = true;
            this.log_tab.BackColor = System.Drawing.Color.Transparent;
            this.log_tab.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.log_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log_tab.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_tab.Location = new System.Drawing.Point(252, 89);
            this.log_tab.Margin = new System.Windows.Forms.Padding(4);
            this.log_tab.Name = "log_tab";
            this.log_tab.Size = new System.Drawing.Size(1218, 732);
            this.log_tab.TabIndex = 4;
            this.log_tab.Load += new System.EventHandler(this.log_tab_Load);
            // 
            // settings_tab
            // 
            this.settings_tab.AutoScroll = true;
            this.settings_tab.AutoScrollMargin = new System.Drawing.Size(40, 40);
            this.settings_tab.AutoSize = true;
            this.settings_tab.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.settings_tab.BackColor = System.Drawing.Color.Gainsboro;
            this.settings_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settings_tab.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settings_tab.Location = new System.Drawing.Point(252, 89);
            this.settings_tab.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.settings_tab.Name = "settings_tab";
            this.settings_tab.Size = new System.Drawing.Size(1218, 732);
            this.settings_tab.TabIndex = 3;
            // 
            // motionControl_tab
            // 
            this.motionControl_tab.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.motionControl_tab.AutoScroll = true;
            this.motionControl_tab.AutoScrollMargin = new System.Drawing.Size(1, 1);
            this.motionControl_tab.AutoSize = true;
            this.motionControl_tab.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motionControl_tab.BackColor = System.Drawing.Color.Gainsboro;
            this.motionControl_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionControl_tab.Location = new System.Drawing.Point(252, 89);
            this.motionControl_tab.Margin = new System.Windows.Forms.Padding(0);
            this.motionControl_tab.MinimumSize = new System.Drawing.Size(1157, 723);
            this.motionControl_tab.Name = "motionControl_tab";
            this.motionControl_tab.Size = new System.Drawing.Size(1218, 732);
            this.motionControl_tab.TabIndex = 2;
            // 
            // ApplicationGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1470, 821);
            this.Controls.Add(this.log_tab);
            this.Controls.Add(this.settings_tab);
            this.Controls.Add(this.motionControl_tab);
            this.Controls.Add(this.headerpanel);
            this.Controls.Add(this.sidepanel);
            this.DoubleBuffered = true;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ApplicationGUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Phantom Control";
            this.Load += new System.EventHandler(this.ApplicationGUI_Load);
            this.sidepanel.ResumeLayout(false);
            this.sidepanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.logopanel.ResumeLayout(false);
            this.logopanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.headerpanel.ResumeLayout(false);
            this.headerpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        #region BUNIFU_ELEMENTS_REPLACED
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_motionControl;
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_log;
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_settings;
        //private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        //private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_Close;
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_Minimise;
        //private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        //private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl3;
        //private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl4;
        //private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl5;
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_Resize;


        private Button flatButton_motionControl;
        private Button flatButton_log;
        private Button flatButton_settings;
        #endregion


        private System.Diagnostics.Process process1;
        private System.Windows.Forms.Panel sidepanel;
        private System.Windows.Forms.Panel headerpanel;
        private System.Windows.Forms.Panel logopanel;
        private System.Windows.Forms.Label label_BrandName;
        
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        
        private System.Windows.Forms.Label label_heading;
        
        private System.Windows.Forms.PictureBox picContainer;
        private MotionControl_tab motionControl_tab;
        private Settings_tab settings_tab;
        
        private Log_tab log_tab;
        private System.Windows.Forms.PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private LinkLabel linkLabel1;
    }
}

