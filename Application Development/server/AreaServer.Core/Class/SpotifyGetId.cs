using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaServer.Core.Class
{
    public class SpotifyGetId
    {
        public SpotifyGetId() { }

        public string GetIdFromUrl(string url)
        {
            var startIndex = url.LastIndexOf("/") + 1;
            return url.Substring(startIndex);
        }
    }
}
