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
    /// Interaction logic for EditCustomer.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        private Customer CustomerToEdit;
        private ApiAccessManager ApiAccess;

        public EditCustomerWindow(Customer customer, ApiAccessManager apiAccess)
        {
            InitializeComponent();
            this.CustomerToEdit = customer;
            this.ApiAccess = apiAccess;
            FillTextBox(this.CustomerToEdit);
        }

        private void FillTextBox(Customer customer) {
            CustomerIdTextBox.Text = customer.CustomerId.ToString();
            FirstNameTextBox.Text = customer.FirstName.ToString();
            LastNameTextBox.Text = customer.LastName.ToString();
            EmailTextBox.Text = customer.Email.ToString();
            PhoneNumberTextBox.Text = customer.PhoneNumber.ToString();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            FillTextBox(this.CustomerToEdit);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            //only proceed after validation
            this.CustomerToEdit.FirstName = firstName;
            this.CustomerToEdit.LastName = lastName;
            this.CustomerToEdit.Email = email;
            this.CustomerToEdit.PhoneNumber = phoneNumber;
            if (await this.ApiAccess.UpdateCustomerById(this.CustomerToEdit.CustomerId, this.CustomerToEdit))
                System.Windows.MessageBox.Show("Successfully updated.");
            else
                System.Windows.MessageBox.Show("Record not updated.");
            this.Close();
        }
    }
}
