using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using WpfApp1.Models;
using System.Windows.Media.Imaging;
using System.IO;

namespace WpfApp1.Operations
{
    class AdminMenuOperations
    {
        SqlDataAdapter adapter;

        public ObservableCollection<Country> initializeCountryData()
        {
            string sqlQuery = @"SELECT * FROM Country";
            DataTable CountryTable = new DataTable();
            SqlConnection connection = null;
            ObservableCollection<Country> CountryCollection = new ObservableCollection<Country>();

            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(CountryTable);

                foreach (DataRow row in CountryTable.Rows)
                {
                    Country county = new Country()
                    {
                        Id = (int)row["Id"],
                        Name = (string)row["Name"]
                    };
                    CountryCollection.Add(county);
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

            this.initializeDriveData();
            return CountryCollection;
        }

        public ObservableCollection<Brand> initializeBrandData()
        {
            string sqlQuery = @"SELECT Brand.Id,BrandName,FoundationYear,CompanyValue, Name as Country, CountryId FROM Brand JOIN Country ON (CountryId = Country.Id)";
            DataTable BrandTable = new DataTable();
            SqlConnection connection = null;
            ObservableCollection<Brand> BrandCollection = new ObservableCollection<Brand>();

            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(BrandTable);

                foreach (DataRow row in BrandTable.Rows)
                {
                    Brand brand = new Brand()
                    {
                        BrandName = (string)row["BrandName"],
                        CompanyValue = (Int64)row["CompanyValue"],
                        CountryId = (int)row["CountryId"],
                        FoundationYear = (int)row["FoundationYear"],
                        Id = (int)row["Id"],
                        Country = (string)row["Country"]
                    };
                    BrandCollection.Add(brand);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            this.initializeDriveData();
            return BrandCollection;
        }

        public void initializeDriveData()
        {
            string sqlQuery = @"SELECT * FROM Drive";
            DataTable DriveTable = new DataTable();
            SqlConnection connection = null;
            ObservableCollection<Drive> DriveCollection = new ObservableCollection<Drive>();

            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(DriveTable);

                foreach (DataRow row in DriveTable.Rows)
                {
                    Drive drive = new Drive()
                    {
                        Id = (int)row["Id"],
                        Name = (string)row["Name"],
                    };
                    DriveCollection.Add(drive);
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
        }

        public ObservableCollection<User> initializeUserData()
        {
            string sqlQuery = @"SELECT Users.Id,Username, Name as Status, UserStatus as StatusId ,Blocked FROM Users JOIN UserRole ON (Users.UserStatus = UserRole.Id) ORDER BY Status";
            DataTable UserTable = new DataTable();
            SqlConnection connection = null;
            ObservableCollection<User> UserCollection = new ObservableCollection<User>();

            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(UserTable);

                foreach (DataRow row in UserTable.Rows)
                {
                    User user = new User()
                    {
                        Id = (int)row["Id"],
                        Username = (string)row["Username"],
                        Status = (string)row["Status"],
                        Blocked = (bool)row["Blocked"],
                        StatusId = (int)row["StatusId"] - 1
                    };
                    UserCollection.Add(user);
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

            return UserCollection;
        }

        public ObservableCollection<Car> initializeCarData()
        {
            string sqlQuery = @"SELECT DriveId, Model.Id,Model.Name as Model,CarBody,Year,BrandName, Seats, Acceleration,TopSpeed,EngineType,price,Drive.Name as Drive,BrandId, Image FROM Model JOIN Brand ON (Brand.Id = BrandId) JOIN Drive ON (DriveId = Drive.Id)";
            DataTable CarTable = new DataTable();
            SqlConnection connection = null;
            ObservableCollection<Car> CarCollection = new ObservableCollection<Car>();

            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(CarTable);

                foreach (DataRow row in CarTable.Rows)
                {
                    Car car = new Car()
                    {
                        Id = (int)row["Id"],
                        BrandName = (string)row["BrandName"],
                        CarBody = (string)row["CarBody"],
                        Name = (string)row["Model"],
                        Year = (int)row["Year"],
                        Acceleration = (double)row["Acceleration"],
                        Drive = (string)row["Drive"],
                        EngineType = (string)row["EngineType"],
                        Price = (int)row["Price"],
                        Seats = (int)row["Seats"],
                        TopSpeed = (int)row["TopSpeed"],
                        DriveId = (int)row["DriveId"],
                        BrandId = (int)row["BrandId"],
                    };
                    if (row["Image"] != System.DBNull.Value)
                    {
                        try
                        {
                            car.Image = LoadImage((byte[])row["Image"]);
                        }catch (Exception ex)
                        {
                        }
                    }
                    if (car.Image == null)
                    {
                        string packUri = "pack://application:,,,/WpfApp1;component/img/noImage.png";
                        car.Image = new BitmapImage(new Uri(packUri));
                    }
                    CarCollection.Add(car);
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

            return CarCollection;
        }

        public bool deleteUserFromDataBasse(User user)
        {
            bool result = false;

            SqlConnection connection = null;
            connection = new SqlConnection(Globals.DBurl);
            connection.Open();

            try
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"DELETE FROM Users WHERE Id={user.Id}";
                cmd.Parameters.AddWithValue("@bool", user.Blocked);
                cmd.ExecuteNonQuery();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result;
            }

            result = true;
            return result;
        }

        public bool saveUserChanges(User user,bool changeStatus = true)
        {
            bool result = false;

            SqlConnection connection = null;
            connection = new SqlConnection(Globals.DBurl);
            connection.Open();
            if (changeStatus)
            {
                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = $"UPDATE Users SET Blocked=@bool, UserStatus={user.StatusId + 1} WHERE Id={user.Id}";
                    cmd.Parameters.AddWithValue("@bool", user.Blocked);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return result;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            else
            {
                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = $"UPDATE Users SET Blocked=@bool WHERE Id={user.Id}";
                    cmd.Parameters.AddWithValue("@bool", user.Blocked);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return result;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            result = true;
            return result;
        }

        public bool deleteCarFromDataBase(Car car)
        {
            bool result = false;

            string sqlQuery = $"DELETE FROM Model WHERE Id={car.Id}";
            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            result = true;
            return result;
        }

        public bool saveCarChanges(Car car)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(Globals.DBurl);
            connection.Open();

            if (car.Id == 0)
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand($"INSERT INTO model(Name,CarBody,Year,BrandId,Price,Seats,DriveId,EngineType,TopSpeed,Acceleration,Image) VALUES ('{car.Name}', '{car.CarBody}', {car.Year}, {car.BrandId}, {car.Price}, {car.Seats}, {car.DriveId}, '{car.EngineType}', {car.TopSpeed}, {car.Acceleration.ToString().Replace(',', '.')},@img)"))
                    {
                        byte[] data;
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(car.Image));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            encoder.Save(ms);
                            data = ms.ToArray();
                        }
                        cmd.Connection = connection;
                        cmd.Parameters.Add(new SqlParameter("img", data));

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Saving car name {car.Name}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return result;
                }
            }
            else
            {

                try
                {

                    using (SqlCommand cmd1 = new SqlCommand($"UPDATE Model SET Acceleration={car.Acceleration.ToString().Replace(',', '.')},BrandId={car.BrandId},CarBody='{car.CarBody}',DriveId={car.DriveId},EngineType='{car.EngineType}',Name='{car.Name}',Price={car.Price},Seats={car.Seats},TopSpeed={car.TopSpeed},Year={car.Year}, Image=@img WHERE Id={car.Id}"))
                    {
                        byte[] data;
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(car.Image));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            encoder.Save(ms);
                            data = ms.ToArray();
                        }
                        cmd1.Connection = connection;
                        cmd1.Parameters.Add(new SqlParameter("img", data));

                        cmd1.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Saving car id {car.Id}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return result;
                }
            }
            if (connection != null)
                connection.Close();

            result = true;
            return result;
        }

