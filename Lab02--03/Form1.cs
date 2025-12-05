using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab02_03
{
    public partial class Form1 : Form
    {
        private List<Button> seatButtons = new List<Button>();

        public Form1()
        {
            InitializeComponent();
            InitializeSeats(); // gọi hàm tạo nút ghế
        }

        private void InitializeSeats()
        {
            int seatNumber = 1;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Button btn = new Button
                    {
                        Text = seatNumber.ToString(),
                        Size = new Size(60, 30),
                        Location = new Point(col * 70 + 10, row * 40 + 20),
                        BackColor = Color.White,
                        Tag = seatNumber
                    };
                    btn.Click += btnChooseASeat;
                    seatButtons.Add(btn);

                    // thêm nút vào groupBoxSeats
                    groupBoxSeats.Controls.Add(btn);
                    seatNumber++;
                }
            }
        }

        private void btnChooseASeat(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor == Color.White)
                btn.BackColor = Color.Blue;
            else if (btn.BackColor == Color.Blue)
                btn.BackColor = Color.White;
            else if (btn.BackColor == Color.Yellow)
                MessageBox.Show("Ghế đã được bán!!");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            int total = 0;
            foreach (Button btn in seatButtons)
            {
                if (btn.BackColor == Color.Blue)
                {
                    btn.BackColor = Color.Yellow;
                    int seatNum = (int)btn.Tag;
                    total += GetPrice(seatNum);
                }
            }
            lblTotal.Text = $"Thành Tiền: {total} đ";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (Button btn in seatButtons)
            {
                if (btn.BackColor == Color.Blue)
                    btn.BackColor = Color.White;
            }
            lblTotal.Text = "Thành Tiền: 0";
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int GetPrice(int seatNum)
        {
            if (seatNum >= 1 && seatNum <= 5) return 30000;
            if (seatNum >= 6 && seatNum <= 10) return 40000;
            if (seatNum >= 11 && seatNum <= 15) return 50000;
            return 80000;
        }
    }
}
