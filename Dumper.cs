using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation
{
    class Dumper
    {
        FileUtils fileUtils;
        Random random;

        public Dumper( FileUtils fileUtils ) {
            this.fileUtils = fileUtils;
            random = new Random();
        }//end Dumper constructor

        public void dumpAll() {
            // 1. Get all file names in bank
            String[] bankFileNames = new DirectoryInfo(this.fileUtils.bankFolder).GetFiles().Select(o => o.Name).ToArray();//Get all files in suspect folder

            // 2. loop. Each name create a new name
            String newFileName = "";
            for(int i= 0; i < bankFileNames.Length; i++) {
                newFileName = bankFileNames[i];
                int randInt = random.Next(100000, 10000000);
                string randomIntString = "." + randInt.ToString() + ".";
                newFileName = newFileName.Replace("..", randomIntString);

                // 3. move file to export. 
                File.Move(this.fileUtils.bankFolder + bankFileNames[i], this.fileUtils.exportFolder + newFileName);

            }//end for each file name



        }//end dump all

    }
}
