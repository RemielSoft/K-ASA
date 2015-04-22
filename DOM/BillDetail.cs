﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOM
{
    [Serializable]
    public class BillDetail:Base
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string measrment { get; set; }
        public int Box { get; set; }
        public int Pieces { get; set; }
        public string ItemName { get; set; }
        public string BoxMeasrument { get; set; }
        public string PiecesMeasrument { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int ItemId { get; set; }
        public decimal TotalAmountWithTax { get; set; }
        public decimal AmountWithTax { get; set; }
        public decimal PerunitCost { get; set; }
        public decimal serviceTax { get; set; }
        public decimal VAT { get; set; }
        public DateTime billDate { get; set; }
    }
}
