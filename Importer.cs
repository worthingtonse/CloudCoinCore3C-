using System;
using System.IO;
using System.Linq;

namespace Foundation
{
    class Importer
    {
        /* INSTANCE VARAIBLES */ 
        FileUtils fileUtils;
        
        /* CONSTRUCTOR */
        public Importer(FileUtils fileUtils)
        {
            this.fileUtils = fileUtils;
        }//Constructor

        /* PUBLIC METHODS */
        public bool importAll()
        {
            String[] fnames = new DirectoryInfo(this.fileUtils.importFolder).GetFiles().Select(o => o.Name).ToArray();//Get a list of all in the folder except the directory "imported"
  
            if (fnames.Length == 0)
            {
                Console.Out.WriteLine("There were no CloudCoins to import. Please place our CloudCoin .jpg and .stack files in your imports" + " folder at " + this.fileUtils.importFolder );
                return false;
            }
            else
            {
                Console.Out.WriteLine("Importing the following files:");
                for (int i = 0; i < fnames.Length; i++)// Loop through each file. 
                { 
                    Console.Out.WriteLine(fnames[i]);
                    this.importOneFile(fnames[i]);
                } // end for each file name
                return true;
            }//end if no files 
        }// End Import All


        /* PRIVATE METHODS */

        /* IMPORT ONE FILE. COULD BE A JPEG, STACK or CHEST */
        private bool importOneFile(String fname)
        {
            String extension = "";
            int indx = fname.LastIndexOf('.');//Get file extension
            if (indx > 0)
            {
                extension = fname.Substring(indx + 1);
            }

            extension = extension.ToLower();
            if (extension == "jpeg" || extension == "jpg")//Run if file is a jpeg
            {
                if (!this.importJPEG(fname))
                {
                    if (!File.Exists(this.fileUtils.trashFolder + fname))
                    {
                        File.Move(this.fileUtils.importFolder + fname, this.fileUtils.trashFolder + fname);
                    }
                    return false;//"Failed to load JPEG file");
                }//end if import fails
            }//end if jpeg
            else if (!this.importStack(fname))// run if file is a stack
            {
                if (! File.Exists(this.fileUtils.trashFolder + fname)) {
                    File.Move(this.fileUtils.importFolder + fname, this.fileUtils.trashFolder + fname);
                }
                return false;//"Failed to load .stack file");
            }

            if (!File.Exists(this.fileUtils.importedFolder + fname))
            {
                File.Move(this.fileUtils.importFolder + fname, this.fileUtils.importedFolder + fname);
            }//End if the file is there
            return true;
        }//End importOneFile


        /* IMPORT ONE JPEG */
        private bool importJPEG(String fileName)//Move one jpeg to suspect folder. 
        {
            bool isSuccessful = false;
            Console.Out.WriteLine("Trying to load: " + this.fileUtils.importFolder + fileName );
            try
            {
                Console.Out.WriteLine("Loading coin: " + fileUtils.importFolder + fileName);
                CloudCoin tempCoin = this.fileUtils.loadOneCloudCoinFromJPEGFile( fileUtils.importFolder + fileName );
                Console.Out.WriteLine("Loaded coin filename: " + tempCoin.fileName);
                this.fileUtils.writeTo(this.fileUtils.suspectFolder, tempCoin);
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Console.Out.WriteLine("File not found: " + fileName + ex);
            }
            catch (IOException ioex)
            {
                Console.Out.WriteLine("IO Exception:" + fileName + ioex);
            }// end try catch
            return isSuccessful;
        }

        /* IMPORT ONE STACK FILE */
        private bool importStack(String fileName)
        {
            bool isSuccessful = false;
            //  System.out.println("Trying to load: " + importFolder + fileName );
            try
            {
                CloudCoin[] tempCoin = this.fileUtils.loadManyCloudCoinFromJsonFile(this.fileUtils.importFolder + fileName);

                for (int i = 0; i < tempCoin.Length; i++ )
                {
                    this.fileUtils.writeTo(this.fileUtils.suspectFolder, tempCoin[i]);
                }//end for each temp Coin
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Console.Out.WriteLine("File not found: " + fileName + ex);
            }
            catch (IOException ioex)
            {
                Console.Out.WriteLine("IO Exception:" + fileName + ioex);
            }

            // end try catch
            return isSuccessful;
        }

    }//end class
}//end namespace
