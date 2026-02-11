using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shard.ErrorModels
{
    public class ValidationErrorResponse : ErrorDetails
    {
        public IEnumerable<ValidationError> Errors { get; set; }
    }
    public class ValidationError
    {
        public string Field { get; set; }   
        public IEnumerable<string> Errors { get; set; }
    }
}
