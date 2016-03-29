using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoviesAssessmentJane
{
    public partial class Form1 : Form
    {
        Database myDatabase = new Database();
      public Form1()
        {
            InitializeComponent();
            LoadDB();
        }
        public void LoadDB()
        { 
        DisplayDataGridViewCustomer();
            DisplayDataGridViewMovie();
        }

        private void DisplayDataGridViewCustomer()
        {
            dgvCustomer.DataSource = null;
            try
            {
                dgvCustomer.DataSource = myDatabase.FilldgvCustomerWithCustomer();
                dgvCustomer.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayDataGridViewMovie()
        {
            dgvMovies.DataSource = null;
            try
            {
                dgvMovies.DataSource = myDatabase.FilldgvMoviesWithMovies();
                dgvMovies.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CustomerID = 0;

            try
            {
                CustomerID = (int)dgvCustomer.Rows[e.RowIndex].Cells[0].Value;
                tbxFN.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbxLN.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbxAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbxPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (e.RowIndex >= 0)
                {
                    tbxCustID.Text = CustomerID.ToString();
                }
            }

            catch (Exception ex)
           {
            MessageBox.Show(ex.Message);
           }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxFN.Text != string.Empty)&& (tbxLN.Text != string.Empty) && (tbxAddress.Text != string.Empty) && (tbxPhone.Text != string.Empty ))
            {
                try
                {
                    result = myDatabase.InsertorUpdateCustomer(tbxFN.Text, tbxLN.Text, tbxAddress.Text, tbxPhone.Text, tbxCustID.Text,
                        "Add");
                    MessageBox.Show(tbxFN.Text + " Added." + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                DisplayDataGridViewCustomer();
                tbxFN.Text = "";
                tbxLN.Text = "";
                tbxAddress.Text = "";
                tbxPhone.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter first name, last name, address and phone.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes(this);
        }

        public void ClearAllTextBoxes(Control root)
        {
            foreach ( Control ctrl in root.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox) ctrl).Text = String.Empty;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdateCust_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxFN.Text != string.Empty) && (tbxLN.Text != string.Empty) && (tbxAddress.Text != string.Empty) && (tbxPhone.Text != string.Empty))
            {
                try
                {
                    result = myDatabase.InsertorUpdateCustomer(tbxFN.Text, tbxLN.Text, tbxAddress.Text, tbxPhone.Text, tbxCustID.Text,
                        "Update");
                    MessageBox.Show(tbxFN.Text + " Updated." + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                DisplayDataGridViewCustomer();
                tbxFN.Text = "";
                tbxLN.Text = "";
                tbxAddress.Text = "";
                tbxPhone.Text = "";
            }
            else
            {
                MessageBox.Show("Are these the changes you wish to make?");
            }
        }
    }


    }

