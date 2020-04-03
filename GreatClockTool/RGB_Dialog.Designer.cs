namespace GreatClockTool
{
    partial class RGB_Dialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_close = new System.Windows.Forms.Button();
            this.panel_view = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label_rgb_fun_preview = new System.Windows.Forms.Label();
            this.panel_window = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_color_preview = new System.Windows.Forms.Label();
            this.label_Time = new System.Windows.Forms.Label();
            this.textBox_Time = new System.Windows.Forms.TextBox();
            this.label_B = new System.Windows.Forms.Label();
            this.textBox_B = new System.Windows.Forms.TextBox();
            this.label_G = new System.Windows.Forms.Label();
            this.textBox_G = new System.Windows.Forms.TextBox();
            this.label_R = new System.Windows.Forms.Label();
            this.textBox_R = new System.Windows.Forms.TextBox();
            this.label_title = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_AddPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_DeletePoint = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ModifyPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_window.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.BackgroundImage = global::GreatClockTool.Properties.Resources.x;
            this.button_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_close.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button_close.FlatAppearance.BorderSize = 0;
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_close.Location = new System.Drawing.Point(453, -1);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(48, 32);
            this.button_close.TabIndex = 22;
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // panel_view
            // 
            this.panel_view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_view.BackColor = System.Drawing.Color.Black;
            this.panel_view.Location = new System.Drawing.Point(3, 30);
            this.panel_view.Name = "panel_view";
            this.panel_view.Size = new System.Drawing.Size(494, 209);
            this.panel_view.TabIndex = 24;
            this.panel_view.Click += new System.EventHandler(this.panel_view_Click);
            this.panel_view.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_view_MouseDown);
            this.panel_view.MouseLeave += new System.EventHandler(this.panel_view_MouseLeave);
            this.panel_view.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_view_MouseMove);
            this.panel_view.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_view_MouseUp);
            // 
            // label_rgb_fun_preview
            // 
            this.label_rgb_fun_preview.BackColor = System.Drawing.Color.Black;
            this.label_rgb_fun_preview.Location = new System.Drawing.Point(6, 16);
            this.label_rgb_fun_preview.Name = "label_rgb_fun_preview";
            this.label_rgb_fun_preview.Size = new System.Drawing.Size(90, 83);
            this.label_rgb_fun_preview.TabIndex = 24;
            // 
            // panel_window
            // 
            this.panel_window.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_window.Controls.Add(this.groupBox1);
            this.panel_window.Controls.Add(this.label_color_preview);
            this.panel_window.Controls.Add(this.label_Time);
            this.panel_window.Controls.Add(this.textBox_Time);
            this.panel_window.Controls.Add(this.label_B);
            this.panel_window.Controls.Add(this.textBox_B);
            this.panel_window.Controls.Add(this.label_G);
            this.panel_window.Controls.Add(this.textBox_G);
            this.panel_window.Controls.Add(this.label_R);
            this.panel_window.Controls.Add(this.textBox_R);
            this.panel_window.Controls.Add(this.label_title);
            this.panel_window.Controls.Add(this.panel_view);
            this.panel_window.Controls.Add(this.button_close);
            this.panel_window.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_window.Location = new System.Drawing.Point(0, 0);
            this.panel_window.Name = "panel_window";
            this.panel_window.Size = new System.Drawing.Size(502, 370);
            this.panel_window.TabIndex = 26;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_rgb_fun_preview);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(11, 253);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(102, 112);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RGB效果预览";
            // 
            // label_color_preview
            // 
            this.label_color_preview.BackColor = System.Drawing.Color.Black;
            this.label_color_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_color_preview.Enabled = false;
            this.label_color_preview.Location = new System.Drawing.Point(269, 267);
            this.label_color_preview.Name = "label_color_preview";
            this.label_color_preview.Size = new System.Drawing.Size(104, 50);
            this.label_color_preview.TabIndex = 37;
            this.label_color_preview.Click += new System.EventHandler(this.label_color_preview_Click);
            // 
            // label_Time
            // 
            this.label_Time.Font = new System.Drawing.Font("宋体", 10F);
            this.label_Time.Location = new System.Drawing.Point(266, 323);
            this.label_Time.Name = "label_Time";
            this.label_Time.Size = new System.Drawing.Size(35, 19);
            this.label_Time.TabIndex = 34;
            this.label_Time.Text = "Time";
            // 
            // textBox_Time
            // 
            this.textBox_Time.Enabled = false;
            this.textBox_Time.Font = new System.Drawing.Font("宋体", 10F);
            this.textBox_Time.Location = new System.Drawing.Point(307, 320);
            this.textBox_Time.Name = "textBox_Time";
            this.textBox_Time.Size = new System.Drawing.Size(66, 23);
            this.textBox_Time.TabIndex = 33;
            this.textBox_Time.Text = "0";
            this.textBox_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Time.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_num_KeyDown);
            this.textBox_Time.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_num_KeyPress);
            // 
            // label_B
            // 
            this.label_B.AutoSize = true;
            this.label_B.Font = new System.Drawing.Font("宋体", 10F);
            this.label_B.Location = new System.Drawing.Point(187, 323);
            this.label_B.Name = "label_B";
            this.label_B.Size = new System.Drawing.Size(14, 14);
            this.label_B.TabIndex = 32;
            this.label_B.Text = "B";
            // 
            // textBox_B
            // 
            this.textBox_B.Enabled = false;
            this.textBox_B.Font = new System.Drawing.Font("宋体", 10F);
            this.textBox_B.ForeColor = System.Drawing.Color.Blue;
            this.textBox_B.Location = new System.Drawing.Point(207, 321);
            this.textBox_B.Name = "textBox_B";
            this.textBox_B.Size = new System.Drawing.Size(49, 23);
            this.textBox_B.TabIndex = 31;
            this.textBox_B.Text = "0";
            this.textBox_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_B.TextChanged += new System.EventHandler(this.textBox_RGB_TextChanged);
            this.textBox_B.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_num_KeyDown);
            this.textBox_B.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_num_KeyPress);
            // 
            // label_G
            // 
            this.label_G.AutoSize = true;
            this.label_G.Font = new System.Drawing.Font("宋体", 10F);
            this.label_G.Location = new System.Drawing.Point(187, 296);
            this.label_G.Name = "label_G";
            this.label_G.Size = new System.Drawing.Size(14, 14);
            this.label_G.TabIndex = 30;
            this.label_G.Text = "G";
            // 
            // textBox_G
            // 
            this.textBox_G.Enabled = false;
            this.textBox_G.Font = new System.Drawing.Font("宋体", 10F);
            this.textBox_G.ForeColor = System.Drawing.Color.Green;
            this.textBox_G.Location = new System.Drawing.Point(207, 294);
            this.textBox_G.Name = "textBox_G";
            this.textBox_G.Size = new System.Drawing.Size(49, 23);
            this.textBox_G.TabIndex = 29;
            this.textBox_G.Text = "0";
            this.textBox_G.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_G.TextChanged += new System.EventHandler(this.textBox_RGB_TextChanged);
            this.textBox_G.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_num_KeyDown);
            this.textBox_G.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_num_KeyPress);
            // 
            // label_R
            // 
            this.label_R.AutoSize = true;
            this.label_R.Font = new System.Drawing.Font("宋体", 10F);
            this.label_R.Location = new System.Drawing.Point(187, 269);
            this.label_R.Name = "label_R";
            this.label_R.Size = new System.Drawing.Size(14, 14);
            this.label_R.TabIndex = 28;
            this.label_R.Text = "R";
            // 
            // textBox_R
            // 
            this.textBox_R.BackColor = System.Drawing.Color.White;
            this.textBox_R.Enabled = false;
            this.textBox_R.Font = new System.Drawing.Font("宋体", 10F);
            this.textBox_R.ForeColor = System.Drawing.Color.Red;
            this.textBox_R.Location = new System.Drawing.Point(207, 267);
            this.textBox_R.Name = "textBox_R";
            this.textBox_R.Size = new System.Drawing.Size(49, 23);
            this.textBox_R.TabIndex = 27;
            this.textBox_R.Text = "0";
            this.textBox_R.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_R.TextChanged += new System.EventHandler(this.textBox_RGB_TextChanged);
            this.textBox_R.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_num_KeyDown);
            this.textBox_R.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_num_KeyPress);
            // 
            // label_title
            // 
            this.label_title.BackColor = System.Drawing.Color.White;
            this.label_title.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.label_title.Location = new System.Drawing.Point(-2, -1);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(455, 30);
            this.label_title.TabIndex = 26;
            this.label_title.Text = " RGB变化函数编辑器";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_title_MouseDown);
            this.label_title.MouseLeave += new System.EventHandler(this.label_title_MouseLeave);
            this.label_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_title_MouseMove);
            this.label_title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_title_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_AddPoint,
            this.ToolStripMenuItem_DeletePoint,
            this.ToolStripMenuItem_ModifyPoint});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // ToolStripMenuItem_AddPoint
            // 
            this.ToolStripMenuItem_AddPoint.Name = "ToolStripMenuItem_AddPoint";
            this.ToolStripMenuItem_AddPoint.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_AddPoint.Text = "添加节点";
            this.ToolStripMenuItem_AddPoint.Click += new System.EventHandler(this.AddPoint_ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_DeletePoint
            // 
            this.ToolStripMenuItem_DeletePoint.Name = "ToolStripMenuItem_DeletePoint";
            this.ToolStripMenuItem_DeletePoint.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_DeletePoint.Text = "删除节点";
            this.ToolStripMenuItem_DeletePoint.Click += new System.EventHandler(this.DeletePoint_ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_ModifyPoint
            // 
            this.ToolStripMenuItem_ModifyPoint.Name = "ToolStripMenuItem_ModifyPoint";
            this.ToolStripMenuItem_ModifyPoint.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_ModifyPoint.Text = "编辑节点";
            this.ToolStripMenuItem_ModifyPoint.Click += new System.EventHandler(this.EditPoint_ToolStripMenuItem_Click);
            // 
            // RGB_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(502, 370);
            this.Controls.Add(this.panel_window);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RGB_Dialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RGB_Dialog";
            this.Shown += new System.EventHandler(this.RGB_Dialog_Shown);
            this.panel_window.ResumeLayout(false);
            this.panel_window.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Panel panel_view;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label_rgb_fun_preview;
        private System.Windows.Forms.Panel panel_window;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_Time;
        private System.Windows.Forms.TextBox textBox_Time;
        private System.Windows.Forms.Label label_B;
        private System.Windows.Forms.TextBox textBox_B;
        private System.Windows.Forms.Label label_G;
        private System.Windows.Forms.TextBox textBox_G;
        private System.Windows.Forms.Label label_R;
        private System.Windows.Forms.TextBox textBox_R;
        private System.Windows.Forms.Label label_color_preview;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddPoint;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_DeletePoint;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ModifyPoint;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}