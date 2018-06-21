namespace pokemonSummative
{
    partial class BattleScreen
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
            this.screenTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // screenTimer
            // 
            this.screenTimer.Interval = 20;
            this.screenTimer.Tick += new System.EventHandler(this.screenTimer_Tick);
            // 
            // BattleScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "BattleScreen";
            this.Size = new System.Drawing.Size(800, 800);
            this.Load += new System.EventHandler(this.BattleScreen_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BattleScreen_Paint);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.BattleScreen_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer screenTimer;
    }
}
