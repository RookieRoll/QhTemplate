using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.AdminWeb.Utils
{
    public class PathUtil
    {
        public static string PathReplace(string content)
        {
            content = content.Replace("\"/upload", "\"http://localhost:59932/upload");
            return content;
        }
    }
}
