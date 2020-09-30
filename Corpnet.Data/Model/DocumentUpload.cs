using Corpnet.Entities;
//using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;



namespace Corpnet.Data.Model
{
   public class DocumentUpload 
    {
        [Key]
        public int id { get; set; }
        public string DocName { get; set; }
        public int Parent_id { get; set; }
        public string DocPath { get; set; }
        
        //public string DocDescription { get; set; }
        //public string Thumbnail { get; set; }
        //public int DocSize { get; set; }
        //public string DocType { get; set; }
        //public int SortOrder { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public bool IsActive { get; set; }

      // [JsonConverter(typeof(Base64FileJsonConverter))]
      //  public byte[] ImageData { get;  }
    //  public string ImageData { get; set; }
      //  public IFormFile ImageData { get; set; }
    }
}
