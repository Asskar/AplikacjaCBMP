using System;
using System.Collections.Generic;

namespace AppCBMP.Model
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateOfRelease { get; set; }
        public int PaymentTime { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}