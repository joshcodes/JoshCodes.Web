using System;
using System.Collections.Generic;
using System.IO;

namespace JoshCodes.Web
{
    public static class UriExtensions
    {
        public static Stream ToStream(this Uri url)
        {
            byte[] imageData = null;

            using (var wc = new System.Net.WebClient())
                imageData = wc.DownloadData(url);

            return new MemoryStream(imageData);
        }
    }
}
