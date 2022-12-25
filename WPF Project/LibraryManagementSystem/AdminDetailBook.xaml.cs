using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibraryMSWF.BL;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for AdminDetailBook.xaml
    /// </summary>
    public partial class AdminDetailBook : Window
    {
        public int bookId;
        //khởi tạo biến để lưu đường dẫn hình ảnh
        public string bookImage = "";
        public static string PATH_IMAGE_LOAD = "\\..\\..\\..\\Images\\";//thư mục chứa hình khi nhấn hiển thị
        //INITIALIZE THE BOOKS UPDATE =>PL
        public AdminDetailBook()
        {
            InitializeComponent();
            this.bookId = AdminBooks.updateBook.BookId;
            tbBName.Text = AdminBooks.updateBook.BookName;
            tbBAuthor.Text = AdminBooks.updateBook.BookAuthor;
            tbBISBN.Text = AdminBooks.updateBook.BookISBN;
            tbBPrice.Text = AdminBooks.updateBook.BookPrice.ToString();
            tbBCopy.Text = AdminBooks.updateBook.BookCopies.ToString();

            //xử lý dữ liệu cho trường status
            if (AdminBooks.updateBook.BookStatus == 1)
            {
                cboStatus.SelectedValue = "Actice";
            } else
            {
                cboStatus.SelectedValue = "Deactice";
            }

            //gán giá trị lấy từ database lên để sử dụng lúc cập nhật hình ảnh
            this.bookImage = AdminBooks.updateBook.BookImage.ToString();
            try
            {
                //màn hình chi tiết hiển thị cuốn sách
                string defaultFolder = System.AppDomain.CurrentDomain.BaseDirectory;//D:\\DH20DT\\hk5\\Net\\CuoiKy\\net\\WPF Project\\LibraryManagementSystem\\bin\\Debug\\ thư mục mặc định                                                                 
                string path = Path.Combine(defaultFolder + PATH_IMAGE_LOAD);
                string linkImage = path + this.bookImage;//lấy link hình trực tiếp từ database
                imagePicture.Source = new BitmapImage(new Uri(linkImage));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
