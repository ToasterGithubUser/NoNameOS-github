using Cosmos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    internal class PanicHandler
    {
        public static void crash(string reason)
        {
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("=====================================================================");
                Console.WriteLine("Aw snap!");
                Console.ForegroundColor = ConsoleColor.Red;
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
                Console.WriteLine("EBP " + Cosmos.Core.CPU.GetEBPValue());
                Console.WriteLine("StackStart " + CPU.GetStackStart());
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine(reason);
                System.Threading.Thread.Sleep(500);
                CPU.Halt();

            }
        }
    }
}