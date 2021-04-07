using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWpfDemoProject
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        /// <summary>
        /// Compare the customer by id.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Customer customer &&
                   CustomerId == customer.CustomerId;
        }

        public override int GetHashCode()
        {
            return 1647736892 + CustomerId.GetHashCode();
        }

    }
}
