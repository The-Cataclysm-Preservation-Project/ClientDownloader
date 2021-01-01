using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace ClientDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> files = new Dictionary<string, string>()
            {
                { "E1FC69A72E4E23A96DBD535B372974A8", "BackgroundDownloader.exe" },
                { "24433A51A32335A39D2AF8CB55C467D3", "Battle.net.dll" },
                { "82EF43D5F8D1B1C87C3505ECD241FFF6", "Blizzard Updater.exe" },
                { "4003E34416EBD25E4C115D49DC15E1A7", "dbghelp.dll" },
                { "57E72CAE12091DAFA29A8E4DB8B4F1D1", "divxdecoder.dll" },
                { "C7C7121E1DD819088403F514FEBD06BA", "Launcher.exe" },
                { "D34B3DA03C59F38A510EAA8CCC151EC7", "Microsoft.VC80.CRT.manifest" },
                { "1169436EE42F860C7DB37A4692B38F0E", "msvcr80.dll" },
                { "DE5A2E274F2D3F2B89A2E6EC9CD8FD2A", "Wow.exe" },
                { "78766BBBFC6F9E5DA5D930CB11F0A1E1", "WowError.exe" },
                { "E198F00FE056B24ED58B36E1C6A048F4", "Repair.exe" }
            };

            string server = "http://blizzard.vo.llnwd.net:80/o16/content/repair/wow/";

            using (WebClient webClient = new WebClient())
            {
                foreach (var file in files)
                {
                    string md5Hash = "";
                    // check if file is valid
                    if (File.Exists(file.Value))
                    {
                        using (var md5 = MD5.Create())
                        {
                            using (var stream = File.OpenRead(file.Value))
                            {
                                md5Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToUpper();
                            }
                        }

                        if (file.Key.Equals(md5Hash))
                        {
                            Console.WriteLine($"{file.Value} already exists. Skip.");
                            Console.WriteLine();
                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"Existing {file.Value} is corrupted!");
                            Console.WriteLine($"Renamed corrupted file {file.Value} to {Path.GetFileNameWithoutExtension(file.Value)}_BACKUP{Path.GetExtension(file.Value)}");
                            Console.WriteLine();
                            File.Move(file.Value, Path.GetFileNameWithoutExtension(file.Value) + "_BACKUP" + Path.GetExtension(file.Value));
                        }
                    }

                    Console.WriteLine($"Downloading: {file.Value} ...");
                    string url = server + file.Key[0] + "/" + file.Key[1] + "/" + file.Key;
                    webClient.DownloadFile(url, file.Value);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();
                }
            }

            Console.WriteLine("Creating WoW.mfil...");
            using (StreamWriter sw = File.CreateText("WoW.mfil"))
            {
                sw.WriteLine("version=2");
                sw.WriteLine("server=akamai");
                sw.WriteLine("	location=http://dist.blizzard.com.edgesuite.net/wow-pod-retail/EU/15050.direct/");
                sw.WriteLine("manifest_partial=wow-15595-0C3502F50D17376754B9E9CB0109F4C5.mfil");
            }

            Console.WriteLine("Sucessfully downloaded client data!");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
