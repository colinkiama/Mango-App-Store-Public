using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mangoAppStore.Helpers
{
   public class youtubeURLParsing
    {
        public static string getYouTubeEmbedLink(string url)
        {
            string videoID = getVideoIDFromUrl(url);
            string embedLinkToReturn = createEmbedLink(videoID);
            return embedLinkToReturn;
        }

        public static string getYoutubeVideoID(string url)
        {
            string videoID = getVideoIDFromUrl(url);
            return videoID;
        }

        private static string createEmbedLink(string videoID)
        {
            string embedLink = $"https://www.youtube.com/embed/{videoID}";
            return embedLink;
        }

        private static string getVideoIDFromUrl(string url)
        {

            string videoID = "";


            if (url.Contains("http"))
            {
                if (url.Contains("https"))
                {
                    url = url = url.Replace("https://", "");
                }
                else
                {
                    url = url.Replace("http://", "");
                }
            }


            //and return nothing in final else



            if (url.Contains("www"))
            {
                url = url.Replace("www.", "");
            }

            //Handle all m.youtube cases in one if
            //all youtube cases in else if
            //all youtu.be in another else if

            if (url.Contains("m.youtube.com"))
            {
                if (url.Contains("m.youtube.com/embed"))
                {
                    videoID = url.Replace("m.youtube.com/embed/", "");
                }
                else
                {
                    videoID = url.Replace("m.youtube.com/watch?v=", "");
                }
            }

            else if (url.Contains("youtube.com"))
            {
                if (url.Contains("youtube.com/embed"))
                {
                    videoID = url.Replace("youtube.com/embed/", "");
                }
                else
                {
                    videoID = url.Replace("youtube.com/watch?v=", "");
                }

            }








            else if (url.Contains("youtu.be"))
            {
                videoID = url.Replace("youtu.be/", "");

            }

            else videoID = $"{url} is invalid.";

            byte videoIDLength = (byte)videoID.Length;
            if (videoID[videoIDLength - 2] == '?' && videoID[videoIDLength - 1] == 'a')
            {
                videoID = videoID.Remove(videoIDLength - 2, 2);
            }

            if (videoID.Contains("?autoplay"))
            {
                int startOfRemoval = videoID.LastIndexOf("?autoplay");
                videoID = videoID.Remove(startOfRemoval);
            }




            return videoID;
        }
    }
}
