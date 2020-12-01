using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Reflection;
using System.Threading;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Imaging;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        ////Sql Adapter
        //SqlDataAdapter adapter;

        ////Data Tables
        //DataTable modelTable, yearTable, brandTable;

        ////Connection String
        //string connectionstring = null;

        //// Data Set For Image Processing
        //DataSet ds;

        //// Strings For Image Show And Upload
        //string strName, imageName;

        // Main Window Initializasion
        public MainWindow()
        {
            InitializeComponent();
        }

        //// Save Button Clicked
        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    insertImageData();
        //}

        //// On Window Load
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    connectionstring = @"data source=.\mssqlserver01;initial catalog=db_project;integrated security=true";
        //    string sql = @"SELECT Brand.BrandName FROM Brand";
        //    brandTable = new DataTable();
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(connectionstring);
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        adapter = new SqlDataAdapter(command);

        //        connection.Open();
        //        adapter.Fill(brandTable);
        //        cmbBrands.ItemsSource = brandTable.DefaultView;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //            connection.Close();
        //    }
        //}

        //// Choose File Button Click
        //private void btnFile_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        FileDialog fl = new OpenFileDialog();
        //        fl.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
        //        fl.ShowDialog();
        //        {
        //            strName = fl.SafeFileName;
        //            imageName = fl.FileName;
        //            ImageSourceConverter isc = new ImageSourceConverter();
        //            image1.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
        //        }
        //        fl = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}

        //// Go Button Click (Car Search)
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    // clearing image value
        //    image1.SetValue(Image.SourceProperty, null);

        //    connectionstring = @"data source=.\mssqlserver01;initial catalog=db_project;integrated security=true";

        //    //check if all select boxes contain something
        //    if (cmbBrands.SelectedIndex != -1 && cmbModel.SelectedIndex != -1 && cmbYear.SelectedIndex != -1)
        //    {
        //        // get select boxes values
        //        object[] selectedModelObj = ((DataRowView)cmbModel.SelectedItem).Row.ItemArray;
        //        string selectedModel = selectedModelObj[0].ToString();
        //        object[] selectedBrandObj = ((DataRowView)cmbBrands.SelectedItem).Row.ItemArray;
        //        string selectedBrand = selectedBrandObj[0].ToString();
        //        object[] selectedYearObj = ((DataRowView)cmbYear.SelectedItem).Row.ItemArray;
        //        string selectedYear = selectedYearObj[0].ToString();

        //        SqlConnection connection = null;

        //        try
        //        {

        //            // running command to sql server
        //            connection = new SqlConnection(connectionstring);
        //            SqlCommand cmd = connection.CreateCommand();
        //            cmd.CommandText = $"SELECT model.Year,Brand.BrandName,model.Name,model.CarBody,model.price,model.Seats,Drive.Name AS Drive,model.EngineType,model.TopSpeed,model.Acceleration, model.Image from Model JOIN Brand ON model.BrandId= Brand.Id JOIN Drive ON model.DriveId = Drive.Id WHERE (model.Name = '{selectedModel}' AND model.Year = '{selectedYear}')";
        //            connection.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {

        //                // setting text value from sql server
        //                carInfoMainText.Text = $"{dr.GetInt32(0)} {dr.GetString(1)} {dr.GetString(2)} specs";
        //                carInfoBodyType.Text = $"{dr.GetString(3)}";
        //                carInfoFirstYear.Text = $"{dr.GetInt32(0)}";
        //                string price = $"{dr.GetInt32(4)}";
        //                int ln = price.Length;
        //                price = price.Insert(ln - 3, ".");
        //                price = price.Insert(price.Length, " €");
        //                carInfoPrice.Text = $"{price}";
        //                carInfoSeats.Text = $"{dr.GetInt32(5)}";
        //                carInfoDrive.Text = $"{dr.GetString(6)}";
        //                carInfoEngine.Text = $"{dr.GetString(7)} engine";
        //                carInfoSpeed.Text = $"{dr.GetInt32(8)} km/h";
        //                carInfoAcc.Text = $"{dr.GetDouble(9)} s";

        //                // check if sql server has an image for this car and set image value in ui
        //                if (dr.GetValue(10) as byte[] != null)
        //                {
        //                    image1.SetValue(Image.SourceProperty, LoadImage(dr.GetValue(10) as byte[]));
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

        //// Model Select Box Selected Changed
        //private void cmbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    cmbYear.ItemsSource = null;

        //    if (cmbModel.SelectedIndex == -1) return;

        //    connectionstring = @"data source=.\mssqlserver01;initial catalog=db_project;integrated security=true";
        //    object[] selectedModelObj = ((DataRowView)cmbModel.SelectedItem).Row.ItemArray;
        //    string selectedModel = selectedModelObj[0].ToString();
        //    string sql = $"SELECT model.Year FROM Model WHERE (model.Name = '{selectedModel}')";

        //    yearTable = new DataTable();
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(connectionstring);
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        adapter = new SqlDataAdapter(command);

        //        connection.Open();
        //        adapter.Fill(yearTable);
        //        cmbYear.ItemsSource = yearTable.DefaultView;
        //        cmbYear.SelectedIndex = 0;

        //        yearTable = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //// Brand Select Box Selected Changed
        //private void cmbBrands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    cmbModel.ItemsSource = null;

        //    if (cmbBrands.SelectedIndex == -1) return;

        //    connectionstring = @"data source=.\mssqlserver01;initial catalog=db_project;integrated security=true";
        //    object[] selectedBrandObj = ((DataRowView)cmbBrands.SelectedItem).Row.ItemArray;
        //    string selectedBrand = selectedBrandObj[0].ToString();
        //    string sql = $"SELECT DISTINCT model.Name AS Model FROM Brand JOIN Model ON (Brand.Id = Model.BrandId AND BrandName = '{selectedBrand}')";
        //    modelTable = new DataTable();
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(connectionstring);
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        adapter = new SqlDataAdapter(command);

        //        connection.Open();
        //        adapter.Fill(modelTable);
        //        cmbModel.ItemsSource = modelTable.DefaultView;
        //        cmbModel.SelectedIndex = 0;

        //        modelTable = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //// Insert Image Into Data Base By BiteMap
        //private void insertImageData()
        //{
        //    object[] selectedModelObj = ((DataRowView)cmbModel.SelectedItem).Row.ItemArray;
        //    string selectedModel = selectedModelObj[0].ToString();
        //    object[] selectedBrandObj = ((DataRowView)cmbBrands.SelectedItem).Row.ItemArray;
        //    string selectedBrand = selectedBrandObj[0].ToString();
        //    object[] selectedYearObj = ((DataRowView)cmbYear.SelectedItem).Row.ItemArray;
        //    string selectedYear = selectedYearObj[0].ToString();
        //    try
        //    {
        //        if (imageName != "")
        //        {
        //            FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
        //            byte[] imgByteArr = new byte[fs.Length];
        //            fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
        //            fs.Close();

        //            using (SqlConnection conn = new SqlConnection(connectionstring))
        //            {
        //                conn.Open();
        //                string sql = $"UPDATE model SET Image = @img WHERE (model.Name = '{selectedModel}' AND model.year = '{selectedYear}')";
        //                using (SqlCommand cmd = new SqlCommand(sql, conn))
        //                {
        //                    cmd.Parameters.Add(new SqlParameter("img", imgByteArr));
        //                    int result = cmd.ExecuteNonQuery();
        //                    if (result == 1)
        //                    {
        //                        MessageBox.Show("Image added successfully.");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //// Load BiteMap Of Image In Data Base To MemoryStream
        //private static BitmapImage LoadImage(byte[] imageData)
        //{
        //    if (imageData == null || imageData.Length == 0) return null;
        //    var image = new BitmapImage();
        //    using (var mem = new MemoryStream(imageData))
        //    {
        //        mem.Position = 0;
        //        image.BeginInit();
        //        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        //        image.CacheOption = BitmapCacheOption.OnLoad;
        //        image.UriSource = null;
        //        image.StreamSource = mem;
        //        image.EndInit();
        //    }
        //    image.Freeze();
        //    return image;
        //}
    }
}
