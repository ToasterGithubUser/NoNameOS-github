using Cosmos.System;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using SoundTest;
using SoundTest.Applications.Task_Scheduler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoNameOS.Applications.About
{
    public static class About
    {
        public static string equasion = "";
        public static string[] numbers;
        public static int pressedsince;
        public static bool movable = false;
        public static bool isclicked = false;

        [ManifestResourceStream(ResourceName = "CosmosKernel2.Calculator.Calculator.bmp")] public static byte[] calculator_base;
        public static Bitmap Calculator_base = new Bitmap(calculator_base);

        //Basically I copied the code from CrystaOS 2.0
        //The code is very easy to understand
        public static void calculator(int x, int y)
        {
            equasion = Task_Manager.Tasks[Task_Manager.indicator].Item5;//In the tasks list the 5th element was a string which contains the equasion
            ImprovedVBE.DrawImageAlpha(Calculator_base, x, y);//Then it renders the base of the calculator

            if (Task_Manager.Tasks[0].Item1 == "calculator" && Task_Manager.indicator == 0)
            {
                //In case no button was pressed, we can also take button inputs.
                //The concept is identical to what we saw in the menu part.

                if (MouseManager.MouseState == MouseState.Left)
                {
                    isclicked = true;
                }

                //This part does the movable/close button action
                if (MouseManager.MouseState == MouseState.Left)
                {
                    if (MouseManager.X > x + 106 && MouseManager.X < x + 148)
                    {
                        if (MouseManager.Y > y && MouseManager.Y < y + 17)
                        {
                            //Bool_Manager.Calculator_Opened = false;
                            Task_Manager.Tasks.RemoveAt(Task_Manager.indicator);//Whith this, you remove the given app from the task manager tasks list
                        }
                    }
                    if (Task_Manager.Tasks[Task_Manager.indicator].Item4 == false)
                    {
                        if (MouseManager.X > x && MouseManager.X < x + 40)
                        {
                            if (MouseManager.Y > y && MouseManager.Y < y + 18)
                            {
                                int f = (int)MouseManager.X;
                                int g = (int)MouseManager.Y;
                                Task_Manager.Tasks.RemoveAt(Task_Manager.indicator);//Here, it removes it from the list and inserts back a new one with the bool now showing true
                                Task_Manager.Tasks.Insert(0, new Tuple<string, int, int, bool, string>("calculator", f, g, true, equasion));
                            }
                        }
                    }
                }

                if (Task_Manager.Tasks[Task_Manager.indicator].Item4 == true)//In this part, since the 4th item is true, movability is enabled until you press the right button
                {
                    int f = (int)MouseManager.X;
                    int g = (int)MouseManager.Y;
                    Task_Manager.Tasks.RemoveAt(0);
                    Task_Manager.Tasks.Insert(0, new Tuple<string, int, int, bool, string>("calculator", f, g, true, equasion));
                    if (MouseManager.MouseState == MouseState.Right)
                    {
                        Task_Manager.Tasks.RemoveAt(0);
                        Task_Manager.Tasks.Insert(0, new Tuple<string, int, int, bool, string>("calculator", f, g, false, equasion));
                        //Task_Manager.Tasks.Reverse();
                        //movable = false;
                    }
                    //Also, it is constantly placed on the top(0) of the tasks list
                    /*
                    if (MouseManager.X > Int_Manager.Settings_X && MouseManager.X < Int_Manager.Settings_X + 352)
                    {
                        if (MouseManager.Y > Int_Manager.Settings_Y && MouseManager.Y < Int_Manager.Settings_Y + 18)
                        {
                            movable = false;
                        }
                    }
                    */
                }

            }
        }
    }
}
