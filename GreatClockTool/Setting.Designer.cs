namespace GreatClockTool
{
    partial class Setting
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel_timing = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label_num1 = new System.Windows.Forms.Label();
            this.label_num2 = new System.Windows.Forms.Label();
            this.label_num6 = new System.Windows.Forms.Label();
            this.label_num3 = new System.Windows.Forms.Label();
            this.label_num5 = new System.Windows.Forms.Label();
            this.label_num4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.textBox_s_hour = new System.Windows.Forms.TextBox();
            this.label_start = new System.Windows.Forms.Label();
            this.label_end = new System.Windows.Forms.Label();
            this.textBox_s_minute = new System.Windows.Forms.TextBox();
            this.textBox_e_hour = new System.Windows.Forms.TextBox();
            this.textBox_e_minute = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_clocktime = new System.Windows.Forms.Label();
            this.label_clocktime1 = new System.Windows.Forms.Label();
            this.label_localtime = new System.Windows.Forms.Label();
            this.label_localtime1 = new System.Windows.Forms.Label();
            this.button_save = new System.Windows.Forms.Button();
            this.panel_timing.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_timing
            // 
            this.panel_timing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_timing.Controls.Add(this.label_end);
            this.panel_timing.Controls.Add(this.label_start);
            this.panel_timing.Controls.Add(this.textBox_e_minute);
            this.panel_timing.Controls.Add(this.textBox_s_minute);
            this.panel_timing.Controls.Add(this.textBox_e_hour);
            this.panel_timing.Controls.Add(this.textBox_s_hour);
            this.panel_timing.Location = new System.Drawing.Point(138, 218);
            this.panel_timing.Name = "panel_timing";
            this.panel_timing.Size = new System.Drawing.Size(187, 91);
            this.panel_timing.TabIndex = 32;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(275, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 41);
            this.button2.TabIndex = 25;
            this.button2.Text = "设置本机时间到时钟";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label_num1
            // 
            this.label_num1.BackColor = System.Drawing.Color.Black;
            this.label_num1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_num1.Font = new System.Drawing.Font("微软雅黑 Light", 100F);
            this.label_num1.ForeColor = System.Drawing.Color.White;
            this.label_num1.Location = new System.Drawing.Point(16, 19);
            this.label_num1.Name = "label_num1";
            this.label_num1.Size = new System.Drawing.Size(133, 182);
            this.label_num1.TabIndex = 26;
            this.label_num1.Text = "0";
            this.label_num1.Click += new System.EventHandler(this.label_num_Click);
            // 
            // label_num2
            // 
            this.label_num2.BackColor = System.Drawing.Color.Black;
            this.label_num2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_num2.Font = new System.Drawing.Font("微软雅黑 Light", 100F);
            this.label_num2.ForeColor = System.Drawing.Color.White;
            this.label_num2.Location = new System.Drawing.Point(155, 19);
            this.label_num2.Name = "label_num2";
            this.label_num2.Size = new System.Drawing.Size(133, 182);
            this.label_num2.TabIndex = 27;
            this.label_num2.Text = "0";
            this.label_num2.Click += new System.EventHandler(this.label_num_Click);
            // 
            // label_num6
            // 
            this.label_num6.BackColor = System.Drawing.Color.Black;
            this.label_num6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_num6.Font = new System.Drawing.Font("微软雅黑 Light", 100F);
            this.label_num6.ForeColor = System.Drawing.Color.White;
            this.label_num6.Location = new System.Drawing.Point(744, 19);
            this.label_num6.Name = "label_num6";
            this.label_num6.Size = new System.Drawing.Size(133, 182);
            this.label_num6.TabIndex = 31;
            this.label_num6.Text = "0";
            this.label_num6.Click += new System.EventHandler(this.label_num_Click);
            // 
            // label_num3
            // 
            this.label_num3.BackColor = System.Drawing.Color.Black;
            this.label_num3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_num3.Font = new System.Drawing.Font("微软雅黑 Light", 100F);
            this.label_num3.ForeColor = System.Drawing.Color.White;
            this.label_num3.Location = new System.Drawing.Point(312, 19);
            this.label_num3.Name = "label_num3";
            this.label_num3.Size = new System.Drawing.Size(133, 182);
            this.label_num3.TabIndex = 28;
            this.label_num3.Text = "0";
            this.label_num3.Click += new System.EventHandler(this.label_num_Click);
            // 
            // label_num5
            // 
            this.label_num5.BackColor = System.Drawing.Color.Black;
            this.label_num5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_num5.Font = new System.Drawing.Font("微软雅黑 Light", 100F);
            this.label_num5.ForeColor = System.Drawing.Color.White;
            this.label_num5.Location = new System.Drawing.Point(605, 19);
            this.label_num5.Name = "label_num5";
            this.label_num5.Size = new System.Drawing.Size(133, 182);
            this.label_num5.TabIndex = 30;
            this.label_num5.Text = "0";
            this.label_num5.Click += new System.EventHandler(this.label_num_Click);
            // 
            // label_num4
            // 
            this.label_num4.BackColor = System.Drawing.Color.Black;
            this.label_num4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_num4.Font = new System.Drawing.Font("微软雅黑 Light", 100F);
            this.label_num4.ForeColor = System.Drawing.Color.White;
            this.label_num4.Location = new System.Drawing.Point(451, 19);
            this.label_num4.Name = "label_num4";
            this.label_num4.Size = new System.Drawing.Size(133, 182);
            this.label_num4.TabIndex = 29;
            this.label_num4.Text = "0";
            this.label_num4.Click += new System.EventHandler(this.label_num_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // textBox_s_hour
            // 
            this.textBox_s_hour.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_s_hour.Location = new System.Drawing.Point(87, 15);
            this.textBox_s_hour.MaxLength = 2;
            this.textBox_s_hour.Name = "textBox_s_hour";
            this.textBox_s_hour.Size = new System.Drawing.Size(33, 29);
            this.textBox_s_hour.TabIndex = 0;
            this.textBox_s_hour.Text = "8";
            this.textBox_s_hour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_s_hour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_s_hour_KeyDown);
            this.textBox_s_hour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_s_hour_KeyPress);
            // 
            // label_start
            // 
            this.label_start.AutoSize = true;
            this.label_start.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_start.Location = new System.Drawing.Point(16, 18);
            this.label_start.Name = "label_start";
            this.label_start.Size = new System.Drawing.Size(65, 19);
            this.label_start.TabIndex = 1;
            this.label_start.Text = "开启时间";
            // 
            // label_end
            // 
            this.label_end.AutoSize = true;
            this.label_end.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_end.Location = new System.Drawing.Point(16, 56);
            this.label_end.Name = "label_end";
            this.label_end.Size = new System.Drawing.Size(65, 19);
            this.label_end.TabIndex = 2;
            this.label_end.Text = "关闭时间";
            // 
            // textBox_s_minute
            // 
            this.textBox_s_minute.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_s_minute.Location = new System.Drawing.Point(126, 15);
            this.textBox_s_minute.MaxLength = 2;
            this.textBox_s_minute.Name = "textBox_s_minute";
            this.textBox_s_minute.Size = new System.Drawing.Size(33, 29);
            this.textBox_s_minute.TabIndex = 0;
            this.textBox_s_minute.Text = "0";
            this.textBox_s_minute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_s_minute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_s_hour_KeyDown);
            this.textBox_s_minute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_s_hour_KeyPress);
            // 
            // textBox_e_hour
            // 
            this.textBox_e_hour.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_e_hour.Location = new System.Drawing.Point(87, 51);
            this.textBox_e_hour.MaxLength = 2;
            this.textBox_e_hour.Name = "textBox_e_hour";
            this.textBox_e_hour.Size = new System.Drawing.Size(33, 29);
            this.textBox_e_hour.TabIndex = 0;
            this.textBox_e_hour.Text = "24";
            this.textBox_e_hour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_e_hour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_s_hour_KeyDown);
            this.textBox_e_hour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_s_hour_KeyPress);
            // 
            // textBox_e_minute
            // 
            this.textBox_e_minute.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_e_minute.Location = new System.Drawing.Point(126, 51);
            this.textBox_e_minute.MaxLength = 2;
            this.textBox_e_minute.Name = "textBox_e_minute";
            this.textBox_e_minute.Size = new System.Drawing.Size(33, 29);
            this.textBox_e_minute.TabIndex = 0;
            this.textBox_e_minute.Text = "0";
            this.textBox_e_minute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_e_minute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_s_hour_KeyDown);
            this.textBox_e_minute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_s_hour_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.label_localtime1);
            this.panel1.Controls.Add(this.label_clocktime1);
            this.panel1.Controls.Add(this.label_localtime);
            this.panel1.Controls.Add(this.label_clocktime);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(331, 218);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 91);
            this.panel1.TabIndex = 33;
            // 
            // label_clocktime
            // 
            this.label_clocktime.AutoSize = true;
            this.label_clocktime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_clocktime.Location = new System.Drawing.Point(22, 17);
            this.label_clocktime.Name = "label_clocktime";
            this.label_clocktime.Size = new System.Drawing.Size(65, 19);
            this.label_clocktime.TabIndex = 26;
            this.label_clocktime.Text = "时钟时间";
            // 
            // label_clocktime1
            // 
            this.label_clocktime1.AutoSize = true;
            this.label_clocktime1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_clocktime1.Location = new System.Drawing.Point(93, 15);
            this.label_clocktime1.Name = "label_clocktime1";
            this.label_clocktime1.Size = new System.Drawing.Size(141, 22);
            this.label_clocktime1.TabIndex = 27;
            this.label_clocktime1.Text = "2019/10/1 0:0:0";
            // 
            // label_localtime
            // 
            this.label_localtime.AutoSize = true;
            this.label_localtime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_localtime.Location = new System.Drawing.Point(22, 47);
            this.label_localtime.Name = "label_localtime";
            this.label_localtime.Size = new System.Drawing.Size(65, 19);
            this.label_localtime.TabIndex = 26;
            this.label_localtime.Text = "本地时间";
            // 
            // label_localtime1
            // 
            this.label_localtime1.AutoSize = true;
            this.label_localtime1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_localtime1.Location = new System.Drawing.Point(93, 45);
            this.label_localtime1.Name = "label_localtime1";
            this.label_localtime1.Size = new System.Drawing.Size(141, 22);
            this.label_localtime1.TabIndex = 27;
            this.label_localtime1.Text = "2019/10/1 0:0:0";
            // 
            // button_save
            // 
            this.button_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_save.Location = new System.Drawing.Point(777, 224);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(83, 74);
            this.button_save.TabIndex = 34;
            this.button_save.Text = "保存设置";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_timing);
            this.Controls.Add(this.label_num1);
            this.Controls.Add(this.label_num2);
            this.Controls.Add(this.label_num6);
            this.Controls.Add(this.label_num3);
            this.Controls.Add(this.label_num5);
            this.Controls.Add(this.label_num4);
            this.Name = "Setting";
            this.Size = new System.Drawing.Size(897, 324);
            this.Load += new System.EventHandler(this.Setting_Load);
            this.panel_timing.ResumeLayout(false);
            this.panel_timing.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_timing;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label_num1;
        private System.Windows.Forms.Label label_num2;
        private System.Windows.Forms.Label label_num6;
        private System.Windows.Forms.Label label_num3;
        private System.Windows.Forms.Label label_num5;
        private System.Windows.Forms.Label label_num4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label_end;
        private System.Windows.Forms.Label label_start;
        private System.Windows.Forms.TextBox textBox_e_minute;
        private System.Windows.Forms.TextBox textBox_s_minute;
        private System.Windows.Forms.TextBox textBox_e_hour;
        private System.Windows.Forms.TextBox textBox_s_hour;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_localtime1;
        private System.Windows.Forms.Label label_clocktime1;
        private System.Windows.Forms.Label label_localtime;
        private System.Windows.Forms.Label label_clocktime;
        private System.Windows.Forms.Button button_save;
    }
}
