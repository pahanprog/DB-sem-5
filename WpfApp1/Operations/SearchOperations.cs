using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Operations
{
    class SearchOperations
    {
        SqlDataAdapter adapter;

        private string url = @"data source=.\;initial catalog=db_project;integrated security=true";

        public ObservableCollection<SearchHelper> getSearchHelper(string search)
        {
            string[] sql = search.Split(" OR ");
            string search1 = sql[sql.Length-1];
            string search2 = search.Replace("OR", "AND");
            string sqlQuery1 = $"SELECT 'CarName' as Type, Name as Helper, Id FROM Model WHERE CONTAINS(Name,'{search1}') UNION ALL SELECT 'BrandName' as Type, BrandName as Helper, Brand.Id FROM Model JOIN Brand ON (Brand.Id = BrandId) WHERE CONTAINS(BrandName, '{search1}') UNION ALL SELECT 'BodyName' as Type, CarBody as Helper,Id FROM Model WHERE CONTAINS(CarBody, '{search1}')";
            string sqlQuery2 = $"SELECT 'CarName' as Type, Name as Helper, Id FROM Model WHERE CONTAINS(Name,'{search2}') UNION ALL SELECT 'BrandName' as Type, BrandName as Helper, Brand.Id FROM Model JOIN Brand ON (Brand.Id = BrandId) WHERE CONTAINS(BrandName, '{search2}') UNION ALL SELECT 'BodyName' as Type, CarBody as Helper,Id FROM Model WHERE CONTAINS(CarBody, '{search2}')";
            string sqlQuery3 = $"SELECT 'CarName' as Type, Name as Helper, Id FROM Model WHERE CONTAINS(Name,'{search}') UNION ALL SELECT 'BrandName' as Type, BrandName as Helper, Brand.Id FROM Model JOIN Brand ON (Brand.Id = BrandId) WHERE CONTAINS(BrandName, '{search}') UNION ALL SELECT 'BodyName' as Type, CarBody as Helper,Id FROM Model WHERE CONTAINS(CarBody, '{search}')";
            DataTable HelperTable = new DataTable();
            SqlConnection connection = null;
            ObservableCollection<SearchHelper> HelperCollection = new ObservableCollection<SearchHelper>();

            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery1, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(HelperTable);

                foreach (DataRow row in HelperTable.Rows)
                {
                    SearchHelper helper = new SearchHelper()
                    {
                        Helper = (string)row["Helper"],
                        Id = (int)row["Id"],
                        Type = (string)row["Type"]
                    };

                    IEnumerable<SearchHelper> Doubles = HelperCollection.Where(x => x.Helper == helper.Helper);
                    if (Doubles.Count() == 0)
                    {
                        HelperCollection.Add(helper);
                    }
                }

                command = new SqlCommand(sqlQuery2, connection);
                adapter = new SqlDataAdapter(command);
                HelperTable = new DataTable();

                adapter.Fill(HelperTable);

                foreach (DataRow row in HelperTable.Rows)
                {
                    SearchHelper helper = new SearchHelper()
                    {
                        Helper = (string)row["Helper"]
                    };

                    IEnumerable<SearchHelper> Doubles = HelperCollection.Where(x => x.Helper == helper.Helper);
                    if (Doubles.Count() == 0)
                    {
                        HelperCollection.Add(helper);
                    }
                }

                command = new SqlCommand(sqlQuery3, connection);
                adapter = new SqlDataAdapter(command);
                HelperTable = new DataTable();

                adapter.Fill(HelperTable);

                foreach (DataRow row in HelperTable.Rows)
                {
                    SearchHelper helper = new SearchHelper()
                    {
                        Helper = (string)row["Helper"]
                    };

                    IEnumerable<SearchHelper> Doubles = HelperCollection.Where(x => x.Helper == helper.Helper);
                    if (Doubles.Count() == 0)
                    {
                        HelperCollection.Add(helper);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return HelperCollection;
        }
    }
}
