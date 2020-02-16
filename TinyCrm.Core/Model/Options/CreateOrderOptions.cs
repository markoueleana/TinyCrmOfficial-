using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
   public class CreateOrderOptions
    {
        public List<Guid> ProductIds{ get; set; }

        public string DeliveryAddress { get; set; }

        public DateTimeOffset CreateDatetime { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }
    }
}
