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
using System.ComponentModel.Design;
using Cosmos.HAL.Drivers;
using Cosmos.HAL;
using System.Buffers;
using Cosmos.Core;


namespace CosmosKernel1
{


    public class Kernel : Sys.Kernel
    {
        Canvas canvas;
        public static VGAScreen VScreen = new VGAScreen();



    


        private string current_directory;
        private string curren_directory;
        private string directory;
        public static class BootManager
        {
            public static void Boot()
            {
                
                
            }

        }
        public static void error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(msg);
            Console.BackgroundColor = ConsoleColor.Red;
        }


        public static void crash(string reason)
        {
            {
                Console.BackgroundColor = ConsoleColor.Black;
                
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("                             NONAMEOS CRASH REPORTER");
                Console.WriteLine("PANIC!");
                Console.Write(Cosmos.Core.CPU.GetCPUBrandString());
                Console.Write("_");
                Console.Write(Cosmos.Core.CPU.GetCPUVendorName());
                Console.Write("_");
                Console.WriteLine(Cosmos.Core.CPU.GetCPUUptime());
                Console.WriteLine("NoNameOS Milestone 3 has encountered an critical error that cant solve.Error:");
                System.Threading.Thread.Sleep(5000);
                Kernel.error(reason);
                System.Threading.Thread.Sleep(500);
                
            }
        }

        protected override void BeforeRun()
        {

            

            
            Console.WriteLine(@"
                                
         >#@$@$@$@$@$#i         
        *@@r.       r@@#        
        m@$0-        'n_        
         ^z$@$@$mI        NONAMEOS PROJECT DELTA           
              `J@@$v            
          }$@$@$$@@$@Y          
        1$@B|      |B@@1        
       z@${          {$@z       
       @$0            0$@       
       $@O            O@$       
       c@@|          |@$c       
        ?$@Bn      nB$@?        
          <8$@$@$@$@%>          
                                                  
");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Black;
            
            
            Cosmos.Core.CPU.GetCPUBrandString();
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
            Console.Write("RAM:");
            Console.Write(GCImplementation.GetAvailableRAM());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[OK!]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
       
            Console.WriteLine("  ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Shell Started. Type help for help,thanks a lot to dontsmi1e for code support");
            Cosmos.Core.CPU.GetCPUBrandString();
            Console.BackgroundColor = ConsoleColor.Blue;
            current_directory = @"0:\";
            
       
        }


        protected override void Run()
        {
            Kernel kernel = new Kernel();
            current_directory = @"0:\";
         
                Console.Write(current_directory);
                string input = Console.ReadLine();
                if (input == "about")
                {

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("thanks a lot to dontsmi1e from discord for code support");
                    Console.WriteLine("Welcome to NoNameOS Project 'Delta'!");
                    Console.WriteLine("Project 'Delta' Is needed LiveCD Testing, and using NoNameOS");
                    Console.WriteLine("without formatting disk!");
                    Console.BackgroundColor = ConsoleColor.Blue;
                }

            if (input == "help")
            {
                Console.BackgroundColor = ConsoleColor.Gray;

                Console.WriteLine("about-about OS                                                                ");
                Console.WriteLine("shutdown-shutdown OS                                                          ");
                Console.WriteLine("reboot-reboot OS                                                              ");
                Console.BackgroundColor = ConsoleColor.Blue;
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
                if (input == "error")
                {
                    Kernel.crash("test");
                }
               
            }
        }
    }

