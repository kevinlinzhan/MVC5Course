using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ClientTotal
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public decimal OrderTotal { get; set; }
    }
}