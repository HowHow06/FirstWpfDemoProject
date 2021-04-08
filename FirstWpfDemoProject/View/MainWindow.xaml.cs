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

namespace FirstWpfDemoProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string CONNECTION_NAME = "DemoCustomerDb";
        private DataAccessManager db;
        public MainWindow()
        {
            InitializeComponent();
            db = new DataAccessManager(CONNECTION_NAME);
            CustomerDataGrid.ItemsSource = db.GetAllCustomers();
        }


        private void RunSearchCustomerButton_Clicked(object sender, RoutedEventArgs e)
        {
            string keywords = SearchCustomerNameTextBox.Text;
            CustomerDataGrid.ItemsSource = db.SearchCustomersByName(keywords);
        }

        private void SearchCustomerNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                RunSearchCustomerButton_Clicked(sender, null);
            }
        }

        private void EditCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CustomerDataGrid.SelectedIndex; //get selected index
            if (selectedIndex == -1)
            { //if no row is selected
                System.Windows.MessageBox.Show("Please select a record");
                return;
            }
            var selectedCustomer = (Customer)CustomerDataGrid.SelectedItem;
            Window editCustomerWindow = new EditCustomerWindow(db.GetCustomerById(selectedCustomer.CustomerId), db);
            editCustomerWindow.ShowDialog();
            CustomerDataGrid.SelectedIndex = -1;//reset selected index
            CustomerDataGrid.ItemsSource = db.GetAllCustomers(); //refresh data grid
        }

        private void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CustomerDataGrid.SelectedIndex; //get selected index
            if (selectedIndex == -1)
            { //if no row is selected
                System.Windows.MessageBox.Show("Please select a record");
                return;
            }
            var selectedCustomer = (Customer)CustomerDataGrid.SelectedItem;
            db.DeleteCustomerById(selectedCustomer.CustomerId);
            CustomerDataGrid.ItemsSource = db.GetAllCustomers(); //refresh data grid
        }

        private void CreateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Window createCustomerWindow = new CreateCustomerWindow(db);
            createCustomerWindow.ShowDialog();
            CustomerDataGrid.ItemsSource = db.GetAllCustomers(); //refresh data grid
        }
    }
}
