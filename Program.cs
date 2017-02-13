using System;
using System.IO;

namespace Foundation
{
    class Program
    {
        /* INSTANCE VARIABLES */
        public static KeyboardReader reader = new KeyboardReader();
        //  public static String rootFolder = System.getProperty("user.dir") + File.separator +"bank" + File.separator ;
        public static String rootFolder = ("C:\\CloudCoins" + Path.DirectorySeparatorChar);
        public static String importFolder = (rootFolder + "Import" + Path.DirectorySeparatorChar);
        public static String importedFolder = (rootFolder + "Imported" + Path.DirectorySeparatorChar);
        public static String trashFolder = (rootFolder + "Trash" + Path.DirectorySeparatorChar);
        public static String suspectFolder = (rootFolder + "Suspect" + Path.DirectorySeparatorChar);
        public static String frackedFolder = (rootFolder + "Fracked" + Path.DirectorySeparatorChar);
        public static String bankFolder = (rootFolder + "Bank" + Path.DirectorySeparatorChar);
        public static String templateFolder = (rootFolder + "Templates" + Path.DirectorySeparatorChar);
        public static String counterfeitFolder = (rootFolder + "Counterfeit" + Path.DirectorySeparatorChar);
        public static String directoryFolder = (rootFolder + "Directory" + Path.DirectorySeparatorChar);
        public static String exportFolder = (rootFolder + "Export" + Path.DirectorySeparatorChar);
        public static String prompt = "Start Mode> ";
        public static String[] commandsAvailable = new String[] { "show coins", "import", "export", "show folders", "fix fracked", "quit" };
        //public static String[] commandsAvailable = new String[] { "import", "show coins", "export", "fix fracked", "quit", "show folders", "test echo", "test detect", "test get_ticket", "test hints", "test fix", };
        public static int timeout = 2000; // Milliseconds to wait until the request is ended. 
        public static FileUtils fileUtils = new FileUtils(rootFolder, importFolder, importedFolder, trashFolder, suspectFolder, frackedFolder, bankFolder, templateFolder, counterfeitFolder, directoryFolder, exportFolder);
        public static Random myRandom = new Random();

        /* MAIN METHOD */
        public static void Main(String[] args)
        {
            printWelcome();
            run(); // Makes all commands available and loops
            Console.Out.WriteLine("Thank you for using RAIDA Tester 1. Goodbye.");
        } // End main

        /* STATIC METHODS */
        public static void run()
        {
            bool restart = false;
            while (!restart)
            {
                Console.Out.WriteLine("========================================");
                Console.Out.WriteLine(prompt + " Commands Available:");
                int commandCounter = 1;
                foreach (String command in commandsAvailable)
                {
                    Console.Out.WriteLine(commandCounter + (". " + command));
                    commandCounter++;
                }

                Console.Out.Write(prompt + ">");
                String commandRecieved = reader.readString(commandsAvailable);
                switch (commandRecieved.ToLower())
                {
                    case "show coins":
                        showCoins();
                        break;
                    case "import":
                        import();
                        break;
                    case "export":
                         export();
                        break;
                    case "fix fracked":
                        fix();
                        break;
                    case "show folders":
                        showFolders();
                        break;
                    case "quit":
                        Console.Out.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Out.WriteLine("Command failed. Try again.");
                        break;
                }// end switch
            }// end while
        }// end run method


        public static void printWelcome()
        {
            Console.Out.WriteLine("Welcome to RAIDA Tester. A CloudCoin Consortium Opensource.");
            Console.Out.WriteLine("The Software is provided as is, with all faults, defects and errors, and without warranty of any kind.");
            Console.Out.WriteLine("You must have an authentic CloudCoin .stack file called 'testcoin.stack' in the same folder as this program to run tests.");
            Console.Out.WriteLine("The test coin will not be written to.");
        } // End print welcome


