namespace Lab02_03
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxSeats;
        private System.Windows.Forms.Label lblScreen;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFinish;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBoxSeats = new System.Windows.Forms.GroupBox();
            this.lblScreen = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // groupBoxSeats
            this.groupBoxSeats.Location = new System.Drawing.Point(50, 50);
            this.groupBoxSeats.Name = "groupBoxSeats";
            this.groupBoxSeats.Size = new System.Drawing.Size(400, 200);
            this.groupBoxSeats.TabIndex = 0;
            this.groupBoxSeats.TabStop = false;
            this.groupBoxSeats.Text = "Sơ đồ ghế";

            // lblScreen
            this.lblScreen.AutoSize = true;
            this.lblScreen.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblScreen.Location = new System.Drawing.Point(200, 20);
            this.lblScreen.Name = "lblScreen";
            this.lblScreen.Size = new System.Drawing.Size(90, 19);
            this.lblScreen.Text = "MÀN ẢNH";

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(50, 270);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(110, 16);
            this.lblTotal.Text = "Thành Tiền: 0";

            // btnSelect
            this.btnSelect.Location = new System.Drawing.Point(50, 300);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(80, 30);
            this.btnSelect.Text = "Chọn";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(150, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // btnFinish
            this.btnFinish.Location = new System.Drawing.Point(250, 300);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(80, 30);
            this.btnFinish.Text = "Kết thúc";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(500, 380);
            this.Controls.Add(this.groupBoxSeats);
            this.Controls.Add(this.lblScreen);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Name = "Form1";
            this.Text = "BÁN VÉ RẠP CHIẾU PHIM";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
