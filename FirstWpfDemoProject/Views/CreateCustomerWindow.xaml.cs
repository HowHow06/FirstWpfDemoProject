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
using System.Windows.Shapes;
using FirstWpfDemoProject.Utils;

namespace FirstWpfDemoProject
{
    /// <summary>
    /// Interaction logic for CreateCustomerWindow.xaml
    /// </summary>
    public partial class CreateCustomerWindow : Window
    {
        private ApiAccessManager ApiAccess;
        public CreateCustomerWindow(ApiAccessManager apiAccess)
        {
            InitializeComponent();
            this.ApiAccess = apiAccess;
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string phoneNumber = PhoneNumberTextBox.Text.Trim().Replace(" ", "");

            StringBuilder errorMsg = new StringBuilder();
            if (firstName.Equals("") || !InputValidator.IsValidName(firstName))
                errorMsg.Append("Invalid first name. \n");

            if (lastName.Equals("") || !InputValidator.IsValidName(lastName))
                errorMsg.Append("Invalid last name. \n");

            if (email.Equals("") || !InputValidator.IsValidEmail(email))
                errorMsg.Append("Invalid email. \n");

            if (phoneNumber.Equals("") || !InputValidator.IsValidPhoneNumber(phoneNumber))
                errorMsg.Append("Invalid phone number. \n");

            //if the error message is not empty
            if (errorMsg.Length > 0)
            {
                System.Windows.MessageBox.Show(errorMsg.ToString());
                return;
            }

            if (await this.ApiAccess.CreateCustomer(new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            }))
                System.Windows.MessageBox.Show("Successfully created!");
            else
                System.Windows.MessageBox.Show("Customer creation failed.");
            this.Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
