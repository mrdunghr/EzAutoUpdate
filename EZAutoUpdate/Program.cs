using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xNet;

namespace EZAutoUpdate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Updates.AutoUpdate();

            //Process.Start("notepad.exe");// ver 1
            Process.Start("chrome.exe");// ver 2
        }
    }
}
