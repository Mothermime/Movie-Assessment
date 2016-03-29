using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAssessmentJane
{
    internal class Database
    {
        private SqlConnection Connection = new SqlConnection();
        private SqlCommand Command = new SqlCommand();
        private SqlDataAdapter da = new SqlDataAdapter();

        public Database()
        {
            string connectionString =  
                @"Data Source= LAPTOP\sqlexpress;Initial Catalog=VBMoviesFullData;Integrated Security=True";
            Connection.ConnectionString = connectionString;
            Command.Connection = Connection;
        }

        public DataTable FilldgvCustomerWithCustomer()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from Customer", Connection))
            {
                Connection.Open();
                da.Fill(dt);
                Connection.Close();
            }
            return dt;
        }

        public DataTable FilldgvMoviesWithMovies()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from Movies", Connection))
            {
                Connection.Open();
                da.Fill(dt);
                Connection.Close();
            }
            return dt;
    }
        public string InsertorUpdateCustomer(string Firstname, string Lastname, string Address, string Phone, string CustID, string AddorUpdate )
        {
            try
            {
                if (AddorUpdate == "Add")
                {
                    var myCommand = new SqlCommand("Insert into Customer (FirstName, LastName, Address, Phone)" +"Values (@FirstName, @LastName, @Address, @Phone)", Connection);

                    myCommand.Parameters.AddWithValue("Firstname", Firstname);
                    myCommand.Parameters.AddWithValue("Lastname", Lastname);
                    myCommand.Parameters.AddWithValue("Address", Address);
                    myCommand.Parameters.AddWithValue("Phone", Phone);

                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();
                }
                else if (AddorUpdate == "Update")
                {
                    var myCommand = new SqlCommand("Update Owner set FirstName = @Firstname, LastName = @LastName, Address = @Address, Phone = @Phone where CustID = @CustID", Connection);
                    myCommand.Parameters.AddWithValue("Firstname", Firstname);
                    myCommand.Parameters.AddWithValue("Lastname", Lastname);
                    myCommand.Parameters.AddWithValue("Address", Address);
                    myCommand.Parameters.AddWithValue("Phone", Phone);

                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();
                }
               
            }
            catch (Exception a)
            {
                
               Connection.Close();
                return " has failed with " + a;
            }
            return "";
        }

       
    }
}
