using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoviesAssessmentJane
{
    public class Database
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

        public string InsertorUpdateCustomer(string Firstname, string Lastname, string Address, string Phone,
            string CustID, string AddorUpdate)
        {
            try
            {
                if (AddorUpdate == "Add")
                {
                    //var myCommand =
                    //      new SqlCommand
                    // using (SqlConnection Con = new SqlConnection(connectionString))
                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "AddCustomer";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", Firstname);
                        cmd.Parameters.AddWithValue("@LastName", Lastname);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        //c Connection.Open();
                        //var myCommand =
                        //    new SqlCommand(
                        //        "Insert into Customer (FirstName, LastName, Address, Phone)" +
                        //        "Values (@FirstName, @LastName, @Address, @Phone)", Connection);

                        //myCommand.Parameters.AddWithValue("Firstname", Firstname);
                        //myCommand.Parameters.AddWithValue("Lastname", Lastname);
                        //myCommand.Parameters.AddWithValue("Address", Address);
                        //myCommand.Parameters.AddWithValue("Phone", Phone);

                        Connection.Open();
                        cmd.ExecuteNonQuery();
                        Connection.Close();
                    }
                }
                else if (AddorUpdate == "Update")
                {
                    var myCommand =
                        new SqlCommand(
                            "Update Customer set FirstName = @Firstname, LastName = @LastName, Address = @Address, Phone = @Phone where CustID = @CustID",
                            Connection);
                    myCommand.Parameters.AddWithValue("Firstname", Firstname);
                    myCommand.Parameters.AddWithValue("Lastname", Lastname);
                    myCommand.Parameters.AddWithValue("Address", Address);
                    myCommand.Parameters.AddWithValue("Phone", Phone);
                    myCommand.Parameters.AddWithValue("CustID", CustID);

                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();

                }
                return " successfuly.";

            }
            catch (Exception a)
            {
                Connection.Close();
                return " has failed with " + a;
            }
            return "";
        }

        public string DeleteCustomer(string CustID)
        {
            if (!object.ReferenceEquals(CustID, string.Empty))
            {
                var myCommand = new SqlCommand();
                myCommand = new SqlCommand("Delete from Customer where CustID = @CustID");

                myCommand.Connection = Connection;
                myCommand.Parameters.AddWithValue("CustID", CustID);

                Connection.Open();
                myCommand.ExecuteNonQuery();
                Connection.Close();
                return "Deletion successful.";
            }
            else
            {
                Connection.Close();
                return "Deletion failed.";
            }
        }


        public string InsertorUpdateMovie(string Rating, string Title, string Year, string Plot, string Genre,
            string MovieID, string AddorUpdate)
        {
            try
            {
                if (AddorUpdate == "Add")
                {
                    var myCommand =
                        new SqlCommand(
                            "Insert into Movies (Rating, Title, Year,  Plot, Genre)" +
                            "Values (@Rating, @Title, @Year, @Plot, @Genre)", Connection);

                    myCommand.Parameters.AddWithValue("Rating", Rating);
                    myCommand.Parameters.AddWithValue("Title", Title);
                    myCommand.Parameters.AddWithValue("Year", Year);
                    myCommand.Parameters.AddWithValue("Plot", Plot);
                    myCommand.Parameters.AddWithValue("Genre", Genre);

                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();
                }
                else if (AddorUpdate == "Update")
                {
                    var myCommand =
                        new SqlCommand(
                            "Update Movies set Rating = @Rating, Title = @Title, Year = @Year,  Plot = @Plot, Genre = @Genre where MovieID = @MovieID",
                            Connection);

                    myCommand.Parameters.AddWithValue("Rating", Rating);
                    myCommand.Parameters.AddWithValue("Title", Title);
                    myCommand.Parameters.AddWithValue("Year", Year);
                    myCommand.Parameters.AddWithValue("Plot", Plot);
                    myCommand.Parameters.AddWithValue("Genre", Genre);
                    myCommand.Parameters.AddWithValue("MovieID", MovieID);

                    Connection.Open();
                    myCommand.ExecuteNonQuery();
                    Connection.Close();

                }
                return " successfuly.";

            }
            catch (Exception b)
            {
                Connection.Close();
                return " has failed with " + b;
            }
            return "";
        }

        public string DeleteMovie(string MovieID)
        {
            if (!object.ReferenceEquals(MovieID, string.Empty))
            {
                var myCommand = new SqlCommand();
                myCommand = new SqlCommand("Delete from Movies where MovieID = @MovieID");

                myCommand.Connection = Connection;
                myCommand.Parameters.AddWithValue("MovieID", MovieID);

                Connection.Open();
                myCommand.ExecuteNonQuery();
                Connection.Close();
                return "Deletion successful.";
            }
            else
            {
                Connection.Close();
                return "Deletion failed.";
            }
        }

        public bool ConnectionUnitTest()
        {
            DataTable dt = new DataTable();
            try
            {
                string SQL = "select * from Customer";

                Connection.Open();

                da.Fill(dt);

                Connection.Close();
                return true;
            }
            catch (Exception)
            {
                Connection.Close();
                return false;
            }
        }

        public List<string> FillListViewwithMostPopularMovies()

        // try
        {
            var myCommand = new SqlCommand();
            myCommand = new SqlCommand ("select * from MaxMostPopular", Connection);
            // da = new SqlDataAdapter(SQL, Connection);
            //DataTable dt = new DataTable();
            List<string> newMaxMostPopular = new List<string>();
            Connection.Open();
            // da.Fill(dt);
            SqlDataReader reader = myCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {// ListViewItem item = new ListViewItem(new[]
                    newMaxMostPopular.Add(reader["Times"].ToString()+ " views   " +(reader["Title"].ToString()));
                   // newMaxMostPopular.Add(reader["Title"].ToString());
                }
            }
            reader.Close();
            Connection.Close();
            return newMaxMostPopular;
        }
    }
}
