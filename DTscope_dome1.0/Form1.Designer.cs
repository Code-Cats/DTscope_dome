namespace DTscope_dome1._0
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Curve1 = new HslCommunication.Controls.UserCurve();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.line1 = new System.Windows.Forms.Label();
            this.sentry2_connect = new System.Windows.Forms.Button();
            this.sentry1_connect = new System.Windows.Forms.Button();
            this.infantry1_connect = new System.Windows.Forms.Button();
            this.infantry2_connect = new System.Windows.Forms.Button();
            this.hero2_connect = new System.Windows.Forms.Button();
            this.hero1_connect = new System.Windows.Forms.Button();
            this.engineer2_connect = new System.Windows.Forms.Button();
            this.engineer1_connect = new System.Windows.Forms.Button();
            this.other1_connect = new System.Windows.Forms.Button();
            this.uav_connect = new System.Windows.Forms.Button();
            this.other2_connect = new System.Windows.Forms.Button();
            this.other3_connect = new System.Windows.Forms.Button();
            this.refresh_online = new System.Windows.Forms.Button();
            this.Connection_rate = new HslCommunication.Controls.UserVerticalProgress();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.channelListBox1 = new System.Windows.Forms.CheckedListBox();
            this.save = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.test_button = new System.Windows.Forms.Button();
            this.CurveOptions_panel = new System.Windows.Forms.Panel();
            this.CurveOptions_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Curve1
            // 
            this.Curve1.BackColor = System.Drawing.Color.Transparent;
            this.Curve1.Location = new System.Drawing.Point(-15, 161);
            this.Curve1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Curve1.Name = "Curve1";
            this.Curve1.Size = new System.Drawing.Size(900, 370);
            this.Curve1.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // line1
            // 
            this.line1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line1.Location = new System.Drawing.Point(30, 139);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(866, 2);
            this.line1.TabIndex = 2;
            // 
            // sentry2_connect
            // 
            this.sentry2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.sentry2_connect.FlatAppearance.BorderSize = 0;
            this.sentry2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sentry2_connect.Location = new System.Drawing.Point(30, 83);
            this.sentry2_connect.Name = "sentry2_connect";
            this.sentry2_connect.Size = new System.Drawing.Size(130, 50);
            this.sentry2_connect.TabIndex = 3;
            this.sentry2_connect.Text = "哨兵2\r\noff-line";
            this.sentry2_connect.UseVisualStyleBackColor = false;
            // 
            // sentry1_connect
            // 
            this.sentry1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.sentry1_connect.FlatAppearance.BorderSize = 0;
            this.sentry1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sentry1_connect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sentry1_connect.Location = new System.Drawing.Point(30, 27);
            this.sentry1_connect.Name = "sentry1_connect";
            this.sentry1_connect.Size = new System.Drawing.Size(130, 50);
            this.sentry1_connect.TabIndex = 4;
            this.sentry1_connect.Text = "哨兵1\r\noff-line";
            this.sentry1_connect.UseVisualStyleBackColor = false;
            // 
            // infantry1_connect
            // 
            this.infantry1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.infantry1_connect.FlatAppearance.BorderSize = 0;
            this.infantry1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.infantry1_connect.Location = new System.Drawing.Point(166, 27);
            this.infantry1_connect.Name = "infantry1_connect";
            this.infantry1_connect.Size = new System.Drawing.Size(130, 50);
            this.infantry1_connect.TabIndex = 5;
            this.infantry1_connect.Text = "步兵1\r\noff-line";
            this.infantry1_connect.UseVisualStyleBackColor = false;
            // 
            // infantry2_connect
            // 
            this.infantry2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.infantry2_connect.FlatAppearance.BorderSize = 0;
            this.infantry2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.infantry2_connect.Location = new System.Drawing.Point(166, 83);
            this.infantry2_connect.Name = "infantry2_connect";
            this.infantry2_connect.Size = new System.Drawing.Size(130, 50);
            this.infantry2_connect.TabIndex = 6;
            this.infantry2_connect.Text = "步兵2\r\noff-line";
            this.infantry2_connect.UseVisualStyleBackColor = false;
            // 
            // hero2_connect
            // 
            this.hero2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.hero2_connect.FlatAppearance.BorderSize = 0;
            this.hero2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hero2_connect.Location = new System.Drawing.Point(302, 83);
            this.hero2_connect.Name = "hero2_connect";
            this.hero2_connect.Size = new System.Drawing.Size(130, 50);
            this.hero2_connect.TabIndex = 7;
            this.hero2_connect.Text = "英雄2\r\noff-line";
            this.hero2_connect.UseVisualStyleBackColor = false;
            // 
            // hero1_connect
            // 
            this.hero1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.hero1_connect.FlatAppearance.BorderSize = 0;
            this.hero1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hero1_connect.Location = new System.Drawing.Point(302, 27);
            this.hero1_connect.Name = "hero1_connect";
            this.hero1_connect.Size = new System.Drawing.Size(130, 50);
            this.hero1_connect.TabIndex = 8;
            this.hero1_connect.Text = "英雄1\r\noff-line";
            this.hero1_connect.UseVisualStyleBackColor = false;
            // 
            // engineer2_connect
            // 
            this.engineer2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.engineer2_connect.FlatAppearance.BorderSize = 0;
            this.engineer2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.engineer2_connect.Location = new System.Drawing.Point(438, 83);
            this.engineer2_connect.Name = "engineer2_connect";
            this.engineer2_connect.Size = new System.Drawing.Size(130, 50);
            this.engineer2_connect.TabIndex = 9;
            this.engineer2_connect.Text = "工程2\r\noff-line";
            this.engineer2_connect.UseVisualStyleBackColor = false;
            // 
            // engineer1_connect
            // 
            this.engineer1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.engineer1_connect.FlatAppearance.BorderSize = 0;
            this.engineer1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.engineer1_connect.Location = new System.Drawing.Point(438, 27);
            this.engineer1_connect.Name = "engineer1_connect";
            this.engineer1_connect.Size = new System.Drawing.Size(130, 50);
            this.engineer1_connect.TabIndex = 10;
            this.engineer1_connect.Text = "工程1\r\noff-line";
            this.engineer1_connect.UseVisualStyleBackColor = false;
            // 
            // other1_connect
            // 
            this.other1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.other1_connect.FlatAppearance.BorderSize = 0;
            this.other1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.other1_connect.Location = new System.Drawing.Point(574, 83);
            this.other1_connect.Name = "other1_connect";
            this.other1_connect.Size = new System.Drawing.Size(130, 50);
            this.other1_connect.TabIndex = 11;
            this.other1_connect.Text = "其他1\r\noff-line";
            this.other1_connect.UseVisualStyleBackColor = false;
            // 
            // uav_connect
            // 
            this.uav_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.uav_connect.FlatAppearance.BorderSize = 0;
            this.uav_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uav_connect.Location = new System.Drawing.Point(574, 27);
            this.uav_connect.Name = "uav_connect";
            this.uav_connect.Size = new System.Drawing.Size(130, 50);
            this.uav_connect.TabIndex = 12;
            this.uav_connect.Text = "无人机\r\noff-line";
            this.uav_connect.UseVisualStyleBackColor = false;
            // 
            // other2_connect
            // 
            this.other2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.other2_connect.FlatAppearance.BorderSize = 0;
            this.other2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.other2_connect.Location = new System.Drawing.Point(710, 27);
            this.other2_connect.Name = "other2_connect";
            this.other2_connect.Size = new System.Drawing.Size(130, 50);
            this.other2_connect.TabIndex = 13;
            this.other2_connect.Text = "其他2\r\noff-line";
            this.other2_connect.UseVisualStyleBackColor = false;
            // 
            // other3_connect
            // 
            this.other3_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.other3_connect.FlatAppearance.BorderSize = 0;
            this.other3_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.other3_connect.Location = new System.Drawing.Point(710, 83);
            this.other3_connect.Name = "other3_connect";
            this.other3_connect.Size = new System.Drawing.Size(130, 50);
            this.other3_connect.TabIndex = 14;
            this.other3_connect.Text = "其他3\r\noff-line";
            this.other3_connect.UseVisualStyleBackColor = false;
            // 
            // refresh_online
            // 
            this.refresh_online.Location = new System.Drawing.Point(896, 27);
            this.refresh_online.Name = "refresh_online";
            this.refresh_online.Size = new System.Drawing.Size(75, 106);
            this.refresh_online.TabIndex = 15;
            this.refresh_online.Text = "刷新";
            this.refresh_online.UseVisualStyleBackColor = true;
            // 
            // Connection_rate
            // 
            this.Connection_rate.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Connection_rate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Connection_rate.Location = new System.Drawing.Point(846, 27);
            this.Connection_rate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Connection_rate.Name = "Connection_rate";
            this.Connection_rate.ProgressColor = System.Drawing.Color.DarkTurquoise;
            this.Connection_rate.Size = new System.Drawing.Size(44, 106);
            this.Connection_rate.TabIndex = 16;
            this.Connection_rate.Value = 10;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(30, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(810, 2);
            this.label1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(842, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "连接进度";
            // 
            // channelListBox1
            // 
            this.channelListBox1.FormattingEnabled = true;
            this.channelListBox1.Items.AddRange(new object[] {
            "ch1",
            "ch2",
            "ch3",
            "ch4"});
            this.channelListBox1.Location = new System.Drawing.Point(17, 19);
            this.channelListBox1.Name = "channelListBox1";
            this.channelListBox1.Size = new System.Drawing.Size(120, 84);
            this.channelListBox1.TabIndex = 19;
            this.channelListBox1.SelectedIndexChanged += new System.EventHandler(this.channelListBox1_SelectedIndexChanged);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(17, 185);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 20;
            this.save.Text = "保存";
            this.save.UseVisualStyleBackColor = true;
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(17, 156);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 21;
            this.clear.Text = "清除";
            this.clear.UseVisualStyleBackColor = true;
            // 
            // test_button
            // 
            this.test_button.Location = new System.Drawing.Point(875, 421);
            this.test_button.Name = "test_button";
            this.test_button.Size = new System.Drawing.Size(120, 58);
            this.test_button.TabIndex = 22;
            this.test_button.Text = "测试按钮外观";
            this.test_button.UseVisualStyleBackColor = true;
            this.test_button.Click += new System.EventHandler(this.test_button_Click);
            // 
            // CurveOptions_panel
            // 
            this.CurveOptions_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.CurveOptions_panel.Controls.Add(this.channelListBox1);
            this.CurveOptions_panel.Controls.Add(this.clear);
            this.CurveOptions_panel.Controls.Add(this.save);
            this.CurveOptions_panel.Location = new System.Drawing.Point(875, 182);
            this.CurveOptions_panel.Name = "CurveOptions_panel";
            this.CurveOptions_panel.Size = new System.Drawing.Size(164, 222);
            this.CurveOptions_panel.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1051, 553);
            this.Controls.Add(this.CurveOptions_panel);
            this.Controls.Add(this.test_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connection_rate);
            this.Controls.Add(this.refresh_online);
            this.Controls.Add(this.other3_connect);
            this.Controls.Add(this.other2_connect);
            this.Controls.Add(this.uav_connect);
            this.Controls.Add(this.other1_connect);
            this.Controls.Add(this.engineer1_connect);
            this.Controls.Add(this.engineer2_connect);
            this.Controls.Add(this.hero1_connect);
            this.Controls.Add(this.hero2_connect);
            this.Controls.Add(this.infantry2_connect);
            this.Controls.Add(this.infantry1_connect);
            this.Controls.Add(this.sentry1_connect);
            this.Controls.Add(this.sentry2_connect);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.Curve1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DT-scope";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.CurveOptions_panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HslCommunication.Controls.UserCurve Curve1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label line1;
        private System.Windows.Forms.Button sentry2_connect;
        private System.Windows.Forms.Button sentry1_connect;
        private System.Windows.Forms.Button infantry1_connect;
        private System.Windows.Forms.Button infantry2_connect;
        private System.Windows.Forms.Button hero2_connect;
        private System.Windows.Forms.Button hero1_connect;
        private System.Windows.Forms.Button engineer2_connect;
        private System.Windows.Forms.Button engineer1_connect;
        private System.Windows.Forms.Button other1_connect;
        private System.Windows.Forms.Button uav_connect;
        private System.Windows.Forms.Button other2_connect;
        private System.Windows.Forms.Button other3_connect;
        private System.Windows.Forms.Button refresh_online;
        private HslCommunication.Controls.UserVerticalProgress Connection_rate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox channelListBox1;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button test_button;
        private System.Windows.Forms.Panel CurveOptions_panel;
    }
}

