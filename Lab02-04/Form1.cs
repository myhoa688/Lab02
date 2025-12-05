using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab02_04
{
    public partial class Form1 : Form
    {
        private List<Account> accounts = new List<Account>();

        public Form1()
        {
            InitializeComponent();
            InitListView();
        }

        private void InitListView()
        {
            dgvAccounts.View = View.Details;
            dgvAccounts.FullRowSelect = true;
            dgvAccounts.GridLines = true;

            dgvAccounts.Columns.Add("STT", 50);
            dgvAccounts.Columns.Add("MÃ TÀI KHOẢN", 120);
            dgvAccounts.Columns.Add("TÊN KHÁCH HÀNG", 150);
            dgvAccounts.Columns.Add("ĐỊA CHỈ", 150);
            dgvAccounts.Columns.Add("SỐ TIỀN", 100);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // NHẬP SẴN DỮ LIỆU VIẾT HOA
            accounts.Add(new Account { STT = 1, MaTaiKhoan = "TK001", TenKhachHang = "NGUYỄN VĂN A", DiaChi = "QUẬN 1, TP.HCM", SoTien = 5000000 });
            accounts.Add(new Account { STT = 2, MaTaiKhoan = "TK002", TenKhachHang = "TRẦN THỊ B", DiaChi = "QUẬN 3, TP.HCM", SoTien = 7500000 });
            accounts.Add(new Account { STT = 3, MaTaiKhoan = "TK003", TenKhachHang = "LÊ VĂN C", DiaChi = "QUẬN 5, TP.HCM", SoTien = 10000000 });
            accounts.Add(new Account { STT = 4, MaTaiKhoan = "TK004", TenKhachHang = "PHẠM THỊ D", DiaChi = "QUẬN 7, TP.HCM", SoTien = 3000000 });
            accounts.Add(new Account { STT = 5, MaTaiKhoan = "TK005", TenKhachHang = "HOÀNG VĂN E", DiaChi = "BÌNH THẠNH, TP.HCM", SoTien = 8500000 });

            RefreshListView();
            UpdateTongTien();
        }

        private void btnThemCapNhat_Click(object sender, EventArgs e)
        {
            string maTk = txtSoTaiKhoan.Text.Trim().ToUpper();
            string ten = txtTenKhachHang.Text.Trim().ToUpper();
            string diaChi = txtDiaChi.Text.Trim().ToUpper();
            if (!decimal.TryParse(txtSoTien.Text.Trim(), out decimal soTien))
            {
                MessageBox.Show("SỐ TIỀN KHÔNG HỢP LỆ!");
                return;
            }

            var existing = accounts.FirstOrDefault(a => a.MaTaiKhoan == maTk);
            if (existing != null)
            {
                existing.TenKhachHang = ten;
                existing.DiaChi = diaChi;
                existing.SoTien = soTien;
            }
            else
            {
                accounts.Add(new Account
                {
                    STT = accounts.Count + 1,
                    MaTaiKhoan = maTk,
                    TenKhachHang = ten,
                    DiaChi = diaChi,
                    SoTien = soTien
                });
            }

            RefreshListView();
            UpdateTongTien();
            ClearInputs();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedItems.Count > 0)
            {
                string maTk = dgvAccounts.SelectedItems[0].SubItems[1].Text;
                var acc = accounts.FirstOrDefault(a => a.MaTaiKhoan == maTk);
                if (acc != null)
                {
                    accounts.Remove(acc);
                    RefreshListView();
                    UpdateTongTien();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRut_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedItems.Count > 0)
            {
                string maTk = dgvAccounts.SelectedItems[0].SubItems[1].Text;
                var acc = accounts.FirstOrDefault(a => a.MaTaiKhoan == maTk);
                if (acc != null)
                {
                    if (!decimal.TryParse(txtSoTien.Text.Trim(), out decimal soTien) || soTien <= 0)
                    {
                        MessageBox.Show("SỐ TIỀN RÚT KHÔNG HỢP LỆ!");
                        return;
                    }

                    if (acc.SoTien < soTien)
                    {
                        MessageBox.Show("KHÔNG ĐỦ SỐ DƯ ĐỂ RÚT!");
                        return;
                    }

                    acc.SoTien -= soTien;
                    RefreshListView();
                    UpdateTongTien();
                }
            }
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedItems.Count > 0)
            {
                string maTk = dgvAccounts.SelectedItems[0].SubItems[1].Text;
                var acc = accounts.FirstOrDefault(a => a.MaTaiKhoan == maTk);
                if (acc != null)
                {
                    if (!decimal.TryParse(txtSoTien.Text.Trim(), out decimal soTien) || soTien <= 0)
                    {
                        MessageBox.Show("SỐ TIỀN NẠP KHÔNG HỢP LỆ!");
                        return;
                    }

                    acc.SoTien += soTien;
                    RefreshListView();
                    UpdateTongTien();
                }
            }
        }

        private void RefreshListView()
        {
            dgvAccounts.Items.Clear();
            int stt = 1;
            foreach (var acc in accounts)
            {
                acc.STT = stt++;
                var item = new ListViewItem(acc.STT.ToString());
                item.SubItems.Add(acc.MaTaiKhoan);
                item.SubItems.Add(acc.TenKhachHang);
                item.SubItems.Add(acc.DiaChi);
                item.SubItems.Add(acc.SoTien.ToString("N0"));
                dgvAccounts.Items.Add(item);
            }
        }

        private void UpdateTongTien()
        {
            decimal tong = accounts.Sum(a => a.SoTien);
            textBox1.Text = tong.ToString("N0");
        }

        private void ClearInputs()
        {
            txtSoTaiKhoan.Clear();
            txtTenKhachHang.Clear();
            txtDiaChi.Clear();
            txtSoTien.Clear();
        }
    }
}
