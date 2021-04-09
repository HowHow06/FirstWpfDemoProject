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
using FirstWpfDemoProject.Utils;

namespace FirstWpfDemoProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiAccessManager ApiAccess;
       
        public MainWindow()
        {
            InitializeComponent();
            ApiAccess = new ApiAccessManager("http://localhost:8000/", "api/customers");
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            RefreshCustomerDataGrid();
        }

        private async void RefreshCustomerDataGrid() {
            CustomerDataGrid.ItemsSource = await ApiAccess.GetCustomers();
        }


        private async void RunSearchCustomerButton_Clicked(object sender, RoutedEventArgs e)
        {
            string keywords = SearchCustomerNameTextBox.Text;
            CustomerDataGrid.ItemsSource = await ApiAccess.GetCustomers(keywords);
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
            Window editCustomerWindow = new EditCustomerWindow(selectedCustomer, ApiAccess);
            editCustomerWindow.ShowDialog();
            CustomerDataGrid.SelectedIndex = -1;//reset selected index
            RefreshCustomerDataGrid(); //refresh data grid
        }

        private async void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CustomerDataGrid.SelectedIndex; //get selected index
            if (selectedIndex == -1)
            { //if no row is selected
                System.Windows.MessageBox.Show("Please select a record");
                return;
            }
            var selectedCustomer = (Customer)CustomerDataGrid.SelectedItem;
            if (await ApiAccess.DeleteCustomerById(selectedCustomer.CustomerId)) 
                System.Windows.MessageBox.Show("Successfully deleted!");
            else
                System.Windows.MessageBox.Show("Deletion failed.");
            RefreshCustomerDataGrid(); //refresh data grid
        }

        private void CreateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Window createCustomerWindow = new CreateCustomerWindow(ApiAccess);
            createCustomerWindow.ShowDialog();
            RefreshCustomerDataGrid(); //refresh data grid
        }

        
    }
}
