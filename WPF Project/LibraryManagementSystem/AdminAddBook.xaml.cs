using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AdminAddBook.xaml
    /// </summary>
    public partial class AdminAddBook : Window
    {
        //MaiNguyen chức năng thêm cuốn sách sẽ nhập liệu thông tin tên,......, trạng thái, updoad hình ảnh
        //Hình ảnh sẽ được lưu vào thư mục Images do mình quy định

        public string bookImage = ""; //khởi tạo biến để lưu tên hình ảnh ví dụ abc.png
        public static string PATH_IMAGE_SAVE = "\\..\\..\\Images\\";//thư mục chứa hình khi thao tác nhấn upload image
        public string linkImageView = "";//đường link vị trí hình ảnh đang chọn để upload

        public AdminAddBook()
        {
            InitializeComponent();
        }
        //ADD THE BOOKS DETAILS INTO BOOK TABEL =>PL
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // mainguyen nút nhấn save

            // mainguyen kiểm tra rổng
            if (tbBName.Text != string.Empty && tbBAuthor.Text != string.Empty && tbBISBN.Text != string.Empty && tbBPrice.Text != string.Empty &&
                tbBCopy.Text != string.Empty
                )
            {
                try
                {
                    //lấy giá trị từ giao diện lưu vào database
                    ComboBoxItem typeItem = (ComboBoxItem)cboStatus.SelectedItem;
                    string status = typeItem.Name.ToString();
                    int bookStatus = 2;
                    if (status == "Actice")
                    {
                        bookStatus = 1;
                    }

                    // mainguyen gọi qua controller
                    BookBL bookBL = new BookBL();
                    string isDone = bookBL.AddBookBL(tbBName.Text, tbBAuthor.Text, tbBISBN.Text, double.Parse(tbBPrice.Text), int.Parse(tbBCopy.Text), this.bookImage, bookStatus);
                    if (isDone == "true")
                    {
                        MessageBox.Show("Đã thêm sách thành công..");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(isDone + "Try again..");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Book price or Book copy!!!,\nThey should not be a string, Try again..");
                }
                catch (Exception)
                {
                    MessageBox.Show("Some unknown exception is occured!!!, Try again..");
                }
            }
            else
            {
                MessageBox.Show("Enter the fields properly!!!, Every field is required..");
            }
        }

        private void Button_View_Click(object sender, RoutedEventArgs e)
        {
            //Khi nhấn vào nút update image là chức năng cập nhật hình ảnh
            try
            {
                //lưu thông tin hình sách
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files|*.bmp;*.jpg;*.png;";// lọc file có đuôi là png, jpg, bmp mới hiển thị ở dialog cho phép lưu hình ảnh
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog() == true)
                {
                    imagePicture.Source = new BitmapImage(new Uri(dialog.FileName));//hiển thị hình ảnh xem trước - view

                    this.linkImageView = System.IO.Path.GetFullPath(dialog.FileName);//đường dẫn hình ảnh mới chọn để xem
                    this.bookImage = System.IO.Path.GetFileName(dialog.FileName);//lấy tên file lưu DB 
                    //gán đường dẫn hình ảnh vào biến tạm > để khi nhấn save lấy để lưu vào database

                    //nếu muốn vừa xem vừa lưu thì 
                    //START: MAINGUYEN
                    string defaultFolder = System.AppDomain.CurrentDomain.BaseDirectory;// lấy vị trí source code
                    // D:\DH20DT\hk5\Net\CuoiKy\loadimage\loadimage\bin\Debug\net6.0-windows\ thư mục mặc định                                                                 
                    string path = Path.Combine(defaultFolder + PATH_IMAGE_SAVE);
                    // D:\DH20DT\hk5\Net\CuoiKy\loadimage\loadimage\Images // đường dẫn sẽ lưu hình ảnh
                    if (!Directory.Exists(path))//kiểm tra thư mục hình ảnh
                    {
                        Directory.CreateDirectory(path);//nếu thự mục hình ảnh chưa có thì tạo mới
                    }
                    string linkImageSave = path + this.bookImage;
                    // D:\DH20DT\hk5\Net\CuoiKy\loadimage\loadimage\Images\abcd.png
                    if (!File.Exists(linkImageSave))
                    {
                        File.Copy(this.linkImageView, linkImageSave);//copy hình ảnh từ ngoài vào trong project(để tạo dữ liệu chứa hình ảnh)
                    }
                    //END: MAINGUYEN
                }
            }
            catch (Exception ex)
            {     
                MessageBox.Show(ex.Message);
            }
        }

        //MaiNguyen tách chức năng view và save hình ảnh lý do người dùng có thể lựa chọn hình ảnh trước khi lưu
        //do người dùng chọn 100 lần thì sẽ lưu vào 100 lần thì hệ thống sẽ bị rác
        //nên cần tách chức năng cho xem trước hình ảnh cần lưu
        //để không lưu file rác
        //nếu hợp lý thì mới save
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Khi nhấn vào nút update image là chức năng cập nhật hình ảnh
            try
            {
                //START: MAINGUYEN
                // lưu hình vào thư mực hình ảnh của sourcecode
                string defaultFolder = System.AppDomain.CurrentDomain.BaseDirectory;////D:\DH20DT\hk5\Net\CuoiKy\loadimage\loadimage\bin\Debug\net6.0-windows\ thư mục mặc định                                                                 
                string path = Path.Combine(defaultFolder + PATH_IMAGE_SAVE);//kiểm tra thư mục hình ảnh
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);//nếu thự mục hình ảnh chưa có thì tạo mới
                }
                string linkImageSave = path + this.bookImage;
                if (!File.Exists(linkImageSave))
                {
                    File.Copy(this.linkImageView, linkImageSave);//copy hình ảnh từ ngoài vào trong project(để tạo dữ liệu chứa hình ảnh)
                }
                //END: MAINGUYEN
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
