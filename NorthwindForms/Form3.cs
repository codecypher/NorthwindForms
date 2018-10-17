using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthwindForms
{
    // Display/Edit Customer Record
    //
    // If you create a new DataSet object (Add New Data Source Wizard) in your project, 
    // you can then go to the Design view and highlight the Customers table in the DataSet, 
    // select Details, then drag that object to the Form and automatically a typed 
    // DataSet, BindingSource, TableAdapter, and TableAdapterManager objects 
    // will appear on the lower part of the Form design window. 
    // If you choose DataGridView then a BindingNavigator object is also created.
    //
    // Save data back to the database
    // https://msdn.microsoft.com/en-us/library/y2ad8t9c.aspx
    //
    // Pass data between forms
    // https://msdn.microsoft.com/en-us/library/ms171925.aspx
    public partial class Form3 : Form
    {
        private Form1 form1;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }

        // Load form with customer record for given customerID
        internal void GetCustomerData(Form1 sender, string customerID)
        {
            form1 = sender;
            customersTableAdapter.FillByCustomerID(northwindDataSet.Customers, customerID);
        }

        // Handle a concurrency exception
        // https://msdn.microsoft.com/en-us/library/ms171936.aspx
        private void UpdateDatabase()
        {
            try
            {
                this.Validate();
                customersBindingSource.EndEdit();
                customersTableAdapter.Update(this.northwindDataSet.Customers);
                MessageBox.Show("Update successful");
            }
            catch (DBConcurrencyException dbcx)
            {
                DialogResult response = MessageBox.Show(
                    CreateMessage((NorthwindDataSet.CustomersRow) (dbcx.Row)), 
                    "Concurrency Exception", MessageBoxButtons.YesNo);
                ProcessDialogResult(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error was thrown while attempting to update the database. " + ex.Message);
            }
        }

        // Generate message to be displayed to user
        private string CreateMessage(NorthwindDataSet.CustomersRow cr)
        {
            return
                "Database: " + GetRowData(GetCurrentRowInDB(cr), DataRowVersion.Default) + "\n" +
                "Original: " + GetRowData(cr, DataRowVersion.Original) + "\n" +
                "Proposed: " + GetRowData(cr, DataRowVersion.Current) + "\n" +
                "Do you still want to update the database with the proposed value?";
        }


        //
        // This method loads a temporary table with current records from the database
        // and returns the current values from the row that caused the exception.
        //
        private NorthwindDataSet.CustomersDataTable tempCustomersDataTable = new NorthwindDataSet.CustomersDataTable();

        private NorthwindDataSet.CustomersRow GetCurrentRowInDB(NorthwindDataSet.CustomersRow RowWithError)
        {
            // Load data from the Customers table to tempCustomersDataTable.
            customersTableAdapter.Fill(tempCustomersDataTable);

            // Get the row for the given customerID
            NorthwindDataSet.CustomersRow currentRowInDb =
                tempCustomersDataTable.FindByCustomerID(RowWithError.CustomerID);

            return currentRowInDb;
        }


        // This method takes a CustomersRow and RowVersion and returns
        // a string of column values to display to the user.
        private string GetRowData(NorthwindDataSet.CustomersRow custRow, DataRowVersion rowVersion)
        {
            string rowData = "";

            for (int i = 0; i < custRow.ItemArray.Length; i++)
            {
                rowData += custRow[i, rowVersion].ToString() + " ";
            }
            return rowData;
        }

        // This method takes the DialogResult selected by the user and updates the database 
        // with the new values or cancels the update and resets the Customers table 
        // (in the dataset) with the values currently in the database.
        private void ProcessDialogResult(DialogResult response)
        {
            switch (response)
            {
                // discard changes made to database and save values entered by user to database
                case DialogResult.Yes:
                    northwindDataSet.Merge(tempCustomersDataTable, true, MissingSchemaAction.Ignore);
                    UpdateDatabase();
                    break;
                // discard changes made by user and display current values in database
                case DialogResult.No:
                    northwindDataSet.Merge(tempCustomersDataTable);
                    MessageBox.Show("Update cancelled");
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
            if (form1 != null) form1.RefreshGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
