using System;
using System.Windows;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using WpfApp1.Models;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1.Operations
{
    class UserOperatioms
    {
        Dictionary<int, string> triedSignIn = new Dictionary<int,string>();

        public string GetPassword()
        {
            string sql = $"SELECT Password FROM AppPassword";
            SqlConnection connection = null;
            string result = null;

            try
            {
                connection = new SqlConnection(Globals.DBurl);
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    result = dr.GetString(0);
                }
                dr.Close();
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
            return result;
        }

        public User LoginUser(string username, string password)
        {
            User userByPassword = new User();
            User userByUsername = new User();
            string hashedPass = ComputeSha256Hash(password);
            string sql = $"SELECT Users.Id,Users.Username,UserRole.Name as Status, UserStatus, Blocked FROM Users JOIN UserRole ON UserRole.Id = Users.UserStatus WHERE (Users.Username = '{username}')";
            string sql1 = $"SELECT Users.Id,Users.Username,UserRole.Name as Status, UserStatus, Blocked FROM Users JOIN UserRole ON UserRole.Id = Users.UserStatus WHERE (Users.Username = '{username}' AND Users.PasswordHash='{hashedPass}')";
            SqlConnection connection = null;

            if (username == "" || password == "")
            {
                MessageBox.Show("Username or password should not be empty");
                return null;
            }

            try
            {
                connection = new SqlConnection(Globals.DBurl);
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    userByUsername.Username = dr.GetString(1);
                    userByUsername.Id = dr.GetInt32(0);
                    userByUsername.Status = dr.GetString(2);
                    userByUsername.StatusId = dr.GetInt32(3) - 1;
                    userByUsername.Blocked = dr.GetBoolean(4);
                }
                dr.Close();

                if (userByUsername.Blocked)
                {
                    MessageBox.Show("This user was blocked");
                    return null;
                }

                if (userByUsername.Id == 0)
                {
                    MessageBox.Show("Username not found");
                    return null;
                }
                else
                {
                    connection = new SqlConnection(Globals.DBurl);
                    SqlCommand cmd1 = connection.CreateCommand();
                    cmd1.CommandText = sql1;
                    connection.Open();
                    SqlDataReader dr1 = cmd1.ExecuteReader();

                    if (dr1.Read())
                    {
                        userByPassword.Username = dr1.GetString(1);
                        userByPassword.Id = dr1.GetInt32(0);
                        userByPassword.Status = dr1.GetString(2);
                        userByPassword.StatusId = dr1.GetInt32(3) - 1;
                        userByPassword.Blocked = dr1.GetBoolean(4);
                    }

                    dr1.Close();

                    if (userByPassword.Blocked)
                    {
                        MessageBox.Show("This user was blocked");
                        return null;
                    }

                    if (userByPassword.Id == 0)
                    {
                        MessageBox.Show("Invalid password");

                        if (!Globals.FailedLogins.Any(x => x.id == userByUsername.Id))
                        {
                            FailedUserLogin failed = new FailedUserLogin()
                            {
                                id = userByUsername.Id,
                                FailedCount = 0,
                                Username = userByUsername.Username
                            };
                            Globals.FailedLogins.Add(failed);
                        }
                        FailedUserLogin lul = Globals.FailedLogins.Single(x => x.id == userByUsername.Id);
                        lul.FailedCount++;
                        if (Globals.FailedLogins.Single(x => x.id == userByUsername.Id).FailedCount >= 3)
                        {
                            MessageBox.Show("This user waas blocked for failing to log in three times");
                            userByUsername.Blocked = true;
                            SqlCommand cmd2 = connection.CreateCommand();
                            cmd2.CommandText = $"UPDATE Users SET Blocked=@bool WHERE Id={userByUsername.Id}";
                            cmd2.Parameters.AddWithValue("@bool", userByUsername.Blocked);
                            cmd2.ExecuteNonQuery();
                        }
                        return null;
                    }
                }

                Globals.FailedLogins.Remove(new FailedUserLogin()
                {
                    id = userByPassword.Id,
                    Username=userByPassword.Username,
                });
                return userByUsername;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public User RegisterUser(string username, string password, int status)
        {
            User user = new User();
            string hashedPass = ComputeSha256Hash(password);
            Console.WriteLine(hashedPass);
            string sql = $"INSERT INTO Users(Username, PasswordHash, UserStatus) VALUES ('{username}','{hashedPass}',{status}) SELECT Users.Id, Users.Username, UserRole.Name, UserStatus FROM Users INNER JOIN UserRole ON Users.UserStatus = UserRole.Id WHERE (Username='{username}' AND PasswordHash='{hashedPass}')";
            SqlConnection connection = null;

            if (username == "" || password == "")
            {
                MessageBox.Show("Username or password should not be empty");
                return null;
            }

            try
            {
                connection = new SqlConnection(Globals.DBurl);
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    user.Username = dr.GetString(1);
                    user.Id = dr.GetInt32(0);
                    user.Status = dr.GetString(2);
                    user.StatusId = dr.GetInt32(3) - 1;
                }

                return user;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
