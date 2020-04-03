using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace GreatClockTool
{
    public partial class Update : UserControl
    {
        public Update()
        {
            InitializeComponent();
        }

        public SerialPort Clock_Serial;
        FileStream firmware;
        public bool connection_ok = true;

        /// <summary>
        /// 展示二进制代码
        /// </summary>
        /// <param name="bytes">二进制代码</param>
        void display_bytes(Byte[] bytes)
        {
            string b = "";
            foreach (Byte i in bytes)
            {
                b += (i.ToString("X2") + " ");
            }
            if (textBox_file.InvokeRequired)
            {
                textBox_file.Invoke(new Action(() => textBox_file.Text = b));
            }
            else
            {
                this.textBox_file.Text = b;
            }
        }

        /// <summary>
        /// 更新固件
        /// </summary>
        void Download_Firmware()
        {
            const int base_address = 0x08005000;
            string s = "";
            Byte[] buf = new Byte[firmware.Length];
            Byte[] data_frame = new byte[13];
            firmware.Read(buf, 0, (int)firmware.Length);
            display_bytes(buf);
            Clock_Serial.ReadTimeout = 5000;
            Clock_Serial.WriteTimeout = 5000;
            for (int i = 0; i < firmware.Length / 8 + 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    data_frame[j] = (Byte)((base_address + i * 8) >> j * 8);
                }
                if (firmware.Length - i * 8 < 8)
                {
                    data_frame[4] = (Byte)(firmware.Length - i * 8);
                }
                else
                {
                    data_frame[4] = 8;
                }
                for (int j = 0; j < data_frame[4]; j++)
                {
                    data_frame[5 + j] = buf[i * 8 + j];
                }
                try
                {
                    Clock_Serial.Write(data_frame, 0, 13);
                    s = Clock_Serial.ReadLine();
                }
                catch (Exception)
                {
                    connection_ok = false;
                }
                if (s == "Success")
                {
                    if (progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke(new Action(() => progressBar1.Value = i * 8 * 100 / (int)firmware.Length));
                    }
                    else
                    {
                        progressBar1.Value = i * 8 * 100 / (int)firmware.Length;
                    }
                    if (label_percentage.InvokeRequired)
                    {
                        label_percentage.Invoke(new Action(() => label_percentage.Text = i * 8 * 100 / (int)firmware.Length + "%"));
                    }
                    else
                    {
                        label_percentage.Text = i * 8 * 100 / (int)firmware.Length + "%";
                    }
                }
                s = "";
            }
            if (label_finish.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() =>
                {
                    label_finish.Visible = true;
                    label_finish.BringToFront();
                }));
            }
            else
            {
                label_finish.Visible = true;
                label_finish.BringToFront();
            }
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

        Thread download;

        private void button_load_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox_path.Text = openFileDialog1.FileName;
            firmware = new FileStream(openFileDialog1.FileName, FileMode.Open);
            button_update.Enabled = true;
            button_update.Cursor = Cursors.Default;
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            download = new Thread(Download_Firmware);
            timer1.Stop();
            download.Start();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Connection_Holding())
            {
                connection_ok = false;

            }
        }
    }
}
