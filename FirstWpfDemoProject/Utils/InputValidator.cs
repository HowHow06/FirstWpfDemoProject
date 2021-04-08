using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FirstWpfDemoProject
{
    public class InputValidator
    {
        /// <summary>
        /// Validate first and last name
        /// </summary>
        /// <param name="name">first name or last name</param>
        public static bool IsValidName(string name) {
            Regex regex = new Regex(@"^[A-Z\s]+$");
            return regex.IsMatch(name.ToUpper());
        }

        /// <summary>
        /// Validate email
        /// </summary>
        /// <param name="email">email</param>
        public static bool IsValidEmail(string email) {
            //email pattern, some domain(after @) can be ip address, i added extra escape to escape the java escape key
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@(((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})(\]?)$)|(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4})$)");
            return regex.IsMatch(email);
        }

        /// <summary>
        /// Validate phone number
        /// </summary>
        /// <param name="phoneNumber">phone number, must include extension</param>
        public static bool IsValidPhoneNumber(string phoneNumber) {
            Regex regex = new Regex(@"(^\+601[\d]-[\d]{6,9}$)|(^\+608[\d]-[\d]{6,8}$)");
            return regex.IsMatch(phoneNumber);
        }
    }
}
