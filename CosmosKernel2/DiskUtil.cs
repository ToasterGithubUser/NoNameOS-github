using System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using Sys = Cosmos.System;
using System.IO;
namespace CosmosKernel1
{
    internal class DiskUtil
    {

        
        public static void call(bool IsDUNeeded , string input)
        {
            Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
            
            IsDUNeeded = true;
            Console.WriteLine("diskutil v0.1. type help for help.");
            while (IsDUNeeded)
            {
                string dsk = Console.ReadLine();
                switch (dsk)
                {
                    case ("list disk"):
                        foreach (Disk disk in fs.GetDisks())
                        {
                            disk.DisplayInformation();
                        }
                        break;
                    case ("format 0:"):
                        Console.WriteLine("WARNING! do you REALLY want to format main disk? y/n");
                        bool CritSituation = true;
                        while (CritSituation)
                        {
                            string format = Console.ReadLine();
                            switch (format)
                            {

                                case "y":
                                    try
                                    {
                                        fs.Disks[0].DeletePartition(0);
                                    }
                                    catch(Exception e)
                                    { Console.WriteLine(e.ToString()); }
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
                        Console.WriteLine("format 0: - formats main disk");
                        break;
                    case { } when input.StartsWith(" "):
                        break;
                    default:
                        Console.WriteLine("syntax invalid");
                        break;
                }


            }
        }
    }
}