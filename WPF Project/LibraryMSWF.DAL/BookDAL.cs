using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMSWF.DAL
{
    public class BookDAL
    {
        //RETURN THE COMPLETE BOOKS FROM BOOK TABLE =>DAL
        String strCon = @"Data Source=LAPTOP-AC0C9BOB\SQLEXPRESS;Initial Catalog=LibraryMSWF;Integrated Security=True";
        SqlConnection sqlCon = null;
        public DataSet GetAllBooksDAL()
        {
            sqlCon = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter("GetAllBooks", sqlCon);
            DataSet ds = new DataSet("Books");
            da.Fill(ds);
            return ds;
        }
        //ADD BOOK INTO BOOK TABLE => DAL
        public bool AddBookDAL(string bookName, string bookAuthor, string bookISBN, double bookPrice, int bookCopies)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("AddBook @name, @author,@isbn,@price,@copy", sqlCon);
            cmd.Parameters.Add(new SqlParameter("@name", bookName));
            cmd.Parameters.Add(new SqlParameter("@author", bookAuthor));
            cmd.Parameters.Add(new SqlParameter("@isbn", bookISBN));
            cmd.Parameters.Add(new SqlParameter("@price", bookPrice));
            cmd.Parameters.Add(new SqlParameter("@copy", bookCopies));
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
        //UPDATE THE BOOK FROM BOOK TABLE =>DAL
        public bool UpdateBookDAL(int bookId, string bookName, string bookAuthor, string bookISBN, double bookPrice, int bookCopies)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("UpdateBook @id,@name, @author,@isbn,@price,@copy", sqlCon);
            cmd.Parameters.Add(new SqlParameter("@id", bookId));
            cmd.Parameters.Add(new SqlParameter("@name", bookName));
            cmd.Parameters.Add(new SqlParameter("@author", bookAuthor));
            cmd.Parameters.Add(new SqlParameter("@isbn", bookISBN));
            cmd.Parameters.Add(new SqlParameter("@price", bookPrice));
            cmd.Parameters.Add(new SqlParameter("@copy", bookCopies));
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
        //DELETE THE BOOK FROM BOOK TABLE =>DAL
        public bool DeleteBookDAL(int bookId)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("DeleteBook @id", sqlCon);
            cmd.Parameters.Add(new SqlParameter("@id", bookId));
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
        //INCREMENT THE BOOK COPIES OF A BOOK BY 1 =>DAL
        public bool IncBookCopyDAL(int bookId)
        {
            sqlCon = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand("IncBookCopy @id", sqlCon);
            cmd.Parameters.Add(new SqlParameter("@id", bookId));
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
