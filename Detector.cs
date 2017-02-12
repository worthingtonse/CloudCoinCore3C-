using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation
{
    class Detector
    {
        //  instance variables - replace the example below with your own
        RAIDA raida;
        FileUtils fileUtils;

        public Detector(FileUtils fileUtils, int timeout)
        {
            this.raida = new RAIDA(timeout);
            this.fileUtils = fileUtils;
        }

        // end Detect constructor
        public int[] detectAll()
        {
            // LOAD THE .suspect COINS ONE AT A TIME AND TEST THEM
            int[] results = new int[3];
            // [0] Coins to bank, [1] Coins to fracked [1] Coins to Counterfeit
            String[] suspectFileNames = new DirectoryInfo(this.fileUtils.suspectFolder).GetFiles().Select(o => o.Name).ToArray();
            int totalValueToBank = 0;
            int totalValueToCounterfeit = 0;
            int totalValueToFractured = 0;
            bool coinSupect = false;
            CloudCoin newCC;
            for (int j = 0; (j < suspectFileNames.Length); j++)
            {
                try
                {
                    // System.out.println("Construct Coin: "+rootFolder + suspectFileNames[j]);
                    newCC = this.fileUtils.loadOneCloudCoinFromJsonFile(this.fileUtils.suspectFolder + suspectFileNames[j]);
                    Console.Out.WriteLine("");
                    Console.Out.WriteLine("");
                    Console.Out.WriteLine("Detecting SN #" + newCC.sn + ", Denomination: " + newCC.getDenomination());
                    CloudCoin detectedCC = this.raida.detectCoin(newCC);
              
                   

                    if(j==0)//If we are detecting the first coin, note if the RAIDA are working
                    { 
                        for (int i=0;i<25;i++)// Checks any servers are down so we don't try to check them again. 
                        {
                            if ( detectedCC.pastStatus[i] != "pass" && detectedCC.pastStatus[i] != "fail") {
                                raida.raidaIsDetecting[i] = false;//Server is not working correctly, don't try it agian
                            }
                        }
                    }//end if it is the first coin we are detecting

                    detectedCC.consoleReport();
                   
                    // Console.Out.WriteLine("The ex");
                  
                    bool alreadyExists = false;//Does the file already been imported?
                    switch (detectedCC.extension)
                    {
                        case "bank":
                            totalValueToBank++;
                            alreadyExists = this.fileUtils.writeTo(this.fileUtils.bankFolder, detectedCC);
                            break;
                        case "fracked":
                            totalValueToFractured++;
                            alreadyExists = this.fileUtils.writeTo(this.fileUtils.frackedFolder, detectedCC);
                            break;
                        case "counterfeit":
                            totalValueToCounterfeit++;
                            alreadyExists = this.fileUtils.writeTo(this.fileUtils.counterfeitFolder, detectedCC);
                            break;  
                        case "suspect":
                            coinSupect = true;//Coin will remail in suspect folder
                            break;
                    }

                    if ( alreadyExists ) {//Coin has already been imported. Delete it from import folder move to trash.
                        Console.Out.WriteLine("You tried to import a coin that has already been imported. Please remove it from you import folder.");
                    }

                    // end switch on the place the coin will go 
                    if (!coinSupect)//Leave coin in the suspect folder if RAIDA is down
                    {
                        File.Delete(this.fileUtils.suspectFolder + suspectFileNames[j]);//Take the coin out of the suspect folder
                    }
                    else {
                        this.fileUtils.writeTo(this.fileUtils.suspectFolder, detectedCC);
                        Console.Out.WriteLine("Not enough RAIDA were contacted to determine if the coin is authentic. Try again later.");
                    }
                    
                }
                catch (FileNotFoundException ex)
                {
                    Console.Out.WriteLine(ex);
                }
                catch (IOException ioex)
                {
                    Console.Out.WriteLine(ioex);
                }// end try catch
            }// end for each coin to import
            // System.out.println("Results of Import:");
            results[0] = totalValueToBank;
            results[1] = totalValueToCounterfeit;
            // System.out.println("Counterfeit and Moved to trash: "+totalValueToCounterfeit);
            results[2] = totalValueToFractured;
            // System.out.println("Fracked and Moved to Fracked: "+ totalValueToFractured);
            return results;
        }//Detect All
    }
}
