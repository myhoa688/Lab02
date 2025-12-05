using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            float number1 = float.Parse(txtNumber1.Text);
            float number2 = float.Parse(txtNumber2.Text);
            float result = number1 + number2;
            txtAnswer.Text= result.ToString();
        }
        private void btnSub_Click(object sender, EventArgs e)
        {
            float number1 = float.Parse(txtNumber1.Text);
            float number2 = float.Parse(txtNumber2.Text);
            float result = number1 - number2;
            txtAnswer.Text = result.ToString();
        }
        private void btnMul_Click(object sender, EventArgs e)
        {
            float number1 = float.Parse(txtNumber1.Text);
            float number2 = float.Parse(txtNumber2.Text);
            float result = number1 * number2;
            txtAnswer.Text = result.ToString();
        }
        private void btnDiv_Click(object sender, EventArgs e)
        {
            float number1 = float.Parse(txtNumber1.Text);
            float number2 = float.Parse(txtNumber2.Text);

            if (number2 == 0)
            {
                MessageBox.Show("Không thể chia cho 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float result = number1 / number2;
            txtAnswer.Text = result.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
