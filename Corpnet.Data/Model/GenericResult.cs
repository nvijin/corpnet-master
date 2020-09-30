using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Data.Model
{
    [Keyless]
    public class GenericResult
    {
        public string result { get; set; }

    }
}
