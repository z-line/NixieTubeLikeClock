namespace GreatClockTool
{
    partial class Main
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_connect_status = new System.Windows.Forms.Label();
            this.label_clock_time = new System.Windows.Forms.Label();
            this.control_container = new System.Windows.Forms.Panel();
            this.info_label = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.control_container.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_connect_status
            // 
            this.label_connect_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_connect_status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label_connect_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_connect_status.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_connect_status.ForeColor = System.Drawing.Color.White;
            this.label_connect_status.Location = new System.Drawing.Point(768, 379);
            this.label_connect_status.Name = "label_connect_status";
            this.label_connect_status.Size = new System.Drawing.Size(153, 27);
            this.label_connect_status.TabIndex = 12;
            this.label_connect_status.Text = "未连接";
            this.label_connect_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_clock_time
            // 
            this.label_clock_time.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_clock_time.BackColor = System.Drawing.Color.Black;
            this.label_clock_time.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label_clock_time.ForeColor = System.Drawing.Color.White;
            this.label_clock_time.Location = new System.Drawing.Point(0, 379);
            this.label_clock_time.Name = "label_clock_time";
            this.label_clock_time.Size = new System.Drawing.Size(768, 27);
            this.label_clock_time.TabIndex = 20;
            this.label_clock_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // control_container
            // 
            this.control_container.Controls.Add(this.info_label);
            this.control_container.Location = new System.Drawing.Point(12, 42);
            this.control_container.Name = "control_container";
            this.control_container.Size = new System.Drawing.Size(897, 324);
            this.control_container.TabIndex = 22;
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.info_label.Location = new System.Drawing.Point(304, 126);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(220, 38);
            this.info_label.TabIndex = 0;
            this.info_label.Text = "寻找时钟端口中";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackgroundImage = global::GreatClockTool.Properties.Resources._;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.button3.Location = new System.Drawing.Point(811, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 36);
            this.button3.TabIndex = 21;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = global::GreatClockTool.Properties.Resources.x;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(869, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 36);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(921, 406);
            this.Controls.Add(this.control_container);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label_connect_status);
            this.Controls.Add(this.label_clock_time);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.control_container.ResumeLayout(false);
            this.control_container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_connect_status;
        private System.Windows.Forms.Label label_clock_time;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel control_container;
        private System.Windows.Forms.Label info_label;
    }
}

