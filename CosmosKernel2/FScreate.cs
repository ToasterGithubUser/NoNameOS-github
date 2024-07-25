using System.IO;

namespace CosmosKernel2
{
    class FScreate
    {
        public static void makeDir(string mkdirPath)
        {
            if (mkdirPath.Contains(":\\"))
            {
                Directory.CreateDirectory(mkdirPath);
            }
            else
            {
                if (Directory.GetCurrentDirectory().EndsWith("\\"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + mkdirPath);
                }
                else
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + mkdirPath);
                }
            }
        }
    }
}