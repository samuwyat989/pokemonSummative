namespace pokemonSummative
{
    partial class MinigameScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rowBox1 = new System.Windows.Forms.RichTextBox();
            this.dexBox1 = new System.Windows.Forms.ListBox();
            this.dexBox2 = new System.Windows.Forms.ListBox();
            this.rowBox2 = new System.Windows.Forms.RichTextBox();
            this.dexBox3 = new System.Windows.Forms.ListBox();
            this.rowBox3 = new System.Windows.Forms.RichTextBox();
            this.dexBox4 = new System.Windows.Forms.ListBox();
            this.rowBox4 = new System.Windows.Forms.RichTextBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.scoreLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rowBox1
            // 
            this.rowBox1.Location = new System.Drawing.Point(59, 71);
            this.rowBox1.Name = "rowBox1";
            this.rowBox1.Size = new System.Drawing.Size(141, 773);
            this.rowBox1.TabIndex = 2;
            this.rowBox1.Text = "";
            this.rowBox1.Enter += new System.EventHandler(this.rowBox1_Enter);
            // 
            // dexBox1
            // 
            this.dexBox1.FormattingEnabled = true;
            this.dexBox1.ItemHeight = 20;
            this.dexBox1.Location = new System.Drawing.Point(13, 71);
            this.dexBox1.Name = "dexBox1";
            this.dexBox1.Size = new System.Drawing.Size(40, 764);
            this.dexBox1.TabIndex = 7;
            this.dexBox1.TabStop = false;
            // 
            // dexBox2
            // 
            this.dexBox2.FormattingEnabled = true;
            this.dexBox2.ItemHeight = 20;
            this.dexBox2.Location = new System.Drawing.Point(210, 71);
            this.dexBox2.Name = "dexBox2";
            this.dexBox2.Size = new System.Drawing.Size(40, 764);
            this.dexBox2.TabIndex = 9;
            this.dexBox2.TabStop = false;
            // 
            // rowBox2
            // 
            this.rowBox2.Location = new System.Drawing.Point(256, 74);
            this.rowBox2.Name = "rowBox2";
            this.rowBox2.Size = new System.Drawing.Size(141, 773);
            this.rowBox2.TabIndex = 8;
            this.rowBox2.TabStop = false;
            this.rowBox2.Text = "";
            // 
            // dexBox3
            // 
            this.dexBox3.FormattingEnabled = true;
            this.dexBox3.ItemHeight = 20;
            this.dexBox3.Location = new System.Drawing.Point(407, 71);
            this.dexBox3.Name = "dexBox3";
            this.dexBox3.Size = new System.Drawing.Size(40, 764);
            this.dexBox3.TabIndex = 11;
            this.dexBox3.TabStop = false;
            // 
            // rowBox3
            // 
            this.rowBox3.Location = new System.Drawing.Point(453, 71);
            this.rowBox3.Name = "rowBox3";
            this.rowBox3.Size = new System.Drawing.Size(141, 773);
            this.rowBox3.TabIndex = 10;
            this.rowBox3.TabStop = false;
            this.rowBox3.Text = "";
            // 
            // dexBox4
            // 
            this.dexBox4.FormattingEnabled = true;
            this.dexBox4.ItemHeight = 20;
            this.dexBox4.Location = new System.Drawing.Point(604, 71);
            this.dexBox4.Name = "dexBox4";
            this.dexBox4.Size = new System.Drawing.Size(40, 764);
            this.dexBox4.TabIndex = 13;
            this.dexBox4.TabStop = false;
            this.dexBox4.UseTabStops = false;
            this.dexBox4.Enter += new System.EventHandler(this.dexBox4_Enter);
            // 
            // rowBox4
            // 
            this.rowBox4.Location = new System.Drawing.Point(650, 71);
            this.rowBox4.Name = "rowBox4";
            this.rowBox4.Size = new System.Drawing.Size(141, 773);
            this.rowBox4.TabIndex = 12;
            this.rowBox4.TabStop = false;
            this.rowBox4.Text = "";
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.Font = new System.Drawing.Font("Pokemon GB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.Location = new System.Drawing.Point(295, 28);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(180, 26);
            this.scoreLabel.TabIndex = 14;
            this.scoreLabel.Text = "151/151";
            this.scoreLabel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.scoreLabel_PreviewKeyDown);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Font = new System.Drawing.Font("Pokemon GB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(481, 27);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(132, 26);
            this.timeLabel.TabIndex = 15;
            this.timeLabel.Text = "12:00";
            // 
            // MinigameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.dexBox4);
            this.Controls.Add(this.rowBox4);
            this.Controls.Add(this.dexBox3);
            this.Controls.Add(this.rowBox3);
            this.Controls.Add(this.dexBox2);
            this.Controls.Add(this.rowBox2);
            this.Controls.Add(this.dexBox1);
            this.Controls.Add(this.rowBox1);
            this.DoubleBuffered = true;
            this.Name = "MinigameScreen";
            this.Size = new System.Drawing.Size(800, 850);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MinigameScreen_Paint);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MinigameScreen_PreviewKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rowBox1;
        private System.Windows.Forms.ListBox dexBox1;
        private System.Windows.Forms.ListBox dexBox2;
        private System.Windows.Forms.RichTextBox rowBox2;
        private System.Windows.Forms.ListBox dexBox3;
        private System.Windows.Forms.RichTextBox rowBox3;
        private System.Windows.Forms.ListBox dexBox4;
        private System.Windows.Forms.RichTextBox rowBox4;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label timeLabel;
    }
}
