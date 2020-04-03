using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace GreatClockTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Point window_last_pos;
        Boolean if_move_mode = false;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if_move_mode = true;
            window_last_pos = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (if_move_mode)
            {
                this.Left = this.Left + e.X - window_last_pos.X;
                this.Top = this.Top + e.Y - window_last_pos.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if_move_mode = false;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            if_move_mode = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            find_clock = new Thread(new ThreadStart(Refresh_Clock_Serial_Port));
            timer1.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        enum ClockMode
        {
            NONE,
            SETTING,
            UPDATE
        }

        ClockMode global_mode = ClockMode.NONE;
        bool is_FDM = false;
        SerialPort Clock_Serial;
        string string_buf = "";
        Thread find_clock;

        /// <summary>
        /// 识别时钟
        /// </summary>
        /// <returns>时钟所在串口参数</returns>
        SerialPort Identify_Clock_Serial_Port()
        {
            SerialPort serial;
            string[] serial_ports = SerialPort.GetPortNames();
            foreach (string i in serial_ports)
            {
                serial = new SerialPort();
                serial.PortName = i;
                serial.BaudRate = 115200;
                serial.Parity = Parity.None;
                serial.StopBits = StopBits.One;
                serial.ReadTimeout = 500;
                serial.WriteTimeout = 500;
                //设置模式判断
                try
                {
                    serial.Open();
                    serial.Write("ID");
                    string_buf = serial.ReadLine();
                }
                catch (Exception)
                {
                }
                if (string_buf == "Clock")
                {
                    string_buf = "";
                    serial.Close();
                    return serial;
                }
                //更新模式判断
                try
                {
                    if (!serial.IsOpen)
                    {
                        serial.Open();
                    }
                    serial.Write("FDM");
                    string_buf = serial.ReadLine();
                }
                catch (Exception)
                {
                }
                if (string_buf == "Ready")
                {
                    string_buf = "";
                    serial.Close();
                    is_FDM = true;
                    return serial;
                }
            }
            return null;
        }

        void Refresh_Clock_Serial_Port()
        {
            Clock_Serial = Identify_Clock_Serial_Port();
        }

        /// <summary>
        /// 复位
        /// </summary>
        void Reset()
        {
            Clock_Serial = null;
            label_clock_time.Text = "";
            label_clock_time.BackColor = Color.Black;
        }

        Setting setting;
        Update update;
        void Create_Setting_Control()
        {
            setting = new Setting();
            setting.Parent = control_container;
            setting.Left = 0;
            setting.Top = 0;
            setting.Clock_Serial = Clock_Serial;
        }

        void Create_Update_Control()
        {
            update = new Update();
            update.Parent = control_container;
            update.Left = 0;
            update.Top = 0;
            update.Clock_Serial = Clock_Serial;
        }

        /// <summary>
        /// 连接成功操作
        /// </summary>
        void Connected()
        {
            info_label.Visible = false;
            label_connect_status.BackColor = Color.ForestGreen;
            label_connect_status.Text = "已连接";
            if (is_FDM)
            {
                global_mode = ClockMode.UPDATE;
            }
            else
            {
                global_mode = ClockMode.SETTING;
            }
            switch (global_mode)
            {
                case ClockMode.SETTING:
                    label_clock_time.Text = "设置时间";
                    Create_Setting_Control();
                    break;
                case ClockMode.UPDATE:
                    label_clock_time.Text = "更新固件";
                    Create_Update_Control();
                    break;
            }
        }

        /// <summary>
        /// 连接丢失指示
        /// </summary>
        void Lost_Connect()
        {
            if (setting != null)
            {
                if (setting.Created)
                {
                    setting.Dispose();
                }
            }
            if (update != null)
            {
                if (update.Created)
                {
                    update.Dispose();
                }
            }
            info_label.Visible = true;
            info_label.Text = "连接丢失";
            label_connect_status.BackColor = Color.Red;
            label_connect_status.Text = "丢失连接";
            global_mode = ClockMode.NONE;
            is_FDM = false;
            Reset();
        }

        /// <summary>
        /// 判断物理连接是否保持
        /// </summary>
        /// <returns>布尔类型</returns>
        bool Connection_Holding()
        {
            if (Clock_Serial.IsOpen)
            {
                return true;
            }
            else
            {
                try
                {
                    Clock_Serial.Open();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Clock_Serial == null)
            {
                if (find_clock.ThreadState == ThreadState.Unstarted || find_clock.ThreadState == ThreadState.Stopped)
                {
                    find_clock = new Thread(new ThreadStart(Refresh_Clock_Serial_Port));
                    find_clock.Start();
                }
                if (info_label.Text.Contains("......"))
                {
                    info_label.Text = "寻找时钟端口中";
                }
                else
                {
                    info_label.Text += '.';
                }
            }
            else
            {
                switch (global_mode)
                {
                    case ClockMode.SETTING:
                        if (!Connection_Holding())
                        {
                            Lost_Connect();
                        }
                        break;
                    case ClockMode.UPDATE:
                        if (!update.connection_ok)
                        {
                            Lost_Connect();
                        }
                        break;
                    case ClockMode.NONE:
                        if (Connection_Holding())
                        {
                            Connected();
                        }
                        break;
                }
            }
        }

    }
}
