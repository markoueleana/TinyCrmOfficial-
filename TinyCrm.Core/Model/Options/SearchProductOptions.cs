using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
  public class SearchProductOptions
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductCategory Category { get; set; }
    }
}
