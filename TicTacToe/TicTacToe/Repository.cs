using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Repository : IRepository
    {
        public bool Add(User user)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = TicTacToeGame; User ID = sa; Password = test123!@#"))
                {
                    string tokken = Guid.NewGuid().ToString();
                    connection.Open();
                    string command = "insert into GameUser(firstName,lastName,userName,email,accessTokken,status) Values(@firstName,@lastName,@userName,@email,@accessToken,@status)";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@firstName", user.firstName);
                    cmd.Parameters.AddWithValue("@lastName", user.lastName);
                    cmd.Parameters.AddWithValue("@userName", user.userName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@accessToken", tokken);
                    cmd.Parameters.AddWithValue("@status", false);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    user.ID = getUserId(tokken);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int getUserId(string tokken)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = TicTacToeGame; User ID = sa; Password = test123!@#"))
                {
                    connection.Open();
                    string command = "select ID from GameUser where accessTokken=@tokken";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@tokken", tokken.ToString());
                    cmd.CommandType = CommandType.Text;
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    return id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public string getTokken(int id)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = TicTacToeGame; User ID = sa; Password = test123!@#"))
                {
                    connection.Open();
                    string command = "select accessTokken from GameUser where ID=@ID";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.CommandType = CommandType.Text;
                    string tokken = cmd.ExecuteScalar().ToString();
                    return tokken;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public int KeyCheck(string tokken)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = TicTacToeGame; User ID = sa; Password = test123!@#"))
                {
                    connection.Open();
                    string command = "select count(accessTokken) from GameUser where accessTokken=@accessTokken";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@accessTokken", tokken.ToString());
                    cmd.CommandType = CommandType.Text;
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result > 0)
                        return 1;
                    else if (tokken == "")
                        return 2;
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Case");
            }

        }
        public bool AddLog(Log log)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = TicTacToeGame; User ID = sa; Password = test123!@#"))
                {
                    connection.Open();
                    string command = "insert into LoggerDB(Request,Response,Exception,Time) Values(@Request,@Response,@Exception,@Time)";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@Request", log.Request);
                    cmd.Parameters.AddWithValue("@Response", log.Response);
                    cmd.Parameters.AddWithValue("@Exception", log.Exception);
                    cmd.Parameters.AddWithValue("@Time", log.Time);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
