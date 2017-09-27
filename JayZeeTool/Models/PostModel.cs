using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JayZeeTool.Models
{
    public class PostModel
    {
        public string requestURL { get; set; }
        public string postJson { get; set; }

        public string responseContent { get; set; }
    }
}