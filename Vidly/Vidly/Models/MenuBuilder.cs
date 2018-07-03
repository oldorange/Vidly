using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MenuBuilder
    {
        public int[] allowedItemIndices { get; set; }

        public int[] Items { get; set; }

        public string errorCode { get; set; }

        public string errorMessage { get; set; }
    }
}