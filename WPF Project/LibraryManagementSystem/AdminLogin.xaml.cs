using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibraryMSWF.BL;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
        }
        //CHECK THE ADMIN LOGIN CREDENTIALS AND OPEN ADMIN HOME =>PL
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((tbAdminEmail.Text != string.Empty || tbAdminPass.DataContext != string.Empty) || (tbAdminEmail.Text != string.Empty && tbAdminPass.DataContext != string.Empty))
            {
                try
                {
                    AdminBL adminBl = new AdminBL();
                    bool isDone = adminBl.AdminLoginBL(tbAdminEmail.Text, tbAdminPass.Password.ToString());
                    if (isDone)
                    {
                        alertAdmin.Content = "";
                        MessageBox.Show("Logged in successfully...");//MAINGUYEN comment code lại dòng này
                        // MessageBox.Show("Logged in successfully...");//MAINGUYEN đăng nhập thành công mặc định vào trang dashboard không cần hiển thị câu thông báo
                        AdminHome adminHome = new AdminHome();
                        adminHome.Show();
                        tbAdminEmail.Clear();
                        tbAdminPass.Clear();
                        // this.Close();//MAINGUYEN mở commenct code dòng này > tắt giao diện trang login admin khi đăng nhập thành công
                    }
                    else
                    {

                        alertAdmin.Content = "Invalid email id or password...";
                        tbAdminEmail.Clear();
                        tbAdminPass.Clear();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Some unknown exception is occured!!!, Try again..");
                }
            }
            else
            {
                alertAdmin.Content = "Enter the fields properly...";
            }
        }
    }
}
