using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace EZAutoUpdate
{
    public class Updates
    {
        public const string linkJsonVersion = "https://raw.githubusercontent.com/Ja-Sa-La/League-Account-Manager/master/Version";

        public const string linkProject = "https://github.com/Ja-Sa-La/League-Account-Manager";
        public const string fileName = "League_Account_Manager.exe";
        public const string linkDownloadReleases = linkProject + "/releases/latest/download/" + fileName;

        public static string getNewVersion(string linkJsonVersion)
        {
            HttpRequest rq = new HttpRequest();
            string html = rq.Get(linkJsonVersion).ToString();

            JObject jsson = JObject.Parse(html);
            string version = jsson["Version"].ToString();
            return version;
        }

        public static string getCurrentVersion()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Version.json");
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                JObject jsonObject = JObject.Parse(jsonContent);
                string currentVersion = jsonObject["Version"].ToString();
                return currentVersion;
            }
            Console.WriteLine("Thiếu json Version");
            return null;
        }

        public static void downloadReleases(string linkReleases, string fileName)
        {
            HttpRequest rq = new HttpRequest();
            HttpResponse response = rq.Get(linkReleases);
            if (response.IsOK)
            {
                Console.WriteLine("Đang Update phiên bản mới nhất");
                byte[] responseData = response.ToBytes();
                int totalBytes = responseData.Length;
                int bytesDownloaded = 0;
                const int chunkSize = 1024; // Kích thước mỗi đoạn download

                File.WriteAllBytes(Path.Combine(Environment.CurrentDirectory, fileName), responseData);
                Console.WriteLine("Thành công");
            }
            else
            {
                Console.WriteLine("Thất bại");
            }

        }

        public static void AutoUpdate()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Version.json");
            if (File.Exists(filePath))
            {
                string currentVersion = getCurrentVersion();
                string newVersion = getNewVersion(linkJsonVersion);
                if (currentVersion != newVersion)
                {
                    downloadReleases(linkDownloadReleases, fileName);

                    string jsonContent = File.ReadAllText(filePath);
                    JObject jsonObject = JObject.Parse(jsonContent);

                    jsonObject["Version"] = newVersion;
                    string updatedJsonContent = jsonObject.ToString();
                    File.WriteAllText(filePath, updatedJsonContent);
                }
                else
                {
                    Console.WriteLine("đã là phiên bản mới nhất");
                }
            }
            else
            {
                JObject newJsonObject = new JObject();
                newJsonObject["Version"] = "mrdunghr";
                string newJsonContent = newJsonObject.ToString();

                File.WriteAllText(filePath, newJsonContent);

                string currentVersion = getCurrentVersion();
                string newVersion = getNewVersion(linkJsonVersion);
                if (currentVersion != newVersion)
                {
                    downloadReleases(linkDownloadReleases, fileName);

                    string jsonContent = File.ReadAllText(filePath);
                    JObject jsonObject = JObject.Parse(jsonContent);

                    jsonObject["Version"] = newVersion;
                    string updatedJsonContent = jsonObject.ToString();
                    File.WriteAllText(filePath, updatedJsonContent);
                }
                else
                {
                    Console.WriteLine("đã là phiên bản mới nhất");
                }
            }
        }
    }
}
