namespace GreatClockTool
{
    partial class Update
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
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_update = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_percentage = new System.Windows.Forms.Label();
            this.label_finish = new System.Windows.Forms.Label();
            this.button_load = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // textBox_path
            // 
            this.textBox_path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_path.Location = new System.Drawing.Point(36, 16);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(329, 21);
            this.textBox_path.TabIndex = 0;
            // 
            // button_update
            // 
            this.button_update.BackColor = System.Drawing.Color.White;
            this.button_update.Cursor = System.Windows.Forms.Cursors.No;
            this.button_update.Enabled = false;
            this.button_update.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_update.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_update.ForeColor = System.Drawing.Color.Black;
            this.button_update.Location = new System.Drawing.Point(198, 43);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(128, 44);
            this.button_update.TabIndex = 1;
            this.button_update.Text = "更新固件";
            this.button_update.UseVisualStyleBackColor = false;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "bin文件|*.bin";
            // 
            // textBox_file
            // 
            this.textBox_file.BackColor = System.Drawing.Color.Silver;
            this.textBox_file.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_file.Location = new System.Drawing.Point(452, 3);
            this.textBox_file.Multiline = true;
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox_file.Size = new System.Drawing.Size(442, 318);
            this.textBox_file.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar1.Location = new System.Drawing.Point(28, 298);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(361, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // label_percentage
            // 
            this.label_percentage.AutoSize = true;
            this.label_percentage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_percentage.Location = new System.Drawing.Point(395, 298);
            this.label_percentage.Name = "label_percentage";
            this.label_percentage.Size = new System.Drawing.Size(33, 21);
            this.label_percentage.TabIndex = 4;
            this.label_percentage.Text = "0%";
            // 
            // label_finish
            // 
            this.label_finish.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_finish.Location = new System.Drawing.Point(3, 3);
            this.label_finish.Name = "label_finish";
            this.label_finish.Size = new System.Drawing.Size(891, 318);
            this.label_finish.TabIndex = 5;
            this.label_finish.Text = "更新完成，请重新连接时钟至PC\r\n并重启该软件";
            this.label_finish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_finish.Visible = false;
            // 
            // button_load
            // 
            this.button_load.BackColor = System.Drawing.Color.White;
            this.button_load.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_load.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_load.ForeColor = System.Drawing.Color.Black;
            this.button_load.Location = new System.Drawing.Point(64, 43);
            this.button_load.Name = "button_load";
            this.button_load.Size = new System.Drawing.Size(128, 44);
            this.button_load.TabIndex = 6;
            this.button_load.Text = "加载固件";
            this.button_load.UseVisualStyleBackColor = false;
            this.button_load.Click += new System.EventHandler(this.button_load_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_load);
            this.Controls.Add(this.label_percentage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBox_file);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.label_finish);
            this.Name = "Update";
            this.Size = new System.Drawing.Size(897, 324);
            this.Load += new System.EventHandler(this.Update_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox_file;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_percentage;
        private System.Windows.Forms.Label label_finish;
        private System.Windows.Forms.Button button_load;
        private System.Windows.Forms.Timer timer1;
    }
}
