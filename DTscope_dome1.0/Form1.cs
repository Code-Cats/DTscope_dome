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
        On_line_busy,   ////在线且已与其他主机连接
        On_line_connectOK,    //在线且已与本机连接
        ROBOT_State_num
    }

    /// <summary>
    /// 表示软件当前对单个机器连接状态
    /// </summary>
    public enum HOST_Connect_State //
    {
        Unconnected,   //放在第一个是默认类型-未连接
        Wait_Reply_connect,    //等待握手回复
        Wait_OSPF,  //等待透传
        ConnectOK,
        ROBOT_Connect_State_num
    }

    public struct ROBOT_Info  //自定义的数据类型。用来描述员工的信息。 
    {
        /// <summary>
        /// 记录当前机器对应设备名
        /// </summary>
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

    /// <summary>
    /// 通道数据类型枚举
    /// </summary>
    public enum ROBOT_DATA_FOMAT_TYPE //
    {
        u8,   //
        s8,   //
        u16,  //
        s16,
        u32,
        s32,
        f32,
        ROBOT_DataType_num
    }

    /// <summary>
    /// 机器回传数据单个通道格式
    /// </summary>
    public struct ROBOT_DATA_FOMAT_CHANNEL
    {
        public ROBOT_DATA_FOMAT_TYPE type;    //数据类型
        public int bytes;  //当前数据类型占用多少字节
        public int index;  //当前数据类型首字节索引
        public List<float> datalist;
    }
    /// <summary>
    /// 机器回传数据信息格式
    /// </summary>
    public struct ROBOT_DATA_FOMAT_INFO
    {
        public ROBOT_DATA_FOMAT_CHANNEL[] ch;    //各个通道信息
        public int inter_frame_time;  //帧与帧之间间隔多少ms
        public int frame_bytes;  //一帧所有通道共含有几个字节
        public int ch_num; //当前有效通道数
    }

    public partial class Form1 : Form
    {
        /// <summary>
        /// 定义下位机用来单播通信的端口
        /// </summary>
        const int ROBOT_UNICAST_PORT = 1815;

        const int ROBOT_BROADCAST_PORT = 1813;

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

        /// <summary>
        /// 所有机器信息结构体数组
        /// </summary>
        static ROBOT_Info[] RobotInfo = new ROBOT_Info[(int)ROBOT_Type.ROBOT_Type_num]; //初始化10个机器信息结构体
        /// <summary>
        /// 本机的连接状态，单连接版本一次只能有一个连接
        /// </summary>
        HOST_Connect_State Host_Connect_State = new HOST_Connect_State();    //机器连接状态，此软件版本为单连接版
        string Currently_connected_Device = null;   //当前连接设备名称-单连接版

        public Form1()
        {
            //添加程序集解析事件  
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            InitializeComponent();
        }
        /// <summary>
        /// 生成时将dll嵌入exe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)  //右侧通知栏
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ROBOT_info_dataInit();

            LocalIP =GetLocalIP();   //获取本地IP
            LocalIPEndPoint_Broadcast = new IPEndPoint(IPAddress.Parse(LocalIP), 1812); //本地IP点

            Thread thrDiscovery = new Thread(Automatic_LAN_discovery);    //局域网发现线程，开机默认开启
            thrDiscovery.Start();
            DrawdataGridView_Form_robotDataFomatInfo(RobotDataFomatInfo, dataGridView_channel);
            dataGridView_channel.ClearSelection();

            //this.Focus();

            // userCurve1.BringToFront();
        }

        /// <summary>
        /// RobotInfo数据初始化：遍历value将key赋值给Robotinfo
        /// </summary>
        void ROBOT_info_dataInit()
        {
            foreach (string key in RobotName_index_Dic.Keys)
            {
                RobotInfo[(int)RobotName_index_Dic[key]].Type_Name = key;   //遍历value将key赋值给Robotinfo
            }
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
            button_color_set(sentry1_connect, button_state);
            button_color_set(sentry2_connect, button_state);
            button_color_set(infantry1_connect, button_state);
            button_color_set(infantry2_connect, button_state);
            button_color_set(hero1_connect, button_state);
            button_color_set(hero2_connect, button_state);
            button_color_set(engineer1_connect, button_state);
            button_color_set(engineer2_connect, button_state);
            button_Enabled_Text_Set(sentry1_connect, button_state);
            button_Enabled_Text_Set(sentry2_connect, button_state);
        }

        private void button_color_set(System.Windows.Forms.Button button , ROBOT_State button_state)
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
                        break;
                    }
                case ROBOT_State.On_line_busy:
                    {
                        button.BackColor = Color.FromArgb(20, 140, 210); //灰色 disconnect
                        button.ForeColor = Color.FromArgb(200, 200, 200); //160 160 160 字体颜色
                                                                          // button.Enabled = false;
                        //SetControlEnabled(button, false);//转移到定时器里了
                        
                        //button.Text = button.Text.Split('\n')[0] + "\nhas_occupied";//fuck this!线程中连按钮文本都不能改变？？？？服  了
                        
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
        static UdpClient UdpBroadcastRev;// = new UdpClient(ROBOT_BROADCAST_PORT);//设置本地端口1813收广播
        /// <summary>
        /// 设置本地端口1814发专线
        /// </summary>
        static UdpClient UdpUnicastSend = new UdpClient(1814);//设置本地端口1814发专线
        /// <summary>
        /// 设置本地端口1815收专线
        /// </summary>
        static UdpClient UdpUnicastRev;// = new UdpClient(ROBOT_UNICAST_PORT);//设置本地端口1815收专线

        /// <summary>
        /// 线程：不断监听广播UDP报文
        /// </summary>
        Thread thrRecv_Broadcast;
        /// <summary>
        /// 开关：在监听UDP报文阶段为true，否则为false
        /// </summary>
        bool IsUdpcRecvStart_Broadcast = false;

        /// <summary>
        /// 线程：监听单播UDP报文
        /// </summary>
        Thread thrRecv_Unicast;
        /// <summary>
        /// 开关：在监听UDP报文阶段为true，否则为false
        /// </summary>
        bool IsUdpcRecvStart_Unicast = false;

        /// <summary>
        /// 所有广播信息储存展示
        /// </summary>
        static string All_recvMsg_Broadcast = ".init";  //所有广播信息储存展示
        /// <summary>
        /// 所有单播信息储存展示
        /// </summary>
        static string All_recvMsg_Unicast = ".init";  //所有单播信息储存展示

        string LocalIP;
        IPEndPoint LocalIPEndPoint_Broadcast;
        /// <summary>
        /// 获取本机主机ip 每次程序开启运行一次
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        string ip = "";
                        ip = IpEntry.AddressList[i].ToString();
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void test_connect_Click(object sender, EventArgs e)
        {
            Thread thrSend = new Thread(SendMessage_Broadcast);    //发送线程
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
                thrRecv_Broadcast = new Thread(ReceiveMessage_DiscoveryRobot);
                thrRecv_Broadcast.Start();
                IsUdpcRecvStart_Broadcast = true;
                LAN_discovery_start_flag = true;
                //test_rev.Text = "连接成功";
            }
            else
            {
            }
            int temp_random = 0;
            while(true) //这个while一运行起来就占用15%CPU
            {
                //if(time_10ms_count%2==0)
                if ((time_10ms_count - last_LAN_discovery_time) >= temp_random)//随机数产生放在if里会导致一个BUG，在100ms-101ms时不断刷新导致刷到101ms从而看起来每次都是101ms
                {
                    Thread thrSend = new Thread(SendMessage_Broadcast);    //发送线程
                    thrSend.Start("#RM-DT=DCY_ROBOT:#END");
                    last_LAN_discovery_time = time_10ms_count;
                    temp_random = LAN_discovery_interval_rd.Next(150, 250);
                    //this.thrDiscovery
                    Thread.Sleep(temp_random*15);
                }
            }
        }

        /// <summary>
        /// 发送广播信息
        /// </summary>
        ///<param name="obj">
        private void SendMessage_Broadcast(object obj)
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
            IPEndPoint revIpep = new IPEndPoint(IPAddress.Any, 0);    //这个没看出来干什么用 直接填0吧 IPAddress.Any
            while (true)
            {
                try
                {
                    byte[] bytRecv = UdpBroadcastRev.Receive(ref revIpep);
                    string message = Encoding.Default.GetString(bytRecv, 0, bytRecv.Length);
                    //rev_msg = rev_msg.Insert(-1, message);//(int)rev_msg.LongCount()
                    Broadcast_Message_Deal(message, revIpep);
                    //byte[] temtt = revIpep.Address.GetAddressBytes(); //以字节数组
                    All_recvMsg_Broadcast = All_recvMsg_Broadcast.Insert(All_recvMsg_Broadcast.LastIndexOf('.'), string.Format("[{0}]{1}", revIpep, message) + "\r\n"); //放在这里可以达到不管什么广播信息都会显示的目的
                    // ShowMessage(txtRecvMssg, string.Format("{0}[{1}]", remoteIpep, message));
                    
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    break;
                }
            }
        }

        /// <summary>
        /// 广播信息接收处理总函数
        /// </summary>
        /// <param name="rev_msg"></param>
        void Broadcast_Message_Deal(string rev_msg, IPEndPoint revipep)
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
                    temp_type = temp_string_split[0];//分割字符串 不包含该字符
                    temp_data = temp_string_split[1];//分割字符串 不包含该字符
                }
                else
                {
                    return;
                }
                
                switch (temp_type)   //广播信息的分发，下面的处理函数将只关注数据内容，不关注帧头或校验
                {
                    case "DCY_ROBOT":   //其他设备的问询
                        {
                            if(LocalIP.Equals(revipep.Address.ToString()))  //如果来源于本机IP，就丢弃
                            {

                            }
                            else //来源其他IP，则为其他设备的问询
                            {
                                if (Host_Connect_State == HOST_Connect_State.Unconnected)
                                {
                                    //如果未连接就不做任何回复
                                }
                                else //如果自己开始连接或已经连接上了
                                {
                                    Othor_host_discover_Message_Deal(); //告诉其他主机该设备已连接
                                }
                            }
                            last_LAN_discovery_time = time_10ms_count;  //不管上面if 如何，都会记录时间
                            break;
                        }
                    case "REP_DCY": //设备的回复
                        {
                            if(!LocalIPEndPoint_Broadcast.Equals(revipep))  //不处理自己发的报文
                            Discover_Message_Deal(temp_data, revipep);  //传递IP进去记录
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
        void Discover_Message_Deal(string rev_msg, IPEndPoint revipep)
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
                            if(temp_on_line_state==(int)ROBOT_State.On_line_free)   //仅当on-line-free才记录机器IP，因为机器只会发送这个状态
                            {
                                RobotInfo[(int)temp_robot_Typeindex].TarIpep = revipep;
                            }
                            RobotInfo[(int)temp_robot_Typeindex].On_line_state_last = RobotInfo[(int)temp_robot_Typeindex].On_line_state;//更新上一次值  //这个迭代之后应该统一放在定时器
                            RobotInfo[(int)temp_robot_Typeindex].On_line_state = (ROBOT_State)temp_on_line_state;    //若成功了就赋值
                            Update_buttons_callback(RobotInfo[(int)temp_robot_Typeindex], temp_robot_Typeindex);  //调用函数进行按钮刷新  //这个之后还是放到定时器吧
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
        /// 局域网其他主机的问询回复
        /// </summary>
        /// <param name="rev_msg"></param>
        void Othor_host_discover_Message_Deal()
        {
            if(Currently_connected_Device!=null)    //不为空，即已经与某个设备正在或已经建立连接
            {
                SendMessage_Broadcast("#RM-DT=REP_DCY:IM="+Currently_connected_Device+ ";STA=2;#END");
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
                        button_color_set(sentry1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Sentry2:
                    {
                        button_color_set(sentry2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Infantry1:
                    {
                        button_color_set(infantry1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Infantry2:
                    {
                        button_color_set(infantry2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Hero1:
                    {
                        button_color_set(hero1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Hero2:
                    {
                        button_color_set(hero2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Engineer1:
                    {
                        button_color_set(engineer1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Engineer2:
                    {
                        button_color_set(engineer2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Uav:
                    {
                        button_color_set(uav_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other1:
                    {
                        button_color_set(other1_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other2:
                    {
                        button_color_set(other2_connect, robotinfo.On_line_state);
                        break;
                    }
                case ROBOT_Type.Other3:
                    {
                        button_color_set(other3_connect, robotinfo.On_line_state);
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
        /// 按钮：是否监听UDP报文
        /// </summary>
        //private void test_rev_Click(object sender, EventArgs e) //广播接收
        //{
        //    if(!IsUdpcRecvStart_Broadcast)
        //    {
        //        UdpBroadcastRev = new UdpClient(1813);
        //        thrRecv_Broadcast = new Thread(ReceiveMessage_DiscoveryRobot);
        //        thrRecv_Broadcast.Start();
        //        IsUdpcRecvStart_Broadcast = true;
        //        test_rev.Text = "连接成功";
        //    }
        //    else
        //    {
        //        thrRecv_Broadcast.Abort();
        //        UdpBroadcastRev.Close();
        //        IsUdpcRecvStart_Broadcast = false;
        //        test_rev.Text = "已关闭";
        //    }
            
        //}

        /// <summary>
        /// UI定时器：更新按键及显示文本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_UI_Tick(object sender, EventArgs e)
        {
            //rev_text.Text = All_recvMsg_Broadcast;
            for(int i=0;i<(int)ROBOT_Type.ROBOT_Type_num;i++)
            {
                Update_buttonsEnabled_andText(RobotInfo[i], (ROBOT_Type)i);
            }

            if (Host_Connect_State == HOST_Connect_State.Unconnected)   //根据主机连接状态决定刷新按钮的信息
            {
                refresh_online_or_break_button.Text = "刷新";
            }
            else 
            {
                refresh_online_or_break_button.Text = "断开\r\n连接";
            }

            Broadcast_communication_textBox.Text = All_recvMsg_Broadcast;   //放入对话框缓存
            Unicast_communication_textBox.Text = All_recvMsg_Unicast;   //放入对话框缓存

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

        /// <summary>
        /// 访问超链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("chrome.exe", "https://github.com/yx19981001/DTscope_dome");    //优先使用chrome浏览器打开
            }
            catch
            {
                System.Diagnostics.Process.Start("https://github.com/yx19981001/DTscope_dome");  //默认浏览器打开
            }
        }

        /// <summary>
        /// 禁止选中textBox文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Broadcast_communication_textBox_MouseDown(object sender, MouseEventArgs e)
        {
            Broadcast_communication_textBox.Select(Debug_SendMsg_textBox.Text.Length, 0);
            
        }
        /// <summary>
        /// 禁止选中textBox文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Broadcast_communication_textBox_MouseMove(object sender, MouseEventArgs e)
        {
            Broadcast_communication_textBox.Select(Debug_SendMsg_textBox.Text.Length, 0);
        }

        private void sentry1_connect_Click(object sender, EventArgs e)
        {
            if(Host_Connect_State==HOST_Connect_State.Unconnected)
            {
                Start_Connect_unicast(ROBOT_Type.Sentry1);
            }
        }

        private void sentry2_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Start_Connect_unicast(ROBOT_Type.Sentry2);
                Currently_connected_Device = RobotInfo[1].Type_Name;
            }
        }

        private void infantry1_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Start_Connect_unicast(ROBOT_Type.Infantry1);
                Currently_connected_Device = RobotInfo[2].Type_Name;
            }
        }

        private void infantry2_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Start_Connect_unicast(ROBOT_Type.Infantry2);
                Currently_connected_Device = RobotInfo[3].Type_Name;
            }
        }

        private void hero1_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Currently_connected_Device = RobotInfo[4].Type_Name;
                Host_Connect_State = HOST_Connect_State.Wait_OSPF;
            }
        }

        private void hero2_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Currently_connected_Device = RobotInfo[5].Type_Name;
            }
        }

        private void engineer1_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Currently_connected_Device = RobotInfo[6].Type_Name;
            }
        }

        private void engineer2_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Currently_connected_Device = RobotInfo[7].Type_Name;
            }
        }

        private void uav_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Currently_connected_Device = RobotInfo[8].Type_Name;
            }
        }

        private void other1_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Currently_connected_Device = RobotInfo[9].Type_Name;
            }
        }

        private void other2_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Currently_connected_Device = RobotInfo[10].Type_Name;
            }
        }

        private void other3_connect_Click(object sender, EventArgs e)
        {
            if (Host_Connect_State == HOST_Connect_State.Unconnected)
            {
                Currently_connected_Device = RobotInfo[11].Type_Name;
            }
        }

        /// <summary>
        /// 开始尝试连接设备，此函数在点击连接按钮时触发，只触发一次
        /// </summary>
        /// <param name="obj"></param>
        private void Start_Connect_unicast(object obj)  //
        {
            int robot_typeindex = (int)obj;
            Currently_connected_Device = RobotInfo[robot_typeindex].Type_Name;    //记录该值，当其他主机问询用
            Host_Connect_State = HOST_Connect_State.Wait_Reply_connect;

            Thread thrSend = new Thread(SendMessage_unicast);    //单播发送握手请求
            string[] sendmeg = new string[] { "#RM-DT=CNET:TAR="+ Currently_connected_Device+ ";TIP="+ RobotInfo[robot_typeindex].TarIpep.Address.ToString()+ ";CIP="+LocalIP+ ";CPT="+"1815"+";#END", RobotInfo[robot_typeindex].TarIpep.Address.ToString() };
            thrSend.Start(sendmeg);

            //thrRecv_Unicast = new Thread();
            ///////////////
            UdpUnicastRev = new UdpClient(1815);
            thrRecv_Unicast = new Thread(ReceiveMessage_Unicast);
            thrRecv_Unicast.Start(RobotInfo[robot_typeindex].TarIpep);
            IsUdpcRecvStart_Unicast = true;
            ////////////////////////
            Connection_rate.Value = 10; //发送后就进度10%

            SendMessage_Broadcast("#RM-DT=REP_DCY:IM=" + Currently_connected_Device + ";STA=2;#END");   //告诉其他主机本机已连接该设备
        }

        /// <summary>
        /// 单播发送函数，传入参数string[]: msg, tarip
        /// </summary>
        /// <param name="obj"></param>
        private void SendMessage_unicast(object obj)//string sendmsg,(string)(tarip)
        {
            string[] sendmsg = (string[])obj;
            //IPEndPoint tempip= IPEndPoint  
            try
            {
                //string message = "#RM-DT=DCY_ROBOT#END";
                byte[] sendbytes = Encoding.Default.GetBytes(sendmsg[0]);
                IPEndPoint tarIpep = new IPEndPoint(IPAddress.Parse(sendmsg[1]), ROBOT_UNICAST_PORT); // 发送到的IP地址和端口号
                UdpUnicastSend.Send(sendbytes, sendbytes.Length, tarIpep);
                //UdpUnicastSend.Close();
            }
            catch
            {
                //提示：直接在按钮上提示或者新建label
            }
        }

        /// <summary>
        /// 接收单播数据线程函数/握手/数据帧
        /// </summary>
        ///<param name="obj">
        private void ReceiveMessage_Unicast(object obj)
        {
            IPEndPoint revIpep = new IPEndPoint(IPAddress.Any, 0);    //这个没看出来干什么用 直接填0吧 IPAddress.Any
            IPEndPoint tarIpep = (IPEndPoint)obj;   //目标IP，便于后面传参分析，或增加新的功能
            while (true)
            {
                try
                {
                    byte[] bytRecv = UdpUnicastRev.Receive(ref revIpep);
                    string strRecv = Encoding.Default.GetString(bytRecv, 0, bytRecv.Length);
                    Unicast_Message_Deal(strRecv,bytRecv, revIpep, tarIpep);
                    //byte[] temtt = revIpep.Address.GetAddressBytes(); //以字节数组
                    All_recvMsg_Unicast = All_recvMsg_Unicast.Insert(All_recvMsg_Unicast.LastIndexOf('.'), string.Format("[{0}]{1}", revIpep, strRecv) + "\r\n"); //放在这里可以达到不管什么单播信息都会显示的目的

                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    break;
                }
            }
        }

        /// <summary>
        /// 接收单播数据总处理函数
        /// </summary>
        /// <param name="bytrecv"></param>
        /// <param name="revipep"></param>
        /// <param name="taripep"></param>
        void Unicast_Message_Deal(string strrecv,byte[] bytrecv, IPEndPoint revipep, IPEndPoint taripep)
        {//后期多连接肯定要对发送方IP判断兵种再实现多连接通用函数，现在先用curr
            
            if (strrecv.IndexOf("#RM-DT=") != -1 && strrecv.IndexOf("#END") != -1)   //该函数会从0开始索引，第一个元素位置是0//如果属于DT-scope数据包
            {   //进入到该if说明#RM-DT=存在
                string temp_datatype;
                string temp_data;
                strrecv = Regex.Split(strrecv, "#RM-DT=", RegexOptions.IgnoreCase)[1];   //#RM-DT=将原字符串分为null 和 REP_DCY……
                string[] temp_string_split = strrecv.Split(':');

                if (temp_string_split.Length == 2) //防止数组越界，数组越界在线程中不会产生错误，会直接终止线程
                {
                    temp_datatype = temp_string_split[0];//分割字符串 不包含该字符
                    temp_data = temp_string_split[1];//分割字符串 不包含该字符
                }
                else
                {
                    return;
                }

                switch (Host_Connect_State) //根据连接状态
                {
                    case HOST_Connect_State.Unconnected:
                        {
                            break;
                        }
                    case HOST_Connect_State.Wait_Reply_connect:
                        {
                            Connection_rate.Value = 30;
                            if (temp_datatype.Equals("RCNET"))
                            {
                                if(Unicast_Handshake_Reply_Check(temp_data, taripep)) //若握手回复协议正确，则进入等待透传阶段
                                {
                                    Host_Connect_State = HOST_Connect_State.Wait_OSPF;
                                    Connection_rate.Value = 60;
                                }

                            }
                            break;
                        }
                    case HOST_Connect_State.Wait_OSPF:
                        {
                            if (temp_datatype.Equals("RCNET"))
                            {
                                if (temp_data.Equals("OK;#END"))    //即回复 #RM-DT=RCNET:OK;#END
                                {
                                    Host_Connect_State = HOST_Connect_State.ConnectOK;
                                    
                                    ROBOT_Type temp_robot_Typeindex = 0;
                                    if (RobotName_index_Dic.TryGetValue(Currently_connected_Device, out temp_robot_Typeindex))   //尝试获取该设备命名描述对应的结构体ID，若有则进if
                                    {
                                        RobotInfo[(int)temp_robot_Typeindex].On_line_state_last = RobotInfo[(int)temp_robot_Typeindex].On_line_state;   //这个迭代之后应该统一放在定时器，防止程序过大编写遗漏
                                        RobotInfo[(int)temp_robot_Typeindex].On_line_state = ROBOT_State.On_line_connectOK;
                                    }
                                    Update_buttons_callback(RobotInfo[(int)temp_robot_Typeindex], temp_robot_Typeindex);  //调用函数进行按钮刷新//这个之后还是放到定时器吧

                                    //在准备工作都做好后 发送一个#RM-DT=CNET:OK;#END
                                    Thread thrSend = new Thread(SendMessage_unicast);    //单播发送握手请求
                                    string[] sendmeg = new string[] { "#RM-DT=CNET:OK;#END", RobotInfo[(int)temp_robot_Typeindex].TarIpep.Address.ToString() };
                                    thrSend.Start(sendmeg);

                                    Connection_rate.Value = 100;
                                }
                            }
                            break;
                        }
                    case HOST_Connect_State.ConnectOK:
                        {
                            //这里放数据接收、缓存、显示相关
                            RobotData_Receive_Main(temp_datatype, temp_data);   //connectOK后数据处理总函数
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
        }
        /// <summary>
        /// 检测单播握手回复是否正确
        /// </summary>
        /// <param name="strrecv"></param>
        /// <returns></returns>
        bool Unicast_Handshake_Reply_Check(string strrecv, IPEndPoint taripep)
        {
            int tip_index, cip_index, cpt_index = 0;
            string[] temp_robot_data = strrecv.Split(new char[3] { '=', ';',':' }); //分割字符串 加上：原因是防止调用出错
            tip_index = temp_robot_data.ToList().IndexOf("TIP") + 1; //机器的目标IP，即本机IP
            cip_index = temp_robot_data.ToList().IndexOf("CIP") + 1;    //机器的当前IP，即HOST的目标IP
            cpt_index = temp_robot_data.ToList().IndexOf("CPT") + 1;   //机器的当前端口，即HOST的目标端口
            //temp_type = temp1[1].Split(':');
            if (tip_index == 0 || cip_index == 0 || cpt_index==0)   //没有状态标识或名称标识，丢弃
            {
                return false;
            }
            else
            {//下面的IF检测双方IP，对方PT是否正确
                if (temp_robot_data[tip_index].Equals(LocalIP) && temp_robot_data[cip_index].Equals(taripep.Address.ToString()) && temp_robot_data[cpt_index].Equals(ROBOT_UNICAST_PORT.ToString()))   //检测双方IP，对方PT是否正确
                {
                    return true;    //回应正确
                }
           
            }
            return false;
        }

        /// <summary>
        /// 刷新或者断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refresh_online_or_break_button_Click(object sender, EventArgs e)   //在非连接状态时刷新用，在连接状态复用为断开连接
        {
            switch(Host_Connect_State)
            {
                case HOST_Connect_State.Unconnected:    //未连接时刷新使用
                    {
                        Thread thrSend = new Thread(SendMessage_Broadcast);    //发送线程
                        thrSend.Start("#RM-DT=DCY_ROBOT:#END");
                        last_LAN_discovery_time = time_10ms_count;
                        break;
                    }
                case HOST_Connect_State.Wait_Reply_connect:
                case HOST_Connect_State.Wait_OSPF:
                case HOST_Connect_State.ConnectOK:  //断开连接//需要停止接收线程、关掉UDP单播
                    {
                        thrRecv_Unicast.Abort();
                        UdpUnicastRev.Close();
                        IsUdpcRecvStart_Unicast = false;
                        Connection_rate.Value = 0; //发送后就进度0%

                        Host_Connect_State = HOST_Connect_State.Unconnected;

                        ROBOT_Type temp_robot_Typeindex = 0;
                        if (RobotName_index_Dic.TryGetValue(Currently_connected_Device, out temp_robot_Typeindex))   //尝试获取该设备命名描述对应的结构体ID，若有则进if
                        {
                            RobotInfo[(int)temp_robot_Typeindex].On_line_state_last = RobotInfo[(int)temp_robot_Typeindex].On_line_state;   //这个迭代之后应该统一放在定时器，防止程序过大编写遗漏
                            RobotInfo[(int)temp_robot_Typeindex].On_line_state = ROBOT_State.On_line_free;
                        }
                        Update_buttons_callback(RobotInfo[(int)temp_robot_Typeindex], temp_robot_Typeindex);  //调用函数进行按钮刷新//这个之后还是放到定时器吧

                        Currently_connected_Device = null;
                        break;
                    }
            }
        }

        private void button_test_Datagrad_Click(object sender, EventArgs e)
        {
            //int index = this.dataGridView_channel.Rows.Add();
            //this.dataGridView_channel.Rows[index].Cells[0].Value = "1";
            //this.dataGridView_channel.Rows[index].Cells[1].Value = "2";

            ////DataGridViewRow row = new DataGridViewRow();
            ////DataGridViewCheckBoxCell comboxcell = new DataGridViewCheckBoxCell();
            ////row.Cells.Add(comboxcell);
            ////DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
            ////textboxcell.Value = "ch0";
            ////row.Cells.Add(textboxcell);
            ////dataGridView_channel.Rows.Add(row);

            ////DataGridViewRow row2 = new DataGridViewRow();
            ////DataGridViewCheckBoxCell comboxcell2 = new DataGridViewCheckBoxCell();
            ////row2.Cells.Add(comboxcell2);
            ////DataGridViewTextBoxCell textboxcell2 = new DataGridViewTextBoxCell();
            ////textboxcell2.Value = "ch1";
            ////row2.Cells.Add(textboxcell2);
            ////dataGridView_channel.Rows.Add(row2);

            ////dataGridView_channel.ClearSelection();

            ////dataGridView_channel.Rows[0].Cells[2].Value = time_10ms_count.ToString();
            ////dataGridView_channel.Rows[1].Cells[0].Value = true;
        }

        /// <summary>
        /// 单击表格内容时发生，用来清除高亮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_channel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Object tem = dataGridView_channel.Rows[0].Cells[0].Value; //无效
            dataGridView_channel.ClearSelection();  //取消高亮选中，更美观
        }

        /// <summary>
        /// 表格初始内容加载一次
        /// </summary>
        bool IsLoadInit_DataGridView = false;
        /// <summary>
        /// 表格控件布局时发生，发现会运行两次？
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_channel_Layout(object sender, LayoutEventArgs e)
        {
            if(!IsLoadInit_DataGridView)
            {
                robotDataFomatInfo_DataInit(out RobotDataFomatInfo);    //数据格式信息初始化

                DrawdataGridView_Form_robotDataFomatInfo(RobotDataFomatInfo, dataGridView_channel);//根据fomatinfo信息生成dataGridView

                //dataGridView_AddNewRow(dataGridView_channel, "ch0"); 
                //dataGridView_AddNewRow(dataGridView_channel, "ch1");
                //dataGridView_AddNewRow(dataGridView_channel, "ch2");
                //dataGridView_AddNewRow(dataGridView_channel, "ch3");

                //dataGridView_channel.Rows[0].Cells[2].Value = time_10ms_count.ToString();
                //dataGridView_channel.Rows[1].Cells[0].Value = true;

                dataGridView_channel.ClearSelection();

                IsLoadInit_DataGridView = true;
            }
            dataGridView_channel.ClearSelection();
        }

        /// <summary>
        /// 在生成dataGridView时添加新行
        /// </summary>
        /// <param name="datagridview"></param>
        /// <param name="ch_name"></param>
        private void dataGridView_AddNewRow(DataGridView datagridview,string ch_name)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCheckBoxCell checkboxcell = new DataGridViewCheckBoxCell();
            row.Cells.Add(checkboxcell);
            DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
            textboxcell.Value = ch_name;
            row.Cells.Add(textboxcell);
            datagridview.Rows.Add(row);
        }

        /// <summary>
        /// 机器回传数据格式信息及通道信息结构体，包含通道信息  version单连接
        /// </summary>
        ROBOT_DATA_FOMAT_INFO RobotDataFomatInfo = new ROBOT_DATA_FOMAT_INFO(); //需要在load前进行初始化
        /// <summary>
        /// RobotDataFomatInfo数据初始化
        /// </summary>
        public void robotDataFomatInfo_DataInit(out ROBOT_DATA_FOMAT_INFO data)
        {
            data.inter_frame_time = 1;
            data.frame_bytes = 8;
            data.ch_num = 5;
            data.ch = new ROBOT_DATA_FOMAT_CHANNEL[data.ch_num];

            for(int i=0;i< data.ch_num;i++)
            {
                data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.s16;
                data.ch[i].index = i * 2;
                data.ch[i].bytes = 10;
            }
            
        }
        /// <summary>
        /// 根据数据格式信息生成DataGridView表格部分信息
        /// </summary>
        /// <param name="fomat_data"></param>
        /// <param name="data_gridview"></param>
        public void DrawdataGridView_Form_robotDataFomatInfo(ROBOT_DATA_FOMAT_INFO fomat_data, DataGridView data_gridview)
        {
            //DataGridView temp_dataGridView = new DataGridView();  //new的方法需要加入列，很麻烦 直接删除原来的行吧

            for (int i = 0; i < data_gridview.Rows.Count; i++)  //删除当前所有行
            {
                DataGridViewRow row = data_gridview.Rows[i];
                data_gridview.Rows.Remove(row);
                i--; //这句是关键。。
            }

            for (int i = 0; i < fomat_data.ch_num; i++) 
            {
                dataGridView_AddNewRow(data_gridview, "ch"+i.ToString());
            }
            //data_gridview = temp_dataGridView;
            dataGridView_channel.ClearSelection();
        }

        /// <summary>
        /// connectOK后对机器信息处理的总函数
        /// </summary>
        /// <param name="strtype"></param>
        /// <param name="strdata"></param>
        private void RobotData_Receive_Main(string strtype, string strdata)
        {//strdata数据格式：TIM=<tim_interavl>;ABYTES=<byte_numbers>;TYPE=<s2.s2.s2.s2>;#END
            switch (strtype)
            {
                case "DATA_INFO":
                    {
                        RobotData_InfoSet(strdata,ref RobotDataFomatInfo);
                        break;
                    }
                case "DATA":
                    {
                        break;
                    }
                case "CMD":
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 设置数据接受信息
        /// </summary>
        /// <param name="strset"></param>
        private void RobotData_InfoSet(string strset,ref ROBOT_DATA_FOMAT_INFO fomat_data)
        {//strdata数据格式：TIM=<tim_interavl>;ABYTES=<byte_numbers>;TYPE=<s2.s2.s2.s2>;#END

            int tim_index, abytes_index, type_index = 0;
            string[] temp_fomatinfo_data = strset.Split(new char[2] { '=', ';' }); //分割字符串 不包含该字符
            tim_index = temp_fomatinfo_data.ToList().IndexOf("TIM") + 1;
            abytes_index = temp_fomatinfo_data.ToList().IndexOf("ABYTES") + 1;
            type_index = temp_fomatinfo_data.ToList().IndexOf("TYPE") + 1;
            //temp_type = temp1[1].Split(':');
            if (tim_index == 0 || abytes_index == 0 || type_index == 0)   //没有任意一个标识，丢弃
            {
                return;
            }
            else //有标识，将fomat信息存入
            {
                if (int.TryParse(temp_fomatinfo_data[tim_index], out fomat_data.inter_frame_time) &&
                    int.TryParse(temp_fomatinfo_data[abytes_index], out fomat_data.frame_bytes))
                {   //到此处该处理s2.s2.s2.s2.s2
                    string[] temp_type_data = temp_fomatinfo_data[type_index].Split('.');
                    if(robotDataFomatInfo_ChannelType_TrySet(temp_type_data, ref fomat_data))   //如果通道信息正常
                    {
                        //刷新datagrid
                        DrawdataGridView_Form_robotDataFomatInfo(RobotDataFomatInfo, dataGridView_channel);
                        //在准备工作都做好后 发送一个#RM-DT=INFOOK:#END
                        ROBOT_Type robot_typeindex;
                        RobotName_index_Dic.TryGetValue(Currently_connected_Device, out robot_typeindex);   //获取当前机器ID
                        Thread thrSend = new Thread(SendMessage_unicast);    //单播发送握手请求
                        string[] sendmeg = new string[] { "#RM-DT=INFOOK:#END", RobotInfo[(int)robot_typeindex].TarIpep.Address.ToString() };
                        thrSend.Start(sendmeg);
                    }
                    else //通道信息解析出错
                    {

                    }


                }

                //data.inter_frame_time = 1;
                //data.frame_bytes = 8;
                //data.ch_num = 5;
                //data.ch = new ROBOT_DATA_FOMAT_CHANNEL[data.ch_num];

                //for (int i = 0; i < data.ch_num; i++)
                //{
                //    data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.s16;
                //    data.ch[i].index = i * 2;
                //    data.ch[i].bytes = 10;
                //}

            }
            
                
        }
        /// <summary>
        /// 机器回传格式信息中对所有通道变量信息的设置，传入格式为s2.s2.s2.s2.s2
        /// </summary>
        /// <param name="strtype"></param>
        /// <param name="fomat_data"></param>
        /// <returns></returns>
        private bool robotDataFomatInfo_ChannelType_TrySet(string[] strtype, ref ROBOT_DATA_FOMAT_INFO fomat_data)//机器回传格式信息中对通道变量信息的设置
        {//目前只支持：s8,u8,s16,u16,s32,u32,float  2019.3.25//data.ch = new ROBOT_DATA_FOMAT_CHANNEL[data.ch_num];
            fomat_data.ch_num = strtype.Length; //先获取分成了几个块、即几个通道
            fomat_data.ch = new ROBOT_DATA_FOMAT_CHANNEL[fomat_data.ch_num];    //刷新信息，刷新之前的信息
            for (int i = 0; i < strtype.Length; i++)
            {
                fomat_data.ch_num = i;  //再更新ch_num值，以免执行到某一步后面的解析失败了
                switch (strtype[i])
                {
                    case "s1":
                        {
                            fomat_data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.s8;   //设置当前通道类型
                            fomat_data.ch[i].bytes = 1; //设置当前通道所占字节
                            if(i==0)    //如果是第一个通道直接索引=0
                            {
                                fomat_data.ch[i].index = 0;
                            }
                            else //后面几个通道索引=前一个索引+前一个bytes
                            {
                                fomat_data.ch[i].index = fomat_data.ch[i - 1].index + fomat_data.ch[i - 1].bytes;
                            }
                            break;
                        }
                    case "u1":
                        {
                            fomat_data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.u8;   //设置当前通道类型
                            fomat_data.ch[i].bytes = 1; //设置当前通道所占字节
                            if (i == 0)    //如果是第一个通道直接索引=0
                            {
                                fomat_data.ch[i].index = 0;
                            }
                            else //后面几个通道索引=前一个索引+前一个bytes
                            {
                                fomat_data.ch[i].index = fomat_data.ch[i - 1].index + fomat_data.ch[i - 1].bytes;
                            }
                            break;
                        }
                    case "s2":
                        {
                            fomat_data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.s16;  //设置当前通道类型
                            fomat_data.ch[i].bytes = 2; //设置当前通道所占字节
                            if (i == 0)    //如果是第一个通道直接索引=0
                            {
                                fomat_data.ch[i].index = 0;
                            }
                            else //后面几个通道索引=前一个索引+前一个bytes
                            {
                                fomat_data.ch[i].index = fomat_data.ch[i - 1].index + fomat_data.ch[i - 1].bytes;
                            }
                            break;
                        }
                    case "u2":
                        {
                            fomat_data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.u16;  //设置当前通道类型
                            fomat_data.ch[i].bytes = 2; //设置当前通道所占字节
                            if (i == 0)    //如果是第一个通道直接索引=0
                            {
                                fomat_data.ch[i].index = 0;
                            }
                            else //后面几个通道索引=前一个索引+前一个bytes
                            {
                                fomat_data.ch[i].index = fomat_data.ch[i - 1].index + fomat_data.ch[i - 1].bytes;
                            }
                            break;
                        }
                    case "s4":
                        {
                            fomat_data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.s32;  //设置当前通道类型
                            fomat_data.ch[i].bytes = 4; //设置当前通道所占字节
                            if (i == 0)    //如果是第一个通道直接索引=0
                            {
                                fomat_data.ch[i].index = 0;
                            }
                            else //后面几个通道索引=前一个索引+前一个bytes
                            {
                                fomat_data.ch[i].index = fomat_data.ch[i - 1].index + fomat_data.ch[i - 1].bytes;
                            }
                            break;
                        }
                    case "u4":
                        {
                            fomat_data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.u32;  //设置当前通道类型
                            fomat_data.ch[i].bytes = 4; //设置当前通道所占字节
                            if (i == 0)    //如果是第一个通道直接索引=0
                            {
                                fomat_data.ch[i].index = 0;
                            }
                            else //后面几个通道索引=前一个索引+前一个bytes
                            {
                                fomat_data.ch[i].index = fomat_data.ch[i - 1].index + fomat_data.ch[i - 1].bytes;
                            }
                            break;
                        }
                    case "f4":
                        {
                            fomat_data.ch[i].type = ROBOT_DATA_FOMAT_TYPE.f32;  //设置当前通道类型
                            fomat_data.ch[i].bytes = 4; //设置当前通道所占字节
                            if (i == 0)    //如果是第一个通道直接索引=0
                            {
                                fomat_data.ch[i].index = 0;
                            }
                            else //后面几个通道索引=前一个索引+前一个bytes
                            {
                                fomat_data.ch[i].index = fomat_data.ch[i - 1].index + fomat_data.ch[i - 1].bytes;
                            }
                            break;
                        }
                    default:    //不存在的类型，直接返回false
                        {
                            return false;
                        }
                }//switch结束
                fomat_data.ch_num = i+1;  //再更新ch_num值，以免执行到某一步后面的解析失败了
            }//for结束
            //下面该检查frame_bytes是否和types计算出来的是否相等
            if(fomat_data.ch[fomat_data.ch_num-1].index+ fomat_data.ch[fomat_data.ch_num - 1].bytes== fomat_data.frame_bytes)
            {
                return true;    //符合一系列条件
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////////////


    }
}
