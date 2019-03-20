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

        }

        void f()
        {
            sentry1_connect.Enabled = false;
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
        static UdpClient udp = new UdpClient(1813);//设置本地端口1813
        static IPEndPoint UDP_So = new IPEndPoint(IPAddress.Any,9999);
        private void test_connect_Click(object sender, EventArgs e)
        {
            try
            {
                udp.Connect("192.168.1.255", 9999);//192.168.1.255用来发到其他的主机上
            }
            catch
            {
                rev_text.Text = "连接失败或已存在";
            }

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        //定义一个字节数组，用来存放发送到远程主机的信息
                        Byte[] sendBytes = Encoding.Default.GetBytes("(" + DateTime.Now.ToLongTimeString() + ")此消息来自上位机1");
                        // Console.WriteLine("(" + DateTime.Now.ToLongTimeString() + ")节目预报：八点有大型晚会，请收听");
                        //调用UdpClient对象的Send方法将UDP数据报发送到远程主机
                        udp.Send(sendBytes, sendBytes.Length);
                        Thread.Sleep(2000);//线程休眠2秒
                                           // Thread.Suspend();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
            thread.Start();//启动线程

        }
        Byte[] revBytes = Encoding.Default.GetBytes("预装信息");
        private void test_rev_Click(object sender, EventArgs e)
        {
            Thread thread_rev = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        //定义一个字节数组，用来存放发送到远程主机的信息
                        //Byte[] revBytes = Encoding.Default.GetBytes("预装信息");

                        //调用UdpClient对象的Send方法将UDP数据报发送到远程主机
                        //udp.Send(revBytes, revBytes.Length);
                        revBytes=udp.Receive(ref UDP_So);
                        Thread.Sleep(1000);//线程休眠2秒
                                           // Thread.Suspend();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
            thread_rev.Start();//启动线程
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //rev_text.Text = BitConverter.ToString(revBytes);
            //revBytes
            rev_text.Text = rev_text.Text.PadRight((char)revBytes[0]);
            //progressBar1
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
            { SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(c.Handle, GWL_STYLE)); }
        }

        //////////////////////////////////////////////////////////////////////////////////
    }
}
