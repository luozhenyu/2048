namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.button_close = new System.Windows.Forms.Button();
            this.button_newGame = new System.Windows.Forms.Button();
            this.button_minimum = new System.Windows.Forms.Button();
            this.label_best = new System.Windows.Forms.Label();
            this.label_score = new System.Windows.Forms.Label();
            this.button_About = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_close
            // 
            this.button_close.BackColor = System.Drawing.Color.OrangeRed;
            this.button_close.CausesValidation = false;
            this.button_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_close.ForeColor = System.Drawing.Color.White;
            this.button_close.Location = new System.Drawing.Point(12, 12);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(20, 20);
            this.button_close.TabIndex = 0;
            this.button_close.TabStop = false;
            this.button_close.UseVisualStyleBackColor = false;
            this.button_close.Click += new System.EventHandler(this.button_Exit_Click);
            this.button_close.MouseLeave += new System.EventHandler(this.button_Exit_MouseLeave);
            this.button_close.MouseHover += new System.EventHandler(this.button_Close_MouseHover);
            // 
            // button_newGame
            // 
            this.button_newGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(122)))), ((int)(((byte)(101)))));
            this.button_newGame.CausesValidation = false;
            this.button_newGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_newGame.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_newGame.ForeColor = System.Drawing.Color.White;
            this.button_newGame.Location = new System.Drawing.Point(330, 95);
            this.button_newGame.Name = "button_newGame";
            this.button_newGame.Size = new System.Drawing.Size(130, 40);
            this.button_newGame.TabIndex = 1;
            this.button_newGame.TabStop = false;
            this.button_newGame.Text = "New Game";
            this.button_newGame.UseVisualStyleBackColor = false;
            this.button_newGame.Click += new System.EventHandler(this.button_NewGame_Click);
            // 
            // button_minimum
            // 
            this.button_minimum.BackColor = System.Drawing.Color.Gold;
            this.button_minimum.CausesValidation = false;
            this.button_minimum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_minimum.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_minimum.ForeColor = System.Drawing.Color.White;
            this.button_minimum.Location = new System.Drawing.Point(38, 12);
            this.button_minimum.Name = "button_minimum";
            this.button_minimum.Size = new System.Drawing.Size(20, 20);
            this.button_minimum.TabIndex = 3;
            this.button_minimum.TabStop = false;
            this.button_minimum.UseVisualStyleBackColor = false;
            this.button_minimum.Click += new System.EventHandler(this.button_Minimum_Click);
            this.button_minimum.MouseLeave += new System.EventHandler(this.button_Minimum_MouseLeave);
            this.button_minimum.MouseHover += new System.EventHandler(this.button_Minimum_MouseHover);
            // 
            // label_best
            // 
            this.label_best.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(187)))), ((int)(((byte)(176)))));
            this.label_best.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_best.ForeColor = System.Drawing.Color.White;
            this.label_best.Location = new System.Drawing.Point(361, 57);
            this.label_best.Name = "label_best";
            this.label_best.Size = new System.Drawing.Size(99, 27);
            this.label_best.TabIndex = 4;
            this.label_best.Text = "0";
            this.label_best.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_score
            // 
            this.label_score.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(187)))), ((int)(((byte)(176)))));
            this.label_score.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_score.ForeColor = System.Drawing.Color.White;
            this.label_score.Location = new System.Drawing.Point(251, 57);
            this.label_score.Name = "label_score";
            this.label_score.Size = new System.Drawing.Size(99, 27);
            this.label_score.TabIndex = 5;
            this.label_score.Text = "0";
            this.label_score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_About
            // 
            this.button_About.BackColor = System.Drawing.Color.SpringGreen;
            this.button_About.CausesValidation = false;
            this.button_About.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_About.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_About.ForeColor = System.Drawing.Color.White;
            this.button_About.Location = new System.Drawing.Point(64, 12);
            this.button_About.Name = "button_About";
            this.button_About.Size = new System.Drawing.Size(20, 20);
            this.button_About.TabIndex = 6;
            this.button_About.TabStop = false;
            this.button_About.UseVisualStyleBackColor = false;
            this.button_About.Click += new System.EventHandler(this.button_About_Click);
            this.button_About.MouseLeave += new System.EventHandler(this.button_About_MouseLeave);
            this.button_About.MouseHover += new System.EventHandler(this.button_About_MouseHover);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.button_close;
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Controls.Add(this.button_About);
            this.Controls.Add(this.label_score);
            this.Controls.Add(this.label_best);
            this.Controls.Add(this.button_minimum);
            this.Controls.Add(this.button_newGame);
            this.Controls.Add(this.button_close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_newGame;
        private System.Windows.Forms.Button button_minimum;
        private System.Windows.Forms.Label label_best;
        private System.Windows.Forms.Label label_score;
        private System.Windows.Forms.Button button_About;
    }
}

