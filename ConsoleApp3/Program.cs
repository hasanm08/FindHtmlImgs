using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAllImages();
            Console.ReadKey(true);
        }
        public static void SaveImage(string filename, ImageFormat format,string imageUrl)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
            {
                bitmap.Save(filename, format);
            }

            stream.Flush();
            stream.Close();
            client.Dispose();
        }
        public static void GetAllImages()
        {
            WebClient x = new WebClient();
            string source = /*Read();*/x.DownloadString(@"https://www.asriran.com/fa/news/158518/زیباترین-تصاویر-مینیاتوری-ایرانی");
            var b = Regex.Match(source, @"<img style="+ "\"" + "border: medium none;"+ "\"" +" (.*?)>g");
            Console.WriteLine(b.Value);
            MatchCollection c = Regex.Matches(b.ToString(), "src=\"(.*?)\"");
            string res = "";
            int i = 0;
            foreach (Match item in c)
            {
                var tmp = Regex.Replace(item.Value, "src=\"", "");
                tmp = Regex.Replace(tmp, "\"", "");
                Console.WriteLine(tmp);
                res +=" \n\n\n\n "+ tmp;
                SaveImage(i.ToString() , ImageFormat.Png, tmp);
                i++;
            }
            //Write(res);
        }
        public static string Read()
        {
            using (StreamReader sr = new StreamReader(@"C:\Users\hasanm08\Downloads\s.html"))
            {
                return sr.ReadToEnd();
            }
        }
        public static void Write(string s)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\hasanm08\Downloads\url.txt"))
            {
                    sw.WriteLine(s);
            }

        }
    }
}
