using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metin2RFT.Models
{
    public class TestUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}