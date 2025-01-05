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
using static CosmosKernel1.PanicHandler;
using CosmosKernel2;
using static CosmosKernel1.DiskUtil;
using static CosmosKernel1.TalosSecurity;
// NOTE: Proper use of Panic
/*catch (Exception e)
{
    PanicHandler.crash(e.ToString());
}*/


namespace CosmosKernel1
{


    public class Kernel : Sys.Kernel
    {
   


       
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();


    


        public string current_directory = @"0:\";
        public string curren_directory;
        public static class BootManager
        {
            public static void Boot()
            {
               
                
            }

        }

        public bool IsMBR { get; }



        protected override void BeforeRun()
        {

            
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs, true, true);
            
            Console.WriteLine(" _   _       _   _                       ___  ____  ");
            Console.WriteLine("| \\ | | ___ | \\ | | __ _ _ __ ___   ___ / _ \\/ ___| ");
            Console.WriteLine("|  \\| |/ _ \\|  \\| |/ _` | '_ ` _ \\ / _ \\ | | \\___ \\ ");
            Console.WriteLine("| |\\  | (_) | |\\  | (_| | | | | | |  __/ |_| |___) |");
            Console.WriteLine("|_| \\_|\\___/|_| \\_|\\__,_|_| |_| |_|\\___|\\___/|____/ ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Black;
            fs.GetDisks();
            System.Threading.Thread.Sleep(1000);
          
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("CPU:");
            Console.Write(Cosmos.Core.CPU.GetCPUBrandString());
            Console.ForegroundColor = ConsoleColor.Green;
            if (! (Cosmos.Core.CPU.GetCPUBrandString() == ""))
            {
                Console.Write("[OK!]");
            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.Write("[FAIL!]");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  ");
            Console.Write("RAM:"+GCImplementation.GetAvailableRAM()+ "MB Avaliable");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[OK!]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
       
            Console.WriteLine("  ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("CLI Started. Type help for help.");
            Console.BackgroundColor = ConsoleColor.Green;
            Cosmos.Core.CPU.GetCPUBrandString();
            Console.BackgroundColor = ConsoleColor.Black;
            if (File.Exists("0:\\nonameos\\User.txt"))
            {
                Console.WriteLine("Hello," + File.ReadAllText("0:\\nonameos\\User.txt"));
            }
            
            if (!File.Exists(@"0:\nonameos\User.txt"))
                curren_directory = current_directory;
            else
                curren_directory = (File.ReadAllText("0:\\nonameos\\User.txt") + "$" + current_directory);
            if (File.Exists("0:\\nonameos\\UserPassword.secure"))
            {
                
                E:
                string password = File.ReadAllText(@"0:\\nonameos\\UserPassword.secure");
                Console.Write("Please enter password:");
                string inpdut = Console.ReadLine();
                if (inpdut == Decrypt(password)) 
                {
                    Console.Clear();
                    Run();
                }
                else
                {

                    if (File.Exists("0:\\nonameos\\UserPassword.secure"))
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
            
            bool IsDUNeeded = false;


            
            fs.Initialize(true);
            Console.BackgroundColor = ConsoleColor.Black;
            if (!File.Exists(@"0:\nonameos\User.txt") && File.Exists(@"0:\nonameos\System.placeholder"))
            {
                fs.CreateFile("0:\\nonameos\\User.txt");
                Console.WriteLine("Something happened during install, or username file corrupted.Please enter username:");
                string user = Console.ReadLine();
                File.WriteAllText("0:\\nonameos\\User.txt", user);
            }
            if (!File.Exists(@"0:\nonameos\System.placeholder"))
            {

                if (fs.Disks[0].Partitions.Count < 1)
                {
                    Console.WriteLine("Welcome to NoNameOS! We're doing final touches...");
                    try
                    {
                        fs.Disks[0].CreatePartition(512);
                        fs.Disks[0].FormatPartition(0, "FAT32", false);
                        Cosmos.System.Power.Reboot();
                    }
                    catch(Exception e) {
                        Console.WriteLine(e.ToString());   
                    }
                }

                try 
                { 
                Directory.CreateDirectory(@"0:\nonameos\");
                Console.WriteLine("Creating System Files.....");
                fs.CreateFile("0:\\nonameos\\System.placeholder");
                fs.CreateFile("0:\\nonameos\\User.txt");
                fs.CreateFile("0:\\nonameos\\readme.txt");
                File.WriteAllText("0:\\nonameos\\readme.txt", "System file currently placeholder, due to installing system not implemented.You can freely edit User.txt and UserPassword.cs(if you have).");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());   
                }
                
                
                Console.Write("Please enter your username:");
                string user = Console.ReadLine();
                File.WriteAllText("0:\\nonameos\\User.txt", user);

            D:
                Console.Write("Add password ?(Y/N) (default N)");
                string passwd = Console.ReadLine();
                if (passwd == "Y" || passwd == "y")
                {
                    fs.CreateFile("0:\\nonameos\\UserPassword.secure");
                    Console.Write("Enter password for your user");
                    string password = Console.ReadLine();
                    string asad = Encrypt(password);
                    Console.WriteLine("Codename 'Talos' check:" + Encrypt(password) + Decrypt(asad));
                    if (Decrypt(asad) == password)
                    {
                        Console.WriteLine("OK!");
                    }
                    File.WriteAllText(@"0:\nonameos\UserPassword.secure",asad);
                }
                else if (passwd == "N" || passwd == "n")
                {

                }
                else
                {
                
                }

            }
            if (!File.Exists(@"0:\nonameos\User.txt"))
                curren_directory = "$" + current_directory;
            else
                curren_directory = (File.ReadAllText("0:\\nonameos\\User.txt") + "$" + current_directory);
            Console.Write(curren_directory);
            string input = Console.ReadLine();
            switch (input)
            {
                case "about":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Welcome to NoNameOS 0.1.9 Pre-alpha! build 318: Milestone 3.7 Codename'Cleaner'");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "changelog":
                    Console.WriteLine("thanks a lot to dontsmi1e for code support");
                    Console.WriteLine("Milestone 2 :adds File system and commands to interact with it!");
                    Console.WriteLine("Milestone 3 :adds login screen and setup");
                    Console.WriteLine("Milestone 3.1 :The Git Repo Update! Adds an GitHub repo.");
                    Console.WriteLine("Milestone 3.5 :Optimizations Update!");
                    Console.WriteLine("Milestone 3.6 :Optimizations update part 2! Puts (almost) all commands in a     switch case, so theoretically they will be faster");
                    Console.WriteLine("Build 314 : Pi Build!");
                    Console.WriteLine("Minestone 3.7:Makes some cosmetical changes to OS. Will be last                 sub-milestone before 0.2 .");
                    break;

                case "help":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine("==============================================================================");
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
                    Console.WriteLine("sigmafetch - neofecth ripoff but worse                                        ");
                    Console.WriteLine("==============================================================================");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case { } when input.StartsWith("cd") && (input != "cd .."):
                    string dirka = input.Remove(0, 3);

                    if(Directory.Exists(current_directory + dirka)) {
                        Console.WriteLine("Ok!");
                        current_directory = current_directory + dirka;
                        curren_directory = (File.ReadAllText("0:\\nonameos\\User.txt") + "$" + current_directory + "\\:");
                    }
                else
                    {
                        Console.WriteLine("FAIL!");
                    }
                    break;
                case "cd ..":
                    current_directory = @"0:\";
                    curren_directory = (File.ReadAllText("0:\\nonameos\\User.txt") + "$" + current_directory );
                    break;
            
                case "shutdown":
                    Cosmos.Core.ACPI.Shutdown();
                    break;
                case "reboot":
                    Cosmos.System.Power.Reboot();
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "dir":
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
                    break;
                case "crash":
                    PanicHandler.crash("test");
                    break;
                case "diskutil":
                    IsDUNeeded = true;
                    Console.WriteLine("diskutil v0.1. type help for help.");
                    while (IsDUNeeded)
                    {
                        string dsl = Console.ReadLine();
                        switch (dsl)
                        {
                            case ("list disk"):
                                Console.WriteLine(fs.GetDisks());
                                break;
                            case { } when dsl.StartsWith("format 0"):
                                Console.WriteLine("WARNING! do you REALLY want to format main disk? y/n");
                                //critical sutiation, user wants to delete disk and we lowkey dont want it.
                                bool CritSituation = true;
                                while (CritSituation)
                                {
                                    string format = Console.ReadLine();
                                    switch (format)
                                    {
                                        
                                        case "y":
                                            //now we're in BIG SHIT. We need to clear disk , create partition , format it, and optionally try to not to shit our pants.
                                            string iLoveCHina = dsl.Remove(0, 9);
                                            int.TryParse(iLoveCHina, out int j);
                                            fs.Disks[0].Clear();
                                            
                                            
                                            fs.Disks[0].CreatePartition(j);
                                            fs.Disks[0].FormatPartition(0, "FAT32", false);
                                            
                                            Sys.Power.Reboot();
                                            break;
                                        case "n":
                                            CritSituation = false;
                                            break;
                                        default:
                                            CritSituation = false;
                                            break;
                                    }
                                }
                                break;
                            case ("exit"):
                                IsDUNeeded = false;
                                break;
                            case ("help"):
                                Console.WriteLine("list disk - lists disk");
                                Console.WriteLine("format 0: - formats main disk AND asks you to specify partition size (mb)");
                                Console.WriteLine("     Usage: 'format 0 1024'");
                                break;
                            case { } when input.StartsWith(" "):
                                break;
                            default:
                                Console.WriteLine("syntax invalid");
                                break;
                        }
                    }
                    break;


                    
            



                case "sigmafetch":
                    //TODO: add some kind of little logo for our system. Any ideas are welcome
                    var fs_type1 = fs.GetFileSystemType(@"0:\");
                    var drive1 = new DriveInfo("0");
                    Console.WriteLine("OS:NoNameOS");
                    Console.Write("CPU:");
                    Console.WriteLine(Cosmos.Core.CPU.GetCPUBrandString());
                    Console.WriteLine("     Drive:");
                    Console.WriteLine("         " + $"{drive1.TotalSize}" + " bytes");
                    Console.WriteLine("         " + $"{drive1.AvailableFreeSpace}" + " bytes free");
                    Console.WriteLine("     File System:" + fs_type1);
                    Console.WriteLine("     RAM:" + GCImplementation.GetAvailableRAM() + "MB(Avaliable)");
                    break;
                case "":
                    break;
                case { } when input.StartsWith("echo"):
                    Console.WriteLine(input.Remove(0, 5));
                    break;
                case { } when input.StartsWith("mkdir"):
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
                    break;
                case { } when input.StartsWith("rmdir"):
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
                    break;
                case { } when input.StartsWith("cat"):
                    {
                        try
                        {
                            Console.WriteLine(File.ReadAllText(current_directory + '/'+ input.Remove(0,4)));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            Console.WriteLine(current_directory);
                            Console.WriteLine(current_directory + input.Remove(0, 4));
                        }
                    }
                    break;
                case { } when input.StartsWith("write"):
                    string write = Console.ReadLine();
                    {
                        try
                        {
                            File.WriteAllText(current_directory + '/' + input.Remove(0, 6), write);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    break;
                case { } when input.StartsWith("rm"):
                    if (input.EndsWith(" "))
                    {
                        try
                        {
                            File.Delete(current_directory + '/' + input.Remove(0, 3));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }
                    break;
                case { } when input.StartsWith("create"):
                    {
                        try
                        {
                            var file_stream = File.Create(current_directory + '/' + input.Remove(0, 7));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }
                    break;
                case { } when input.StartsWith(" "):
                    break;
                default:
                    Console.WriteLine("Theres no such command as '" + input + "'. Type help for command list.");
                    break ;
                
            }

            }
        }
    }

