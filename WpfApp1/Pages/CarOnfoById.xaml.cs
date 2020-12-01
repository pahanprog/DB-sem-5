using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Interaction logic for CarOnfoById.xaml
    /// </summary>
    public partial class CarOnfoById : Page
    {
        string strName, imageName;

        Car car = new Car();

        public CarOnfoById(Object CarById)
        {
            InitializeComponent();
            car = CarById as Car;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Globals.LoggedInUser.Status == "Admin")
            {
                btnSave.Visibility = Visibility.Visible;
                btnFile.Visibility = Visibility.Visible;
            }

            LoadCarInfo();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            insertImageData();
        }

        // Choose File Button Click
        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fl = new OpenFileDialog();
                fl.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fl.ShowDialog();
                {
                    strName = fl.SafeFileName;
                    imageName = fl.FileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    image1.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
                }
                fl = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        // Insert Image Into Data Base By BiteMap
        private void insertImageData()
        {
            try
            {
                if (imageName != "")
                {
                    FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                    byte[] imgByteArr = new byte[fs.Length];
                    fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                    fs.Close();

                    using (SqlConnection conn = new SqlConnection(Globals.DBurl))
                    {
                        conn.Open();
                        string sql = $"UPDATE model SET Image = @img WHERE (model.Id = {car.Id})";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.Add(new SqlParameter("img", imgByteArr));
                            int result = cmd.ExecuteNonQuery();
                            if (result == 1)
                            {
                                MessageBox.Show("Image added successfully.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Load BiteMap Of Image In Data Base To MemoryStream
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void LoadCarInfo()
        {
            // clearing image value
            image1.SetValue(Image.SourceProperty, null);

            SqlConnection connection = null;

                try
                {

                    // running command to sql server
                    connection = new SqlConnection(Globals.DBurl);
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = $"SELECT model.Year,Brand.BrandName,model.Name,model.CarBody,model.price,model.Seats,Drive.Name AS Drive,model.EngineType,model.TopSpeed,model.Acceleration, model.Image, Brand.FoundationYear, Brand.CompanyValue, Country.Name from Model JOIN Brand ON model.BrandId= Brand.Id JOIN Drive ON model.DriveId = Drive.Id JOIN COUNTRY ON Brand.CountryId = Country.Id WHERE (Model.Id = {car.Id})";
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {

                    // setting text value from sql server
                        CarName.Text = $"{dr.GetInt32(0)} {dr.GetString(1)} {dr.GetString(2)} specs";
                        carInfoBodyType.Text = $"{dr.GetString(3)}";
                        carInfoFirstYear.Text = $"{dr.GetInt32(0)}";
                        string price = $"{dr.GetInt32(4)}";
                        int ln = price.Length;
                        price = price.Insert(ln - 3, ".");
                        price = price.Insert(price.Length, " €");
                        carInfoPrice.Text = $"{price}";
                        carInfoSeats.Text = $"{dr.GetInt32(5)}";
                        carInfoDrive.Text = $"{dr.GetString(6)}";
                        carInfoEngine.Text = $"{dr.GetString(7)} engine";
                        carInfoSpeed.Text = $"{dr.GetInt32(8)} km/h";
                        carInfoAcc.Text = $"{dr.GetDouble(9)} s";
                        brandInfoName.Text = $"{dr.GetString(1)}";
                        brandInfoFoundationYear.Text = $"{dr.GetInt32(11)}";
                        brandInfoValue.Text = $"{dr.GetInt64(12)}";
                        brandInfoCounty.Text = $"{dr.GetString(13)}";

                    // check if sql server has an image for this car and set image value in ui
                    if (dr.GetValue(10) as byte[] != null)
                        {
                            image1.SetValue(Image.SourceProperty, LoadImage(dr.GetValue(10) as byte[]));
                        }else
                        {
                            string packUri = "pack://application:,,,/WpfApp1;component/img/noImage.png";
                            image1.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
