namespace pokemonSummative
{
    partial class IntroScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntroScreen));
            this.introPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.introTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.introPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // introPlayer
            // 
            this.introPlayer.Enabled = true;
            this.introPlayer.Location = new System.Drawing.Point(0, 16);
            this.introPlayer.Name = "introPlayer";
            this.introPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("introPlayer.OcxState")));
            this.introPlayer.Size = new System.Drawing.Size(800, 781);
            this.introPlayer.TabIndex = 10;
            // 
            // introTimer
            // 
            this.introTimer.Enabled = true;
            this.introTimer.Interval = 1000;
            this.introTimer.Tick += new System.EventHandler(this.introTimer_Tick);
            // 
            // IntroScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::pokemonSummative.Properties.Resources.PokemonRedStartScreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.introPlayer);
            this.DoubleBuffered = true;
            this.Name = "IntroScreen";
            this.Size = new System.Drawing.Size(800, 800);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.IntroScreen_Paint);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.IntroScreen_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.introPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer introPlayer;
        private System.Windows.Forms.Timer introTimer;
    }
}
