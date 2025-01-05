using System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using Sys = Cosmos.System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
//abandoned bc putting in kernel.cs is easier
namespace CosmosKernel1
{
    internal class DiskUtil
    {

        
        public static void call(bool IsDUNeeded , string input , bool delete )
        {
            Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
            
            IsDUNeeded = true;
            Console.WriteLine("diskutil v0.1. type help for help.");
            while (IsDUNeeded)
            {
                string dsk = Console.ReadLine();
                switch (dsk)
                {

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
                                        delete = true;
                                        CritSituation = false;
                                        
                                        break;

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
                        Console.WriteLine("exit - exit");
                        Console.WriteLine("format 0: - formats main disk");
                        Console.WriteLine("create part (mb) - makes partition with specified size");
                        break;
                    case { } when dsk.StartsWith(" "):
                        break;
                    default:
                        Console.WriteLine("syntax invalid");
                        break;
                }


            }
        }
    }
}