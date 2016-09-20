using System;
using System.Collections.Generic;

namespace Model
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateOfRelease { get; set; }
        public int PaymentTime { get; set; }

    }
}