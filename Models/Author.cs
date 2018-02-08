using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Www.Models
{
    public class Author
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string FacebookProfileUrl { get; set; }
        public string TwitterAccount { get; set; }
    }
}