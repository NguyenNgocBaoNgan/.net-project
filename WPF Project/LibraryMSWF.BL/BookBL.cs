using System;
using System.Collections.Generic;
using System.Linq;
using LibraryMSWF.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMSWF.BL
{
    public class BookBL
    {
        //VALIDATION METHOD FOR VALIDATE BOOK DETAILS
        public string BookValidate(string bookName, string bookAuthor, string bookISBN, double bookPrice, int bookCopies, string bookImage)
        {
            //MaiNguyen những yêu cầu, quy đinh nhập liệu thông tin cuốn sách
            if (bookName.Equals(string.Empty)||bookName.Length>30 || bookName.Length<3)
            {
                return "Tên sách không hợp lệ!!!,\ncho phép tối thiểu 3 tối đa 30 ký tự,";
            }
            else if(bookAuthor.Equals(string.Empty) || bookAuthor.Length > 30 || bookAuthor.Length < 3)
            {
                return "Tên tác giả không hợp lệ!!!,\ncho phép tối thiểu 3 tối đa 30 ký tự,";
            }
            else if (bookName.Any(char.IsDigit))
            {
                return "Tên sách không hợp lệ!!!,\ntên không được chứa chữ số,";
            }
            else if (bookAuthor.Any(char.IsDigit))
            {
                return "Tên tác giả không hợp lệ!!!,\ntên không được chứa chữ số,";
            }
            else if (bookISBN.Equals(string.Empty) || bookISBN.Length > 15 || bookISBN.Length < 3)
            {
                return "Mã ISBN sách không hợp lệ!!!, \nCho phép tối thiểu 3 tối đa 15 ký tự,";
            }
            else if(bookPrice==0 || bookPrice <= 10)
            {
                return "Giá sách không hợp lệ!!!,\nit không thấp hơn 10,";
            }
            else if(bookCopies<0 || bookCopies > 200)
            {
                return "Bản sao sách không hợp lệ!!!, \nit không được lớn hơn 200,";
            }
            else if (bookImage.Equals(string.Empty))
            {  //MaiNguyen bắt buộc chọn hình ảnh  
                return "Hình ảnh sách không hợp lệ!!!,\nVui lòng chọn hình ảnh cuốn sách,";
            }
            else
            {
                return "true";
            }
            
        }
        //RETURN THE COMPLETE BOOKS FROM BOOK TABLE =>BL
        public DataSet GetAllBooksBL()
        {
            BookDAL bookDal = new BookDAL();
            DataSet ds = bookDal.GetAllBooksDAL();// gọi store procedure lấy tất cả cuốn sách
            return ds;
        }

        //MAINGUYEN tầng controller là nơi kết nối giữa View (giao diện) với DAL (xử lí query)
        //ADD BOOK INTO BOOK TABLE => BL
        public string AddBookBL(string bookName, string bookAuthor, string bookISBN, double bookPrice, int bookCopies, string bookImage, int bookStatus)
        {
            //trước khi insert thì phải kiểm tra 
            // mainguyen kiểm tra dữ liệu đúng định dạng không
            string isBookValid = BookValidate(bookName,bookAuthor,bookISBN,bookPrice,bookCopies, bookImage);
            if (isBookValid=="true")
            {
                // mainguyen gọi qua dao - database xử lí sql
                BookDAL bookDAL = new BookDAL();
                bool isDone = bookDAL.AddBookDAL(bookName, bookAuthor, bookISBN, bookPrice, bookCopies, bookImage, bookStatus);
                if (isDone!=true)
                {
                    return "Server error, ";
                }
                else
                {
                    return "true";
                }
            }
            else
            {
                return isBookValid;
            }
            
        }
        //UPDATE THE BOOK FROM BOOK TABLE =>BL
        public string UpdateBookBL(int bookId, string bookName, string bookAuthor, string bookISBN, double bookPrice, int bookCopies, string bookImage, int bookStatus)
        {
            string isBookValid = BookValidate(bookName, bookAuthor, bookISBN, bookPrice, bookCopies, bookImage);
            if (isBookValid == "true")
            {
                BookDAL bookDAL = new BookDAL();
                bool isDone = bookDAL.UpdateBookDAL(bookId, bookName, bookAuthor, bookISBN, bookPrice, bookCopies, bookImage, bookStatus);
                if (isDone != true)
                {
                    return "Server error, ";
                }
                else
                {
                    return "true";
                }
            }
            else
            {
                return isBookValid;
            }
        }
        /*public bool UpdateBookBL(int bookId, string bookName, string bookAuthor, string bookISBN, double bookPrice, int bookCopies)
        {
            BookDAL bookDAL = new BookDAL();
            bool isDone = bookDAL.UpdateBookDAL(bookId, bookName, bookAuthor, bookISBN, bookPrice, bookCopies);
            return isDone;
        }*/
        //DELETE THE BOOK FROM BOOK TABLE =>BL
        public bool DeleteBookBL(int bookId)
        {
            BookDAL bookDAL = new BookDAL();
            bool isDone = bookDAL.DeleteBookDAL(bookId);
            return isDone;
        }
        //INCREMENT THE BOOK COPIES OF A BOOK BY 1 =>BL
        public bool IncBookCopyBL(int bookId)
        {
            BookDAL bookDAL = new BookDAL();
            bool isDone = bookDAL.IncBookCopyDAL(bookId);
            return isDone;
        }

    }
}
