﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public class SearchOrderOptions
    {
        public Guid OrderId { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTimeOffset CreateDatetime { get; set; }

        public string VatNumber{ get; set; }

        public Guid CustomerId { get; set; }
    }
}
