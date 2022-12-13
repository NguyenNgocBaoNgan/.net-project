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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using LibraryMSWF.BL;
using LibraryMSWF.Entity;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for UserBorrow.xaml
    /// </summary>
    public partial class UserBorrow : UserControl
    {
        public int userId;
        //INITIALIZE THE BORROW GV =>PL
        public UserBorrow()
        {
            InitializeComponent();
            InitializeUserBorrow();
        }
        public void InitializeUserBorrow()
        {
            try
            {
                BookBL bookBl = new BookBL();
                DataSet ds = bookBl.GetAllBooksBL();
                userId = UserLogin.userId;
                ObservableCollection<Book> lst = new ObservableCollection<Book>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new Book
                    {
                        BookName = Convert.ToString(dr["BookName"]),
                        BookId = Convert.ToInt32(dr["BookId"]),
                        BookAuthor = Convert.ToString(dr["BookAuthor"]),
                        BookISBN = Convert.ToString(dr["BookISBN"]),
                        BookCopies = Convert.ToInt32(dr["BookCopies"]),
                        BookPrice = Convert.ToInt32(dr["BookPrice"]),
                    });
                }
                dgBorrow.ItemsSource = lst;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Some unknown exception is occured!!!, Try again..");
            }

        }
        //REQUEST TO BORROW A BOOK FROM BORROW BOOK TABLE =>PL
        private void BtnRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book book = dgBorrow.SelectedItem as Book;
                
                if (book != null)
                {
                    if (book.BookCopies == 0)
                    {
                        MessageBox.Show("Book is empty...");
                    }
                    else
                    {
                        int BookCopy = book.BookCopies - 1;
                        BookBL bookBL = new BookBL();
                        UserRequestBL userRequestBL = new UserRequestBL();
                        string isDone1 = bookBL.UpdateBookBL(book.BookId, book.BookName, book.BookAuthor, book.BookISBN, book.BookPrice, BookCopy);
                        bool isDone2 = GetIsDone2(book, userRequestBL);
                        if (isDone1 == "true" && isDone2 == true)
                        {
                            MessageBox.Show("Requested successfully..");
                            InitializeUserBorrow();
                        }
                        else
                        {
                            MessageBox.Show("Try again..");
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Select a book to request...");
                }
                
            }
            catch (Exception ex)
            {
                
                //MessageBox.Show("Some unknown exception is occured!!!, Try again..");
                MessageBox.Show(ex.Message);
            }
        }

        private bool GetIsDone2(Book book, UserRequestBL userRequestBL)
        {
            return userRequestBL.AddRequestBL(book.BookId, book.BookName, userId);
        }
    }
}
