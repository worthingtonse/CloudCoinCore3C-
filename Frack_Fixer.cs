using System;
using System.IO;
using System.Linq;

namespace Foundation
{
    class Frack_Fixer
    {
        /* INSTANCE VARIABLES */
        private FileUtils fileUtils;
        private int totalValueToBank;
        private int totalValueToFractured;
        private int totalValueToCounterfeit;
        private RAIDA raida;

        /* CONSTRUCTORS */
        public Frack_Fixer(FileUtils fileUtils, int timeout)
        {
            this.fileUtils = fileUtils;
            raida = new RAIDA(timeout);
            totalValueToBank = 0;
            totalValueToCounterfeit = 0;
            totalValueToFractured = 0;
        }//constructor


        /* PUBLIC METHODS */
        public int[] fixAll()
        {
            int[] results = new int[3];
            String[] frackedFileNames = new DirectoryInfo(this.fileUtils.frackedFolder ).GetFiles().Select(o => o.Name).ToArray();
            CloudCoin frackedCC;
            if ( frackedFileNames.Length < 0) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Out.WriteLine("You have no fracked coins.");
                Console.ForegroundColor = ConsoleColor.White;
            }//no coins to unfrack

            
            for ( int i = 0; i < frackedFileNames.Length; i++ )
            {
                Console.WriteLine("UnFracking coin " + (i+1) +" of " + frackedFileNames.Length);
                try
                {
                    frackedCC = fileUtils.loadOneCloudCoinFromJsonFile( this.fileUtils.frackedFolder + frackedFileNames[i] );
                 //   Console.WriteLine( "UnFracking SN " + frackedCC.sn + ", Denomination: " + frackedCC.getDenomination() );

                  String value = frackedCC.aoid["fracked"];
                  // Console.WriteLine("Fracked value is of the frackedCC is in frack_fixer " + value);
                  //  Console.WriteLine("pastestatus for one " + frackedCC.pastStatus[0]);

                    CloudCoin fixedCC = this.raida.fixCoin( frackedCC ); // Will attempt to unfrack the coin. 
                    
                    fixedCC.consoleReport();
                    switch (fixedCC.extension)
                    {
                        case "bank":
                            this.totalValueToBank++;
                            this.fileUtils.overWrite(this.fileUtils.bankFolder, fixedCC);
                            this.deleteCoin(this.fileUtils.frackedFolder + frackedFileNames[i]);
                            break;
                        case "counterfeit":
                            this.totalValueToCounterfeit++;
                            this.fileUtils.overWrite(this.fileUtils.counterfeitFolder, fixedCC);
                            this.deleteCoin(this.fileUtils.frackedFolder + frackedFileNames[i]);
                            break;
                        default://Move back to fracked folder
                            this.totalValueToFractured++;
                            this.deleteCoin(this.fileUtils.frackedFolder + frackedFileNames[i]);
                            this.fileUtils.overWrite(this.fileUtils.frackedFolder, fixedCC);
                            break;
                    }
                    // end switch on the place the coin will go 
                    
                }
                catch (FileNotFoundException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (IOException ioex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ioex);
                    Console.ForegroundColor = ConsoleColor.White;
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
