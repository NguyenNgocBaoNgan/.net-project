using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMSWF.DAL
{
    public class UserRecieveDAL
    {
        //ADD THE RECIEVED BOOK INTO RECIEVED TABLE =>DAL
        String strCon = @"Data Source=LAPTOP-AC0C9BOB\SQLEXPRESS;Initial Catalog=LibraryMSWF;Integrated Security=True";
        SqlConnection sqlCon = null;
        public bool AddRecieveDAL(int bookId, string bookName, int userId, string userName)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("AddRecieve @bId, @bName, @date, @uId, @uName", sqlCon);
            cmd.Parameters.Add(new SqlParameter("@bId", bookId));
            cmd.Parameters.Add(new SqlParameter("@bName", bookName));
            cmd.Parameters.Add(new SqlParameter("@date", DateTime.Now.Date));
            cmd.Parameters.Add(new SqlParameter("@uId", userId));
            cmd.Parameters.Add(new SqlParameter("@uName", userName));
            sqlCon.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            sqlCon.Close();
            if (rowAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //RETURN THE COMPLETE RECIEVED BOOKS FROM RECIEVED TABLE  =>DAL
        public DataSet GetAllRecieveDAL()
        {
            sqlCon = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter("GetAllRecieve", sqlCon);
            DataSet ds = new DataSet("Recieved");
            da.Fill(ds);
            return ds;
        }
        //RETURN THE PERTICULAR USER RECIEVED BOOKS FROM RECIEVED TABLE  =>DAL
        public DataSet GetAllRecieveUserDAL(int userId)
        {
            sqlCon = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter("GetAllRecieveUser '" + userId + "'", sqlCon);
            DataSet ds = new DataSet("UserRecieved");
            da.Fill(ds);
            return ds;
        }
        //DELETE THE RECIEVED BOOK FROM RECIEVED TABLE =>DAL
        public bool DeleteRecieveDAL(int bookId, int userId)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("DeleteRecieve @bId, @uId", sqlCon);
            cmd.Parameters.Add(new SqlParameter("@bId", bookId));
            cmd.Parameters.Add(new SqlParameter("@uId", userId));
            sqlCon.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            sqlCon.Close();
            if (rowAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
