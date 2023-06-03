using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBOC222410103067
{
    public partial class MenuLaptop : Form
    {
        DatabaseHelpers database = new DatabaseHelpers();
        public string id;

        public String ambilid
        {
            get
            {
                return this.id;
            }
        }

        public MenuLaptop()
        {
            InitializeComponent();
        }

        private void MenuLaptop_Load(object sender, EventArgs e)
        {
            DataLaptop();
        }

        private void DataLaptop()
        {
            string sql = "select * from laptop order by id_laptop asc";
            dataGridView1.DataSource = database.getData(sql);
            dataGridView1.Columns["id_laptop"].HeaderText = "ID Laptop";
            dataGridView1.Columns["nama_laptop"].HeaderText = "Nama Laptop";
            dataGridView1.Columns["harga_laptop"].HeaderText = "Harga Laptop";
            dataGridView1.Columns["stok"].HeaderText = "Stok";
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                id = dataGridView1.Rows[hit.RowIndex].Cells["id_laptop"].Value.ToString();
                this.Close();
            }
        }
    }
}