        public static void showCoins()
        {
            // This is for consol apps.
            Banker bank = new Banker(fileUtils);
            int[] bankTotals = bank.countCoins(bankFolder);
            int[] frackedTotals = bank.countCoins(frackedFolder);
            // int[] counterfeitTotals = bank.countCoins( counterfeitFolder );

            Console.Out.WriteLine("Total CloudCoins in Bank:" + (bankTotals[0] + frackedTotals[0]));
            Console.Out.WriteLine("Perfect Coins:");

            Console.Out.WriteLine("  1s: " + (bankTotals[1] ));
            Console.Out.WriteLine("  5s: " + (bankTotals[2] ));
            Console.Out.WriteLine(" 25s: " + (bankTotals[3] ));
            Console.Out.WriteLine("100s: " + (bankTotals[4] ));
            Console.Out.WriteLine("250s: " + (bankTotals[5] ));

            Console.Out.WriteLine(("Total Perfect Coins: " + bankTotals[0]));
            Console.Out.WriteLine("");
            if (frackedTotals[0] > 0) {
                Console.Out.WriteLine("Fracked Coins:");
                
                Console.Out.WriteLine("  1s: " + (frackedTotals[1]));
                Console.Out.WriteLine("  5s: " + (frackedTotals[2]));
                Console.Out.WriteLine(" 25s: " + (frackedTotals[3]));
                Console.Out.WriteLine("100s: " + (frackedTotals[4]));
                Console.Out.WriteLine("250s: " + (frackedTotals[5]));
                Console.Out.WriteLine(("Total Fracked Coins: " + frackedTotals[0]));
            }
            else {
                Console.Out.WriteLine("You have no fracked Coins!");
            }//end if not showCoins
        }// end show


        public static void showFolders()
        {
            Console.Out.WriteLine("Your Root folder is " + rootFolder);
            Console.Out.WriteLine("Your Import folder is " + importFolder);
            Console.Out.WriteLine("Your Imported folder is " + importedFolder);
            Console.Out.WriteLine("Your Suspect folder is " + suspectFolder);
            Console.Out.WriteLine("Your Trash folder is " + trashFolder);
            Console.Out.WriteLine("Your Bank folder is " + bankFolder);
            Console.Out.WriteLine("Your Fracked folder is " + frackedFolder);
            Console.Out.WriteLine("Your Templates folder is " + templateFolder);
            Console.Out.WriteLine("Your Directory folder is " + directoryFolder);
            Console.Out.WriteLine("Your Counterfeits folder is " + counterfeitFolder);
            Console.Out.WriteLine("Your Export folder is " + exportFolder);
        } // end show folders


        public static void import()
        {
            Console.Out.WriteLine("This software loads .chest, .stack and .jpg coins into your bank folder.");
            Console.Out.WriteLine("The software will authenticate/take ownership your coins and fix any fracks before importing.");
            Console.Out.WriteLine("Because chest files typically have 1000 coins in them, this process may take over 30 minutes per chest file");
            Console.Out.WriteLine("Any RAIDA servers that are not working may cause (buy may not) cause coins to be fracked. ");
            Console.Out.WriteLine("It is a good idea to make sure the RAIDA is working before importing. ");
            Console.Out.WriteLine("Once your coins are imported into your bank folder, only you will be able to use these coins.");
            Console.Out.WriteLine("If you lose them you may have to wait 2 years to recover them assuming you file a lost coin report.");

            Console.Out.WriteLine("This program will now load all files that are located in your import folder");
            Console.Out.WriteLine("Please put your CloudCoin files in this folder now:" + importFolder);
            Console.Out.WriteLine("Press Enter when you are ready");
            Console.In.Read();
            Console.Out.WriteLine("Loading all CloudCoins in your import folder:" + importFolder);
            Importer importer = new Importer(fileUtils);
            if (!importer.importAll())//Moves all CloudCoins from the Import folder into the Suspect folder. 
            {
                Console.Out.WriteLine("No coins in import folder. Checking if some coins need to be authenticated.");
                Console.Out.WriteLine("Detecting Authentication of Suspect Coins");
                Detector detector = new Detector(fileUtils, 3000);
                int[] detectionResults = detector.detectAll();
                Console.Out.WriteLine(("Total Received in bank: " + (detectionResults[0] + detectionResults[2])));
                // And the bank and the fractured for total
                Console.Out.WriteLine(("Total Counterfeit: " + detectionResults[1]));
                showCoins();
            }
            else {
                // Move all coins to seperate JSON files in the the suspect folder.
                Console.Out.WriteLine("Detecting Authentication of Suspect Coins");
                Detector detector = new Detector(fileUtils, 3000);
                int[] detectionResults = detector.detectAll();
                Console.Out.WriteLine(("Total Received in bank: " + (detectionResults[0] + detectionResults[2])));
                // And the bank and the fractured for total
                Console.Out.WriteLine(("Total Counterfeit: " + detectionResults[1]));
                showCoins();
            }//end if coins to import
        }   // end import


