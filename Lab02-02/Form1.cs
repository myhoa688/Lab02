using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbFaculty.Items.Clear();
            cmbFaculty.Items.AddRange(new object[] { "QTKD", "CNTT", "NNA" });
            cmbFaculty.SelectedIndex = 0;        // Khoa mặc định: QTKD
            optFemale.Checked = true;            // Giới tính mặc định: Nữ

            lblMaleCount.Text = "0";
            lblFemaleCount.Text = "0";

            dgvStudent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudent.AllowUserToAddRows = false;

            // Thêm cột cho DataGridView nếu chưa có
            if (dgvStudent.Columns.Count == 0)
            {
                dgvStudent.Columns.Add("StudentID", "MSSV");
                dgvStudent.Columns.Add("FullName", "Họ tên");
                dgvStudent.Columns.Add("Gender", "Giới tính");
                dgvStudent.Columns.Add("AverageScore", "Điểm TB");
                dgvStudent.Columns.Add("Faculty", "Khoa");
            }

            // Thêm danh sách sinh viên mẫu
            dgvStudent.Rows.Add("SV001", "Nguyễn Văn A", "Nam", "8.5", "CNTT");
            dgvStudent.Rows.Add("SV002", "Trần Thị B", "Nữ", "7.8", "QTKD");
            dgvStudent.Rows.Add("SV003", "Lê Văn C", "Nam", "6.9", "NNA");
            dgvStudent.Rows.Add("SV004", "Phạm Thị D", "Nữ", "9.1", "CNTT");
            dgvStudent.Rows.Add("SV005", "Hoàng Văn E", "Nam", "5.5", "QTKD");

            // Cập nhật lại số lượng Nam/Nữ
            UpdateGenderCount();
        }

        private int GetSelectedRow(string studentID)
        {
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (dgvStudent.Rows[i].Cells[0].Value.ToString() == studentID)
                {
                    return i;
                }
            }
            return -1;
        }

        private void InsertUpdate(int selectedRow)
        {
            dgvStudent.Rows[selectedRow].Cells[0].Value = txtStudentID.Text;
            dgvStudent.Rows[selectedRow].Cells[1].Value = txtFullName.Text;
            dgvStudent.Rows[selectedRow].Cells[2].Value = optFemale.Checked ? "Nữ" : "Nam";
            dgvStudent.Rows[selectedRow].Cells[3].Value = float.Parse(txtAverageScore.Text).ToString();
            dgvStudent.Rows[selectedRow].Cells[4].Value = cmbFaculty.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentID.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtAverageScore.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            int selectedRow = GetSelectedRow(txtStudentID.Text);
            if (selectedRow == -1)
            {
                dgvStudent.Rows.Add(txtStudentID.Text,
                                    txtFullName.Text,
                                    optFemale.Checked ? "Nữ" : "Nam",
                                    txtAverageScore.Text,
                                    cmbFaculty.Text);
                MessageBox.Show("Thêm mới dữ liệu thành công!");
            }
            else
            {
                InsertUpdate(selectedRow);
                MessageBox.Show("Cập nhật dữ liệu thành công!");
            }

            UpdateGenderCount();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedRow = GetSelectedRow(txtStudentID.Text);
            if (selectedRow == -1)
            {
                MessageBox.Show("Không tìm thấy MSSV cần xóa!");
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                dgvStudent.Rows.RemoveAt(selectedRow);
                MessageBox.Show("Xóa sinh viên thành công!");
                UpdateGenderCount();
            }
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];
                txtStudentID.Text = row.Cells[0].Value.ToString();
                txtFullName.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value.ToString() == "Nam")
                    optMale.Checked = true;
                else
                    optFemale.Checked = true;
                txtAverageScore.Text = row.Cells[3].Value.ToString();
                cmbFaculty.Text = row.Cells[4].Value.ToString();
            }
        }

        private void UpdateGenderCount()
        {
            int maleCount = 0, femaleCount = 0;
            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    if (row.Cells[2].Value.ToString() == "Nam") maleCount++;
                    else femaleCount++;
                }
            }
            lblMaleCount.Text = maleCount.ToString();
            lblFemaleCount.Text = femaleCount.ToString();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string genderFilter = optMale.Checked ? "Nam" : (optFemale.Checked ? "Nữ" : "");
            string facultyFilter = cmbFaculty.Text;
            float.TryParse(txtAverageScore.Text, out float scoreFilter);

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                bool matchGender = string.IsNullOrEmpty(genderFilter) || row.Cells[2].Value.ToString() == genderFilter;
                bool matchFaculty = string.IsNullOrEmpty(facultyFilter) || row.Cells[4].Value.ToString() == facultyFilter;
                bool matchScore = string.IsNullOrEmpty(txtAverageScore.Text) ||
                                  float.TryParse(row.Cells[3].Value.ToString(), out float score) && score >= scoreFilter;

                row.Visible = matchGender && matchFaculty && matchScore;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!");
                return;
            }

            bool found = false;
            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    string studentID = row.Cells[0].Value.ToString().ToLower();
                    string fullName = row.Cells[1].Value.ToString().ToLower();

                    bool isMatch = studentID.Contains(keyword) || fullName.Contains(keyword);
                    row.Visible = isMatch;

                    if (isMatch && !found)
                    {
                        row.Selected = true;
                        dgvStudent.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("Không tìm thấy sinh viên phù hợp!");
            }
        }

    }

}


