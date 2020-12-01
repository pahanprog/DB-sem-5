using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Threading;
using System.Windows;

namespace WpfApp1.Models
{
    class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int FoundationYear { get; set; }
        public Int64 CompanyValue { get; set; }
        public int CountryId { get; set; }

        public bool Changed { get; set; }

        public string Country { get; set; }
    }

    public static class BrandData
    {
        public static Dictionary<int, string> GetCountries()
        {
            DataTable CountyTable = new DataTable();
            string url = @"data source=.\;initial catalog=db_project;integrated security=true";

            Dictionary<int, string> countries = new Dictionary<int, string>();
            string sqlQuery = @"SELECT * FROM Country";
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(CountyTable);

                foreach (DataRow row in CountyTable.Rows)
                {
                    countries.Add((int)row["Id"], (string)row["Name"]);
                }
            }
            catch (Exception ex)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => MessageBox.Show(ex.Message)));
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return countries;
        }
    }
}