        public bool deleteBrandFromDataBase(Brand brand)
        {
            bool result = false;

            string sqlQuery = $"DELETE b FROM Model b INNER JOIN Brand ON BrandId=Brand.Id WHERE Brand.Id={brand.Id} DELETE Brand WHERE Id={brand.Id}";
            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            result = true;
            return result;
        }

        public bool saveBrandChanges(Brand brand)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(Globals.DBurl);
            connection.Open();

            if (brand.Id == 0)
            {
                try
                {

                    string sql = $"INSERT INTO Brand VALUES ('{brand.BrandName}',{brand.CountryId},{brand.FoundationYear},{brand.CompanyValue})";
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"Saving brand name {brand.BrandName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return result;
                }
            }
            else
            {

                try
                {
                    string sql = $"UPDATE Brand SET BrandName = '{brand.BrandName}', CountryId={brand.CountryId}, FoundationYear={brand.FoundationYear}, CompanyValue={brand.CompanyValue} WHERE Id = {brand.Id}";
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"Saving brand id {brand.Id}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return result;
                }
            }
            if (connection != null)
                connection.Close();

            result = true;
            return result;
        }

        public bool deleteCountyFromDataBase(Country country)
        {
            bool result = false;

            string sqlQuery = $"DELETE a FROM Model a INNER JOIN Brand ON BrandId=Brand.Id INNER JOIN Country ON CountryId=Country.Id WHERE Country.Id = {country.Id} DELETE b FROM Brand b INNER JOIN Country ON CountryId=Country.Id WHERE CountryId={country.Id} DELETE Country WHERE Id={country.Id}";
            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            result = true;
            return result;
        }

        public bool saveCountryChanges(Country country)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(Globals.DBurl);
            connection.Open();

            if (country.Id == 0)
            {
                try
                {

                    string sql = $"INSERT INTO Country VALUES ('{country.Name}')";
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"Saving country name {country.Name}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return result;
                }
            }
            else
            {

                try
                {
                    string sql = $"UPDATE Country SET Name = '{country.Name}'";
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"Saving country id {country.Id}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return result;
                }
            }
            if (connection != null)
                connection.Close();

            result = true;
            return result;
        }

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
    }
}
