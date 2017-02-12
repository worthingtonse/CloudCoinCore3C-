using System;
using System.IO;
using System.Linq;

namespace Foundation
{
    class Frack_Fixer
    {
        //  instance variables - replace the example below with your own
        private FileUtils fileUtils;
        private int totalValueToBank;
        private int totalValueToFractured;
        private int totalValueToCounterfeit;
        private RAIDA raida;

        public Frack_Fixer(FileUtils fileUtils, int timeout)
        {
            this.fileUtils = fileUtils;
            raida = new RAIDA(timeout);
            totalValueToBank = 0;
            totalValueToCounterfeit = 0;
            totalValueToFractured = 0;
        }//constructor


        /* Loads all fracked coins and tries to fix them. */
        public int[] fixAll()
        {
            int[] results = new int[3];
            String[] frackedFileNames = new DirectoryInfo(this.fileUtils.frackedFolder ).GetFiles().Select(o => o.Name).ToArray();
            CloudCoin frackedCC;
            Console.WriteLine("UnFracking coin 1 of " + frackedFileNames.Length);
            for ( int j = 0; j < frackedFileNames.Length; j++ )
            {
                try
                {
                    frackedCC = fileUtils.loadOneCloudCoinFromJsonFile( this.fileUtils.frackedFolder + frackedFileNames[j] );
                    Console.WriteLine( "UnFracking SN #" + frackedCC.sn + ", Denomination: " + frackedCC.getDenomination() );

                    CloudCoin fixedCC = this.raida.fixCoin( frackedCC ); // Will attempt to unfrack the coin. 

                    fixedCC.consoleReport();
                    switch (fixedCC.extension)
                    {
                        case "bank":
                            this.totalValueToBank++;
                            this.fileUtils.writeTo(this.fileUtils.bankFolder, fixedCC);
                            break;
                        case "fractured":
                            this.totalValueToFractured++;
                            this.fileUtils.writeTo(this.fileUtils.frackedFolder, fixedCC);
                            break;
                        case "counterfeit":
                            this.totalValueToCounterfeit++;
                            this.fileUtils.writeTo(this.fileUtils.counterfeitFolder, fixedCC);
                            break;
                    }
                    // end switch on the place the coin will go 
                    this.deleteCoin(this.fileUtils.frackedFolder + frackedFileNames[j]);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (IOException ioex)
                {
                    Console.WriteLine(ioex);
                } // end try catch
            }// end for each file name that is fracked

            results[0] = this.totalValueToBank;
            results[1] = this.totalValueToCounterfeit; // System.out.println("Counterfeit and Moved to trash: "+totalValueToCounterfeit);
            results[2] = this.totalValueToFractured; // System.out.println("Fracked and Moved to Fracked: "+ totalValueToFractured);
            return results;
        }// end fix all

        // End select all file names in a folder
        public bool deleteCoin(String path)
        {
            bool deleted = false;

            // System.out.println("Deleteing Coin: "+path + this.fileName + extension);
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return deleted;
        }//end delete coin
    }//end class
}//end namespace
