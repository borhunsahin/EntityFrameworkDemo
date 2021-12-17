using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace EntityFrameworkDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }
        private void LoadProducts()
        {
            Data_Grid_View_Products.DataSource = _productDal.GetAll();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = Add_Name_Text.Text,
                Price = Convert.ToDecimal(Add_Price_Text.Text),
                Stock = Convert.ToInt32(Add_Stock_Text.Text)
            });
            LoadProducts();
            MessageBox.Show("Added");
        }

        private void Update_Button_Click(object sender, EventArgs e)
        {
            _productDal.Update(new Product
            {
                Id = Convert.ToInt32(Data_Grid_View_Products.CurrentRow.Cells[0].Value),
                Name = Update_Name_Text.Text,
                Price = Convert.ToDecimal(Update_Price_Text.Text),
                Stock = Convert.ToInt32(Update_Stock_Text.Text)
            });
            LoadProducts();
            MessageBox.Show("Updated");
        }

        private void Data_Grid_View_Products_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Update_Name_Text.Text = Data_Grid_View_Products.CurrentRow.Cells[1].Value.ToString();
            Update_Price_Text.Text = Data_Grid_View_Products.CurrentRow.Cells[2].Value.ToString();
            Update_Stock_Text.Text = Data_Grid_View_Products.CurrentRow.Cells[3].Value.ToString();
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            _productDal.Delete(new Product
            {
                Id = Convert.ToInt32(Data_Grid_View_Products.CurrentRow.Cells[0].Value),
            });
            LoadProducts();
            MessageBox.Show("Deleted");
        }
        private void SearchProducts(string key)
        {
            var result = _productDal.GetByName(key);
            Data_Grid_View_Products.DataSource = result;
        }
        private void Search_Text_TextChanged(object sender, EventArgs e)
        {
            SearchProducts(Search_Text.Text);
        }
    }
}
