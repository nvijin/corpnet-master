using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Entities.Model
{
    public class ErrorLog : IEntity
    {
        [Key]
        public int id { get; set; }
        public string Url { get; set; }
        public string MethodName { get; set; }
        public string ErrorType { get; set; }
        public string ErrorDetails { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int IsDeleted { get; set; }
    }
}
