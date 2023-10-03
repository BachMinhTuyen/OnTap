using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnTapKiemTraLan1.Models
{
    public class ConnectUserAccount
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["ConnectBD"].ConnectionString;
        private List<UserAccount> GetUserAccounts()
        {
            List<UserAccount> lst = new List<UserAccount>();

            SqlConnection connection = new SqlConnection(connectionString);

            string sqlComand = "SELECT * FROM tbl_User";
            SqlCommand cmd = new SqlCommand(sqlComand, connection);
            cmd.CommandType = System.Data.CommandType.Text;

            connection.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var user = new UserAccount();
                user.UserName = rdr[0].ToString();
                user.PassWord = rdr[1].ToString();

                lst.Add(user);
            }

            connection.Close();
            return lst;
        }
        public int insertAccount(string userName, string PassWord)
        {
            List<UserAccount> lst = GetUserAccounts();
            
            foreach(var user in lst)
            {
                if (string.Compare(userName, user.UserName,false) == 0) // Có phân biệt hoa thường
                    return 0;
            }
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlComand = "INSERT INTO tbl_User(UserName, MatKhau) VALUES ('" + userName + "', '" + PassWord + "')";
            SqlCommand cmd = new SqlCommand(sqlComand, connection);
            cmd.CommandType = System.Data.CommandType.Text;

            connection.Open();

            int rows = cmd.ExecuteNonQuery();
            connection.Close();
            return rows;
        }
        public int confirmAccount(string userName, string PassWord)
        {
            List<UserAccount> lst = GetUserAccounts();

            foreach (var user in lst)
            {
                if (string.Compare(userName, user.UserName, false) == 0 && string.Compare(PassWord, user.PassWord, false) == 0) // Có phân biệt hoa thường
                    return 1;
            }
            return 0;
        }
    }
}