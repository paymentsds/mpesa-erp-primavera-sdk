using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primavera.Paymentsds.Mpesa.Models
{
    public class Invoice
    {
        public double amount { get; set; }

        public string invoice_number { get; set; }

        public string reference { get; set; }

        public string phonenumber { get; set; }
    }
}
