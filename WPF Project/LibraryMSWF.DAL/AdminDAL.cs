using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMSWF.DAL
{
    public class AdminDAL
    {
        String strCon = @"Data Source=LAPTOP-AC0C9BOB\SQLEXPRESS;Initial Catalog=LibraryMSWF;Integrated Security=True";
        SqlConnection sqlCon = null;
        //CHECK THE ADMIN LOGIN CREDENTIALS =>DAL
        public bool AdminLoginDAL(string adminEmail, string adminPass)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("AdminLogin @email, @pwd", sqlCon);
            cmd.Parameters.Add(new SqlParameter("@email", adminEmail));
            cmd.Parameters.Add(new SqlParameter("@pwd", adminPass));
            sqlCon.Open();
            int rowAffected = (int)cmd.ExecuteScalar();
            
            if (rowAffected!=0)
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
