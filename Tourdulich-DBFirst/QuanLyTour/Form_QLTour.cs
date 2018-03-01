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
    public partial class Form_QLTour : Form
    {
        
        List<Models.Tour> tour;
        List<Models.DiaDiem> diadiem;
        int ID=0;
        public Form_QLTour()
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
                item.SubItems.Add(String.Format("{0:C0}",t.GiaTour));
                listView_dstour.Items.Add(item);
            }
        }

        
        public void LayDSDiaDiem(int matour)
        {
            diadiem = new DiaDiemDAO().DSDiaDiem_Tour(matour);
            foreach (var dd in diadiem)
            {
                ListViewItem item = new ListViewItem(dd.MaDiaDiem.ToString());
                item.SubItems.Add(dd.TenDiaDiem);
                item.SubItems.Add(dd.ThongTinDD);
                item.SubItems.Add(dd.Tinh);
                item.SubItems.Add(dd.Nuoc);
                listView_dsdd.Items.Add(item);
            }
        }

        public void LoadComboBox()
        {
            diadiem = new DiaDiemDAO().DanhSachDiaDiem();
            comboBox_dd.DataSource = diadiem;
            comboBox_dd.DisplayMember = "TenDiaDiem";
            comboBox_dd.ValueMember = "MaDiaDiem";
        }
        private void Formchinh_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(100, 100);
            this.comboBox2.SelectedIndex = 0;
            tabControl2.Visible = false;
            this.ClientSize = new System.Drawing.Size(764, 521);
            listView_dstour.View = View.Details;
            textBox_matour.Hide();
            label_error_tentour.Visible = false;
            label_error_tttour.Visible = false;
            label_errorgiatour.Visible = false;
            label_errordd.Visible = false;
        }

        public static bool IsNumber(string value)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]*\.?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(value);
        }

        private bool TestInput()
        {
            if (textBox_tentour.Text == "")
            {
                errorProvider1.SetError(textBox_tentour, "Tên tour không được để trống!");
                label_error_tentour.Visible = true;
                label_error_tentour.Text = "Tên tour không được để trống!";

                return false;
            }
            else
            {
                errorProvider1.Clear();
                label_error_tentour.Visible = false;
            }
            if (textBox_thongtintour.Text == "")
            {
                errorProvider1.SetError(textBox_thongtintour, "Thông tin tour không được để trống!");
                label_error_tttour.Visible = true;
                label_error_tttour.Text = "Thông tin tour không được để trống!";
                return false;
            }
            else
            {
                errorProvider1.Clear();
                label_error_tttour.Visible = false;
            }
            if (textBox_giatour.Text == "")
            {
                errorProvider1.SetError(textBox_giatour, "Giá tour không được để trống!");
                label_errorgiatour.Visible = true;
                label_errorgiatour.Text = "Giá tour không được để trống!";
                return false;
            }
            else
            {
                string str = textBox_giatour.Text;
                if (str.IndexOf(" ₫") != -1) str = str.Remove(str.IndexOf(" ₫"), 2);
                else
                    if (str.IndexOf("₫") != -1) str = str.Remove(str.IndexOf("₫"), 1);

                if (IsNumber(str) == false)
                {
                    label_errorgiatour.Visible = true;
                    label_errorgiatour.Text = "Giá tour phải là số!";
                    return false;
                }
                else
                {
                    errorProvider1.Clear();
                    label_errorgiatour.Visible = false;
                }
            }
            if (listView_dd.Items.Count == 0)
            {
                errorProvider1.SetError(comboBox_dd, "Địa điểm không được để trống!");
                label_errordd.Visible = true;
                label_errordd.Text = "Địa điểm không được để trống!";
                return false;
            }
            else
            {
                errorProvider1.Clear();
                label_errordd.Visible = false;
            }
            return true;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(1102, 521);
            tabControl2.Visible = true;
            Exit.Visible = true;
            Save.Visible = false;
            Add1.Visible = true;
            textBox_tentour.Text = "";
            textBox_thongtintour.Text = "";
            textBox_giatour.Text = "";
            errorProvider1.Clear();
            label_error_tentour.Visible = false;
            label_error_tttour.Visible = false;
            label_errorgiatour.Visible = false;
            label_errordd.Visible = false;
            listView_dd.Items.Clear();
            LoadComboBox();
        }

        private void Add1_Click(object sender, EventArgs e)
        {
            if (TestInput() == true)
            {
                TourDTO t = new TourDTO();
                t.TenTour = textBox_tentour.Text;
                t.ThongTinTour = textBox_thongtintour.Text;
                string str = textBox_giatour.Text;
                        if (str.Contains("₫"))
                        {
                            while (str.IndexOf("₫") != -1)
                            {
                                str = str.Remove(str.IndexOf("₫"), 1);
                            }
                            
                        }
                        if (str.Contains(" "))
                        {
                            while (str.IndexOf(" ") != -1)
                            {
                                str = str.Remove(str.IndexOf(" "), 1);
                            }

                        }
                        
                            
                        if(str.Contains("."))
                        {
                            while (str.IndexOf(".") != -1)
                            {
                                str = str.Remove(str.IndexOf("."), 1);
                            }
                        }
                        Regex regex = new Regex(@"^\d+$");
                        if (!regex.IsMatch(str))
                            MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            t.GiaTour = int.Parse(str);
                            TourBUS tour = new TourBUS();
                            int kq = tour.themTour(t);
                            if (kq != 0)
                            {
                                MessageBox.Show("Thêm thành công!", "Thông báo");
                                ListViewItem item = new ListViewItem(new String[]{
                        textBox_matour.Text=kq.ToString(),
                        textBox_tentour.Text,
                        textBox_thongtintour.Text,
                        textBox_giatour.Text});
                                this.listView_dstour.Items.AddRange(new ListViewItem[]{
                        item 
                    });
                                foreach (ListViewItem item1 in listView_dd.Items)
                                {
                                    DiaDiemBUS dd = new DiaDiemBUS();
                                    int ma = dd.search_madd(item1.SubItems[0].Text);
                                    ChiTietTourDTO ct = new ChiTietTourDTO();
                                    ct.MaTour = kq;
                                    ct.MaDiaDiem = ma;
                                    ChiTietTourBUS chitiet = new ChiTietTourBUS();
                                    chitiet.themChiTiet(ct);
                                }
                                textBox_tentour.Text = "";
                                textBox_thongtintour.Text = "";
                                textBox_giatour.Text = "";
                                listView_dd.Items.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Dispose();
                            }
                        }
            }

        }

        private bool KiemTraGia(string gia1, string gia2)
        {
            Regex regex = new Regex(@"^\d+$");
            if (regex.IsMatch(gia1) && regex.IsMatch(gia2) && (int.Parse(gia1)<=int.Parse(gia2)))
                return true;
            return false;
        }

        private void Search_Click(object sender, EventArgs e)
        {
            //listView1.View = View.Details;
            listView_dstour.Items.Clear();
            if(ID==0)
                tour = new TourDAO().TimTour_Ten(textBox_search.Text);
            if (ID == 1)
                tour = new TourDAO().TimTour_Thongtin(textBox_search.Text);
            if (ID == 2)
            {
                if (!KiemTraGia(textBox1.Text, textBox2.Text))
                    MessageBox.Show("Vui lòng nhập lại giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    tour = new TourDAO().TimTour_Gia(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            }
            foreach (var t in tour)
            {
                ListViewItem item = new ListViewItem(t.MaTour.ToString());
                item.SubItems.Add(t.TenTour);
                item.SubItems.Add(t.ThongTinTour);
                item.SubItems.Add(String.Format("{0:C0}", t.GiaTour));
                listView_dstour.Items.Add(item);
            }
            //listView1.View = View.Details;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Exit.Visible = false;
            tabControl2.Visible = false;
            Add_t.Enabled = true;
            this.ClientSize = new System.Drawing.Size(764, 521);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView_dstour.Items)
            {
                if (item.Selected)
                {
                    listView_dsdd.Items.Clear();
                    LayDSDiaDiem(int.Parse(item.Text));
                    textBox_tentour.Text = item.SubItems[1].Text;
                    textBox_thongtintour.Text = item.SubItems[2].Text;
                    textBox_giatour.Text = item.SubItems[3].Text;
                    listView_dd.Items.Clear();
                    foreach (ListViewItem item1 in listView_dsdd.Items)
                    {
                        ListViewItem item2 = new ListViewItem(new String[]{
                        item1.SubItems[1].Text});
                        this.listView_dd.Items.AddRange(new ListViewItem[]{
                        item2});
                    }
                }
            }
            LoadComboBox();
            this.ClientSize = new System.Drawing.Size(1102, 521);
            tabControl2.Visible = true;
            Exit.Visible = true;
            Save.Visible = true;
            Add1.Visible = false;
        }

        private void listView_dstour_SelectedIndexChanged(object sender, EventArgs e)
        {
            Edit_Click(sender, e);
            LoadComboBox();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (TestInput() == true)
            {
                TourBUS tour = new TourBUS();
                TourDTO t = new TourDTO();
                foreach (ListViewItem item in listView_dstour.Items)
                {
                    if (item.Selected)
                    {
                        t.MaTour = int.Parse(item.SubItems[0].Text);
                        t.TenTour = textBox_tentour.Text;
                        t.ThongTinTour = textBox_thongtintour.Text;
                        string str = textBox_giatour.Text;
                        if (str.Contains("₫"))
                        {
                            while (str.IndexOf("₫") != -1)
                            {
                                str = str.Remove(str.IndexOf("₫"), 1);
                            }
                            
                        }
                        if (str.Contains(" "))
                        {
                            while (str.IndexOf(" ") != -1)
                            {
                                str = str.Remove(str.IndexOf(" "), 1);
                            }

                        }
                        
                            
                        if(str.Contains("."))
                        {
                            while (str.IndexOf(".") != -1)
                            {
                                str = str.Remove(str.IndexOf("."), 1);
                            }
                        }
                        Regex regex = new Regex(@"^\d+$");
                        if (!regex.IsMatch(str))
                            MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                        
                        
                        t.GiaTour = int.Parse(str);
                        if (tour.suaTour(t) == 1)
                        {
                            MessageBox.Show("Lưu thành công!", "Thông báo");
                            ChiTietTourBUS ctt = new ChiTietTourBUS();
                            ctt.xoaChiTiet(int.Parse(item.SubItems[0].Text));
                            foreach (ListViewItem item1 in listView_dd.Items)
                            {
                                DiaDiemBUS dd = new DiaDiemBUS();
                                int ma = dd.search_madd(item1.SubItems[0].Text);
                                ChiTietTourDTO ct = new ChiTietTourDTO();
                                ct.MaTour = int.Parse(item.SubItems[0].Text);
                                ct.MaDiaDiem = ma;
                                ChiTietTourBUS chitiet = new ChiTietTourBUS();
                                chitiet.themChiTiet(ct);
                            }
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

        private void button_add_dd_Click(object sender, EventArgs e)
        {
            
                ListViewItem item = new ListViewItem(new String[]{
                comboBox_dd.Text
                        });
                this.listView_dd.Items.AddRange(new ListViewItem[]{
                        item 
                    });
                comboBox_dd.SelectedValue = "";
            
        }

        private void button_delete_dd_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView_dd.Items)
            {
                if (item.Checked || item.Selected)
                {
                    item.Remove();
                }
            }
            
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_start form = new Form_start();
            form.ShowDialog();
            
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                ID = 0;
                textBox_search.Text = "";
                textBox_search.Visible = true;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label5.Visible = false;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                ID = 1;
                textBox_search.Text = "";
                textBox_search.Visible = true;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label5.Visible = false;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                ID = 2;
                textBox_search.Visible = false;
                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox1.Visible = true;
                textBox2.Visible = true;
                label5.Visible = true;
            }
        }
        ToolTip t1=new ToolTip();
        private void Refresh_MouseHover(object sender, EventArgs e)
        {
            t1.Show("Làm mới danh sách tour",button_refresh);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            listView_dstour.Items.Clear();
            LayDSTour();
        }
    }
}
