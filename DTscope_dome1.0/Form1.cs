using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                button.Enabled = true;
                button.Text=button.Text.Replace("off-line", "click_connect");//= " 哨兵1   connect ";
                Connection_rate.Value = 50;
            }
            else
            {


                button.BackColor = Color.FromArgb(65, 65, 65); //灰色 disconnect
                button.ForeColor = Color.FromArgb(1, 1, 1);
                button.Enabled = false;
                //button.Text = "哨兵1\r\noff-line ";
                button.Text = button.Text.Replace("click_connect", "off-line");
                Connection_rate.Value = 10;
            }
        }

        private void channelListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