        public static void export()
        {
            Banker bank = new Banker(fileUtils);
            int[] bankTotals = bank.countCoins(bankFolder);
            int[] frackedTotals = bank.countCoins(frackedFolder);
            Console.Out.WriteLine("Your Bank Inventory:");
            int grandTotal = (bankTotals[0] + frackedTotals[0]);
            Console.Out.WriteLine("Total: " + grandTotal);
            Console.Out.WriteLine("  1s: "+ (bankTotals[1] + frackedTotals[1]));
            Console.Out.WriteLine("  5s: "+ (bankTotals[2] + frackedTotals[2]));
            Console.Out.WriteLine(" 25s: "+ (bankTotals[3] + frackedTotals[3]));
            Console.Out.WriteLine("100s: "+ (bankTotals[4] + frackedTotals[4])) ;
            Console.Out.WriteLine("250s: " + (bankTotals[5] + frackedTotals[5]));
            // state how many 1, 5, 25, 100 and 250
            int exp_1 = 0;
            int exp_5 = 0;
            int exp_25 = 0;
            int exp_100 = 0;
            int exp_250 = 0;
            Console.Out.WriteLine("Do you want to export your CloudCoin to (1)jpgs or (2) stack (JSON) file?");
            int file_type = reader.readInt(1, 2);
            // 1 jpg 2 stack
            if ((bankTotals[1] + frackedTotals[1]) > 0)
            {
                Console.Out.WriteLine("How many 1s do you want to export?");
                exp_1 = reader.readInt(0, (bankTotals[1] + frackedTotals[1]));
            }

            // if 1s not zero 
            if ((bankTotals[2] + frackedTotals[2]) > 0)
            {
                Console.Out.WriteLine("How many 5s do you want to export?");
                exp_5 = reader.readInt(0, (bankTotals[2] + frackedTotals[2]));
            }

            // if 1s not zero 
            if ((bankTotals[3] + frackedTotals[3] > 0))
            {
                Console.Out.WriteLine("How many 25s do you want to export?");
                exp_25 = reader.readInt(0, (bankTotals[3] + frackedTotals[3]));
            }

            // if 1s not zero 
            if ((bankTotals[4] + frackedTotals[4]) > 0)
            {
                Console.Out.WriteLine("How many 100s do you want to export?");
                exp_100 = reader.readInt(0, (bankTotals[4] + frackedTotals[4]));
            }

            // if 1s not zero 
            if ((bankTotals[5] + frackedTotals[5]) > 0)
            {
                Console.Out.WriteLine("How many 250s do you want to export?");
                exp_250 = reader.readInt(0, (bankTotals[5] + frackedTotals[5]));
            }

            // if 1s not zero 
            // move to export
            Exporter exporter = new Exporter(fileUtils);
            Console.Out.WriteLine("What tag will you add to the file?");
            String tag = reader.readString(false);
            Console.Out.WriteLine(("Exporting to:" + exportFolder));
            if (file_type == 1)
            {
                exporter.writeJPEGFiles(exp_1, exp_5, exp_25, exp_100, exp_250, tag);
                // stringToFile( json, "test.txt");
            }
            else
            {
                exporter.writeJSONFile(exp_1, exp_5, exp_25, exp_100, exp_250, tag);
            }

            // end if type jpge or stack
            Console.Out.WriteLine("Exporting CloudCoins Completed.");
        }// end export One

        public static void fix() {
            Console.Out.WriteLine("Attempting attempt to fix all fracked coins.");
            Frack_Fixer fixer = new Frack_Fixer(fileUtils, timeout);
            fixer.fixAll();
        }//end fix


    }//End class
}//end namespace
