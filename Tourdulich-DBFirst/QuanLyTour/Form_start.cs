using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTour
{
    public partial class Form_start : Form
    {
        public Form_start()
        {
            InitializeComponent();
            label.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_start_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(100, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form_QLTour form = new Form_QLTour();
            form.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form_ThongKeDT form = new Form_ThongKeDT();
            form.ShowDialog();
        }
    }
}
