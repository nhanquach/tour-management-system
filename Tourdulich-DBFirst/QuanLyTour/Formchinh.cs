using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Models.DAO;
using Models.DTO;
using Models.BUS;
namespace QuanLyTour
{
    public partial class Formchinh : Form
    {
        List<Models.Tour> tour;
        public Formchinh()
        {
            InitializeComponent();
        }
        public void LayDSTour()
        {
            tour = new TourDAO().DanhSachTour();
            foreach (var t in tour)
            {
                ListViewItem item = new ListViewItem(t.MaTour.ToString());
                item.SubItems.Add(t.TenTour);
                item.SubItems.Add(t.ThongTinTour);
                item.SubItems.Add(t.GiaTour.ToString());
                listView1.Items.Add(item);
            }
            listView1.CheckBoxes = true;
        }

        private void Formchinh_Load(object sender, EventArgs e)
        {
            tabControl2.Visible = false;
            this.ClientSize = new System.Drawing.Size(493, 271);
            listView1.View = View.Details;
            LayDSTour();
            //listView1.Sorting = SortOrder.Ascending;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(746, 268);
            tabControl2.Visible = true;
            Exit.Visible = true;
            Save.Visible = false;
            Add1.Visible = true;
            //textBox_matour.Enabled = false;
            //Add.Enabled = false;
            int n = listView1.Items.Count;
            //textBox_matour.Text = listView1.Items[n].SubItems[0].Text;
            textBox_matour.Text = "";
            textBox_tentour.Text = "";
            textBox_thongtintour.Text = "";
            textBox_giatour.Text = "";
        }

        private void Add1_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (textBox_matour.Text == "")
            {
                k = 1;
                errorProvider1.SetError(textBox_matour, "Mã tour không được để trống!");
            }
            if (textBox_tentour.Text == "")
            {
                k = 1;
                errorProvider1.SetError(textBox_tentour, "Tên tour không được để trống!");
            }
            if (textBox_thongtintour.Text == "")
            {
                k = 1;
                errorProvider1.SetError(textBox_thongtintour, "Thông tin tour không được để trống!");
            }
            if (textBox_giatour.Text == "")
            {
                k = 1;
                errorProvider1.SetError(textBox_giatour, "Giá tour không được để trống!");
            }
            if (k == 0)
            {
                
                TourDTO t = new TourDTO();
                t.MaTour = int.Parse(textBox_matour.Text);
                t.TenTour = textBox_tentour.Text;
                t.ThongTinTour = textBox_thongtintour.Text;
                t.GiaTour = int.Parse(textBox_giatour.Text);
                
                TourBUS tour = new TourBUS();
                int kq = tour.themTour(t);
                if (kq == t.MaTour)
                    MessageBox.Show("Mã tour đã tồn tại.", "Thông báo");
                else
                    if (kq == 1)
                    {
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                        ListViewItem item = new ListViewItem(new String[]{
                        textBox_matour.Text,
                        textBox_tentour.Text,
                        textBox_thongtintour.Text,
                        textBox_giatour.Text});
                        this.listView1.Items.AddRange(new ListViewItem[]{
                        item 
                    });
                        textBox_matour.Text = "";
                        textBox_tentour.Text = "";
                        textBox_thongtintour.Text = "";
                        textBox_giatour.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Dispose();

                    }
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            TourBUS tour = new TourBUS();
            foreach(ListViewItem item in listView1.Items)
            {
                if (item.Checked || item.Selected)
                {
                    tour.xoaTour(int.Parse(item.SubItems[0].Text));
                    item.Remove();
                }
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            int n = listView1.Items.Count;
            MessageBox.Show(n.ToString(), "thông báo");
            textBox_matour.Text = listView1.Items[n].SubItems[0].Text;
            
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Exit.Visible = false;
            tabControl2.Visible = false;
            Add.Enabled = true;
            this.ClientSize = new System.Drawing.Size(480, 268);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    textBox_matour.Text = item.Text;
                    textBox_tentour.Text = item.SubItems[1].Text;
                    textBox_thongtintour.Text = item.SubItems[2].Text;
                    textBox_giatour.Text = item.SubItems[3].Text;
                }
            }
            this.ClientSize = new System.Drawing.Size(746, 268);
            tabControl2.Visible = true;
            Exit.Visible = true;
            Save.Visible = true;
            Add1.Visible = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Edit_Click(sender, e);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (textBox_matour.Text == "")
            {
                k = 1;
                errorProvider1.SetError(textBox_matour, "Mã tour không được để trống!");
            }
            if (textBox_tentour.Text == "")
            {
                k = 1;
                errorProvider1.SetError(textBox_tentour, "Tên tour không được để trống!");
            }
            if (textBox_thongtintour.Text == "")
            {
                k = 1;
                errorProvider1.SetError(textBox_thongtintour, "Thông tin tour không được để trống!");
            }
            if (textBox_giatour.Text == "")
            {
                k = 1;
                errorProvider1.SetError(textBox_giatour, "Giá tour không được để trống!");
            }
            if (k == 0)
            {
                TourBUS tour = new TourBUS();
                TourDTO t = new TourDTO();
                t.MaTour = int.Parse(textBox_matour.Text);
                t.TenTour = textBox_tentour.Text;
                t.ThongTinTour = textBox_thongtintour.Text;
                t.GiaTour = int.Parse(textBox_giatour.Text);
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Selected)
                    {
                        if (tour.suaTour(t) == 1)
                        {
                            MessageBox.Show("Lưu thành công!", "Thông báo");
                            item.Text = textBox_matour.Text;
                            item.SubItems[1].Text = textBox_tentour.Text;
                            item.SubItems[2].Text = textBox_thongtintour.Text;
                            item.SubItems[3].Text = textBox_giatour.Text;
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Dispose();

                        }
                    }
                }
            }
        }

        



    }
}
