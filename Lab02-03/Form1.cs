using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RapChieuPhim
{
    public partial class FrmBanVe : Form
    {
        private const int SoGhe = 20;
        private const int GiaVe = 70000; // VND mỗi ghế
        private readonly HashSet<int> gheDaBan = new HashSet<int>(); // lưu ghế đã bán
        private readonly HashSet<int> gheDangChon = new HashSet<int>(); // ghế đang chọn tạm thời

        private FlowLayoutPanel pnlGhe;
        private Label lblManAnh;
        private Label lblThanhTien;
        private TextBox txtThanhTien;
        private Button btnChon;
        private Button btnHuyBo;
        private Button btnKetThuc;

        public FrmBanVe()
        {
            InitializeComponent();
            KhoiTaoUI();
            TaoLuoiGhe();
            CapNhatThanhTien();
        }

        private void KhoiTaoUI()
        {
            Text = "BÁN VÉ RẠP CHIẾU PHIM";
            Size = new Size(600, 500);
            StartPosition = FormStartPosition.CenterScreen;

            lblManAnh = new Label
            {
                Text = "MÀN ẢNH",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.Silver,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            Controls.Add(lblManAnh);

            pnlGhe = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 300,
                Padding = new Padding(20),
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };
            Controls.Add(pnlGhe);

            var panelBottom = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };
            Controls.Add(panelBottom);

            lblThanhTien = new Label
            {
                Text = "Thành Tiền:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            panelBottom.Controls.Add(lblThanhTien);

            txtThanhTien = new TextBox
            {
                Location = new Point(120, 16),
                Width = 150,
                ReadOnly = true
            };
            panelBottom.Controls.Add(txtThanhTien);

            btnChon = new Button
            {
                Text = "Chọn",
                Location = new Point(20, 60),
                Width = 100
            };
            btnChon.Click += BtnChon_Click;
            panelBottom.Controls.Add(btnChon);

            btnHuyBo = new Button
            {
                Text = "Hủy bỏ",
                Location = new Point(130, 60),
                Width = 100
            };
            btnHuyBo.Click += BtnHuyBo_Click;
            panelBottom.Controls.Add(btnHuyBo);

            btnKetThuc = new Button
            {
                Text = "Kết thúc",
                Location = new Point(240, 60),
                Width = 100
            };
            btnKetThuc.Click += (s, e) => Close();
            panelBottom.Controls.Add(btnKetThuc);
        }

        private void TaoLuoiGhe()
        {
            pnlGhe.Controls.Clear();

            for (int i = 1; i <= SoGhe; i++)
            {
                var btn = new Button
                {
                    Text = i.ToString(),
                    Name = $"btnGhe_{i}",
                    Width = 60,
                    Height = 40,
                    Margin = new Padding(8)
                };

                // trạng thái ban đầu
                DatTrangThaiGhe(btn, i);

                // sự kiện click
                btn.Click += (s, e) => XuLyClickGhe((Button)s, i);

                pnlGhe.Controls.Add(btn);
            }
        }

        private void DatTrangThaiGhe(Button btn, int soGhe)
        {
            if (gheDaBan.Contains(soGhe))
            {
                btn.BackColor = Color.Crimson;
                btn.ForeColor = Color.White;
                btn.Enabled = false;
                btn.Tag = "DA_BAN";
            }
            else if (gheDangChon.Contains(soGhe))
            {
                btn.BackColor = Color.DodgerBlue;
                btn.ForeColor = Color.White;
                btn.Enabled = true;
                btn.Tag = "DANG_CHON";
            }
            else
            {
                btn.BackColor = Color.LightGray;
                btn.ForeColor = Color.Black;
                btn.Enabled = true;
                btn.Tag = "TRONG";
            }
        }

        private void XuLyClickGhe(Button btn, int soGhe)
        {
            // toggle chọn/bỏ chọn
            if (gheDangChon.Contains(soGhe))
                gheDangChon.Remove(soGhe);
            else
                gheDangChon.Add(soGhe);

            DatTrangThaiGhe(btn, soGhe);
            CapNhatThanhTien();
        }

        private void CapNhatThanhTien()
        {
            int soVe = gheDangChon.Count;
            long thanhTien = soVe * GiaVe;
            txtThanhTien.Text = thanhTien.ToString("N0"); // format 1.000.000
        }

        private void BtnChon_Click(object sender, EventArgs e)
        {
            if (gheDangChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 ghế.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Xác nhận bán
            var confirm = MessageBox.Show(
                $"Xác nhận mua {gheDangChon.Count} vé?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // chuyển ghế đang chọn thành đã bán
                foreach (int g in gheDangChon.ToList())
                    gheDaBan.Add(g);

                gheDangChon.Clear();

                // cập nhật giao diện tất cả ghế
                foreach (Control c in pnlGhe.Controls)
                {
                    if (c is Button b)
                    {
                        int soGhe = int.Parse(b.Text);
                        DatTrangThaiGhe(b, soGhe);
                    }
                }

                CapNhatThanhTien();
                // In hoá đơn đơn giản (tuỳ chọn)
                MessageBox.Show("Thanh toán thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnHuyBo_Click(object sender, EventArgs e)
        {
            gheDangChon.Clear();
            foreach (Control c in pnlGhe.Controls)
            {
                if (c is Button b)
                {
                    int soGhe = int.Parse(b.Text);
                    DatTrangThaiGhe(b, soGhe);
                }
            }
            CapNhatThanhTien();
        }
    }
}
