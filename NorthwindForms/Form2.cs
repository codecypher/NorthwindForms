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
    // Master/Detail Form using DataGridView control
    // https://msdn.microsoft.com/en-us/library/c12c1kx4(v=vs.110).aspx
    // https://msdn.microsoft.com/en-us/library/e0ywh3cz(v=vs.110).aspx
    public partial class Form2 : Form
    {
        private DataGridView masterDataGridView = new DataGridView();
        private DataGridView detailDataGridView = new DataGridView();
        private BindingSource masterBindingSource = new BindingSource();
        private BindingSource detailBindingSource = new BindingSource();
        private DataSet dataset = new DataSet();

        public Form2()
        {
            InitializeComponent();

            // Set default properties for DataGridView controls
            masterDataGridView.Dock = DockStyle.Fill;
            detailDataGridView.Dock = DockStyle.Fill;
            masterDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            detailDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            masterDataGridView.ReadOnly = true;
            detailDataGridView.ReadOnly = true;

            // Create horizontal SplitContainer and add the DataGridView controls
            SplitContainer splitContainer1 = new SplitContainer();
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Orientation = Orientation.Horizontal;
            splitContainer1.Panel1.Controls.Add(masterDataGridView);
            splitContainer1.Panel2.Controls.Add(detailDataGridView);

            // Add SplitContainer to controls collection of form
            this.Controls.Add(splitContainer1);

            this.Text = "DataGridView master/detail demo";
        }

        static private string GetConnectionString()
        {
            // To avoid storing the connection string in your code, 
            // you can retrieve it from a configuration file.
            //string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;";
            string connectionString = Properties.Settings.Default["NorthwindConnectionString"].ToString();
            return connectionString;
        }

        private void PopulateDataGridView()
        {
            try
            {
                // Setup database connection
                string connectionString = GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);

                // Create a DataSet.
                //DataSet dataset = new DataSet();
                dataset.Locale = System.Globalization.CultureInfo.InvariantCulture;

                // Add data from the Customers table to the DataSet.
                SqlDataAdapter masterDataAdapter = new SqlDataAdapter("select * from Customers", connection);
                masterDataAdapter.Fill(dataset, "Customers");

                // Add data from the Orders table to the DataSet.
                SqlDataAdapter detailsDataAdapter = new SqlDataAdapter("select * from Orders", connection);
                detailsDataAdapter.Fill(dataset, "Orders");

                // Establish a relationship between the two tables.
                DataColumn parentColumn = dataset.Tables["Customers"].Columns["CustomerID"];
                DataColumn childColumn = dataset.Tables["Orders"].Columns["CustomerID"];
                DataRelation relation = new DataRelation("CustomersOrders", parentColumn, childColumn);
                dataset.Relations.Add(relation);

                // Bind the master data connector to the Customers table.
                masterBindingSource.DataSource = dataset;
                masterBindingSource.DataMember = "Customers";

                // Bind the details data connector to the master data connector,
                // using the DataRelation name to filter the information in the 
                // details table based on the current row in the master table. 
                detailBindingSource.DataSource = masterBindingSource;
                detailBindingSource.DataMember = "CustomersOrders";
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Bind the DataGridView controls to the BindingSource
            // components 
            masterDataGridView.DataSource = masterBindingSource;
            detailDataGridView.DataSource = detailBindingSource;

            // Load the data from the database
            PopulateDataGridView();

            // Resize the master DataGridView columns to fit the newly loaded data.
            masterDataGridView.AutoResizeColumns();

            // Configure the details DataGridView so that its columns automatically
            // adjust their widths when the data changes.
            detailDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
