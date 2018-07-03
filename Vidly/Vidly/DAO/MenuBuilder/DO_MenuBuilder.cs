using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DAO.MenuBuilder
{
    public class DO_MenuBuilder
    {
        public int[] allowedItemIndices { get; set; }

        public int[] Items { get; set; }

        public string errorCode { get; set; }

        public string errorMessage { get; set; }
    }
}