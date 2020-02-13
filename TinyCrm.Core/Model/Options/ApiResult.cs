using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public class ApiResult<T>
    {
        public T Data{get; set;}

        public string ErrorText { get; set;}

        public StatusCode ErrorCode { get; set; }
    }
}
