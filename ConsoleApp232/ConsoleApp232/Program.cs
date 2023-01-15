using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace ConsoleApp232
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Random rnd = new Random();
            WebClient x = new WebClient();
            x.Encoding = System.Text.Encoding.UTF8;
            for (int i = 6000; i < 20000; i++)
            {
                string url = "https://www.spreadthesign.com/pl.pl/word/" + i.ToString()+"/";
                try
                {
                    string source = x.DownloadString(url);
                    string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                    int charLocation = title.IndexOf("[", StringComparison.Ordinal);
                    if (charLocation > 0)
                    {
                        title = title.Substring(0, charLocation);
                    }
                    Console.WriteLine(title);
                    File.AppendAllText(@"C:\Users\tymekw\Desktop\baza_pjm.txt", title + Environment.NewLine);
                    int wait = rnd.Next(10, 100);
                    Thread.Sleep(wait);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            Console.ReadKey();
        }
    }
}