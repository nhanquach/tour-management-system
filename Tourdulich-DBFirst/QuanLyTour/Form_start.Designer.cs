namespace QuanLyTour
{
    partial class Form_start
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
            this.label = new System.Windows.Forms.Label();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_QLTour = new System.Windows.Forms.Button();
            this.button_QLTK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.SystemColors.Control;
            this.label.Font = new System.Drawing.Font("Cooper Black", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label.Location = new System.Drawing.Point(97, 27);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(581, 36);
            this.label.TabIndex = 0;
            this.label.Text = "CÔNG TY CỔ PHẦN TNHH DU LỊCH";
            // 
            // button_Exit
            // 
            this.button_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Exit.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Exit.Location = new System.Drawing.Point(499, 195);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(95, 95);
            this.button_Exit.TabIndex = 1;
            this.button_Exit.Text = "Thoát";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_QLTour
            // 
            this.button_QLTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_QLTour.Location = new System.Drawing.Point(127, 195);
            this.button_QLTour.Name = "button_QLTour";
            this.button_QLTour.Size = new System.Drawing.Size(95, 95);
            this.button_QLTour.TabIndex = 2;
            this.button_QLTour.Text = "QUẢN LÝ TOUR";
            this.button_QLTour.UseVisualStyleBackColor = true;
            this.button_QLTour.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_QLTK
            // 
            this.button_QLTK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_QLTK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_QLTK.Location = new System.Drawing.Point(323, 195);
            this.button_QLTK.Name = "button_QLTK";
            this.button_QLTK.Size = new System.Drawing.Size(95, 95);
            this.button_QLTK.TabIndex = 3;
            this.button_QLTK.Text = "THỐNG KÊ DOANH THU";
            this.button_QLTK.UseVisualStyleBackColor = true;
            this.button_QLTK.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form_start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::QuanLyTour.Properties.Resources.anhdep;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(739, 438);
            this.Controls.Add(this.button_QLTK);
            this.Controls.Add(this.button_QLTour);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.label);
            this.Location = new System.Drawing.Point(100, 200);
            this.Name = "Form_start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form_start";
            this.Load += new System.EventHandler(this.Form_start_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_QLTour;
        private System.Windows.Forms.Button button_QLTK;
    }
}