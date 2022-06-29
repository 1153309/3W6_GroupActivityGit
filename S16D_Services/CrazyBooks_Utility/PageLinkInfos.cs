using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyBooks_Utility
{
    public class PageLinkInfos
    {
        public PageLinkInfos()
        { }
        public PageLinkInfos(string classSuffix, string action, string icon, string title, bool usesId = false)
        {
            ClassSuffix = classSuffix;
            Action = action;
            Icon = icon;
            Title = title;
            UsesId = usesId;
        }

        public string ClassSuffix { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public bool UsesId { get; set; }
    }
}
