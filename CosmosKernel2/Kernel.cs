using Cosmos.HAL.BlockDevice.Registers;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System.ComponentModel.Design;
using Cosmos.HAL.Drivers;
using Cosmos.HAL;
using System.Buffers;
using Cosmos.Core;

using Cosmos.System;
using Console = System.Console;
using System.Linq.Expressions;
using IL2CPU.API.Attribs;
using Cosmos.Core.Memory;
using SoundTest;
using Youtube_tut;
using SoundTest.Applications.Task_Scheduler;
// NOTE: Proper use of Panic
///catch (Exception e)
///{
///    crash(e.ToString());
///}



namespace CosmosKernel1
{


    public class Kernel : Sys.Kernel
    {
        public static Canvas canvas;// = new Canvas(new Mode(1920, 1080, ColorDepth.ColorDepth32)); //Set the graphic mode: 1920 -> width 1080 -> height
        //set the bitmap you want to be displayed as Build action: Embedded resource
        [ManifestResourceStream(ResourceName = "CosmosKernel2.cursor.bmp")] public static byte[] cursor;
        public static Bitmap curs = new Bitmap(cursor);
        //defines the cursor image
        //Important: If you want to draw a bitmap make sure that it is in 32bpp
        

        public static int bmp_x = 50;
        public static int bmp_y = 50;
        public static bool movable = false;

        public static VGAScreen VScreen = new VGAScreen();

        public void FormatDisk(int index, string format, bool quick = true) { }
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();





        private string current_directory;
        private string curren_directory;
        private string directory;
        public string a;

        public static class BootManager
        {
            public static void Boot()
            {


            }

        }

        public bool IsMBR { get; }


        public static void crash(string reason)
        {
            {

                Console.BackgroundColor = ConsoleColor.Black;
                
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("=====================================================================");
                Console.WriteLine("Aw snap!");
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("PANIC!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("NoNameOS Milestone 3 has encountered an critical error that cant solve.Possible solutions:");
                Console.WriteLine("1.Try to restart your device"); 
                Console.WriteLine("2.If problem still persists,email bug ( aworldman1000@gmail.com ) or create new issue on github");
                Console.WriteLine("=====================================================================");
                Console.Write(Cosmos.Core.CPU.GetCPUBrandString());
                Console.Write("_");
                Console.Write(Cosmos.Core.CPU.GetCPUVendorName());
                Console.Write("_");
                Console.WriteLine("UpTime " + Cosmos.Core.CPU.GetCPUUptime());
                Console.WriteLine("EBP "+Cosmos.Core.CPU.GetEBPValue());
                Console.WriteLine("StackStart "+CPU.GetStackStart());
                CPU.Halt();
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine(reason);
                System.Threading.Thread.Sleep(500);

            }
        }

        protected override void BeforeRun()
        {
            MouseManager.ScreenWidth = 1920;
            MouseManager.ScreenHeight = 1080;
            
            //Set the cursor to the middle of the screen
            MouseManager.X = 1920 / 2;
            MouseManager.Y = 1080 / 2;
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs, true, true);
            foreach (Disk disk in fs.GetDisks())
            {
                disk.DisplayInformation();
            }

            Console.WriteLine(" _   _       _   _                       ___  ____  ");
            Console.WriteLine("| \\ | | ___ | \\ | | __ _ _ __ ___   ___ / _ \\/ ___| ");
            Console.WriteLine("|  \\| |/ _ \\|  \\| |/ _` | '_ ` _ \\ / _ \\ | | \\___ \\ ");
            Console.WriteLine("| |\\  | (_) | |\\  | (_| | | | | | |  __/ |_| |___) |");
            Console.WriteLine("|_| \\_|\\___/|_| \\_|\\__,_|_| |_| |_|\\___|\\___/|____/ ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning! command line is for PROFFESIONAL USE ONLY.");
            Console.WriteLine("Do you want to go in Graphics Mode? (y/n)");
            string inpduta = Console.ReadLine();
            if (inpduta == "Y" || inpduta == "y")
            {
                /*
           You don't have to specify the Mode, but here we do to show that you can.
           To not specify the Mode and pick the best one, use:
           canvas = FullScreenCanvas.GetFullScreenCanvas();
           */
                canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1920, 1080, ColorDepth.ColorDepth32));
                // This will clear the canvas with the specified color.
                a = "graphics";
            }
            else
            {
                a = "text";
            }

