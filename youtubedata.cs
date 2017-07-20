using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GSATPES_Data1.Models
{
    public class YouTubeData
    {
        public static float getdata(Double latitude, Double longitude, String radius, String yy, String mm, String dd, String hh, String min, String sec)
        {
            /*  Double latitude, longitude;
              String publishedAfter, publishedBefore, type, radius;

              latitude = 38.924186;
              longitude = -77.28;
              radius = "500m";*/
            String publishedAfter, publishedBefore, type;
            /* publishedAfter = "2015-12-15T23%3A24%3A43.000Z";
             publishedBefore = "2015-12-31T23%3A24%3A43.000Z";*/
            publishedAfter = yy + "-" + mm + "-" + dd + "T" + hh + "%3A" + min + "%3A" + sec + ".000Z";
            publishedBefore = yy + "-" + mm + "-" + Convert.ToString(Convert.ToInt32(dd) - 1) + "T" + hh + "%3A" + min + "%3A" + sec + ".000Z";
            type = "video";

            String API_KEY = "AIzaSyD4G_MmyAROWEF8SASYLZmVlx6ChrSICRs";

            //https://www.googleapis.com/youtube/v3/search?part=snippet&location=38.924186%2C-77.28&locationRadius=500m&publishedAfter=2015-12-29T23%3A24%3A43.000Z&publishedBefore=2015-12-31T23%3A24%3A43.000Z&type=video&key={YOUR_API_KEY}

            String URL = "https://www.googleapis.com/youtube/v3/search?part=snippet" +
                "&location={0}%2C{1}" +
                "&locationRadius={2}"
                + "&publishedAfter={3}"
                + "&publishedBefore={4}"
                + "&type={5}"
                + "&key={6}";
            var baseURL = String.Format(URL, latitude, longitude, radius, publishedAfter, publishedBefore, type, API_KEY);
            //String baseURL = "https://www.googleapis.com/youtube/v3/search?part=snippet&location=38.924186%2C-77.28&locationRadius=500m&publishedAfter=2015-12-29T23%3A24%3A43.000Z&publishedBefore=2015-12-31T23%3A24%3A43.000Z&type=video&key=AIzaSyCGJnZrmeDEKAv0yI3gfFqEPtkiMd54MZ0";
            using (var webClient = new WebClient())
            {
                // webClient.Headers.Add("Authorization", "Bearer 2NEWI72RJPV4KP2G64V3");
                // Send request to an address and get respose
                var response = webClient.DownloadString(baseURL);
                // Console.WriteLine(response);

                YoutubeAPIdata outputdata = JsonConvert.DeserializeObject<YoutubeAPIdata>(response);
               // Console.WriteLine("Total number of posts:{0}", outputdata.pageInfo.totalResults);
                //  Console.ReadKey();
                float totalcount = outputdata.pageInfo.totalResults;
                return totalcount;
            }

        }
    }
    public class PageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }

    public class Id
    {
        public string kind { get; set; }
        public string videoId { get; set; }
    }

    public class Default
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Medium
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class High
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Thumbnails
    {
        public Default @default { get; set; }
        public Medium medium { get; set; }
        public High high { get; set; }
    }

    public class Snippet
    {
        public string publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnails thumbnails { get; set; }
        public string channelTitle { get; set; }
        public string liveBroadcastContent { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public Id id { get; set; }
        public Snippet snippet { get; set; }
    }

    public class YoutubeAPIdata
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string regionCode { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<Item> items { get; set; }
    }
}
