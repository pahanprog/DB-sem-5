using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WpfApp1.Models
{
    class Car
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string CarBody { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public int Seats { get; set; }
        public string Drive { get; set; }
        public string EngineType { get; set; }
        public int TopSpeed { get; set; }
        public double Acceleration { get; set; }
        public BitmapImage Image { get; set; }

        public bool Changed { get; set; }

        public int DriveId { get; set; }
        public int BrandId { get; set; }
    }

    public static class CarData
    {

        public static Dictionary<int, string> GetBrands()
        {
            DataTable BrandTable = new DataTable();
            Dictionary<int, string> brands = new Dictionary<int, string>();

            string sqlQuery = @"SELECT Brand.Id,BrandName FROM Brand";
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(BrandTable);

                foreach (DataRow row in BrandTable.Rows)
                {
                    brands.Add((int)row["Id"], (string)row["BrandName"]);
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
            return brands;
        }

        public static Dictionary<int, string> GetDrives()
        {
            DataTable DriveTable = new DataTable();

            Dictionary<int, string> drives = new Dictionary<int, string>();
            string sqlQuery = @"SELECT * FROM Drive";
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(DriveTable);

                foreach (DataRow row in DriveTable.Rows)
                {
                    drives.Add((int)row["Id"], (string)row["Name"]);
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

            return drives;
        }

        public static Dictionary<int, string> GetEngineTypes()
        {
            DataTable EngineTypesTable = new DataTable();

            Dictionary<int, string> engineTypes = new Dictionary<int, string>();
            string sqlQuery = @" Select distinct EngineType FROM Model";
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(EngineTypesTable);

                foreach (DataRow row in EngineTypesTable.Rows)
                {
                    engineTypes.Add(engineTypes.Count, (string)row["EngineType"]);
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

            return engineTypes;

        }

        public static Dictionary<int, string> GetCarBodies()
        {
            DataTable CarBodiesTable = new DataTable();

            Dictionary<int, string> carBodies = new Dictionary<int, string>();
            string sqlQuery = @" Select distinct CarBody FROM Model";
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(CarBodiesTable);

                foreach (DataRow row in CarBodiesTable.Rows)
                {
                    carBodies.Add(carBodies.Count, (string)row["CarBody"]);
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

            return carBodies;

        }
    }
}