            System.Threading.Thread.Sleep(1000);

            foreach (Disk disk in fs.GetDisks())
            {
                Console.BackgroundColor = ConsoleColor.Black;
                disk.DisplayInformation();

            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("CPU:");
            Console.Write(Cosmos.Core.CPU.GetCPUBrandString());
            Console.ForegroundColor = ConsoleColor.Green;
            if (!(Cosmos.Core.CPU.GetCPUBrandString() == ""))
            {
                Console.Write("[OK!]");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[FAIL!]");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  ");
            Console.Write("RAM:" + GCImplementation.GetAvailableRAM() + "MB Avaliable");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[OK!]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine("  ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Shell Started. Type help for help,thanks a lot to dontsmi1e for code support");
            Console.BackgroundColor = ConsoleColor.Green;
            Cosmos.Core.CPU.GetCPUBrandString();
            Console.BackgroundColor = ConsoleColor.Black;
            if (File.Exists("0:\\nonameos\\User.txt"))
            {
                Console.WriteLine("Hello," + File.ReadAllText("0:\\nonameos\\User.txt"));
            }
            current_directory = @"0:\";
            if (!File.Exists(@"0:\nonameos\User.txt"))
                curren_directory = current_directory;
            else
                curren_directory = (File.ReadAllText("0:\\nonameos\\User.txt") + "$" + current_directory);
            if (File.Exists("0:\\nonameos\\UserPassword.cs"))
            {

            E:
                string password = File.ReadAllText(@"0:\\nonameos\\UserPassword.cs");
                Console.Write("Please enter password:");
                string inpdut = Console.ReadLine();
                if (inpdut == password)
                {
                    Console.Clear();
                    Run();
                }
                else
                {

                    if (File.Exists("0:\\nonameos\\UserPassword.cs"))
                    {
                        Console.WriteLine("Unknown password! Try again!");
                        goto E;
                    }
                    else
                    {

                        Run();
                    }
                }
            }
        }


