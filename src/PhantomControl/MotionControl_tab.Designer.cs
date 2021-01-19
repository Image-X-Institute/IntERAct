using System.Windows.Forms;

namespace PhantomControl
{
    partial class MotionControl_tab
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
            this.flatButton_LoadTraces = new System.Windows.Forms.Button();
            this.flatButton_PlayStopMotion = new System.Windows.Forms.Button();
            this.bunifuElipse1 = new System.Windows.Forms.Button();
            this.bunifuElipse2 = new System.Windows.Forms.Button();
            this.flatButton_SetStartPos = new System.Windows.Forms.Button();
            this.bunifuElipse3 = new System.Windows.Forms.Button();
            this.bunifuElipse4 = new System.Windows.Forms.Button();
            this.progressbar_Motion = new System.Windows.Forms.ProgressBar();
            this.bunifuCustomLabel2 = new System.Windows.Forms.Label();
            this.bunifuCustomLabel1 = new System.Windows.Forms.Label();
            this.txtBox_rAP = new System.Windows.Forms.TextBox();
            this.label_TCPrz = new System.Windows.Forms.Label();
            this.txtBox_rLR = new System.Windows.Forms.TextBox();
            this.label_TCPrx = new System.Windows.Forms.Label();
            this.txtBox_rSI = new System.Windows.Forms.TextBox();
            this.label_TCPry = new System.Windows.Forms.Label();
            this.txtBox_SI = new System.Windows.Forms.TextBox();
            this.label_TCPy = new System.Windows.Forms.Label();
            this.txtBox_AP = new System.Windows.Forms.TextBox();
            this.label_TCPz = new System.Windows.Forms.Label();
            this.txtBox_LR = new System.Windows.Forms.TextBox();
            this.label_TCPx = new System.Windows.Forms.Label();
            this.bunifuCustomLabel3 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cartesianChart_translation = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart_rotation = new LiveCharts.WinForms.CartesianChart();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel24 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel25 = new System.Windows.Forms.Panel();
            this.panel26 = new System.Windows.Forms.Panel();
            this.panel27 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel28 = new System.Windows.Forms.Panel();
            this.panel29 = new System.Windows.Forms.Panel();
            this.panel30 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel31 = new System.Windows.Forms.Panel();
            this.panel32 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flatButton_Home = new System.Windows.Forms.Button();
            this.lblProgressVal = new System.Windows.Forms.Label();
            this.textBox_filename = new System.Windows.Forms.TextBox();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel26.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel29.SuspendLayout();
            this.panel30.SuspendLayout();
            this.panel31.SuspendLayout();
            this.panel32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flatButton_LoadTraces
            // 
            this.flatButton_LoadTraces.BackColor = System.Drawing.Color.DarkGray;
            this.flatButton_LoadTraces.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flatButton_LoadTraces.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flatButton_LoadTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatButton_LoadTraces.ForeColor = System.Drawing.Color.White;
            this.flatButton_LoadTraces.Location = new System.Drawing.Point(16, 90);
            this.flatButton_LoadTraces.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flatButton_LoadTraces.Name = "flatButton_LoadTraces";
            this.flatButton_LoadTraces.Size = new System.Drawing.Size(165, 54);
            this.flatButton_LoadTraces.TabIndex = 4;
            this.flatButton_LoadTraces.Text = "Load Motion Traces ";
            this.flatButton_LoadTraces.UseVisualStyleBackColor = false;
            this.flatButton_LoadTraces.Click += new System.EventHandler(this.flatButton_LoadTraces_Click);
            // 
            // flatButton_PlayStopMotion
            // 
            this.flatButton_PlayStopMotion.BackColor = System.Drawing.Color.DarkGray;
            this.flatButton_PlayStopMotion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flatButton_PlayStopMotion.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flatButton_PlayStopMotion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatButton_PlayStopMotion.ForeColor = System.Drawing.Color.White;
            this.flatButton_PlayStopMotion.Location = new System.Drawing.Point(16, 208);
            this.flatButton_PlayStopMotion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flatButton_PlayStopMotion.Name = "flatButton_PlayStopMotion";
            this.flatButton_PlayStopMotion.Size = new System.Drawing.Size(162, 54);
            this.flatButton_PlayStopMotion.TabIndex = 5;
            this.flatButton_PlayStopMotion.Text = "Play Motion";
            this.flatButton_PlayStopMotion.UseVisualStyleBackColor = false;
            this.flatButton_PlayStopMotion.Click += new System.EventHandler(this.flatButton_PlayStopMotion_Click);
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.Location = new System.Drawing.Point(0, 0);
            this.bunifuElipse1.Name = "bunifuElipse1";
            this.bunifuElipse1.Size = new System.Drawing.Size(75, 23);
            this.bunifuElipse1.TabIndex = 0;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.Location = new System.Drawing.Point(0, 0);
            this.bunifuElipse2.Name = "bunifuElipse2";
            this.bunifuElipse2.Size = new System.Drawing.Size(75, 23);
            this.bunifuElipse2.TabIndex = 0;
            // 
            // flatButton_SetStartPos
            // 
            this.flatButton_SetStartPos.BackColor = System.Drawing.Color.DarkGray;
            this.flatButton_SetStartPos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flatButton_SetStartPos.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flatButton_SetStartPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatButton_SetStartPos.ForeColor = System.Drawing.Color.White;
            this.flatButton_SetStartPos.Location = new System.Drawing.Point(16, 17);
            this.flatButton_SetStartPos.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flatButton_SetStartPos.Name = "flatButton_SetStartPos";
            this.flatButton_SetStartPos.Size = new System.Drawing.Size(162, 54);
            this.flatButton_SetStartPos.TabIndex = 8;
            this.flatButton_SetStartPos.Text = "Set Start Position";
            this.flatButton_SetStartPos.UseVisualStyleBackColor = false;
            this.flatButton_SetStartPos.Click += new System.EventHandler(this.flatButton_SetStartPos_Click);
            // 
            // bunifuElipse3
            // 
            this.bunifuElipse3.Location = new System.Drawing.Point(0, 0);
            this.bunifuElipse3.Name = "bunifuElipse3";
            this.bunifuElipse3.Size = new System.Drawing.Size(75, 23);
            this.bunifuElipse3.TabIndex = 0;
            // 
            // bunifuElipse4
            // 
            this.bunifuElipse4.Location = new System.Drawing.Point(0, 0);
            this.bunifuElipse4.Name = "bunifuElipse4";
            this.bunifuElipse4.Size = new System.Drawing.Size(75, 23);
            this.bunifuElipse4.TabIndex = 0;
            // 
            // progressbar_Motion
            // 
            this.progressbar_Motion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressbar_Motion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(126)))), ((int)(((byte)(49)))));
            this.progressbar_Motion.Location = new System.Drawing.Point(16, 273);
            this.progressbar_Motion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressbar_Motion.Name = "progressbar_Motion";
            this.progressbar_Motion.Size = new System.Drawing.Size(228, 29);
            this.progressbar_Motion.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressbar_Motion.TabIndex = 11;
            this.progressbar_Motion.Value = 1;
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(25, 0);
            this.bunifuCustomLabel2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(60, 21);
            this.bunifuCustomLabel2.TabIndex = 8;
            this.bunifuCustomLabel2.Text = "Ready";
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(20, 0);
            this.bunifuCustomLabel1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(101, 24);
            this.bunifuCustomLabel1.TabIndex = 8;
            this.bunifuCustomLabel1.Text = "Connected";
            // 
            // txtBox_rAP
            // 
            this.txtBox_rAP.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBox_rAP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_rAP.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_rAP.ForeColor = System.Drawing.Color.Red;
            this.txtBox_rAP.Location = new System.Drawing.Point(0, 0);
            this.txtBox_rAP.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtBox_rAP.Name = "txtBox_rAP";
            this.txtBox_rAP.ReadOnly = true;
            this.txtBox_rAP.Size = new System.Drawing.Size(52, 26);
            this.txtBox_rAP.TabIndex = 6;
            this.txtBox_rAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_TCPrz
            // 
            this.label_TCPrz.AutoSize = true;
            this.label_TCPrz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_TCPrz.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TCPrz.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_TCPrz.Location = new System.Drawing.Point(0, 0);
            this.label_TCPrz.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_TCPrz.Name = "label_TCPrz";
            this.label_TCPrz.Size = new System.Drawing.Size(38, 20);
            this.label_TCPrz.TabIndex = 4;
            this.label_TCPrz.Text = "rAP:";
            this.label_TCPrz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBox_rLR
            // 
            this.txtBox_rLR.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBox_rLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_rLR.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_rLR.ForeColor = System.Drawing.Color.Blue;
            this.txtBox_rLR.Location = new System.Drawing.Point(0, 0);
            this.txtBox_rLR.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtBox_rLR.Name = "txtBox_rLR";
            this.txtBox_rLR.ReadOnly = true;
            this.txtBox_rLR.Size = new System.Drawing.Size(52, 26);
            this.txtBox_rLR.TabIndex = 4;
            this.txtBox_rLR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_TCPrx
            // 
            this.label_TCPrx.AutoSize = true;
            this.label_TCPrx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_TCPrx.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TCPrx.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_TCPrx.Location = new System.Drawing.Point(0, 0);
            this.label_TCPrx.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_TCPrx.Name = "label_TCPrx";
            this.label_TCPrx.Size = new System.Drawing.Size(34, 20);
            this.label_TCPrx.TabIndex = 4;
            this.label_TCPrx.Text = "rLR:";
            this.label_TCPrx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBox_rSI
            // 
            this.txtBox_rSI.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBox_rSI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_rSI.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_rSI.ForeColor = System.Drawing.Color.Green;
            this.txtBox_rSI.Location = new System.Drawing.Point(0, 0);
            this.txtBox_rSI.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtBox_rSI.Name = "txtBox_rSI";
            this.txtBox_rSI.ReadOnly = true;
            this.txtBox_rSI.Size = new System.Drawing.Size(52, 26);
            this.txtBox_rSI.TabIndex = 5;
            this.txtBox_rSI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_TCPry
            // 
            this.label_TCPry.AutoSize = true;
            this.label_TCPry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_TCPry.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TCPry.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_TCPry.Location = new System.Drawing.Point(0, 0);
            this.label_TCPry.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_TCPry.Name = "label_TCPry";
            this.label_TCPry.Size = new System.Drawing.Size(30, 20);
            this.label_TCPry.TabIndex = 4;
            this.label_TCPry.Text = "rSI:";
            this.label_TCPry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBox_SI
            // 
            this.txtBox_SI.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBox_SI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_SI.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_SI.ForeColor = System.Drawing.Color.Green;
            this.txtBox_SI.Location = new System.Drawing.Point(0, 0);
            this.txtBox_SI.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtBox_SI.Name = "txtBox_SI";
            this.txtBox_SI.ReadOnly = true;
            this.txtBox_SI.Size = new System.Drawing.Size(52, 26);
            this.txtBox_SI.TabIndex = 2;
            this.txtBox_SI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_TCPy
            // 
            this.label_TCPy.AutoSize = true;
            this.label_TCPy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_TCPy.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TCPy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_TCPy.Location = new System.Drawing.Point(0, 0);
            this.label_TCPy.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_TCPy.Name = "label_TCPy";
            this.label_TCPy.Size = new System.Drawing.Size(25, 20);
            this.label_TCPy.TabIndex = 4;
            this.label_TCPy.Text = "SI:";
            this.label_TCPy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBox_AP
            // 
            this.txtBox_AP.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBox_AP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_AP.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_AP.ForeColor = System.Drawing.Color.Red;
            this.txtBox_AP.Location = new System.Drawing.Point(0, 0);
            this.txtBox_AP.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtBox_AP.Name = "txtBox_AP";
            this.txtBox_AP.ReadOnly = true;
            this.txtBox_AP.Size = new System.Drawing.Size(52, 26);
            this.txtBox_AP.TabIndex = 3;
            this.txtBox_AP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_TCPz
            // 
            this.label_TCPz.AutoSize = true;
            this.label_TCPz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_TCPz.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TCPz.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_TCPz.Location = new System.Drawing.Point(0, 0);
            this.label_TCPz.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_TCPz.Name = "label_TCPz";
            this.label_TCPz.Size = new System.Drawing.Size(33, 20);
            this.label_TCPz.TabIndex = 4;
            this.label_TCPz.Text = "AP:";
            this.label_TCPz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBox_LR
            // 
            this.txtBox_LR.AcceptsReturn = true;
            this.txtBox_LR.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBox_LR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_LR.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_LR.ForeColor = System.Drawing.Color.Blue;
            this.txtBox_LR.Location = new System.Drawing.Point(0, 0);
            this.txtBox_LR.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtBox_LR.Name = "txtBox_LR";
            this.txtBox_LR.ReadOnly = true;
            this.txtBox_LR.Size = new System.Drawing.Size(52, 26);
            this.txtBox_LR.TabIndex = 1;
            this.txtBox_LR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBox_LR.TextChanged += new System.EventHandler(this.txtBox_LR_TextChanged);
            // 
            // label_TCPx
            // 
            this.label_TCPx.AutoSize = true;
            this.label_TCPx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_TCPx.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TCPx.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_TCPx.Location = new System.Drawing.Point(0, 0);
            this.label_TCPx.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_TCPx.Name = "label_TCPx";
            this.label_TCPx.Size = new System.Drawing.Size(29, 20);
            this.label_TCPx.TabIndex = 4;
            this.label_TCPx.Text = "LR:";
            this.label_TCPx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Century Gothic", 12.9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(21, 306);
            this.bunifuCustomLabel3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(242, 21);
            this.bunifuCustomLabel3.TabIndex = 33;
            this.bunifuCustomLabel3.Text = "         Current Position          ";
            this.bunifuCustomLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.Controls.Add(this.bunifuCustomLabel1);
            this.panel9.Location = new System.Drawing.Point(57, 566);
            this.panel9.Margin = new System.Windows.Forms.Padding(1);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(121, 24);
            this.panel9.TabIndex = 12;
            // 
            // cartesianChart_translation
            // 
            this.cartesianChart_translation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChart_translation.BackColor = System.Drawing.Color.White;
            this.cartesianChart_translation.Font = new System.Drawing.Font("Century Gothic", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cartesianChart_translation.Location = new System.Drawing.Point(1, 1);
            this.cartesianChart_translation.Margin = new System.Windows.Forms.Padding(1);
            this.cartesianChart_translation.MinimumSize = new System.Drawing.Size(450, 348);
            this.cartesianChart_translation.Name = "cartesianChart_translation";
            this.cartesianChart_translation.Size = new System.Drawing.Size(1206, 348);
            this.cartesianChart_translation.TabIndex = 6;
            this.cartesianChart_translation.Text = "cartesianChart";
            // 
            // cartesianChart_rotation
            // 
            this.cartesianChart_rotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChart_rotation.BackColor = System.Drawing.Color.White;
            this.cartesianChart_rotation.Font = new System.Drawing.Font("Century Gothic", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cartesianChart_rotation.Location = new System.Drawing.Point(1, 1);
            this.cartesianChart_rotation.Margin = new System.Windows.Forms.Padding(1);
            this.cartesianChart_rotation.MinimumSize = new System.Drawing.Size(550, 348);
            this.cartesianChart_rotation.Name = "cartesianChart_rotation";
            this.cartesianChart_rotation.Size = new System.Drawing.Size(1207, 348);
            this.cartesianChart_rotation.TabIndex = 7;
            this.cartesianChart_rotation.Text = "cartesianChart";
            this.cartesianChart_rotation.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.cartesianChart_rotation_ChildChanged);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.bunifuCustomLabel2);
            this.panel8.Location = new System.Drawing.Point(57, 601);
            this.panel8.Margin = new System.Windows.Forms.Padding(1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(85, 24);
            this.panel8.TabIndex = 13;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.label10);
            this.panel15.Controls.Add(this.label5);
            this.panel15.Controls.Add(this.panel16);
            this.panel15.Controls.Add(this.panel17);
            this.panel15.Location = new System.Drawing.Point(53, 523);
            this.panel15.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(138, 29);
            this.panel15.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(99, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "deg";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(138, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 12;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.txtBox_rAP);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel16.Location = new System.Drawing.Point(45, 0);
            this.panel16.Margin = new System.Windows.Forms.Padding(1);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(52, 29);
            this.panel16.TabIndex = 13;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.label_TCPrz);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Margin = new System.Windows.Forms.Padding(1);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(45, 29);
            this.panel17.TabIndex = 13;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.label8);
            this.panel18.Controls.Add(this.label3);
            this.panel18.Controls.Add(this.panel19);
            this.panel18.Controls.Add(this.panel20);
            this.panel18.Location = new System.Drawing.Point(53, 458);
            this.panel18.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(138, 29);
            this.panel18.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(99, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "deg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(138, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 12;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.txtBox_rLR);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel19.Location = new System.Drawing.Point(45, 0);
            this.panel19.Margin = new System.Windows.Forms.Padding(1);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(52, 29);
            this.panel19.TabIndex = 13;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.label_TCPrx);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel20.Location = new System.Drawing.Point(0, 0);
            this.panel20.Margin = new System.Windows.Forms.Padding(1);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(45, 29);
            this.panel20.TabIndex = 13;
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.label9);
            this.panel21.Controls.Add(this.label6);
            this.panel21.Controls.Add(this.panel22);
            this.panel21.Controls.Add(this.panel23);
            this.panel21.Location = new System.Drawing.Point(53, 491);
            this.panel21.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(138, 29);
            this.panel21.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(99, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "deg";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(138, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 20);
            this.label6.TabIndex = 12;
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.txtBox_rSI);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel22.Location = new System.Drawing.Point(45, 0);
            this.panel22.Margin = new System.Windows.Forms.Padding(1);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(52, 29);
            this.panel22.TabIndex = 13;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.label_TCPry);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel23.ForeColor = System.Drawing.Color.Green;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Margin = new System.Windows.Forms.Padding(1);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(45, 29);
            this.panel23.TabIndex = 13;
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.label2);
            this.panel24.Controls.Add(this.panel25);
            this.panel24.Controls.Add(this.panel26);
            this.panel24.Location = new System.Drawing.Point(53, 378);
            this.panel24.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(138, 29);
            this.panel24.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(103, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "mm";
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.txtBox_SI);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel25.Location = new System.Drawing.Point(45, 0);
            this.panel25.Margin = new System.Windows.Forms.Padding(1);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(52, 29);
            this.panel25.TabIndex = 13;
            // 
            // panel26
            // 
            this.panel26.Controls.Add(this.label_TCPy);
            this.panel26.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel26.Location = new System.Drawing.Point(0, 0);
            this.panel26.Margin = new System.Windows.Forms.Padding(1);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(45, 29);
            this.panel26.TabIndex = 13;
            // 
            // panel27
            // 
            this.panel27.Controls.Add(this.label4);
            this.panel27.Controls.Add(this.panel28);
            this.panel27.Controls.Add(this.panel29);
            this.panel27.Location = new System.Drawing.Point(53, 410);
            this.panel27.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(138, 29);
            this.panel27.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(103, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "mm";
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.txtBox_AP);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel28.Location = new System.Drawing.Point(45, 0);
            this.panel28.Margin = new System.Windows.Forms.Padding(1);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(52, 29);
            this.panel28.TabIndex = 13;
            // 
            // panel29
            // 
            this.panel29.Controls.Add(this.label_TCPz);
            this.panel29.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel29.Location = new System.Drawing.Point(0, 0);
            this.panel29.Margin = new System.Windows.Forms.Padding(1);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(45, 29);
            this.panel29.TabIndex = 13;
            // 
            // panel30
            // 
            this.panel30.Controls.Add(this.label1);
            this.panel30.Controls.Add(this.panel31);
            this.panel30.Controls.Add(this.panel32);
            this.panel30.Location = new System.Drawing.Point(53, 346);
            this.panel30.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(138, 29);
            this.panel30.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "mm";
            // 
            // panel31
            // 
            this.panel31.Controls.Add(this.txtBox_LR);
            this.panel31.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel31.Location = new System.Drawing.Point(45, 0);
            this.panel31.Margin = new System.Windows.Forms.Padding(1);
            this.panel31.Name = "panel31";
            this.panel31.Size = new System.Drawing.Size(52, 29);
            this.panel31.TabIndex = 13;
            // 
            // panel32
            // 
            this.panel32.Controls.Add(this.label_TCPx);
            this.panel32.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel32.Location = new System.Drawing.Point(0, 0);
            this.panel32.Margin = new System.Windows.Forms.Padding(1);
            this.panel32.Name = "panel32";
            this.panel32.Size = new System.Drawing.Size(45, 29);
            this.panel32.TabIndex = 13;
            // 
            // splitContainer1
            // 
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(277, 10);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cartesianChart_translation);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cartesianChart_rotation);
            this.splitContainer1.Size = new System.Drawing.Size(1220, 735);
            this.splitContainer1.SplitterDistance = 356;
            this.splitContainer1.SplitterWidth = 7;
            this.splitContainer1.TabIndex = 34;
            // 
            // flatButton_Home
            // 
            this.flatButton_Home.BackColor = System.Drawing.Color.DarkGray;
            this.flatButton_Home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flatButton_Home.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flatButton_Home.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatButton_Home.ForeColor = System.Drawing.Color.White;
            this.flatButton_Home.Location = new System.Drawing.Point(184, 17);
            this.flatButton_Home.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flatButton_Home.Name = "flatButton_Home";
            this.flatButton_Home.Size = new System.Drawing.Size(82, 54);
            this.flatButton_Home.TabIndex = 9;
            this.flatButton_Home.Text = "Home";
            this.flatButton_Home.UseVisualStyleBackColor = false;
            this.flatButton_Home.Click += new System.EventHandler(this.flatButton_Home_Click);
            // 
            // lblProgressVal
            // 
            this.lblProgressVal.AutoSize = true;
            this.lblProgressVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressVal.Location = new System.Drawing.Point(216, 225);
            this.lblProgressVal.Name = "lblProgressVal";
            this.lblProgressVal.Size = new System.Drawing.Size(0, 15);
            this.lblProgressVal.TabIndex = 35;
            // 
            // textBox_filename
            // 
            this.textBox_filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_filename.Location = new System.Drawing.Point(16, 166);
            this.textBox_filename.Name = "textBox_filename";
            this.textBox_filename.Size = new System.Drawing.Size(250, 22);
            this.textBox_filename.TabIndex = 36;
            this.textBox_filename.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // MotionControl_tab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.textBox_filename);
            this.Controls.Add(this.lblProgressVal);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.panel18);
            this.Controls.Add(this.panel21);
            this.Controls.Add(this.panel24);
            this.Controls.Add(this.panel27);
            this.Controls.Add(this.panel30);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.progressbar_Motion);
            this.Controls.Add(this.flatButton_Home);
            this.Controls.Add(this.flatButton_SetStartPos);
            this.Controls.Add(this.flatButton_PlayStopMotion);
            this.Controls.Add(this.flatButton_LoadTraces);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "MotionControl_tab";
            this.Size = new System.Drawing.Size(1820, 2687);
            this.Load += new System.EventHandler(this.MotionControl_tab_Load);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.panel29.ResumeLayout(false);
            this.panel29.PerformLayout();
            this.panel30.ResumeLayout(false);
            this.panel30.PerformLayout();
            this.panel31.ResumeLayout(false);
            this.panel31.PerformLayout();
            this.panel32.ResumeLayout(false);
            this.panel32.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Bunifu_ReplacedLElements
        private Button flatButton_LoadTraces;
        private Button flatButton_PlayStopMotion;
        private Button bunifuElipse1;
        private Button bunifuElipse2;
        private Button flatButton_SetStartPos;
        private Button bunifuElipse3;
        private Button bunifuElipse4;
        private System.Windows.Forms.ProgressBar progressbar_Motion;
        private System.Windows.Forms.Label bunifuCustomLabel2;
        private Label bunifuCustomLabel1;
        private TextBox txtBox_rAP;
        private Label label_TCPrz;
        private TextBox txtBox_rLR;
        private Label label_TCPrx;
        private TextBox txtBox_rSI;
        private Label label_TCPry;
        private TextBox txtBox_SI;
        private Label label_TCPy;
        private TextBox txtBox_AP;
        private Label label_TCPz;
        private TextBox txtBox_LR;
        private Label label_TCPx;
        private Label bunifuCustomLabel3;

        #endregion



        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_LoadTraces;
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_PlayStopMotion;

        //private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        //private Bunifu.Framework.UI.BunifuElipse bunifuElipse2;
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_SetStartPos;
        //private Bunifu.Framework.UI.BunifuElipse bunifuElipse3;
        //private Bunifu.Framework.UI.BunifuFlatButton flatButton_Home;
        //private Bunifu.Framework.UI.BunifuElipse bunifuElipse4;
        //private Bunifu.Framework.UI.BunifuCircleProgressbar progressbar_Motion;
        //private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;




        //private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        //private WindowsFormsControlLibrary1.BunifuCustomTextbox txtBox_rAP;
        //private Bunifu.Framework.UI.BunifuCustomLabel label_TCPrz;
        //private WindowsFormsControlLibrary1.BunifuCustomTextbox txtBox_rLR;
        //private Bunifu.Framework.UI.BunifuCustomLabel label_TCPrx;
        //private WindowsFormsControlLibrary1.BunifuCustomTextbox txtBox_rSI;
        //private Bunifu.Framework.UI.BunifuCustomLabel label_TCPry;
        //private WindowsFormsControlLibrary1.BunifuCustomTextbox txtBox_SI;
        //private Bunifu.Framework.UI.BunifuCustomLabel label_TCPy;
        //private WindowsFormsControlLibrary1.BunifuCustomTextbox txtBox_AP;
        //private Bunifu.Framework.UI.BunifuCustomLabel label_TCPz;
        //private WindowsFormsControlLibrary1.BunifuCustomTextbox txtBox_LR;
        //private Bunifu.Framework.UI.BunifuCustomLabel label_TCPx;
        //private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;



        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private LiveCharts.WinForms.CartesianChart cartesianChart_translation;
        private LiveCharts.WinForms.CartesianChart cartesianChart_rotation;
        
        private System.Windows.Forms.Panel panel8;
        
        private System.Windows.Forms.Panel panel9;
        private Bulb.LedBulb ledBulb_Connected;
        
        private Bulb.LedBulb ledBulb_Ready;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel16;
        
        private System.Windows.Forms.Panel panel17;
        
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel19;
        
        private System.Windows.Forms.Panel panel20;
        
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel22;
        
        private System.Windows.Forms.Panel panel23;
        
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel25;
        
        private System.Windows.Forms.Panel panel26;
        
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel28;
        
        private System.Windows.Forms.Panel panel29;
        
        private System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel31;
        
        private System.Windows.Forms.Panel panel32;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Button flatButton_Home;
        private Label lblProgressVal;
        private TextBox textBox_filename;
    }
}
