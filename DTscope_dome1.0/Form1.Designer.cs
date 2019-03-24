using System.Windows.Forms;

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
            this.refresh_online_or_break_button = new System.Windows.Forms.Button();
            this.Connection_rate = new HslCommunication.Controls.UserVerticalProgress();
            this.label2 = new System.Windows.Forms.Label();
            this.channelListBox1 = new System.Windows.Forms.CheckedListBox();
            this.save = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.test_button = new System.Windows.Forms.Button();
            this.CurveOptions_panel = new System.Windows.Forms.Panel();
            this.revnum_label = new System.Windows.Forms.Label();
            this.test_rev = new System.Windows.Forms.Button();
            this.rev_text = new System.Windows.Forms.Label();
            this.test_connect = new System.Windows.Forms.Button();
            this.timer_UI = new System.Windows.Forms.Timer(this.components);
            this.btn_connect_panel = new System.Windows.Forms.Panel();
            this.label_version = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel_github = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl_Main = new System.Windows.Forms.TabControl();
            this.tabPage_chart = new System.Windows.Forms.TabPage();
            this.tabPage_debug = new System.Windows.Forms.TabPage();
            this.Broadcast_communication_textBox = new System.Windows.Forms.TextBox();
            this.checkedListBox_dubug = new System.Windows.Forms.CheckedListBox();
            this.Unicast_communication_textBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Debug_SendMsg_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage_config = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage_help = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.timer_count = new System.Windows.Forms.Timer(this.components);
            this.CurveOptions_panel.SuspendLayout();
            this.btn_connect_panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl_Main.SuspendLayout();
            this.tabPage_chart.SuspendLayout();
            this.tabPage_debug.SuspendLayout();
            this.tabPage_config.SuspendLayout();
            this.tabPage_help.SuspendLayout();
            this.SuspendLayout();
            // 
            // Curve1
            // 
            this.Curve1.BackColor = System.Drawing.Color.Black;
            this.Curve1.Location = new System.Drawing.Point(6, 188);
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
            // sentry2_connect
            // 
            this.sentry2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.sentry2_connect.FlatAppearance.BorderSize = 0;
            this.sentry2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sentry2_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.sentry2_connect.Location = new System.Drawing.Point(9, 82);
            this.sentry2_connect.Name = "sentry2_connect";
            this.sentry2_connect.Size = new System.Drawing.Size(130, 50);
            this.sentry2_connect.TabIndex = 3;
            this.sentry2_connect.Text = "哨兵2\r\noff-line";
            this.sentry2_connect.UseVisualStyleBackColor = false;
            this.sentry2_connect.Click += new System.EventHandler(this.sentry2_connect_Click);
            // 
            // sentry1_connect
            // 
            this.sentry1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.sentry1_connect.FlatAppearance.BorderSize = 0;
            this.sentry1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sentry1_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.sentry1_connect.Location = new System.Drawing.Point(9, 26);
            this.sentry1_connect.Name = "sentry1_connect";
            this.sentry1_connect.Size = new System.Drawing.Size(130, 50);
            this.sentry1_connect.TabIndex = 4;
            this.sentry1_connect.Text = "哨兵1\r\noff-line";
            this.sentry1_connect.UseVisualStyleBackColor = false;
            this.sentry1_connect.Click += new System.EventHandler(this.sentry1_connect_Click);
            // 
            // infantry1_connect
            // 
            this.infantry1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.infantry1_connect.FlatAppearance.BorderSize = 0;
            this.infantry1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.infantry1_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.infantry1_connect.Location = new System.Drawing.Point(145, 26);
            this.infantry1_connect.Name = "infantry1_connect";
            this.infantry1_connect.Size = new System.Drawing.Size(130, 50);
            this.infantry1_connect.TabIndex = 5;
            this.infantry1_connect.Text = "步兵1\r\noff-line";
            this.infantry1_connect.UseVisualStyleBackColor = false;
            this.infantry1_connect.Click += new System.EventHandler(this.infantry1_connect_Click);
            // 
            // infantry2_connect
            // 
            this.infantry2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.infantry2_connect.FlatAppearance.BorderSize = 0;
            this.infantry2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.infantry2_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.infantry2_connect.Location = new System.Drawing.Point(145, 82);
            this.infantry2_connect.Name = "infantry2_connect";
            this.infantry2_connect.Size = new System.Drawing.Size(130, 50);
            this.infantry2_connect.TabIndex = 6;
            this.infantry2_connect.Text = "步兵2\r\noff-line";
            this.infantry2_connect.UseVisualStyleBackColor = false;
            this.infantry2_connect.Click += new System.EventHandler(this.infantry2_connect_Click);
            // 
            // hero2_connect
            // 
            this.hero2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.hero2_connect.FlatAppearance.BorderSize = 0;
            this.hero2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hero2_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.hero2_connect.Location = new System.Drawing.Point(281, 82);
            this.hero2_connect.Name = "hero2_connect";
            this.hero2_connect.Size = new System.Drawing.Size(130, 50);
            this.hero2_connect.TabIndex = 7;
            this.hero2_connect.Text = "英雄2\r\noff-line";
            this.hero2_connect.UseVisualStyleBackColor = false;
            this.hero2_connect.Click += new System.EventHandler(this.hero2_connect_Click);
            // 
            // hero1_connect
            // 
            this.hero1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.hero1_connect.FlatAppearance.BorderSize = 0;
            this.hero1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hero1_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.hero1_connect.Location = new System.Drawing.Point(281, 26);
            this.hero1_connect.Name = "hero1_connect";
            this.hero1_connect.Size = new System.Drawing.Size(130, 50);
            this.hero1_connect.TabIndex = 8;
            this.hero1_connect.Text = "英雄1\r\noff-line";
            this.hero1_connect.UseVisualStyleBackColor = false;
            this.hero1_connect.Click += new System.EventHandler(this.hero1_connect_Click);
            // 
            // engineer2_connect
            // 
            this.engineer2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.engineer2_connect.FlatAppearance.BorderSize = 0;
            this.engineer2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.engineer2_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.engineer2_connect.Location = new System.Drawing.Point(417, 82);
            this.engineer2_connect.Name = "engineer2_connect";
            this.engineer2_connect.Size = new System.Drawing.Size(130, 50);
            this.engineer2_connect.TabIndex = 9;
            this.engineer2_connect.Text = "工程2\r\noff-line";
            this.engineer2_connect.UseVisualStyleBackColor = false;
            this.engineer2_connect.Click += new System.EventHandler(this.engineer2_connect_Click);
            // 
            // engineer1_connect
            // 
            this.engineer1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.engineer1_connect.FlatAppearance.BorderSize = 0;
            this.engineer1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.engineer1_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.engineer1_connect.Location = new System.Drawing.Point(417, 26);
            this.engineer1_connect.Name = "engineer1_connect";
            this.engineer1_connect.Size = new System.Drawing.Size(130, 50);
            this.engineer1_connect.TabIndex = 10;
            this.engineer1_connect.Text = "工程1\r\noff-line";
            this.engineer1_connect.UseVisualStyleBackColor = false;
            this.engineer1_connect.Click += new System.EventHandler(this.engineer1_connect_Click);
            // 
            // other1_connect
            // 
            this.other1_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.other1_connect.FlatAppearance.BorderSize = 0;
            this.other1_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.other1_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.other1_connect.Location = new System.Drawing.Point(553, 82);
            this.other1_connect.Name = "other1_connect";
            this.other1_connect.Size = new System.Drawing.Size(130, 50);
            this.other1_connect.TabIndex = 11;
            this.other1_connect.Text = "其他1\r\noff-line";
            this.other1_connect.UseVisualStyleBackColor = false;
            this.other1_connect.Click += new System.EventHandler(this.other1_connect_Click);
            // 
            // uav_connect
            // 
            this.uav_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.uav_connect.FlatAppearance.BorderSize = 0;
            this.uav_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uav_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.uav_connect.Location = new System.Drawing.Point(553, 26);
            this.uav_connect.Name = "uav_connect";
            this.uav_connect.Size = new System.Drawing.Size(130, 50);
            this.uav_connect.TabIndex = 12;
            this.uav_connect.Text = "无人机\r\noff-line";
            this.uav_connect.UseVisualStyleBackColor = false;
            this.uav_connect.Click += new System.EventHandler(this.uav_connect_Click);
            // 
            // other2_connect
            // 
            this.other2_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.other2_connect.FlatAppearance.BorderSize = 0;
            this.other2_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.other2_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.other2_connect.Location = new System.Drawing.Point(689, 26);
            this.other2_connect.Name = "other2_connect";
            this.other2_connect.Size = new System.Drawing.Size(130, 50);
            this.other2_connect.TabIndex = 13;
            this.other2_connect.Text = "其他2\r\noff-line";
            this.other2_connect.UseVisualStyleBackColor = false;
            this.other2_connect.Click += new System.EventHandler(this.other2_connect_Click);
            // 
            // other3_connect
            // 
            this.other3_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.other3_connect.FlatAppearance.BorderSize = 0;
            this.other3_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.other3_connect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.other3_connect.Location = new System.Drawing.Point(689, 82);
            this.other3_connect.Name = "other3_connect";
            this.other3_connect.Size = new System.Drawing.Size(130, 50);
            this.other3_connect.TabIndex = 14;
            this.other3_connect.Text = "其他3\r\noff-line";
            this.other3_connect.UseVisualStyleBackColor = false;
            this.other3_connect.Click += new System.EventHandler(this.other3_connect_Click);
            // 
            // refresh_online_or_break_button
            // 
            this.refresh_online_or_break_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(180)))), ((int)(((byte)(155)))));
            this.refresh_online_or_break_button.FlatAppearance.BorderSize = 0;
            this.refresh_online_or_break_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_online_or_break_button.Font = new System.Drawing.Font("微软雅黑 Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.refresh_online_or_break_button.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.refresh_online_or_break_button.Location = new System.Drawing.Point(844, 19);
            this.refresh_online_or_break_button.Name = "refresh_online_or_break_button";
            this.refresh_online_or_break_button.Size = new System.Drawing.Size(75, 145);
            this.refresh_online_or_break_button.TabIndex = 15;
            this.refresh_online_or_break_button.Text = "刷\r\n\r\n新";
            this.refresh_online_or_break_button.UseVisualStyleBackColor = false;
            // 
            // Connection_rate
            // 
            this.Connection_rate.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Connection_rate.BorderColor = System.Drawing.Color.Transparent;
            this.Connection_rate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Connection_rate.Location = new System.Drawing.Point(72, 156);
            this.Connection_rate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Connection_rate.Name = "Connection_rate";
            this.Connection_rate.ProgressColor = System.Drawing.Color.DarkTurquoise;
            this.Connection_rate.ProgressStyle = HslCommunication.Controls.ProgressStyle.Horizontal;
            this.Connection_rate.Size = new System.Drawing.Size(766, 8);
            this.Connection_rate.TabIndex = 16;
            this.Connection_rate.UseAnimation = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("等线", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(17, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "连接进度";
            // 
            // channelListBox1
            // 
            this.channelListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.channelListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.channelListBox1.CheckOnClick = true;
            this.channelListBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.channelListBox1.FormattingEnabled = true;
            this.channelListBox1.Items.AddRange(new object[] {
            "ch1",
            "ch2",
            "ch3",
            "ch4"});
            this.channelListBox1.Location = new System.Drawing.Point(17, 19);
            this.channelListBox1.Name = "channelListBox1";
            this.channelListBox1.Size = new System.Drawing.Size(120, 88);
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
            this.test_button.Location = new System.Drawing.Point(108, 156);
            this.test_button.Name = "test_button";
            this.test_button.Size = new System.Drawing.Size(120, 58);
            this.test_button.TabIndex = 22;
            this.test_button.Text = "测试按钮外观";
            this.test_button.UseVisualStyleBackColor = true;
            this.test_button.Click += new System.EventHandler(this.test_button_Click);
            // 
            // CurveOptions_panel
            // 
            this.CurveOptions_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.CurveOptions_panel.Controls.Add(this.revnum_label);
            this.CurveOptions_panel.Controls.Add(this.channelListBox1);
            this.CurveOptions_panel.Controls.Add(this.test_rev);
            this.CurveOptions_panel.Controls.Add(this.rev_text);
            this.CurveOptions_panel.Controls.Add(this.test_connect);
            this.CurveOptions_panel.Controls.Add(this.clear);
            this.CurveOptions_panel.Controls.Add(this.save);
            this.CurveOptions_panel.Controls.Add(this.test_button);
            this.CurveOptions_panel.Location = new System.Drawing.Point(913, 188);
            this.CurveOptions_panel.Name = "CurveOptions_panel";
            this.CurveOptions_panel.Size = new System.Drawing.Size(273, 370);
            this.CurveOptions_panel.TabIndex = 23;
            // 
            // revnum_label
            // 
            this.revnum_label.AutoSize = true;
            this.revnum_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.revnum_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.revnum_label.Location = new System.Drawing.Point(146, 240);
            this.revnum_label.Name = "revnum_label";
            this.revnum_label.Size = new System.Drawing.Size(55, 15);
            this.revnum_label.TabIndex = 27;
            this.revnum_label.Text = "label7";
            // 
            // test_rev
            // 
            this.test_rev.Location = new System.Drawing.Point(17, 300);
            this.test_rev.Name = "test_rev";
            this.test_rev.Size = new System.Drawing.Size(75, 23);
            this.test_rev.TabIndex = 25;
            this.test_rev.Text = "测试接收";
            this.test_rev.UseVisualStyleBackColor = true;
            this.test_rev.Click += new System.EventHandler(this.test_rev_Click);
            // 
            // rev_text
            // 
            this.rev_text.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.rev_text.Location = new System.Drawing.Point(146, 264);
            this.rev_text.Name = "rev_text";
            this.rev_text.Size = new System.Drawing.Size(82, 59);
            this.rev_text.TabIndex = 26;
            this.rev_text.Text = "label3";
            // 
            // test_connect
            // 
            this.test_connect.Location = new System.Drawing.Point(17, 260);
            this.test_connect.Name = "test_connect";
            this.test_connect.Size = new System.Drawing.Size(75, 23);
            this.test_connect.TabIndex = 24;
            this.test_connect.Text = "测试发送";
            this.test_connect.UseVisualStyleBackColor = true;
            this.test_connect.Click += new System.EventHandler(this.test_connect_Click);
            // 
            // timer_UI
            // 
            this.timer_UI.Enabled = true;
            this.timer_UI.Interval = 50;
            this.timer_UI.Tick += new System.EventHandler(this.timer_UI_Tick);
            // 
            // btn_connect_panel
            // 
            this.btn_connect_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.btn_connect_panel.Controls.Add(this.label_version);
            this.btn_connect_panel.Controls.Add(this.label6);
            this.btn_connect_panel.Controls.Add(this.label5);
            this.btn_connect_panel.Controls.Add(this.label4);
            this.btn_connect_panel.Controls.Add(this.label3);
            this.btn_connect_panel.Controls.Add(this.linkLabel_github);
            this.btn_connect_panel.Controls.Add(this.groupBox1);
            this.btn_connect_panel.Controls.Add(this.label2);
            this.btn_connect_panel.Controls.Add(this.Connection_rate);
            this.btn_connect_panel.Controls.Add(this.refresh_online_or_break_button);
            this.btn_connect_panel.Location = new System.Drawing.Point(6, 6);
            this.btn_connect_panel.Name = "btn_connect_panel";
            this.btn_connect_panel.Size = new System.Drawing.Size(1200, 175);
            this.btn_connect_panel.TabIndex = 27;
            // 
            // label_version
            // 
            this.label_version.Font = new System.Drawing.Font("微软雅黑", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_version.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label_version.Location = new System.Drawing.Point(1095, 0);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(68, 52);
            this.label_version.TabIndex = 25;
            this.label_version.Text = "Versions:1.0\r\nsingle connection";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("华文新魏", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label6.Location = new System.Drawing.Point(935, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "更多功能待添加中";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("华文新魏", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label5.Location = new System.Drawing.Point(1039, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "YuXin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("华文新魏", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.label4.Location = new System.Drawing.Point(936, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 9);
            this.label4.TabIndex = 22;
            this.label4.Text = "North China University of Technology";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("华文新魏", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.label3.Location = new System.Drawing.Point(935, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 34);
            this.label3.TabIndex = 21;
            this.label3.Text = "ROBOMASTER\r\nDreamTeam 控制组";
            // 
            // linkLabel_github
            // 
            this.linkLabel_github.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.linkLabel_github.AutoSize = true;
            this.linkLabel_github.Font = new System.Drawing.Font("Rage Italic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_github.LinkArea = new System.Windows.Forms.LinkArea(0, 38);
            this.linkLabel_github.LinkColor = System.Drawing.Color.DarkCyan;
            this.linkLabel_github.Location = new System.Drawing.Point(933, 121);
            this.linkLabel_github.Name = "linkLabel_github";
            this.linkLabel_github.Size = new System.Drawing.Size(160, 31);
            this.linkLabel_github.TabIndex = 20;
            this.linkLabel_github.TabStop = true;
            this.linkLabel_github.Text = "Click to access github";
            this.linkLabel_github.UseCompatibleTextRendering = true;
            this.linkLabel_github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_github_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.other3_connect);
            this.groupBox1.Controls.Add(this.infantry2_connect);
            this.groupBox1.Controls.Add(this.sentry2_connect);
            this.groupBox1.Controls.Add(this.other2_connect);
            this.groupBox1.Controls.Add(this.sentry1_connect);
            this.groupBox1.Controls.Add(this.uav_connect);
            this.groupBox1.Controls.Add(this.infantry1_connect);
            this.groupBox1.Controls.Add(this.other1_connect);
            this.groupBox1.Controls.Add(this.engineer1_connect);
            this.groupBox1.Controls.Add(this.hero2_connect);
            this.groupBox1.Controls.Add(this.engineer2_connect);
            this.groupBox1.Controls.Add(this.hero1_connect);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.groupBox1.Location = new System.Drawing.Point(10, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 140);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "on-line state";
            // 
            // tabControl_Main
            // 
            this.tabControl_Main.Controls.Add(this.tabPage_chart);
            this.tabControl_Main.Controls.Add(this.tabPage_debug);
            this.tabControl_Main.Controls.Add(this.tabPage_config);
            this.tabControl_Main.Controls.Add(this.tabPage_help);
            this.tabControl_Main.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl_Main.Location = new System.Drawing.Point(-5, -1);
            this.tabControl_Main.Name = "tabControl_Main";
            this.tabControl_Main.SelectedIndex = 0;
            this.tabControl_Main.Size = new System.Drawing.Size(1173, 647);
            this.tabControl_Main.TabIndex = 0;
            this.tabControl_Main.TabStop = false;
            this.tabControl_Main.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl_Main_DrawItem);
            this.tabControl_Main.SelectedIndexChanged += new System.EventHandler(this.tabControl_Main_SelectedIndexChanged);
            // 
            // tabPage_chart
            // 
            this.tabPage_chart.BackColor = System.Drawing.Color.DimGray;
            this.tabPage_chart.Controls.Add(this.Curve1);
            this.tabPage_chart.Controls.Add(this.btn_connect_panel);
            this.tabPage_chart.Controls.Add(this.CurveOptions_panel);
            this.tabPage_chart.Location = new System.Drawing.Point(4, 25);
            this.tabPage_chart.Name = "tabPage_chart";
            this.tabPage_chart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_chart.Size = new System.Drawing.Size(1165, 618);
            this.tabPage_chart.TabIndex = 0;
            this.tabPage_chart.Text = "chart";
            // 
            // tabPage_debug
            // 
            this.tabPage_debug.BackColor = System.Drawing.Color.DimGray;
            this.tabPage_debug.Controls.Add(this.Broadcast_communication_textBox);
            this.tabPage_debug.Controls.Add(this.checkedListBox_dubug);
            this.tabPage_debug.Controls.Add(this.Unicast_communication_textBox);
            this.tabPage_debug.Controls.Add(this.button1);
            this.tabPage_debug.Controls.Add(this.Debug_SendMsg_textBox);
            this.tabPage_debug.Controls.Add(this.label1);
            this.tabPage_debug.Controls.Add(this.label7);
            this.tabPage_debug.Location = new System.Drawing.Point(4, 25);
            this.tabPage_debug.Name = "tabPage_debug";
            this.tabPage_debug.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_debug.Size = new System.Drawing.Size(1165, 618);
            this.tabPage_debug.TabIndex = 1;
            this.tabPage_debug.Text = "debug";
            // 
            // Broadcast_communication_textBox
            // 
            this.Broadcast_communication_textBox.BackColor = System.Drawing.Color.Black;
            this.Broadcast_communication_textBox.ForeColor = System.Drawing.Color.DarkGray;
            this.Broadcast_communication_textBox.Location = new System.Drawing.Point(13, 21);
            this.Broadcast_communication_textBox.Multiline = true;
            this.Broadcast_communication_textBox.Name = "Broadcast_communication_textBox";
            this.Broadcast_communication_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Broadcast_communication_textBox.Size = new System.Drawing.Size(504, 536);
            this.Broadcast_communication_textBox.TabIndex = 8;
            this.Broadcast_communication_textBox.Text = ".init";
            this.Broadcast_communication_textBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Broadcast_communication_textBox_MouseDown);
            this.Broadcast_communication_textBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Broadcast_communication_textBox_MouseMove);
            // 
            // checkedListBox_dubug
            // 
            this.checkedListBox_dubug.BackColor = System.Drawing.Color.DimGray;
            this.checkedListBox_dubug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox_dubug.CheckOnClick = true;
            this.checkedListBox_dubug.Font = new System.Drawing.Font("微软雅黑 Light", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkedListBox_dubug.FormattingEnabled = true;
            this.checkedListBox_dubug.Items.AddRange(new object[] {
            "广播",
            "点播"});
            this.checkedListBox_dubug.Location = new System.Drawing.Point(1092, 514);
            this.checkedListBox_dubug.Name = "checkedListBox_dubug";
            this.checkedListBox_dubug.Size = new System.Drawing.Size(56, 40);
            this.checkedListBox_dubug.TabIndex = 7;
            this.checkedListBox_dubug.TabStop = false;
            this.checkedListBox_dubug.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_debug_ItemCheck);
            this.checkedListBox_dubug.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_dubug_SelectedIndexChanged);
            // 
            // Unicast_communication_textBox
            // 
            this.Unicast_communication_textBox.BackColor = System.Drawing.Color.Black;
            this.Unicast_communication_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Unicast_communication_textBox.ForeColor = System.Drawing.Color.DarkGray;
            this.Unicast_communication_textBox.Location = new System.Drawing.Point(523, 21);
            this.Unicast_communication_textBox.Multiline = true;
            this.Unicast_communication_textBox.Name = "Unicast_communication_textBox";
            this.Unicast_communication_textBox.ReadOnly = true;
            this.Unicast_communication_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Unicast_communication_textBox.Size = new System.Drawing.Size(610, 483);
            this.Unicast_communication_textBox.TabIndex = 6;
            this.Unicast_communication_textBox.Text = ".init";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("等线", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(1030, 507);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 50);
            this.button1.TabIndex = 5;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Debug_SendMsg_textBox
            // 
            this.Debug_SendMsg_textBox.Location = new System.Drawing.Point(523, 507);
            this.Debug_SendMsg_textBox.Multiline = true;
            this.Debug_SendMsg_textBox.Name = "Debug_SendMsg_textBox";
            this.Debug_SendMsg_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Debug_SendMsg_textBox.Size = new System.Drawing.Size(501, 50);
            this.Debug_SendMsg_textBox.TabIndex = 4;
            this.Debug_SendMsg_textBox.Text = resources.GetString("Debug_SendMsg_textBox.Text");
            this.Debug_SendMsg_textBox.WordWrap = false;
            this.Debug_SendMsg_textBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(523, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "单播通信";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(13, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "广播通信";
            // 
            // tabPage_config
            // 
            this.tabPage_config.BackColor = System.Drawing.Color.DimGray;
            this.tabPage_config.Controls.Add(this.label8);
            this.tabPage_config.Location = new System.Drawing.Point(4, 25);
            this.tabPage_config.Name = "tabPage_config";
            this.tabPage_config.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_config.Size = new System.Drawing.Size(1165, 618);
            this.tabPage_config.TabIndex = 2;
            this.tabPage_config.Text = "config";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("华文新魏", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.label8.Location = new System.Drawing.Point(299, 230);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(524, 51);
            this.label8.TabIndex = 23;
            this.label8.Text = "remain to be improved";
            // 
            // tabPage_help
            // 
            this.tabPage_help.BackColor = System.Drawing.Color.DimGray;
            this.tabPage_help.Controls.Add(this.label9);
            this.tabPage_help.Location = new System.Drawing.Point(4, 25);
            this.tabPage_help.Name = "tabPage_help";
            this.tabPage_help.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_help.Size = new System.Drawing.Size(1165, 618);
            this.tabPage_help.TabIndex = 3;
            this.tabPage_help.Text = "help";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("华文新魏", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.label9.Location = new System.Drawing.Point(33, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(651, 391);
            this.label9.TabIndex = 24;
            this.label9.Text = resources.GetString("label9.Text");
            // 
            // timer_count
            // 
            this.timer_count.Enabled = true;
            this.timer_count.Interval = 10;
            this.timer_count.Tick += new System.EventHandler(this.timer_count_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1164, 590);
            this.Controls.Add(this.tabControl_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DT-scope";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.CurveOptions_panel.ResumeLayout(false);
            this.CurveOptions_panel.PerformLayout();
            this.btn_connect_panel.ResumeLayout(false);
            this.btn_connect_panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabControl_Main.ResumeLayout(false);
            this.tabPage_chart.ResumeLayout(false);
            this.tabPage_debug.ResumeLayout(false);
            this.tabPage_debug.PerformLayout();
            this.tabPage_config.ResumeLayout(false);
            this.tabPage_config.PerformLayout();
            this.tabPage_help.ResumeLayout(false);
            this.tabPage_help.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private HslCommunication.Controls.UserCurve Curve1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
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
        private System.Windows.Forms.Button refresh_online_or_break_button;
        private HslCommunication.Controls.UserVerticalProgress Connection_rate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox channelListBox1;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button test_button;
        private System.Windows.Forms.Panel CurveOptions_panel;
        private System.Windows.Forms.Button test_connect;
        private System.Windows.Forms.Button test_rev;
        private System.Windows.Forms.Label rev_text;
        private System.Windows.Forms.Timer timer_UI;
        private System.Windows.Forms.Panel btn_connect_panel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel_github;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label revnum_label;
        private System.Windows.Forms.TabControl tabControl_Main;
        private System.Windows.Forms.TabPage tabPage_chart;
        private System.Windows.Forms.TabPage tabPage_debug;
        private Label label7;
        private TextBox Debug_SendMsg_textBox;
        private Label label1;
        private Button button1;
        private TextBox Unicast_communication_textBox;
        private CheckedListBox checkedListBox_dubug;
        private TabPage tabPage_config;
        private Label label8;
        private TabPage tabPage_help;
        private Label label9;
        private Label label_version;
        private Timer timer_count;
        private TextBox Broadcast_communication_textBox;
    }
}

