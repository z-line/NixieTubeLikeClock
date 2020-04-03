using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreatClockTool
{
    public partial class RGB_Dialog : Form
    {
        public RGB_Dialog()
        {
            InitializeComponent();
        }

        Graphics g;
        BufferedGraphicsContext current = BufferedGraphicsManager.Current; //(1) 
        BufferedGraphics gb;

        Point window_last_pos;
        Boolean if_move_mode = false;

        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            if_move_mode = true;
            window_last_pos = new Point(e.X, e.Y);
        }

        private void label_title_MouseLeave(object sender, EventArgs e)
        {
            if_move_mode = false;
        }

        private void label_title_MouseMove(object sender, MouseEventArgs e)
        {
            if (if_move_mode)
            {
                this.Left = this.Left + e.X - window_last_pos.X;
                this.Top = this.Top + e.Y - window_last_pos.Y;
            }
        }

        private void label_title_MouseUp(object sender, MouseEventArgs e)
        {
            if_move_mode = false;
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        enum Function
        {
            Linear,
            sin,
            cos
        }

        public struct Inflection_Point
        {
            public Byte Fun;
            public int X;
            public Byte R;
            public Byte G;
            public Byte B;
        }

        public enum Group_Index
        {
            Hour_High,          //时高位，十位
            Hour_Low,           //时低位，个位
            Minute_High,        //分高位，十位
            Minute_Low,         //分低位，个位
            Second_High,        //秒高位，十位
            Second_Low,         //秒低位，个位
            Year_High,          //年高位，十位
            Year_Low,           //年低位，个位
            Month_High,         //月高位，十位
            Month_Low,          //月低位，个位
            Date_High,          //日高位，十位
            Date_Low            //日低位，个位
        }

        const int points_count_limit = 13;

        public List<Inflection_Point> points = new List<Inflection_Point>();

        private void RGB_Dialog_Shown(object sender, EventArgs e)
        {
            timer1.Start();
            gb = (new BufferedGraphicsContext()).Allocate(panel_view.CreateGraphics(), panel_view.DisplayRectangle);
            g = gb.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            panel_view.MouseWheel += new MouseEventHandler(panel_view_MouseWheel);
            Refresh_Viewer();
        }

        void Freeze_Edit_Control()
        {
            lock_select = false;
            textBox_R.Enabled = false;
            textBox_G.Enabled = false;
            textBox_B.Enabled = false;
            textBox_Time.Enabled = false;
            label_color_preview.Enabled = false;
        }

        void Unfreeze_Edit_Control()
        {
            lock_select = true;
            textBox_R.Enabled = true;
            textBox_G.Enabled = true;
            textBox_B.Enabled = true;
            textBox_Time.Enabled = true;
            label_color_preview.Enabled = true;
        }
        //***************************************绘图函数***************************************
        const int points_radius = 3;    //强调点半径
        int axis_x_max = 0xff;          //X轴最大值
        const int axis_x_offset = 25;    //X轴坐标偏移
        const int axis_y_offset = 40;    //Y轴坐标偏移
        const int axis_y_max = 255;     //Y轴最大值
        const int axis_font_size = 10;  //坐标轴标识字体大小
        const int line_width = 2;       //颜色曲线线粗
        Color view_background = Color.FromArgb(0, 0, 0);

        float Get_Axis_Y(float value)
        {
            float y;
            y = axis_y_offset + (panel_view.Height - 80) * (1 - value / axis_y_max);
            return y;
        }

        int Get_Axis_X(int value)
        {
            int x;
            x = axis_x_offset + (panel_view.Width - 60) * value / (axis_x_max);
            return x;
        }

        void Refresh_Viewer()
        {
            g.Clear(view_background);
            Draw_Axises();
            Draw_RGB_Points_Line();
            if (!lock_select)
            {
                if (select_point < points_count_limit)
                {
                    Select_Point(select_point);
                }
            }
            gb.Render();
        }

        void Draw_Color_Time()
        {
            for (int i = 0; i < (panel_view.Width - axis_x_offset); i++)
            {
                g.DrawLine(new Pen(Get_Value(points, i)), Get_Axis_X(i), panel_view.Height - 20, Get_Axis_X(i), panel_view.Height - 5);
            }
        }

        void Draw_Axises()
        {
            //绘制坐标轴
            g.DrawLine(new Pen(Color.LightGray), Get_Axis_X(0), Get_Axis_Y(0), Get_Axis_X(axis_x_max), Get_Axis_Y(0));  //x轴
            g.DrawLine(new Pen(Color.LightGray), Get_Axis_X(0), Get_Axis_Y(0), Get_Axis_X(0), Get_Axis_Y(axis_y_max) - 20);  //y轴
            //绘制箭头
            g.FillPolygon(new SolidBrush(Color.LightGray), new PointF[] { new PointF(Get_Axis_X(axis_x_max) + 10, Get_Axis_Y(0)), new PointF(Get_Axis_X(axis_x_max), Get_Axis_Y(0) - 3), new PointF(Get_Axis_X(axis_x_max), Get_Axis_Y(0) + 3) });
            g.FillPolygon(new SolidBrush(Color.LightGray), new PointF[] { new PointF(Get_Axis_X(0), Get_Axis_Y(axis_y_max) - 30), new PointF(Get_Axis_X(0) - 3, Get_Axis_Y(axis_y_max) - 20), new PointF(Get_Axis_X(0) + 3, Get_Axis_Y(axis_y_max) - 20) });
            //绘制坐标轴标识
            g.DrawString("0", new Font(new FontFamily("宋体"), axis_font_size), new SolidBrush(Color.LightGray), new PointF(Get_Axis_X(0) - axis_font_size, Get_Axis_Y(0)));
            g.DrawString(axis_y_max.ToString(), new Font(new FontFamily("宋体"), axis_font_size), new SolidBrush(Color.LightGray), new PointF(Get_Axis_X(0) - axis_font_size * 2.5f, Get_Axis_Y(axis_y_max)));
            g.DrawString(axis_x_max.ToString(), new Font(new FontFamily("宋体"), axis_font_size), new SolidBrush(Color.LightGray), new PointF(Get_Axis_X(axis_x_max) - axis_font_size * 2, Get_Axis_Y(0) + axis_font_size * 0.5f));
        }

        void Draw_RGB_Points_Line()
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (i >= 1)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.Red), line_width), Get_Axis_X(points[i - 1].X), Get_Axis_Y(points[i - 1].R), Get_Axis_X(points[i].X), Get_Axis_Y(points[i].R));
                }
                g.FillEllipse(new SolidBrush(Color.Red), Get_Axis_X(points[i].X) - points_radius, Get_Axis_Y(points[i].R) - points_radius, points_radius * 2, points_radius * 2);
                if (i >= 1)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.Green), line_width), Get_Axis_X(points[i - 1].X), Get_Axis_Y(points[i - 1].G), Get_Axis_X(points[i].X), Get_Axis_Y(points[i].G));
                }
                g.FillEllipse(new SolidBrush(Color.Green), Get_Axis_X(points[i].X) - points_radius, Get_Axis_Y(points[i].G) - points_radius, points_radius * 2, points_radius * 2);
                if (i >= 1)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.Blue), line_width), Get_Axis_X(points[i - 1].X), Get_Axis_Y(points[i - 1].B), Get_Axis_X(points[i].X), Get_Axis_Y(points[i].B));
                }
                g.FillEllipse(new SolidBrush(Color.Blue), Get_Axis_X(points[i].X) - points_radius, Get_Axis_Y(points[i].B) - points_radius, points_radius * 2, points_radius * 2);
            }
            Draw_Color_Time();
        }

        int select_point = 0xff;
        Boolean lock_select = false;
        void Select_Point(int point_index)
        {
            g.DrawEllipse(new Pen(Color.White, 2), Get_Axis_X(points[point_index].X) - points_radius * 1.5f, Get_Axis_Y(points[point_index].R) - points_radius * 1.5f, points_radius * 3, points_radius * 3);
            g.DrawEllipse(new Pen(Color.White, 2), Get_Axis_X(points[point_index].X) - points_radius * 1.5f, Get_Axis_Y(points[point_index].G) - points_radius * 1.5f, points_radius * 3, points_radius * 3);
            g.DrawEllipse(new Pen(Color.White, 2), Get_Axis_X(points[point_index].X) - points_radius * 1.5f, Get_Axis_Y(points[point_index].B) - points_radius * 1.5f, points_radius * 3, points_radius * 3);
            textBox_R.Text = points[point_index].R.ToString();
            textBox_G.Text = points[point_index].G.ToString();
            textBox_B.Text = points[point_index].B.ToString();
            textBox_Time.Text = points[point_index].X.ToString();
        }

        //***************************************交互事件***************************************
        //曲线图鼠标交互事件
        private void panel_view_MouseWheel(object sender, MouseEventArgs e)
        {
            if (axis_x_max - e.Delta > 0)
            {
                axis_x_max -= e.Delta;
            }
            Refresh_Viewer();
        }

        private void panel_view_Click(object sender, EventArgs e)
        {

        }

        private void panel_view_MouseDown(object sender, MouseEventArgs e)
        {
            if (!lock_select)
            {
                for (int i = 1; i < points.Count; i++)
                {
                    if (e.X < Get_Axis_X(points[i].X))
                    {
                        if (Math.Abs(Get_Axis_X(points[i - 1].X) - e.X) > Math.Abs(Get_Axis_X(points[i].X) - e.X))
                        {
                            select_point = i;
                            Refresh_Viewer();
                        }
                        else
                        {
                            select_point = i - 1;
                            Refresh_Viewer();
                        }
                        break;
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(panel_view, e.Location);
                }
            }
        }

        private void panel_view_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel_view_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void panel_view_MouseUp(object sender, MouseEventArgs e)
        {

        }

        //右键菜单交互事件
        private void AddPoint_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_point = 0xff;
            Refresh_Viewer();
            Unfreeze_Edit_Control();
        }

        private void DeletePoint_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            points.RemoveAt(select_point);
            Refresh_Viewer();
        }

        private void EditPoint_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unfreeze_Edit_Control();
        }

        private void textBox_RGB_TextChanged(object sender, EventArgs e)
        {
            int buf;
            if (int.TryParse(((TextBox)sender).Text, out buf))
            {
                if (buf > 255)
                {
                    ((TextBox)sender).Text = "255";
                }
                label_color_preview.BackColor = Color.FromArgb(int.Parse(textBox_R.Text), int.Parse(textBox_G.Text), int.Parse(textBox_B.Text));
            }
            else
            {
                ((TextBox)sender).Text = "255";
            }
        }



        private void label_color_preview_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            if (colorDialog1.Color != Color.Black)
            {
                label_color_preview.BackColor = colorDialog1.Color;
                textBox_R.Text = colorDialog1.Color.R.ToString();
                textBox_G.Text = colorDialog1.Color.G.ToString();
                textBox_B.Text = colorDialog1.Color.B.ToString();
            }
        }

        private void Default_RGB()
        {
            points.Clear();
            Inflection_Point buf = new Inflection_Point();
            buf.X = 0;
            buf.R = 255;
            buf.G = 0;
            buf.B = 0;
            points.Add(buf);
            buf.X = 50;
            buf.R = 255;
            buf.G = 165;
            buf.B = 0;
            points.Add(buf);
            buf.X = 100;
            buf.R = 255;
            buf.G = 255;
            buf.B = 0;
            points.Add(buf);
            buf.X = 150;
            buf.R = 0;
            buf.G = 255;
            buf.B = 0;
            points.Add(buf);
            buf.X = 200;
            buf.R = 0;
            buf.G = 127;
            buf.B = 255;
            points.Add(buf);
            buf.X = 250;
            buf.R = 0;
            buf.G = 0;
            buf.B = 255;
            points.Add(buf);
            buf.X = 300;
            buf.R = 139;
            buf.G = 0;
            buf.B = 255;
            points.Add(buf);
            buf.X = 350;
            buf.R = 255;
            buf.G = 0;
            buf.B = 0;
            points.Add(buf);
        }

        //RGB变化效果预览
        int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label_rgb_fun_preview.BackColor = Get_Value(points, count);
            count++;
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

        private void textBox_num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (select_point < points_count_limit)
                {
                    //编辑模式
                    Edit_InflectionPoint();
                }
                else
                {
                    //添加模式
                    Add_InflectionPoint();
                }
            }
        }

        void Add_InflectionPoint()
        {
            if (points.Count < points_count_limit)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X == (Byte)int.Parse(textBox_Time.Text))
                    {
                        if (MessageBox.Show("添加位置已存在点，是否替换", "错误", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            points[i] = new Inflection_Point
                            {
                                R = (Byte)int.Parse(textBox_R.Text),
                                G = (Byte)int.Parse(textBox_G.Text),
                                B = (Byte)int.Parse(textBox_B.Text),
                                X = (Byte)int.Parse(textBox_Time.Text)
                            };
                            points.Sort((x, y) => x.X.CompareTo(y.X));
                            Freeze_Edit_Control();
                            Refresh_Viewer();
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (i == points.Count - 1)
                    {
                        points.Add(new Inflection_Point
                        {
                            R = (Byte)int.Parse(textBox_R.Text),
                            G = (Byte)int.Parse(textBox_G.Text),
                            B = (Byte)int.Parse(textBox_B.Text),
                            X = (Byte)int.Parse(textBox_Time.Text)
                        });
                        points.Sort((x, y) => x.X.CompareTo(y.X));
                        Freeze_Edit_Control();
                        Refresh_Viewer();
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("已经到达最大点数，不能再添加点");
            }
        }

        void Edit_InflectionPoint()
        {
            points[select_point] = new Inflection_Point
            {
                R = (Byte)int.Parse(textBox_R.Text),
                G = (Byte)int.Parse(textBox_G.Text),
                B = (Byte)int.Parse(textBox_B.Text),
                X = (Byte)int.Parse(textBox_Time.Text)
            };
            points.Sort((x, y) => x.X.CompareTo(y.X));
            Freeze_Edit_Control();
            Refresh_Viewer();
        }

        private void textBox_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("0123456789\x8".IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }
        }
    }
}
