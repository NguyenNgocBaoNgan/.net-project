using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LibraryMSWF.DAL
{
    public class UserReturnDAL
    {
        //ADD THE BOOK RETURN INTO RETURN TABLE =>DAL
        String strCon = @"Data Source=LAPTOP-AC0C9BOB\SQLEXPRESS;Initial Catalog=LibraryMSWF;Integrated Security=True";
        SqlConnection sqlCon = null;
        public bool AddReturntDAL(int bookId, string bookName, int userId, string userName)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("AddReturn @bId, @bName, @date, @uId, @uName", sqlCon);
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

        //RETURN THE COMPLETE RETURN BOOKS FROM RETURN TABLE  =>DAL
        public DataSet GetAllReturnDAL()
        {
            sqlCon = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter("GetAllReturn", sqlCon);
            DataSet ds = new DataSet("Returns");
            da.Fill(ds);
            return ds;
        }
        //DELETE THE BOOK RETURN FROM RETURN TABLE =>DAL
        public bool DeleteReturntDAL(int bookId, int userId)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("DeleteReturn @bId, @uId", sqlCon);
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
