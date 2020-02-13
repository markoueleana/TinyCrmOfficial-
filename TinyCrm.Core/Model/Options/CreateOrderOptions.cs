using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
   public class CreateOrderOptions
    {
        
        public string DeliveryAddress { get; set; }

        public DateTimeOffset CreateDatetime { get; set; }

        public Customer Customer { get; set; }

        public Guid CustomerId { get; set; }
    }
}
