using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model
{
    public class Order
    {
        public Guid Id { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTimeOffset CreateDatetime { get; set; }

        public DeliveryStatus Status { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
        
        
        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }
    }
    
}
