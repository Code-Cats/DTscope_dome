using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTscope_dome1._0
{
    /// <summary>
    /// 所有设备名称的枚举，相当于宏
    /// </summary>
    public enum ROBOT_Type
    {
        Sentry1,
        Sentry2,
        Infantry1,
        Infantry2,
        Hero1,
        Hero2,
        Engineer1,
        Engineer2,
        Uav,
        Other1,
        Other2,
        Other3,
        ROBOT_Type_num
    }

    /// <summary>
    /// 表示机器当前在线状态
    /// </summary>
    public enum ROBOT_State
    {
        Off_line,   //放在第一个是默认类型-不在线
        On_line_free,
        On_line_busy,
        On_line_connectOK,    //在线且已连接
        ROBOT_State_num
    }

    /// <summary>
    /// 表示软件当前对单个机器连接状态
    /// </summary>
    public enum ROBOT_Connect_State //
    {
        Unconnected,   //放在第一个是默认类型-未连接
        Wait_Reply_connect,    //等待握手回复
        Wait_OSPF,  //等待透传
        ConnectOK,
        ROBOT_Connect_State_num
    }

    public struct ROBOT_Info  //自定义的数据类型。用来描述员工的信息。 
    {
        public string Type_Name;
        /// <summary>
        /// 在线状态
        /// </summary>
        public ROBOT_State On_line_state;   //在线状态
        /// <summary>
        /// 上一次在线状态 仅在状态更新时-即UDP数据回传时更新上一次
        /// </summary>
        public ROBOT_State On_line_state_last;   //上一次在线状态  //仅在状态更新时-即UDP数据回传时更新上一次
        public bool IsNot_RM;   //是否非RM
        public IPEndPoint TarIpep;  //目标IP
    }

    public partial class Form1 : Form
    {
        /// <summary>
        /// 构造一个字典:将设备描述名称转换为对应结构体数组index
        /// </summary>
        static Dictionary<string, ROBOT_Type>  RobotName_index_Dic =new Dictionary<string, ROBOT_Type>
            {
                {"SENTRY1",ROBOT_Type.Sentry1 },
                {"SENTRY2",ROBOT_Type.Sentry2 },
                {"INFANTRY1",ROBOT_Type.Infantry1 },
                {"INFANTRY2",ROBOT_Type.Infantry2 },
                {"HERO1",ROBOT_Type.Hero1 },
                {"HERO2",ROBOT_Type.Hero2 },
                {"ENGINEER1",ROBOT_Type.Engineer1 },
                {"ENGINEER2",ROBOT_Type.Engineer2 },
                {"UAV",ROBOT_Type.Uav },
                {"OTHER1",ROBOT_Type.Other1 },
                {"OTHER2",ROBOT_Type.Other2 },
                {"OTHER3",ROBOT_Type.Other3 },
            };

        /// <summary>
        /// 构造一个字典:将机器对应结构体的index转换为对应按钮句柄
        /// </summary>
        //static Dictionary<ROBOT_Type, System.Windows.Forms.Button> RobotIndex_button_Dic = new Dictionary<ROBOT_Type, System.Windows.Forms.Button>
        //    {
        //        {ROBOT_Type.Sentry1,new Button(sentry1_connect)  },
        //        {ROBOT_Type.Sentry2 },
        //        {ROBOT_Type.Infantry1 },
        //        {"Infantry2",ROBOT_Type.Infantry2 },
        //        {"Hero1",ROBOT_Type.Hero1 },
        //        {"Hero2",ROBOT_Type.Hero2 },
        //        {"Engineer1",ROBOT_Type.Engineer1 },
        //        {"Engineer2",ROBOT_Type.Engineer2 },
        //        {"Uav",ROBOT_Type.Uav },
        //        {"Other1",ROBOT_Type.Other1 },
        //        {"Other2",ROBOT_Type.Other2 },
        //        {"Other3",ROBOT_Type.Other3 },
        //    };


        static ROBOT_Info[] RobotInfo = new ROBOT_Info[(int)ROBOT_Type.ROBOT_Type_num]; //初始化10个机器信息结构体
        ROBOT_Connect_State Robot_Connect_State = new ROBOT_Connect_State();    //机器连接状态，此软件版本为单连接版


        public Form1()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)  //右侧通知栏
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thrDiscovery = new Thread(Automatic_LAN_discovery);    //局域网发现线程，开机默认开启
            thrDiscovery.Start();

            //this.Focus();

            // userCurve1.BringToFront();
        }

        /// <summary>
        /// 测试用的buttonstate
        /// </summary>
        ROBOT_State button_state = new ROBOT_State();
        private void test_button_Click(object sender, EventArgs e)
        {
            button_state++;
            if (button_state == ROBOT_State.ROBOT_State_num) button_state = 0;
            test_button.Text = button_state.ToString();
            button_set(sentry1_connect, button_state);
            button_set(sentry2_connect, button_state);
            button_set(infantry1_connect, button_state);
            button_set(infantry2_connect, button_state);
            button_set(hero1_connect, button_state);
            button_set(hero2_connect, button_state);
            button_set(engineer1_connect, button_state);
            button_set(engineer2_connect, button_state);
            button_Enabled_Text_Set(sentry1_connect, button_state);
            button_Enabled_Text_Set(sentry2_connect, button_state);
        }

        private void button_set(System.Windows.Forms.Button button , ROBOT_State button_state)
        {
            //button.Name.Length//可以通过长度、字符串识别来识别传入的按钮属于什么684835
            switch (button_state)
            {
                case ROBOT_State.Off_line:
                    {
                        button.BackColor = Color.FromArgb(65, 65, 65); //灰色 disconnect
                        button.ForeColor = Color.FromArgb(140, 140, 140); //160 160 160
                        //button.Enabled = false;
                        //SetControlEnabled(button, false);
                        //button.Text = button.Text.Split('\n')[0]+ "\noff-line";//fuck this!线程中连按钮文本都不能改变？？？？服  了 
                        Connection_rate.Value = 10;
                        break;
                    }
                case ROBOT_State.On_line_busy:
                    {
                        button.BackColor = Color.FromArgb(20, 140, 210); //灰色 disconnect
                        button.ForeColor = Color.FromArgb(200, 200, 200); //160 160 160 字体颜色
                                                                          // button.Enabled = false;
                        //SetControlEnabled(button, false);//转移到定时器里了
                        
                        //button.Text = button.Text.Split('\n')[0] + "\nhas_occupied";//fuck this!线程中连按钮文本都不能改变？？？？服  了
                        
                        Connection_rate.Value = 10;
                        break;
                    }
                case ROBOT_State.On_line_connectOK:
                    {
                        button.BackColor = Color.FromArgb(2, 131, 201);
                        button.ForeColor = Color.FromArgb(243, 249, 252);//46 58 132
                                                                         // button.Enabled = false;
                        //SetControlEnabled(button, false); //转移到定时器里去了
                        
                        //button.Text = button.Text.Split('\n')[0] + "\nconnect_OK";//fuck this!线程中连按钮文本都不能改变？？？？服  了
                        /////////////////////////button.Text = button.Text.Replace("click_connect", "off-line");
                        Connection_rate.Value = 10;
                        break;
                    }
                case ROBOT_State.On_line_free:
                    {
                        button.BackColor = Color.FromArgb(2, 131, 201);
                        button.ForeColor = Color.FromArgb(243, 249, 252);//46 58 132
                                                                         //button.Enabled = true;
                        //SetControlEnabled(button, true);//它被线程调用会发生死循环异常2019.3.21 所以转移到定时器里去了
                        //button.Text = button.Text.Replace("off-line", "click_connect");//= " 哨兵1   connect ";
                        //button.Text = button.Text.Split('\n')[0] + "\nclick_connect"; //fuck this!线程中连按钮文本都不能改变？？？？服  了
                        Connection_rate.Value = 50;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 微笑，这个函数存在的原因是网上魔改的buttonEnabled函数无法在线程中使用，只能在定时器定时刷新
        /// </summary>
        /// <param name="button"></param>
        /// <param name="button_state"></param>
        private void button_Enabled_Text_Set(System.Windows.Forms.Button button, ROBOT_State button_state)
        {
            switch (button_state)
            {
                case ROBOT_State.Off_line:
                    {
                        SetControlEnabled(button, false);
                        button.Text = button.Text.Split('\n')[0] + "\noff-line";
                        break;
                    }
                case ROBOT_State.On_line_busy:
                    {
                        SetControlEnabled(button, false);
                        button.Text = button.Text.Split('\n')[0] + "\nhas_occupied";
                        break;
                    }
                case ROBOT_State.On_line_connectOK:
                    {
                        SetControlEnabled(button, false);
                        button.Text = button.Text.Split('\n')[0] + "\nconnect_OK";
                        break;
                    }
                case ROBOT_State.On_line_free:
                    {
                        SetControlEnabled(button, true);//它被线程调用会发生死循环异常2019.3.21
                        button.Text = button.Text.Split('\n')[0] + "\nclick_connect";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void channelListBox1_SelectedIndexChanged(object sender, EventArgs e)   //通道选择发生改变
        {

        }

        //创建UdpClient对象
        /// <summary>
        /// 设置本地端口1812发广播
        /// </summary>
        static UdpClient UdpBroadcastSend = new UdpClient(1812);//设置本地端口1812发广播
        /// <summary>
        /// 设置本地端口1813收广播
        /// </summary>
        static UdpClient UdpBroadcastRev;// = new UdpClient(1813);//设置本地端口1813收广播
        /// <summary>
        /// 设置本地端口1812发专线
        /// </summary>
        static UdpClient UdpDedicatedSend = new UdpClient(1814);//设置本地端口1812发专线
        /// <summary>
        /// 设置本地端口1813收专线
        /// </summary>
        static UdpClient UdpDedicatedRev = new UdpClient(1815);//设置本地端口1813收专线

        
        private void test_connect_Click(object sender, EventArgs e)
        {
            Thread thrSend = new Thread(SendMessage_DiscoveryRobot);    //发送线程
            thrSend.Start("#RM-DT=TEST:#END");
        }

        /// <summary>
        /// 时间计数，在定时器中运行;单位：10ms
        /// </summary>
        int time_10ms_count = 0;
        /// <summary>
        /// 使UDP监听只在最开始开启一次
        /// </summary>
        bool LAN_discovery_start_flag = false;
        /// <summary>
        /// 上一次局域网问询的时间，避免同一时间大量问询
        /// </summary>
        int last_LAN_discovery_time = 0;
        /// <summary>
        /// 局域网发现每次随机间隔，防止所有主机同步发送
        /// </summary>
        Random LAN_discovery_interval_rd = new Random();
        /// <summary>
        /// 局域网机器自动启动发现函数，Form加载时自动运行
        /// </summary>
        /// <param name="obj"></param>
        private void Automatic_LAN_discovery(object obj)
        {
            if (LAN_discovery_start_flag==false)   //初始化时默认为false，所以会默认连接
            {
                UdpBroadcastRev = new UdpClient(1813);
                thrRecv = new Thread(ReceiveMessage_DiscoveryRobot);
                thrRecv.Start();
                IsUdpcRecvStart = true;
                LAN_discovery_start_flag = true;
                //test_rev.Text = "连接成功";
            }
            else
            {
            }
            int temp_random = 0;
            while(true)
            {
                if((time_10ms_count-last_LAN_discovery_time)>= temp_random)//随机数产生放在if里会导致一个BUG，在100ms-101ms时不断刷新导致刷到101ms从而看起来每次都是101ms
                {
                    Thread thrSend = new Thread(SendMessage_DiscoveryRobot);    //发送线程
                    thrSend.Start("#RM-DT=DCY_ROBOT:#END");
                    last_LAN_discovery_time = time_10ms_count;
                    temp_random = LAN_discovery_interval_rd.Next(100, 200);
                }
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        ///<param name="obj">
        private void SendMessage_DiscoveryRobot(object obj)
        {
            try
            {
                string message = (string)obj;
                //string message = "#RM-DT=DCY_ROBOT#END";
                byte[] sendbytes = Encoding.Default.GetBytes(message);
                IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse("192.168.1.255"), 1813); // 发送到的IP地址和端口号
                UdpBroadcastSend.Send(sendbytes, sendbytes.Length, remoteIpep);
                //UdpBroadcastSend.Close();
            }
            catch
            {
                //提示：直接在按钮上提示或者新建label
            }
            //thrSend.Abort();

        }

        /// <summary>
        /// 接收数据线程函数
        /// </summary>
        ///<param name="obj">
        private void ReceiveMessage_DiscoveryRobot(object obj)
        {
            IPEndPoint revIpep = new IPEndPoint(IPAddress.Any, 0);    //这个没看出来干什么用 直接填0吧
            while (true)
            {
                try
                {
                    byte[] bytRecv = UdpBroadcastRev.Receive(ref revIpep);
                    string message = Encoding.Default.GetString(bytRecv, 0, bytRecv.Length);
                    //rev_msg = rev_msg.Insert(-1, message);//(int)rev_msg.LongCount()
                    Broadcast_Message_Deal(message);
                    
                    rev_msg = rev_msg.Insert(rev_msg.LastIndexOf('.'), message+"\r\n");
                    // ShowMessage(txtRecvMssg, string.Format("{0}[{1}]", remoteIpep, message));
                    
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
        static string rev_msg=".init";  //对话框的内容储存
        /// <summary>
        /// 广播信息接收处理总函数
        /// </summary>
        /// <param name="rev_msg"></param>
        void Broadcast_Message_Deal(string rev_msg)
        {
            //可以用两种方式检测1、string函数查找帧头帧尾 2、每个字节状态机分析
            if (rev_msg.IndexOf("#RM-DT=") != -1 && rev_msg.IndexOf("#END") != -1)   //该函数会从0开始索引，第一个元素位置是0//如果属于DT-scope数据包
            {   //进入到该if说明#RM-DT=存在
                string temp_type;
                string temp_data;
                rev_msg = Regex.Split(rev_msg, "#RM-DT=", RegexOptions.IgnoreCase)[1];   //#RM-DT=将原字符串分为null 和 REP_DCY……
                string[] temp_string_split = rev_msg.Split(':');

                if(temp_string_split.Length==2) //防止数组越界，数组越界在线程中不会产生错误，会直接终止线程
                {
                    temp_type = rev_msg.Split(':')[0];//分割字符串 不包含该字符
                    temp_data = rev_msg.Split(':')[1];//分割字符串 不包含该字符
                }
                else
                {
                    return;
                }

                switch (temp_type)   //广播信息的分发，下面的处理函数将只关注数据内容，不关注帧头或校验
                {
                    case "DCY_ROBOT":   //其他设备的问询
                        {
                            last_LAN_discovery_time = time_10ms_count;
                            break;
                        }
                    case "REP_DCY": //设备的回复
                        {
                            Discover_Message_Deal(temp_data);
                            break;
                        }
                    case "TEST":
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                //rev_msg.Contains("#RM-DT=DCY_ROBOT:");
            }
        }

        /// <summary>
        /// 局域网自动发现函数：只处理discover reply信息
        /// </summary>
        void Discover_Message_Deal(string rev_msg)
        {
            int im_index, sta_index, type_index = 0;
            string[] temp_robot_data = rev_msg.Split(new char[2] { '=', ';' }); //分割字符串 不包含该字符
            im_index = temp_robot_data.ToList().IndexOf("IM") + 1;
            sta_index = temp_robot_data.ToList().IndexOf("STA") + 1;
            type_index= temp_robot_data.ToList().IndexOf("TYPE") + 1;
            //temp_type = temp1[1].Split(':');
            if(im_index==0||sta_index==0)   //没有状态标识或名称标识，丢弃
            {
                return;
            }
            else
            {
                if(type_index==0)   //若设备没有描述自己IS_RM?，则默认IS RM
                {////////////////////////////////////RobotName_index_Dic
                    ROBOT_Type temp_robot_Typeindex;    //获取该设备命名描述对应的结构体ID存入的变量
                    if (RobotName_index_Dic.TryGetValue(temp_robot_data[im_index], out temp_robot_Typeindex))   //尝试获取该设备命名描述对应的结构体ID，若有则进if
                    {
                        //int temp_robot_index = (int)(RobotName_index_Dic[]);   //获取该设备命名描述对应的结构体ID
                        int temp_on_line_state = 0;
                        if (int.TryParse(temp_robot_data[sta_index], out temp_on_line_state)&& temp_on_line_state!=3)    //将在线状态<string>转换为一个整形值并检查是否成功 //限制3，不允许设备通过广播连接成功
                        {//！！！！！！！！！！！！！！！！！！这里有个问题，当对方传递STA超过限定值，会发生意想不到的情况2019.3.22，应该加上安全限定//该问题无需解决，因为枚举值允许放入定义范围外的int值，且后面都是当作int的，swicth会直接丢弃3.22
                            RobotInfo[(int)temp_robot_Typeindex].On_line_state_last = RobotInfo[(int)temp_robot_Typeindex].On_line_state;//更新上一次值
                            RobotInfo[(int)temp_robot_Typeindex].On_line_state = (ROBOT_State)temp_on_line_state;    //若成功了就赋值
                            Update_buttons_callback(RobotInfo[(int)temp_robot_Typeindex], temp_robot_Typeindex);  //调用函数进行按钮刷新
                        }
                    }
                    else    ////尝试获取该设备命名描述对应的结构体ID，若无则说明对方没有按照规则发送，直接丢弃信息
                    {

                    }
                }
                else    //以后在这里加其他设备类型(和RM类同一级别类)
                {

                }
                
            }
        }

        /// <summary>
        /// 按钮状态刷新函数：传入参数为对应按钮结构体，按钮在结构体中对应ID
        /// </summary>
        void Update_buttons_callback(ROBOT_Info robotinfo,ROBOT_Type robot_index)
        {
            switch(robot_index)
            {
                case ROBOT_Type.Sentry1:
                    {
                        button_set(sentry1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Sentry2:
                    {
                        button_set(sentry2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Infantry1:
                    {
                        button_set(infantry1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Infantry2:
                    {
                        button_set(infantry2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Hero1:
                    {
                        button_set(hero1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Hero2:
                    {
                        button_set(hero2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Engineer1:
                    {
                        button_set(engineer1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Engineer2:
                    {
                        button_set(engineer2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Uav:
                    {
                        button_set(uav_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other1:
                    {
                        button_set(other1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other2:
                    {
                        button_set(other2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other3:
                    {
                        button_set(other3_connect, robotinfo.On_line_state);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 按钮使能状态刷新函数：传入参数为对应按钮结构体，按钮在结构体中对应ID，该函数可以在值刷新时才调用
        /// </summary>
        private void Update_buttonsEnabled_andText(ROBOT_Info robotinfo, ROBOT_Type robot_index) 
        {
            switch (robot_index)
            {
                case ROBOT_Type.Sentry1:
                    {
                        button_Enabled_Text_Set(sentry1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Sentry2:
                    {
                        button_Enabled_Text_Set(sentry2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Infantry1:
                    {
                        button_Enabled_Text_Set(infantry1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Infantry2:
                    {
                        button_Enabled_Text_Set(infantry2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Hero1:
                    {
                        button_Enabled_Text_Set(hero1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Hero2:
                    {
                        button_Enabled_Text_Set(hero2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Engineer1:
                    {
                        button_Enabled_Text_Set(engineer1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Engineer2:
                    {
                        button_Enabled_Text_Set(engineer2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Uav:
                    {
                        button_Enabled_Text_Set(uav_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other1:
                    {
                        button_Enabled_Text_Set(other1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other2:
                    {
                        button_Enabled_Text_Set(other2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other3:
                    {
                        button_Enabled_Text_Set(other3_connect, robotinfo.On_line_state);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 开关：在监听UDP报文阶段为true，否则为false
        /// </summary>
        bool IsUdpcRecvStart = false;

        /// <summary>
        /// 线程：不断监听UDP报文
        /// </summary>
        Thread thrRecv;

        /// <summary>
        /// 按钮：是否监听UDP报文
        /// </summary>
        private void test_rev_Click(object sender, EventArgs e) //广播接收
        {
            if(!IsUdpcRecvStart)
            {
                UdpBroadcastRev = new UdpClient(1813);
                thrRecv = new Thread(ReceiveMessage_DiscoveryRobot);
                thrRecv.Start();
                IsUdpcRecvStart = true;
                test_rev.Text = "连接成功";
            }
            else
            {
                thrRecv.Abort();
                UdpBroadcastRev.Close();
                IsUdpcRecvStart = false;
                test_rev.Text = "已关闭";
            }
            
        }

        /// <summary>
        /// UI定时器：更新按键及显示文本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_UI_Tick(object sender, EventArgs e)
        {
            rev_text.Text = rev_msg;
            for(int i=0;i<(int)ROBOT_Type.ROBOT_Type_num;i++)
            {
                Update_buttonsEnabled_andText(RobotInfo[i], (ROBOT_Type)i);
            }
            
            network_communication_labal.Text = rev_msg;

        }

        /// <summary>
        /// 重绘tabControl更改颜色和背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_Main_DrawItem(object sender, DrawItemEventArgs e)   //重绘tabControl更改颜色
        {
            TabControl tc = sender as TabControl;
            Font font = new Font("微软雅黑", 10F);
            SolidBrush bruFont = new SolidBrush(Color.White);   //字体颜色
            SolidBrush tabPageBlack = new SolidBrush(Color.FromArgb(1, 1, 1));//Tab选项卡背景颜色150
            SolidBrush backgroundBlack = new SolidBrush(Color.FromArgb(1, 1, 1));//Tab整体背景颜色77
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;//选项卡文字位置

            //填充选项卡颜色
            Rectangle rec1 = tc.GetTabRect(0);
            e.Graphics.FillRectangle(tabPageBlack, rec1);
            Rectangle rec2 = tc.GetTabRect(1);
            e.Graphics.FillRectangle(tabPageBlack, rec2);
            Rectangle rec3 = tc.GetTabRect(2);
            e.Graphics.FillRectangle(tabPageBlack, rec3);
            Rectangle rec4 = tc.GetTabRect(3);
            e.Graphics.FillRectangle(tabPageBlack, rec4);

            //填充背景色
            Rectangle rec_background = tc.ClientRectangle;
            e.Graphics.FillRectangle(backgroundBlack, rec_background);

            //填写选项卡名称
            e.Graphics.DrawString("chart", font, bruFont, rec1, stringFormat);
            e.Graphics.DrawString("debug", font, bruFont, rec2, stringFormat);
            e.Graphics.DrawString("config", font, bruFont, rec3, stringFormat);
            e.Graphics.DrawString("help", font, bruFont, rec4, stringFormat);

            tabPage_debug.BackColor = System.Drawing.Color.DimGray;
            tabPage_chart.BackColor = System.Drawing.Color.DimGray;
            tabPage_config.BackColor = System.Drawing.Color.DimGray;
            tabPage_help.BackColor = System.Drawing.Color.DimGray;

            ////获取TabControl主控件的工作区域
            //Rectangle rec = tabControl2.ClientRectangle;

            ////获取背景图片，我的背景图片在项目资源文件中。
            ////////Image backImage = Resources.枫叶;

            ////新建一个StringFormat对象，用于对标签文字的布局设置
            //StringFormat StrFormat = new StringFormat();
            //StrFormat.LineAlignment = StringAlignment.Center;// 设置文字垂直方向居中
            //StrFormat.Alignment = StringAlignment.Center;// 设置文字水平方向居中           
            //                                             // 标签背景填充颜色，也可以是图片
            //SolidBrush bru = new SolidBrush(Color.FromArgb(72, 181, 250));
            //SolidBrush bruFont = new SolidBrush(Color.FromArgb(217, 54, 26));// 标签字体颜色
            //Font font = new System.Drawing.Font("微软雅黑", 12F);//设置标签字体样式

            ////绘制主控件的背景
            ////////e.Graphics.DrawImage(backImage, 0, 0, tabControl2.Width, tabControl2.Height);
            ////绘制标签样式
            //for (int i = 0; i < tabControl2.TabPages.Count; i++)
            //{
            //    //获取标签头的工作区域
            //    Rectangle recChild = tabControl2.GetTabRect(i);
            //    //绘制标签头背景颜色
            //    e.Graphics.FillRectangle(bru, recChild);
            //    //绘制标签头的文字
            //    e.Graphics.DrawString(tabControl2.TabPages[i].Text, font, bruFont, recChild, StrFormat);
            //}
        }

        private void tabControl_Main_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tabControl1.TabPages[tabControl1.SelectedIndex].Focus();
            //this.tabControl1.SelectedTab.BackColor = System.Drawing.Color.Transparent;
            //tabPage_chart
        }


        /// <summary>
        /// 关闭程序，强制退出
        /// </summary>
        ///<param name="sender">
        ///<param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        //////////////////////////////////////以下是为了防止按钮在失能时字体颜色发生改变,代替 button.Enabled = <bool>;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public const int GWL_STYLE = -16;
        public const int WS_DISABLED = 0x8000000;

        public void SetControlEnabled(Control c, bool enabled)  //该函数被线程调用会发生死循环
        {
            if (enabled)
            { SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(c.Handle, GWL_STYLE)); }
            else
            { SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(c.Handle, GWL_STYLE)); }
        }//

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void checkedListBox_debug_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked) return;//取消选中就不用进行以下操作
            for (int i = 0; i < ((CheckedListBox)sender).Items.Count; i++)
            {
                ((CheckedListBox)sender).SetItemChecked(i, false);//将所有选项设为不选中
            }
            e.NewValue = CheckState.Checked;//刷新
        }

        private void checkedListBox_dubug_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 提供10ms的时间基准，计数用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_count_Tick(object sender, EventArgs e)
        {
            time_10ms_count++;
        }


        //////////////////////////////////////////////////////////////////////////////////


    }
}
