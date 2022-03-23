using SharpAdbClient;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;

namespace SnapCrap_v1
{
    internal class SnapCrap
    {
        public static bool isDownloading = false;
        static void Main(string[] args)
        {
            
            // IF Banner function return true exit
            if (Banner()) { 
                AdbStopOnExit();
                return;
            }

            if (!Directory.Exists("platform-tools"))
            {
                DownloadSDK();
                ExtractSDK();
            }
            AdbStartFirst();

            DeviceConnection DC = new DeviceConnection();
            DC.MonitorConnection();

            // if startserver function return true exit
            if (StartServer()) { 
            AdbStopOnExit();
            return;
            }


            //platform-tools directory doesn't exist download adb and unzip it (unzipping it will give us the directory we need)
            
            // else directly download the script for android to control all the procedure 
            DownloadSnapcrap();
            
            if (UploadFile("sdcard/snapcrap.sh", "snapcrap.sh")) { return; }
            
            var ADBClient = new AdbClient();
            var devices = ADBClient.GetDevices().ToArray();
            var CurrentDevice = devices[0];
            var receiver = new ConsoleOutputReceiver();

            Ext();
            ADBClient.ExecuteRemoteCommand("su -c sh /sdcard/snapcrap.sh", CurrentDevice, receiver);
            ADBClient.ExecuteRemoteCommand("cd sdcard", CurrentDevice, receiver);
            Console.WriteLine("We're Finished!");


        }

        static bool StartServer()
        {
            AdbServer server = new AdbServer();
            server.StartServer($"{Directory.GetCurrentDirectory()}/platform-tools/adb.exe", restartServerIfNewer: true);
            var ADBClient = new AdbClient();
            var devices = ADBClient.GetDevices().ToArray();
            

            if (devices == null || devices.Length == 0)
            {
                Console.WriteLine("No Device Connected! Exiting");
                return true;
            }

            var CurrentDevice = devices[0];
            var receiver = new ConsoleOutputReceiver();
            var CheckAppStatus = new ConsoleOutputReceiver();
            ADBClient.ExecuteRemoteCommand("getprop ro.product.model", CurrentDevice, receiver);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connected Device: " + receiver.ToString());
            ADBClient.ExecuteRemoteCommand("pm list packages | grep com.snapchat.android", CurrentDevice, CheckAppStatus); 
            string package = CheckAppStatus.ToString();

            if (package == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Snapchat Not Found! Press Any Key To Exit...");
                Console.ReadLine();
                return true;
            }
            
                Console.WriteLine("Snapchat Found!");
                Console.WriteLine("Has anyone ever found a lost treasure?");
                return false;
       

        }
        static void DownloadSDK()
        {
            File.Delete("adb.zip");
            Console.WriteLine("Downloading Android SDK Tools");
            var DownloadSDK = new WebClient();
            DownloadSDK.DownloadFile("https://dl.google.com/android/repository/platform-tools-latest-windows.zip", "adb.zip");
            Console.WriteLine("Download Completed ");
        }
        static void DownloadSnapcrap()
        {
            File.Delete("snapcrap.sh");
            Console.WriteLine("Downloading Snapcrap");
            var DownloadSDK = new WebClient();
            DownloadSDK.DownloadFile("https://gist.githubusercontent.com/V3rB0se/db843dd1691318409570d95dd8108635/raw/6948418f9668b939e6b7487d2cd68929097579d3/snapcrap.sh", "snapcrap.sh");
            Console.WriteLine("Download Completed ");
        }

        static void ExtractSDK()
        {
            Console.WriteLine("Extracting Android SDK Tools");
            var AdbPath = $"{Directory.GetCurrentDirectory()}/adb.zip";
            var AdbFolder = $"{Directory.GetCurrentDirectory()}/";
            System.IO.Compression.ZipFile.ExtractToDirectory(AdbPath, AdbFolder);
        }
        static bool UploadFile(string Location, string CurrentFile)
        {
            var ADBClient = new AdbClient();
            var devices = ADBClient.GetDevices().ToArray();
            if (devices == null || devices.Length == 0)
            {
                Console.WriteLine("No Device Connected! Exiting");
                AdbStopOnExit();
                return true;
            }
            
                var CurrentDevice = devices[0];
                SyncService service = new SyncService(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)), CurrentDevice);
                using (Stream stream = File.OpenRead(CurrentFile))
                {
                    service.Push(stream, Location, 666, DateTime.Now, null, CancellationToken.None);
                }
                return false;
            
        }

        static void Ext()
        {
            Console.Write("Extracting");
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
        }
        static bool PromptConfirmation(string confirmText)
        {
            Console.Write(confirmText + " [y/n] : ");
            ConsoleKey response = Console.ReadKey(false).Key;
            Console.WriteLine();
            return (response == ConsoleKey.Y);
        }
        static bool Banner()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("███████ ███    ██  █████  ██████   ██████ ██████   █████  ██████  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("██      ████   ██ ██   ██ ██   ██ ██      ██   ██ ██   ██ ██   ██ ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("███████ ██ ██  ██ ███████ ██████  ██      ██████  ███████ ██████  ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("     ██ ██  ██ ██ ██   ██ ██      ██      ██   ██ ██   ██ ██      ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("███████ ██   ████ ██   ██ ██       ██████ ██   ██ ██   ██ ██      ");
            Console.WriteLine("\n----------------------------Hoch + Alynx------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The script deletes all contents in the folder snapchat_exports before copying the new data into it.\nBefore running the script, make sure you have saved all data within this folder. if you've used it before");
            if (PromptConfirmation("Continue?"))
            {
                return false;
            }
            Console.WriteLine("Exiting! ");
            return true;

        }
        static void AdbStartFirst()
        {
            
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo()
            {
                WorkingDirectory = $"{Directory.GetCurrentDirectory()}/platform-tools",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false,
                Arguments = "/C adb.exe start-server"
            };
            process.StartInfo = startInfo;
            process.Start();
            Console.WriteLine("Starting ADB server...");
            Thread.Sleep(5000);

        }
        static void AdbStopOnExit()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo()
            {
                WorkingDirectory = $"{Directory.GetCurrentDirectory()}/platform-tools",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false,
                Arguments = "/C adb.exe kill-server"
            };
            process.StartInfo = startInfo;
            process.Start();
            Console.WriteLine("Stoping ADB server...");
            Thread.Sleep(5000);
        }


    }
}
class DeviceConnection
{
    public void MonitorConnection()
    {
        var monitor = new DeviceMonitor(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)));
        monitor.DeviceConnected += this.OnDeviceConnected;
        monitor.DeviceDisconnected += this.OnDeviceDisconnected;
        monitor.Start();
    }
    public void OnDeviceConnected(object sender, DeviceDataEventArgs e)
    {
        Console.WriteLine($"The device has connected to this PC");
    }
    public void OnDeviceDisconnected(object sender, DeviceDataEventArgs e)
    {
        Console.WriteLine($"The device has Disconnected ");
    }
}