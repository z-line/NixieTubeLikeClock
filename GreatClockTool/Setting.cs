using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;

namespace GreatClockTool
{
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();
        }
        public SerialPort Clock_Serial;
        DateTime_TypeDef Clock_DateTime;
        static string string_buf;
        bool got_color_mode = false;

        struct DateTime_TypeDef
        {
            public int year;
            public int month;
            public int date;
            public int weekday;
            public int days;
            public int hour;
            public int minute;
            public int second;

            public DateTime_TypeDef(int year, int month, int date, int weekday, int days, int hour, int minute, int second) : this()
            {
                this.year = year;
                this.month = month;
                this.date = date;
                this.weekday = weekday;
                this.days = days;
                this.hour = hour;
                this.minute = minute;
                this.second = second;
            }
        }

        List<RGB_Dialog.Inflection_Point>[] points_groups = new List<RGB_Dialog.Inflection_Point>[12];

        DateTime_TypeDef String2Time(string input)
        {
            string[] buf = input.Split(' ');
            return new DateTime_TypeDef(int.Parse(buf[0]), int.Parse(buf[1]), int.Parse(buf[2]), int.Parse(buf[3]), 0, int.Parse(buf[4]), int.Parse(buf[5]), int.Parse(buf[6]));
        }

        string Time2String(DateTime_TypeDef datetime)
        {
            string buf = datetime.year.ToString() + " " +
                datetime.month.ToString() + " " +
                datetime.date.ToString() + " " +
                datetime.weekday.ToString() + " " +
                datetime.hour.ToString() + " " +
                datetime.minute.ToString() + " " +
                (datetime.second + 1).ToString();
            return buf;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime_TypeDef datetime = new DateTime_TypeDef();
            datetime.year = DateTime.Now.Year;
            datetime.month = DateTime.Now.Month;
            datetime.date = DateTime.Now.Day;
            datetime.hour = DateTime.Now.Hour;
            datetime.minute = DateTime.Now.Minute;
            datetime.second = DateTime.Now.Second;
            timer1.Stop();
            Set_Clock_Time(datetime);
            timer1.Start();
        }

        bool Equal(RGB_Dialog.Inflection_Point a, RGB_Dialog.Inflection_Point b)
        {
            if (a.Fun == b.Fun && a.R == b.R && a.G == b.G && a.B == b.B && a.X == b.X)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label_num_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            RGB_Dialog rgb_dialog = new RGB_Dialog();
            int target_index = int.Parse(((Label)sender).Name[9].ToString()) - 1;
            points_groups[target_index].ForEach(i => rgb_dialog.points.Add(i));     //将时钟RGB变化函数载入编辑器
            rgb_dialog.ShowDialog();                                                //对话框形式显示编辑器
            if (rgb_dialog.points.Count != points_groups[target_index].Count)
            {
                Set_Color_Mode(target_index, rgb_dialog.points.Count, true);               //向时钟发送设置RGB变换函数拐点数量命令
            }
            if (points_groups[target_index].Count > rgb_dialog.points.Count)
            {
                points_groups[target_index].RemoveRange(rgb_dialog.points.Count, points_groups[target_index].Count - rgb_dialog.points.Count);
            }
            else
            {
                for (int i = points_groups[target_index].Count; i < rgb_dialog.points.Count; i++)
                {
                    points_groups[target_index].Add(rgb_dialog.points[i]);
                    Set_Color_Mode(target_index, i, false);
                }
            }
            for (int i = 0; i < rgb_dialog.points.Count; i++)
            {
                if (!Equal(rgb_dialog.points[i], points_groups[target_index][i]))
                {
                    points_groups[target_index][i] = rgb_dialog.points[i];
                    Set_Color_Mode(target_index, i, false);
                }
            }
            timer1.Start();
        }

        /// <summary>
        /// UI重置
        /// </summary>
        void Reset()
        {
            count = 0;
            label_num1.ForeColor = Color.White;
            label_num2.ForeColor = Color.White;
            label_num3.ForeColor = Color.White;
            label_num4.ForeColor = Color.White;
            label_num5.ForeColor = Color.White;
            label_num6.ForeColor = Color.White;
            label_num1.Text = "0";
            label_num2.Text = "0";
            label_num3.Text = "0";
            label_num4.Text = "0";
            label_num5.Text = "0";
            label_num6.Text = "0";
        }

        //
        void Save_Settings()
        {
            Clock_Serial.ReadTimeout = 5000;
            Clock_Serial.Write("Save");
            try
            {
                if (Clock_Serial.ReadLine() == "Successed")
                {
                    MessageBox.Show("Successed");
                }
                else
                {
                    MessageBox.Show("Failed");
                }
            }
            catch (Exception)
            {

            }
            Clock_Serial.ReadTimeout = 500;
        }

        /// <summary>
        /// 获取时钟目前时间
        /// </summary>
        /// <returns>时钟目前时间</returns>
        string Get_Clock_Time()
        {
            string s = null;
            try
            {
                Clock_Serial.Write("GTI");
                s = Clock_Serial.ReadLine();
            }
            catch (Exception)
            {

            }
            return s;
        }

        /// <summary>
        /// 设置时钟时间
        /// </summary>
        /// <param name="datetime">时间</param>
        void Set_Clock_Time(DateTime_TypeDef datetime)
        {
            Clock_Serial.Write("STI" + Time2String(datetime));
        }

        /// <summary>
        /// 设置时钟颜色配置
        /// </summary>
        void Set_Color_Mode(int group_index, int point_index, bool set_count)
        {
            //"SCO group_index point_index Fun-R-G-B-X"
            //"SCO group_index point_count"
            Byte[] buf = new Byte[18];
            buf[0] = (Byte)'S'; buf[1] = (Byte)'C'; buf[2] = (Byte)'O';
            buf[3] = (Byte)' ';
            buf[4] = (Byte)group_index;
            buf[5] = (Byte)' ';
            buf[6] = (Byte)point_index;
            if (set_count)
            {
                Clock_Serial.Write(buf, 0, 7);
            }
            else
            {
                buf[7] = (Byte)' ';
                buf[8] = points_groups[group_index][point_index].Fun;
                buf[9] = (Byte)'-';
                buf[10] = points_groups[group_index][point_index].R;
                buf[11] = (Byte)'-';
                buf[12] = points_groups[group_index][point_index].G;
                buf[13] = (Byte)'-';
                buf[14] = points_groups[group_index][point_index].B;
                buf[15] = (Byte)'-';
                buf[16] = (Byte)(points_groups[group_index][point_index].X & 0xff);
                buf[17] = (Byte)(points_groups[group_index][point_index].X >> 8);
                Clock_Serial.Write(buf, 0, 18);
            }
        }

        /// <summary>
        /// 获取时钟颜色配置
        /// </summary>
        void Get_Color_Mode(int group_index)
        {
            int count;
            Byte[] buf = new Byte[10];
            Clock_Serial.Write("GCO " + (char)group_index + ' ' + (char)0xff);
            count = Clock_Serial.ReadByte();
            //transmit:"GCO group_index point_index"    如果point_index大于最大拐点数，则返回该组的拐点数目，否则返回指定数据
            //receive:"Fun-R-G-B-X"
            try
            {
                points_groups[group_index] = new List<RGB_Dialog.Inflection_Point>();
                for (int i = 0; i < count; i++)
                {
                    Clock_Serial.Write("GCO " + (char)group_index + ' ' + (char)i);
                    //Thread.Sleep(100);
                    //Clock_Serial.Read(buf, 0, 10);
                    for (int j = 0; j < 10; j++)
                    {
                        buf[j] = (Byte)Clock_Serial.ReadByte();
                    }
                    if (buf[1] == '-' && buf[3] == '-' && buf[5] == '-' && buf[7] == '-')
                    {
                        RGB_Dialog.Inflection_Point point = new RGB_Dialog.Inflection_Point();
                        point.Fun = buf[0];
                        point.R = buf[2];
                        point.G = buf[4];
                        point.B = buf[6];
                        point.X = buf[8] + (buf[9] << 8);
                        points_groups[group_index].Add(point);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 获得时钟点灯时间
        /// </summary>
        void Get_Show_Time()
        {
            Clock_Serial.Write("GST");
            string[] buf;
            try
            {
                buf = Clock_Serial.ReadLine().Split('-');
                textBox_s_hour.Text = buf[0].Split(':')[0];
                textBox_s_minute.Text = buf[0].Split(':')[1];
                textBox_e_hour.Text = buf[1].Split(':')[0];
                textBox_e_minute.Text = buf[1].Split(':')[1];
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 设置时钟点灯时间
        /// </summary>
        void Set_Show_Time()
        {
            Clock_Serial.Write("SST" + textBox_s_hour.Text + ':' + textBox_s_minute.Text + '-' + textBox_e_hour.Text + ':' + textBox_e_minute.Text);
        }

        /// <summary>
        /// 显示日期
        /// </summary>
        void Show_Date()
        {
            label_num1.Text = (Clock_DateTime.year % 100 / 10).ToString();
            label_num2.Text = (Clock_DateTime.year % 10).ToString();
            label_num3.Text = (Clock_DateTime.month / 10).ToString();
            label_num4.Text = (Clock_DateTime.month % 10).ToString();
            label_num5.Text = (Clock_DateTime.date / 10).ToString();
            label_num6.Text = (Clock_DateTime.date % 10).ToString();
        }

        /// <summary>
        /// 显示时间
        /// </summary>
        void Show_ClockTime()
        {
            label_num1.Text = (Clock_DateTime.hour / 10).ToString();
            label_num2.Text = (Clock_DateTime.hour % 10).ToString();
            label_num3.Text = (Clock_DateTime.minute / 10).ToString();
            label_num4.Text = (Clock_DateTime.minute % 10).ToString();
            label_num5.Text = (Clock_DateTime.second / 10).ToString();
            label_num6.Text = (Clock_DateTime.second % 10).ToString();
            label_clocktime1.Text = Clock_DateTime.year.ToString() + '/' + Clock_DateTime.month.ToString() + '/' + Clock_DateTime.date.ToString()
                + ' ' + Clock_DateTime.hour.ToString("D2") + ':' + Clock_DateTime.minute.ToString("D2") + ':' + Clock_DateTime.second.ToString("D2");
        }

        void Show_Time()
        {
            label_localtime1.Text = DateTime.Now.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Show_Time();
            if (Clock_Serial != null)
            {
                if (Clock_Serial.IsOpen)
                {
                    if (!got_color_mode)
                    {
                        Get_Show_Time();
                        Get_Color_Mode(0);
                        Get_Color_Mode(1);
                        Get_Color_Mode(2);
                        Get_Color_Mode(3);
                        Get_Color_Mode(4);
                        Get_Color_Mode(5);
                        Get_Color_Mode(6);
                        Get_Color_Mode(7);
                        Get_Color_Mode(8);
                        Get_Color_Mode(9);
                        Get_Color_Mode(10);
                        Get_Color_Mode(11);
                        got_color_mode = true;
                    }
                    string_buf = Get_Clock_Time();
                    if (string_buf != null)
                    {
                        Clock_DateTime = String2Time(string_buf);
                        Show_ClockTime();
                    }
                }
                else
                {
                    try
                    {
                        Clock_Serial.Open();
                        Get_Show_Time();
                        Get_Color_Mode(0);
                    }
                    catch (Exception E)
                    {
                        if (E.Message.Contains("不存在"))
                        {
                        }
                    }
                }

            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
        }

        int count = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (got_color_mode)
            {
                label_num1.ForeColor = Get_Value(points_groups[0], count);
                label_num2.ForeColor = Get_Value(points_groups[1], count);
                label_num3.ForeColor = Get_Value(points_groups[2], count);
                label_num4.ForeColor = Get_Value(points_groups[3], count);
                label_num5.ForeColor = Get_Value(points_groups[4], count);
                label_num6.ForeColor = Get_Value(points_groups[5], count);
                count++;
            }
        }

        Color Get_Value(List<RGB_Dialog.Inflection_Point> group, int t)
        {
            int R;
            int G;
            int B;
            int buf_t = t % group[group.Count - 1].X;
            for (int i = 0; i < group.Count - 1; i++)
            {
                if (buf_t <= group[i + 1].X)
                {
                    R = (group[i].R - group[i + 1].R) * (buf_t - group[i].X) / (group[i].X - group[i + 1].X) + group[i].R;
                    G = (group[i].G - group[i + 1].G) * (buf_t - group[i].X) / (group[i].X - group[i + 1].X) + group[i].G;
                    B = (group[i].B - group[i + 1].B) * (buf_t - group[i].X) / (group[i].X - group[i + 1].X) + group[i].B;
                    return Color.FromArgb(R, G, B);
                }
            }
            return Color.White;
        }

        private void textBox_s_hour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (("1234567890\x8").IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }
        }

        private void textBox_s_hour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Show_Time();
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            Save_Settings();
        }
    }
}
