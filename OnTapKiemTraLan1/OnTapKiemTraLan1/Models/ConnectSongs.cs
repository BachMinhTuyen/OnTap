using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OnTapKiemTraLan1.Models
{
    public class ConnectSongs
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["ConnectBD"].ConnectionString;
        public List<Songs> getData()
        {
            List<Songs> songs = new List<Songs>();

            SqlConnection connection = new SqlConnection(connectionString);

            string sqlCommand = "SELECT * FROM tbl_BaiHat";
            SqlCommand cmd = new SqlCommand(sqlCommand, connection);
            cmd.CommandType = System.Data.CommandType.Text;

            connection.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                var s = new Songs();
                s.maBH = rdr.GetValue(0).ToString();
                s.tenBH = rdr.GetValue(1).ToString();
                s.maTheLoai = rdr.GetValue(2).ToString();
                s.maNhacSi = rdr.GetValue(3).ToString();

                songs.Add(s);
            }

            connection.Close();

            return songs;
        }
        public Songs getDetail(string id)
        {
            List<Songs> songs = new List<Songs>();

            SqlConnection connection = new SqlConnection(connectionString);

            string sqlCommand = "SELECT * FROM tbl_BaiHat WHERE MaBH = '" + id + "'";
            SqlCommand cmd = new SqlCommand(sqlCommand, connection);
            cmd.CommandType = System.Data.CommandType.Text;

            connection.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var s = new Songs();
                s.maBH = rdr.GetValue(0).ToString();
                s.tenBH = rdr.GetValue(1).ToString();
                s.maTheLoai = rdr.GetValue(2).ToString();
                s.maNhacSi = rdr.GetValue(3).ToString();

                songs.Add(s);
            }

            connection.Close();

            //Songs s = songs.FirstOrDefault(row => row.maBH == id);
            Songs song = songs.FirstOrDefault(row => string.Compare(row.maBH, id, false) == 0);
            return song;
        }

        public int EditData(string maBH, string tenBH, string maTheLoai, string maNhacSi)
        {
            List<Songs> songs = new List<Songs>();

            SqlConnection connection = new SqlConnection(connectionString);

            string sqlCommand = "UPDATE tbl_BaiHat SET TenBH = '"+ tenBH + "', MaTheLoai = '" + maTheLoai + "', MaNhacSi = '" + maNhacSi + "'WHERE MaBH = '" + maBH + "'";
            SqlCommand cmd = new SqlCommand(sqlCommand, connection);
            cmd.CommandType = System.Data.CommandType.Text;

            //SqlParameter para_TenBH = new SqlParameter("@tenBH", newSong.tenBH);
            //SqlParameter para_MaTheLoai= new SqlParameter("@maTheLoai", newSong.maTheLoai);
            //SqlParameter para_MaNhacSi = new SqlParameter("@maNhacSi", newSong.maNhacSi);
            //cmd.Parameters.Add(para_TenBH);
            //cmd.Parameters.Add(para_MaTheLoai);
            //cmd.Parameters.Add(para_MaNhacSi);

            connection.Open();

            int rows = cmd.ExecuteNonQuery();
            connection.Close();
            return rows;
        }
        //-- Điều này xoá một bài hát trong bảng tbl_BaiHat dựa trên mã bài hát
        //DELETE FROM tbl_BaiHat WHERE MaBH = 'MãBaiHatCanXoa';

        //-- Đây là câu truy vấn để lấy danh sách bài hát từ bảng tbl_BaiHat sắp xếp theo tên bài hát
        //SELECT* FROM tbl_BaiHat ORDER BY TenBH ASC;

    }
}