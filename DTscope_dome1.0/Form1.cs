using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTscope_dome1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)  //右侧通知栏
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Focus();

           // userCurve1.BringToFront();
        }
       
        static bool button_flag = false;
        private void test_button_Click(object sender, EventArgs e)
        {
            button_flag = !button_flag;
            button_set(sentry1_connect);
            button_set(sentry2_connect);
            button_set(infantry1_connect);
            button_set(infantry2_connect);
            button_set(hero1_connect);
            button_set(hero2_connect);
            button_set(engineer1_connect);
            button_set(engineer2_connect);
        }

        private void button_set(System.Windows.Forms.Button button)
        {
            //button.Name.Length//可以通过长度、字符串识别来识别传入的按钮属于什么684835
            if(button_flag)
            {

                button.BackColor = Color.FromArgb(2, 131, 201);
                button.ForeColor = Color.FromArgb(243, 249, 252);//46 58 132
                //button.Enabled = true;
                SetControlEnabled(button, true);
                button.Text=button.Text.Replace("off-line", "click_connect");//= " 哨兵1   connect ";
                Connection_rate.Value = 50;
            }
            else
            {
                button.BackColor = Color.FromArgb(65, 65, 65); //灰色 disconnect
                button.ForeColor = Color.FromArgb(140, 140, 140); //160 160 160
               // button.Enabled = false;
                SetControlEnabled(button,false);
                //button.Text = "哨兵1\r\noff-line ";
                button.Text = button.Text.Replace("click_connect", "off-line");
                Connection_rate.Value = 10;
            }
        }

        private void channelListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //创建UdpClient对象
        static UdpClient UdpBroadcastSend = new UdpClient(1812);//设置本地端口1812发广播
        static UdpClient UdpBroadcastRev = new UdpClient(1813);//设置本地端口1813收广播
       
        static UdpClient UdpDedicatedSend = new UdpClient(1814);//设置本地端口1812发专线
        static UdpClient UdpDedicatedRev = new UdpClient(1815);//设置本地端口1813收专线
         
        //static IPEndPoint UDP_So = new IPEndPoint(IPAddress.Parse("192.168.1.25"),9999);  //监听端口9999 IPAddress.Any
        private void test_connect_Click(object sender, EventArgs e)
        {
            Thread thrSend = new Thread(SendMessage_DiscoveryRobot);
            thrSend.Start();
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        ///<param name="obj">
        private static void SendMessage_DiscoveryRobot()
        {
            try
            {
                string message = "#RM-DT=DCY_ROBOT#END";
                byte[] sendbytes = Encoding.Default.GetBytes(message);
                IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse("192.168.1.255"), 1813); // 发送到的IP地址和端口号
                UdpBroadcastSend.Send(sendbytes, sendbytes.Length, remoteIpep);
                UdpBroadcastSend.Close();
            }
            catch
            {
                //提示：直接在按钮上提示或者新建label
            }

        }

        /// <summary>
        /// 接收数据
        /// </summary>
        ///<param name="obj">
        private static void ReceiveMessage_DiscoveryRobot(object obj)
        {
            IPEndPoint revIpep = new IPEndPoint(IPAddress.Any, 0);    //这个没看出来干什么用 直接填0吧
            while (true)
            {
                try
                {
                    byte[] bytRecv = UdpBroadcastRev.Receive(ref revIpep);
                    string message = Encoding.Default.GetString(bytRecv, 0, bytRecv.Length);
                    //rev_msg = rev_msg.Insert(-1, message);//(int)rev_msg.LongCount()
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
        static string rev_msg=".init";

        private void test_rev_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// 线程：不断监听UDP报文
            /// </summary>
            Thread thrRecv;
            thrRecv = new Thread(ReceiveMessage_DiscoveryRobot);
            thrRecv.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rev_text.Text = rev_msg;
            network_communication_labal.Text = rev_msg;
        }


        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tc = sender as TabControl;
            Font font = new Font("微软雅黑", 10F);
            SolidBrush bruFont = new SolidBrush(Color.White);//字体颜色
            SolidBrush tabPageBlack = new SolidBrush(Color.FromArgb(1, 1, 1));//Tab选项卡背景颜色150
            SolidBrush backgroundBlack = new SolidBrush(Color.FromArgb(1, 1, 1));//Tab整体背景颜色77
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;//选项卡文字位置

            //填充选项卡颜色
            Rectangle rec1 = tc.GetTabRect(0);
            e.Graphics.FillRectangle(tabPageBlack, rec1);
            Rectangle rec2 = tc.GetTabRect(1);
            e.Graphics.FillRectangle(tabPageBlack, rec2);

            //填充背景色
            Rectangle rec3 = tc.ClientRectangle;
            e.Graphics.FillRectangle(backgroundBlack, rec3);

            //填写选项卡名称
            e.Graphics.DrawString("chart", font, bruFont, rec1, stringFormat);
            e.Graphics.DrawString("debug", font, bruFont, rec2, stringFormat);

            tabPage_debug.BackColor = System.Drawing.Color.DimGray;
            tabPage_chart.BackColor = System.Drawing.Color.DimGray;

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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tabControl1.TabPages[tabControl1.SelectedIndex].Focus();
            //this.tabControl1.SelectedTab.BackColor = System.Drawing.Color.Transparent;
            //tabPage_chart
        }


        /// <summary>
        /// 关闭程序，强制退出
        /// </summary>
        ///<param name="sender">
        ///<param name="e">
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

        public static void SetControlEnabled(Control c, bool enabled)
        {
            if (enabled)
            { SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(c.Handle, GWL_STYLE)); }
            else
            { SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE)); }
        }//

        //////////////////////////////////////////////////////////////////////////////////


    }
}
