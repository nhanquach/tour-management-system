using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models.DAO;
using Models.DTO;
using Models.BUS;

namespace QuanLyTour
{
    public partial class Form_ThongKeDT : Form
    {
        List<QTTour> dsTK;
        public Form_ThongKeDT()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string ngaybd = dateTimePicker1.Text;
            string ngaykt = dateTimePicker2.Text;
            dsTK = new TourDAO().TKDT(DateTime.Parse(ngaybd), DateTime.Parse(ngaykt));
            int i = 1;
            foreach (var t in dsTK)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(t.TenTour);
                item.SubItems.Add(t.NgayDi.Value.ToString("dd/MM/yyyy"));
                item.SubItems.Add(t.NgayKT.Value.ToString("dd/MM/yyyy"));
                item.SubItems.Add(String.Format("{0:C0}", t.GiaTour));
                item.SubItems.Add(t.SoNguoi);
                item.SubItems.Add(String.Format("{0:C0}", t.TongTien));
                listView1.Items.Add(item);
                i++;
            }
            label5.Text = String.Format("{0:C0}",new TourDAO().TongTien(DateTime.Parse(ngaybd), DateTime.Parse(ngaykt)));
        }

        private void button31_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_start form = new Form_start();
            form.ShowDialog();
            
        }

        private void Form_ThongKeDT_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(200, 100);
        }
    }
}
