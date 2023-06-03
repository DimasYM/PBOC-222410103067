using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PBOC222410103067
{
    public partial class Form1 : Form
    {
        public string id;
        DatabaseHelpers datab = new DatabaseHelpers();
        private int CurrentIDLaptop;
        private Dictionary<int, int> CurrentCart = new Dictionary<int, int>() { };
        public Form1()
        {
            InitializeComponent();
            ShowDataLaptop();
        }

        private void ShowDataLaptop()
        {
            string sql = "select * from laptop order by id_laptop asc";
            dataGridView1.DataSource = datab.getData(sql);
            dataGridView1.Columns["id_laptop"].HeaderText = "ID Laptop";
            dataGridView1.Columns["nama_laptop"].HeaderText = "Nama Laptop";
            dataGridView1.Columns["harga_laptop"].HeaderText = "Harga Laptop";
            dataGridView1.Columns["stok"].HeaderText = "Stok";
            dataGridView1.Columns["Edit"].DisplayIndex = 5;
            dataGridView1.Columns["Delete"].DisplayIndex = 5;

            string sql1 = "select * from detail_transaksi";
            dataGridView2.DataSource = datab.getData(sql1);
            dataGridView2.Columns["id_detail_transaksi"].HeaderText = "ID Detail Transaksi";
            dataGridView2.Columns["id_transaksi"].HeaderText = "ID Transaksi";
            dataGridView2.Columns["id_laptop"].HeaderText = "ID Laptop";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (CurrentIDLaptop == 0)
            {
                string sql = $"INSERT INTO laptop(id_laptop, nama_laptop, harga_laptop, stok) VALUES ({textBox1.Text}, '{textBox2.Text}', {textBox3.Text}, {textBox4.Text});";
                datab.exc(sql);
                ShowDataLaptop();
                button3.PerformClick();

            }
            else
            {
                string sql = $"UPDATE laptop SET nama_laptop='{textBox2.Text}', harga_laptop={textBox3.Text}, stok={textBox4.Text} WHERE id_laptop = {CurrentIDLaptop}";
                datab.exc(sql);
                ShowDataLaptop();
                button3.PerformClick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CurrentIDLaptop = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
            {
                textBox1.Enabled = false;
                int a = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id_laptop"].Value.ToString());
                CurrentIDLaptop = a;
                textBox1.Text = CurrentIDLaptop.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["nama_laptop"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["harga_laptop"].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["stok"].Value.ToString();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                int id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id_laptop"].Value.ToString());
                string sql = $"DELETE FROM laptop WHERE id_laptop = {id};";
                datab.exc(sql);
                ShowDataLaptop();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                MenuLaptop form = new MenuLaptop();
                form.ShowDialog();
                id = form.ambilid;
                textBox5.Text = id.ToString();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (id != null)
            {
                string sql = $"select * from laptop where id_laptop = {id}";
                textBox6.Text = datab.getValue(sql, "nama_laptop");
                textBox7.Text = datab.getValue(sql, "harga_laptop");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO public.transaksi(id_transaksi, tanggal_transaksi) VALUES ({textBox12.Text}, {DateTime.Now.ToString("yyyy/MM/dd")}');";
            datab.exc(sql);
            DetailTransaksi();



        }
        private void DetailTransaksi()
        {
            foreach (DataGridViewRow rw in dataGridView3.Rows)
            {
                if (rw.Cells["id_laptop"].Value == null || rw.Cells["id_laptop"].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells["id_laptop"].Value.ToString()))
                {

                }
                else
                {
                    String idLaptop = rw.Cells["id_laptop"].Value.ToString();

                    String sqlInsertdetailBaru = $"Insert into detailbaru (id_transaksi, id_laptop) VALUES ({textBox12.Text},{idLaptop}) ";
                    datab.exc(sqlInsertdetailBaru);
                }
            }
        }

        private void HitungTotal()
        {
            int total = 0;
            if (dataGridView3.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    total += Convert.ToInt32(dataGridView3.Rows[i].Cells["total"].Value);
                }
            }
            textBox9.Text = Convert.ToDecimal(total).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(textBox7.Text);
            int b = Convert.ToInt32(textBox8.Text);
            int total = a * b;
            dataGridView3.Rows.Add(textBox5.Text, textBox6.Text, textBox8.Text, total);
            HitungTotal();
            Clear();
        }
        private void Clear()
        {
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {


            if (int.TryParse(textBox10.Text, out int bayar) && int.TryParse(textBox9.Text, out int total))
            {

                int kembalian = bayar - total;
                textBox11.Text = kembalian.ToString();
            }
            else
            {

            }

        }

    }
}