        protected override void Run()
        {

            Kernel kernel = new Kernel();
            current_directory = @"0:\";
            if (a == "graphics")
            {


                //16777215 is white
                ImprovedVBE.clear(16777215);

                //init
                Task_Manager.Task_manager();

                //Now lets make a menu
                Menu.Menu_mgr();
                //And that's it. Making a button is the same, but you don't have to do the same thing backwords
                ImprovedVBE.DrawImageAlpha(curs, (int)MouseManager.X, (int)MouseManager.Y);
                Heap.Collect();
                //This will help running your OS much longer

                ImprovedVBE.display(canvas);
                ImprovedVBE._DrawACSIIString((MouseManager.X + " " + MouseManager.Y), 1920, 10, 16777215);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("thanks a lot to dontsmi1e from discord for code support");
                    Console.WriteLine("Welcome to NoNameOS 0.1.7 Pre-alpha! build 234: Milestone 3.5.Codename'Mirage'");
                    Console.WriteLine("Milestone 2 adds such thing as: File system and commands to interact with it!");
                    Console.WriteLine("Milestone 3 From now milestone 3 adds login screen and setup");
                    Console.WriteLine("Milestone 3.1 :The Git Repo Update! Adds an GitHub repo.");
                    Console.WriteLine("Milestone 3.5 :Optimizations Update!");
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
            
            if (input == "help")
            {
                Console.BackgroundColor = ConsoleColor.Gray;

                Console.WriteLine("about-about OS                                                                ");
                Console.WriteLine("shutdown-shutdown OS                                                          ");
                Console.WriteLine("reboot-reboot OS                                                              ");
                Console.WriteLine("dir - shows list of files                                                     ");
                Console.WriteLine("mkdir- makes directory                                                        ");
                Console.WriteLine("rmdir- deletes directory                                                      ");
                Console.WriteLine("clear - clears screen                                                         ");
                Console.WriteLine("cat - read from file (type with.txt ending)                                   ");
                Console.WriteLine("write-info to file(type file with . ending,then type on 2nd line info to file)");
                Console.WriteLine("rm - delete file (type with . ending)                                         ");
                Console.WriteLine("create - Create an file (type with . ending)                                  ");
                Console.WriteLine("install - formats disk and starts setup                                       ");
                Console.WriteLine("specs - shows PC specifications                                               ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (input == "cd")
            {
                current_directory = current_directory + input.Remove(3, 0);
            }
            if (input == "cd .. ")
            {
                current_directory = @"0:\";
            }
            if (input == "install")
            {
                fs.Disks[0].CreatePartition(512);
                fs.Disks[0].FormatPartition(0, "FAT32", false);
                Sys.Power.Reboot();
            }
            if (input == "shutdown")
            {
                canvas.Display();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                if (!File.Exists(@"0:\nonameos\System.txt"))
                {

                    if (fs.Disks[0].Partitions.Count < 1)
                    {
                        Console.WriteLine("Welcome to NoNameOS! We're doing final touches...");
                        fs.Disks[0].CreatePartition(512);
                        fs.Disks[0].FormatPartition(0, "FAT32", false);
                        Cosmos.Core.ACPI.Reboot();
                    }

                    fs.Initialize(true);
                    Directory.CreateDirectory(@"0:\nonameos\");
                    Console.WriteLine("Creating System Files.....");
                    fs.CreateFile("0:\\nonameos\\System.txt");
                    fs.CreateFile("0:\\nonameos\\User.cs");
                    Console.Write("Please enter your username:");
                    string user = Console.ReadLine();
                    File.WriteAllText("0:\\nonameos\\User.cs", user);

                D:
                    Console.Write("Add password ?(Y/N)");
                    string passwd = Console.ReadLine();
                    if (passwd == "Y" || passwd == "y")
                    {
                        fs.CreateFile("0:\\nonameos\\UserPassword.cs");
                        Console.Write("Enter password for your user");
                        string password = Console.ReadLine();
                        File.WriteAllText(@"0:\nonameos\UserPassword.cs", password);
                    }
                    else if (passwd == "N" || passwd == "n")
                    {

                    }
                    else
                    {
                        goto D;
                    }

                }
                Console.Write(curren_directory);
                string input = Console.ReadLine();
                if (input == "about")
                {

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("thanks a lot to dontsmi1e from discord for code support");
                    Console.WriteLine("Welcome to NoNameOS 0.1.7 Pre-alpha! build 282: Milestone 4 Codename'Aero'");
                    Console.WriteLine("Milestone 2 adds such thing as: File system and commands to interact with it!");
                    Console.WriteLine("Milestone 3 From now milestone 3 adds login screen and setup");
                    Console.WriteLine("Milestone 3.1 :The Git Repo Update! Adds an GitHub repo.");
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                if (input == "help")
                {
                    Console.BackgroundColor = ConsoleColor.Gray;

                    Console.WriteLine("about-about OS                                                                ");
                    Console.WriteLine("shutdown-shutdown OS                                                          ");
                    Console.WriteLine("reboot-reboot OS                                                              ");
                    Console.WriteLine("dir - shows list of files                                                     ");
                    Console.WriteLine("mkdir- makes directory                                                        ");
                    Console.WriteLine("rmdir- deletes directory                                                      ");
                    Console.WriteLine("clear - clears screen                                                         ");
                    Console.WriteLine("cat - read from file (type with.txt ending)                                   ");
                    Console.WriteLine("write-info to file(type file with . ending,then type on 2nd line info to file)");
                    Console.WriteLine("rm - delete file (type with . ending)                                         ");
                    Console.WriteLine("create - Create an file (type with . ending)                                  ");
                    Console.WriteLine("install - formats disk and starts setup                                       ");
                    Console.WriteLine("specs - shows PC specifications                                               ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                if (input == "cd")
                {
                    current_directory = current_directory + input.Remove(3, 0);
                    curren_directory = curren_directory + input.Remove(3, 0);
                }
                if (input == "cd .. ")
                {
                    current_directory = @"0:\";
                }
                if (input == "install")
                {
                    fs.Disks[0].CreatePartition(512);
                    fs.Disks[0].FormatPartition(0, "FAT32", false);
                    Sys.Power.Reboot();
                }
                if (input == "shutdown")
                {

                    Cosmos.Core.ACPI.Shutdown();
                }
                if (input == "reboot")
                {
                    Cosmos.System.Power.Reboot();

                }

                if (input.StartsWith("echo ")) { Console.WriteLine(input.Remove(0, 5)); }

                if (input == "clear")
                {
                    Console.Clear();
                }
                if (input == "cd ")
                {
                    if (Directory.Exists(input.Remove(0, 3)) == true)
                    {
                        current_directory = current_directory + input.Remove(0, 3);
                    }
                    else
                    {
                        Console.WriteLine("not found!");
                    }
                }
                if (input == "dir")
                {
                    var fs_type = fs.GetFileSystemType(@"0:\");
                    string[] filePaths = Directory.GetFiles(current_directory);
                    var drive = new DriveInfo("0");
                    Console.WriteLine("Volume in drive 0 is " + $"{drive.VolumeLabel}");
                    Console.WriteLine("Directory of " + current_directory);
                    Console.WriteLine("\n");
                    for (int i = 0; i < filePaths.Length; ++i)
                    {
                        string path = filePaths[i];
                        Console.WriteLine(System.IO.Path.GetFileName(path));
                    }
                    foreach (var d in System.IO.Directory.GetDirectories(current_directory))
                    {
                        var dir = new DirectoryInfo(d);
                        var dirName = dir.Name;

                        Console.WriteLine(dirName + " <DIR>");
                    }
                    Console.WriteLine("\n");
                    Console.WriteLine("        " + $"{drive.TotalSize}" + " bytes");
                    Console.WriteLine("        " + $"{drive.AvailableFreeSpace}" + " bytes free");
                    Console.WriteLine("File System:" + fs_type);
                }
                if (input.StartsWith("mkdir"))
                {
                    {
                        try
                        {
                            Directory.CreateDirectory(@"0:\" + input.Remove(0, 6) + @"\");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
                if (input == "error")
                {
                    Kernel.crash("test");
                }
                if (input.StartsWith("rmdir"))
                {
                    {
                        try
                        {
                            Directory.Delete(@"0:\" + input.Remove(0, 6) + @"\");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }
                }
                if (input.StartsWith("cat"))
                {
                    {
                        try
                        {
                            Console.WriteLine(File.ReadAllText(@"0:\" + input.Remove(0, 4)));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
                if (input.StartsWith("write"))
                {
                    string write = Console.ReadLine();
                    {
                        try
                        {
                            File.WriteAllText(@"0:\" + input.Remove(0, 6), write);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
                if (input.StartsWith("rm"))
                {
                    if (input.EndsWith(" "))
                    {
                        try
                        {
                            File.Delete(@"0:\" + input.Remove(0, 6));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }
                }
                if (input.StartsWith("create"))
                {
                    {
                        try
                        {
                            var file_stream = File.Create(@"0:\" + input.Remove(0, 7));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }
                }
                if (input == ("specs"))
                {
                    var fs_type = fs.GetFileSystemType(@"0:\");
                    var drive = new DriveInfo("0");
                    Console.Write("CPU:");
                    Console.WriteLine(Cosmos.Core.CPU.GetCPUBrandString());
                    Console.WriteLine("Drive:");
                    Console.WriteLine("        " + $"{drive.TotalSize}" + " bytes");
                    Console.WriteLine("        " + $"{drive.AvailableFreeSpace}" + " bytes free");
                    Console.WriteLine("File System:" + fs_type);
                    Console.WriteLine("RAM:" + GCImplementation.GetAvailableRAM() + "MB(Avaliable)");
                }
            }
            if (input == ("specs"))
            {
                var fs_type = fs.GetFileSystemType(@"0:\");
                var drive = new DriveInfo("0");
                Console.WriteLine("OS:");
                Console.Write("CPU:");
                Console.WriteLine(Cosmos.Core.CPU.GetCPUBrandString());
                Console.WriteLine("Drive:");
                Console.WriteLine("     " + $"{drive.TotalSize}" + " bytes");
                Console.WriteLine("     " + $"{drive.AvailableFreeSpace}" + " bytes free");
                Console.WriteLine("File System:" + fs_type);
                Console.WriteLine("RAM:"+GCImplementation.GetAvailableRAM()+"MB(Avaliable)");
            }
            }
        }
    }

}