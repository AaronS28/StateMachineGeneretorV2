using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachineGeneretorV2
{
    public class PageModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string CurrentPage { get; set; }
        public List<string> Methods { get; set; }
        public List<string> SubPages { get; set; }

        public PageModel()
        {
            Methods = new List<string>();
            SubPages = new List<string>();
        }
    }
}
