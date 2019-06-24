using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using IWshRuntimeLibrary;

namespace GetVersion
{
    class Program
    {
        public static string pathNew = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        

        static void Main(string[] args)
        {
            pathNew += "\\GouvisPrinting\\";

            dropBoxHandler dBH = new dropBoxHandler();
            try
            {
                dBH.downloadVersion().Wait(5000);
            }
            catch {
                Console.WriteLine("\nCouldn't get version file.\n");
            }
            if (compareVersion(pathNew)) {
                try
                {
                    dBH.downloadZipFile().Wait(5000);
                }
                catch { }
                exactFile(pathNew);

                if (System.IO.File.Exists("GEPRINT.zip"))
                {
                    try
                    {
                        System.IO.File.Delete("GEPRINT.zip");

                    }
                    catch
                    {
                        Console.WriteLine("\nProgram.cs -- Main func: Error: another process is blocking GEPRINT.zip, please check this again.\n");
                    }
                }
                else
                {
                    Process.Start("https://www.dropbox.com/sh/e1b6t8358txpb97/AABxnaZLHYcN1JLhhaInxAYVa?dl=1");

                }

            }
            if (System.IO.File.Exists(pathNew + "GEPRINT\\AutoCadPrinting.exe"))
            {
                Process.Start(pathNew + "GEPRINT\\AutoCadPrinting.exe");
            }
            if (System.IO.File.Exists("version.gev"))
            {
                try
                {
                    System.IO.File.Delete("version.gev");
                    
                }
                catch
                {
                    Console.WriteLine("\nProgram.cs -- Main func: Error: another process is blocking deleting this version.gev, please check this again.\n");
                }
            }
            addShortcutToDesktop();
        }

        private static void exactFile(string path)
        {    
            if (System.IO.File.Exists("GEPRINT.zip"))
            {
                if (Directory.Exists(pathNew + "GEPRINT\\"))
                {
                    try
                    {
                        Directory.Delete(pathNew + "GEPRINT\\", true);
                        ZipFile.ExtractToDirectory("GEPRINT.ZIP", path);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    ZipFile.ExtractToDirectory("GEPRINT.ZIP", path);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nCOULDN'T GET FILE GEPRINT.ZIP, CONTACT PROGRAMMER\n");
                Console.WriteLine("press <Enter> to continue...");
                Console.ReadLine();
            }
        }

        private static bool compareVersion(string path)
        {
            if (System.IO.File.Exists("version.gev"))
            {
                if (!System.IO.File.Exists(pathNew + "GEPRINT\\version.gev")) return true;

                double currentVersion = Convert.ToDouble(System.IO.File.ReadAllText(path + "GEPRINT\\version.gev"));
                double newVersion = Convert.ToDouble(System.IO.File.ReadAllText("version.gev"));
                if (newVersion > currentVersion)
                {
                    stopKillGEPrint();
                    return true;
                }
                return false;
            }
            Console.Clear();
            Console.WriteLine("COULDN'T GET FILE VERSION.GEV, CONTACT PROGRAMMER\n");
            Console.WriteLine("press <Enter> to continue...");
            Console.ReadLine();
            return false;
        }

        private static bool replaceOldFileWithNewFile()
        {
            if(AppDomain.CurrentDomain.BaseDirectory == pathNew + "PRINTS\\")
            {
                return false;
            }
            if (Directory.Exists(pathNew + "PRINTS"))
            {
                try
                {
                    Directory.Delete(pathNew + "PRINTS", true);
                }
                catch
                {
                    return false;
                }
            }
            if(!Directory.Exists(pathNew + "PRINTS"))
            {
                Directory.CreateDirectory(pathNew + "PRINTS");
            }
            foreach (string newPath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.*", SearchOption.AllDirectories))
                System.IO.File.Copy(newPath, newPath.Replace(AppDomain.CurrentDomain.BaseDirectory, pathNew + "PRINTS\\"), true);
            return true;
        }

        private static void addShortcutToDesktop()
        {
            if (replaceOldFileWithNewFile())
            {
                string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                object shDesktop = (object)"Desktop";
                WshShell shell = new WshShell();
                string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\GEPRINT.lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.Description = "GE Print";
                shortcut.Hotkey = "Ctrl+Shift+G";
                shortcut.TargetPath = pathNew + "PRINTS\\" + "GetVersion.exe";
                shortcut.Save();
            }
        }

        private static void stopKillGEPrint()
        {
            try
            {

                foreach (Process proc in Process.GetProcessesByName("AutoCAD component"))
                {
                    proc.Kill();
                }

            }
            catch
            {
                Console.WriteLine("\nCouldn't kill 'AutoCAD component' process");
            }
            try
            {
                foreach (Process proc in Process.GetProcessesByName("accoreconsole"))
                {
                    proc.Kill();
                }
            }
            catch
            {
                Console.WriteLine("\nCouldn't kill 'accoreconsole.exe' process");
            }
            try
            {
                foreach (Process proc in Process.GetProcessesByName("PrintForm"))
                {
                    proc.Kill();
                }
            }
            catch
            {
                Console.WriteLine("\nCan't Kill PrintForm.exe");
            }
            try
            {
                foreach (Process proc in Process.GetProcessesByName("AutocadPrinting"))
                {
                    proc.Kill();
                }
            }
            catch
            {

            }
        }
    }
}
