using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthwindForms
{
    // Displays Customers table from Northwinds database.
    // Windows Forms Application
    // DataGridView Control
    // https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview(v=vs.110).aspx
    // https://msdn.microsoft.com/en-us/library/e0ywh3cz(v=vs.110).aspx
    public partial class Form1 : Form
    {
        private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();

        public Form1()
        {
            InitializeComponent();

            // Set default properties for DataGridView control
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.Width = this.Width - 10;
            dataGridView1.Height = this.Height - 40;

            // Set event handler
            dataGridView1.CellDoubleClick += dataGridView_CellDoubleClick;

            this.Controls.Add(dataGridView1);
            this.Text = "DataGridView Demo";
        }

        // Get connection string from application settings file
        static private string GetConnectionString()
        {
            // To avoid storing the connection string in your code, 
            // you can retrieve it from a configuration file.
            //string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;";
            string connectionString = Properties.Settings.Default["NorthwindConnectionString"].ToString();
            return connectionString;
        }

        // Get the data from database and populate DataGridView
        internal void PopulateDataGridView()
        {
            try
            {
                // Retrieve connection string.
                string connectionString = GetConnectionString();

                // Create a DataSet.
                DataSet dataset = new DataSet();
                dataset.Locale = System.Globalization.CultureInfo.InvariantCulture;

                // Create a new data adapter based on the specified query.
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * from Customers", connectionString);

                // Retrieve the data from the data source using a SELECT statement.
                dataAdapter.Fill(dataset, "Customers");

                // Bind the data connector to the Customers table.
                bindingSource1.DataSource = dataset;
                bindingSource1.DataMember = "Customers";
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width - 10;
            dataGridView1.Height = this.Height - 40;
        }

        // Refresh DataGridView control
        internal void RefreshGrid()
        {
            PopulateDataGridView();
        }

        /// <summary>
        /// How to: Get the Selected Cells, Rows, and Columns in the Windows Forms DataGridView Control
        /// https://msdn.microsoft.com/en-us/library/x8x9zk5a(v=vs.110).aspx
        /// </summary>
        /// <param name="sender">The cell that was clicked</param>
        /// <param name="e"></param>
        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();

                //for (int i = 0; i < selectedRowCount; i++)
                //{
                //    sb.Append("Row: ");
                //    sb.Append(dataGridView.SelectedRows[i].Index.ToString());
                //    sb.Append(Environment.NewLine);
                //}

                //sb.Append("Total: " + selectedRowCount.ToString());
                //MessageBox.Show(sb.ToString(), "Selected Rows");

                string customerID = dataGridView1.SelectedCells[0].Value.ToString();

                Form3 form3 = new Form3();
                form3.GetCustomerData(this, customerID);
                form3.Show();
            }
        }

        /// <summary>
        /// Initialize Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Bind the DataGridView control to the BindingSource component 
            dataGridView1.DataSource = bindingSource1;

            // Load the data from the database
            PopulateDataGridView();

            // Resize the DataGridView columns to fit the newly loaded data.
            dataGridView1.AutoResizeColumns();
        }
    }
}
