using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;


namespace Foundation
{
    class RAIDA
    {
        /* INSTANCE VARIABLE */
        public DetectionAgent[] agent;
        public int milliSecondsToTimeOut;
        public CloudCoin returnCoin;
        private String ticketStatus0 = "empty";
        private String ticketStatus1 = "empty";
        private String ticketStatus2 = "empty";
        private String ticket1 = "";
        private String ticket2 = "";
        private String ticket3 = "";

        private int[] working_triad = { 0, 1, 2 };//place holder
        public bool[] raidaIsDetecting = { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        public string[] lastDetectStatus = { "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected", "notdetected" };
        /* CONSTRUCTOR */
        public RAIDA(int milliSecondsToTimeOut)
        { //  initialise instance variables
            this.milliSecondsToTimeOut = milliSecondsToTimeOut;
            this.agent = new DetectionAgent[25];
            for (int i = 0; (i < 25); i++)
            {
                this.agent[i] = new DetectionAgent(i, milliSecondsToTimeOut);
            } // end for each Raida
        }//End Constructor

       
                public string[] echo()
                {
                    Stopwatch sw = new Stopwatch();
                    string[] results = new string[25];
                    int i = 0;
                    sw.Start();
                    for(int raidaID = 0; raidaID < 25; raidaID++)
                    {
                        DetectionAgent da = new DetectionAgent(raidaID, 2000);
                        da.echo(raidaID);
                        if (da.lastResponse.Contains("ready"))//echo was good
                         {
                                 results[raidaID] = "ready";
                          }
                         else //echo was bad
                          {
                               results[raidaID] = "notready";
                          }//end if pass

                  }
            return results;
                }//end echo

                public DetectResponse Detect(CloudCoin coin)
                {
                    var client = new RestClient();
                    client.BaseUrl = BaseUri;
                    var request = new RestRequest("detect");
                    request.AddQueryParameter("nn", coin.nn.ToString());
                    request.AddQueryParameter("sn", coin.sn.ToString());
                    request.AddQueryParameter("an", coin.an[Number]);
                    request.AddQueryParameter("pan", coin.pans[Number]);
                    request.AddQueryParameter("denomination", Utils.Denomination2Int(coin.denomination).ToString());
                    request.Timeout = 2000;
                    DetectResponse getDetectResult = new DetectResponse();

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        getDetectResult = JsonConvert.DeserializeObject<DetectResponse>(client.Execute(request).Content);
                    }
                    catch (JsonException e)
                    {
                        getDetectResult = new DetectResponse(Name, coin.sn.ToString(), "Invalid response", "THe server does not respond or returns invalid data", DateTime.Now.ToString());
                    }
                    getDetectResult = getDetectResult ?? new DetectResponse(Name, coin.sn.ToString(), "Network problem", "Node not found", DateTime.Now.ToString());
                    if (getDetectResult.ErrorException != null)
                        getDetectResult = new DetectResponse(Name, coin.sn.ToString(), "Network problem", "Problems with network connection", DateTime.Now.ToString());

                    sw.Stop();
                    getDetectResult.responseTime = sw.Elapsed;

                    return getDetectResult;
                }
                */
        /* PUBLIC METHODS */
        public CloudCoin detectCoin(CloudCoin cc)
        {
            returnCoin = cc;

            var t00 = Task.Factory.StartNew(() => detectOne(00, cc.nn, cc.sn, cc.ans[00], cc.pans[00], cc.getDenomination()));
            var t01 = Task.Factory.StartNew(() => detectOne(01, cc.nn, cc.sn, cc.ans[01], cc.pans[01], cc.getDenomination()));
            var t02 = Task.Factory.StartNew(() => detectOne(02, cc.nn, cc.sn, cc.ans[02], cc.pans[02], cc.getDenomination()));
            var t03 = Task.Factory.StartNew(() => detectOne(03, cc.nn, cc.sn, cc.ans[03], cc.pans[03], cc.getDenomination()));
            var t04 = Task.Factory.StartNew(() => detectOne(04, cc.nn, cc.sn, cc.ans[04], cc.pans[04], cc.getDenomination()));
            var t05 = Task.Factory.StartNew(() => detectOne(05, cc.nn, cc.sn, cc.ans[05], cc.pans[05], cc.getDenomination()));
            var t06 = Task.Factory.StartNew(() => detectOne(06, cc.nn, cc.sn, cc.ans[06], cc.pans[06], cc.getDenomination()));
            var t07 = Task.Factory.StartNew(() => detectOne(07, cc.nn, cc.sn, cc.ans[07], cc.pans[07], cc.getDenomination()));
            var t08 = Task.Factory.StartNew(() => detectOne(08, cc.nn, cc.sn, cc.ans[08], cc.pans[08], cc.getDenomination()));
            var t09 = Task.Factory.StartNew(() => detectOne(09, cc.nn, cc.sn, cc.ans[09], cc.pans[09], cc.getDenomination()));
            var t10 = Task.Factory.StartNew(() => detectOne(10, cc.nn, cc.sn, cc.ans[10], cc.pans[10], cc.getDenomination()));
            var t11 = Task.Factory.StartNew(() => detectOne(11, cc.nn, cc.sn, cc.ans[11], cc.pans[11], cc.getDenomination()));
            var t12 = Task.Factory.StartNew(() => detectOne(12, cc.nn, cc.sn, cc.ans[12], cc.pans[12], cc.getDenomination()));
            var t13 = Task.Factory.StartNew(() => detectOne(13, cc.nn, cc.sn, cc.ans[13], cc.pans[13], cc.getDenomination()));
            var t14 = Task.Factory.StartNew(() => detectOne(14, cc.nn, cc.sn, cc.ans[14], cc.pans[14], cc.getDenomination()));
            var t15 = Task.Factory.StartNew(() => detectOne(15, cc.nn, cc.sn, cc.ans[15], cc.pans[15], cc.getDenomination()));
            var t16 = Task.Factory.StartNew(() => detectOne(16, cc.nn, cc.sn, cc.ans[16], cc.pans[16], cc.getDenomination()));
            var t17 = Task.Factory.StartNew(() => detectOne(17, cc.nn, cc.sn, cc.ans[17], cc.pans[17], cc.getDenomination()));
            var t18 = Task.Factory.StartNew(() => detectOne(18, cc.nn, cc.sn, cc.ans[18], cc.pans[18], cc.getDenomination()));
            var t19 = Task.Factory.StartNew(() => detectOne(19, cc.nn, cc.sn, cc.ans[19], cc.pans[19], cc.getDenomination()));
            var t20 = Task.Factory.StartNew(() => detectOne(20, cc.nn, cc.sn, cc.ans[20], cc.pans[20], cc.getDenomination()));
            var t21 = Task.Factory.StartNew(() => detectOne(21, cc.nn, cc.sn, cc.ans[21], cc.pans[21], cc.getDenomination()));
            var t22 = Task.Factory.StartNew(() => detectOne(22, cc.nn, cc.sn, cc.ans[22], cc.pans[22], cc.getDenomination()));
            var t23 = Task.Factory.StartNew(() => detectOne(23, cc.nn, cc.sn, cc.ans[23], cc.pans[23], cc.getDenomination()));
            var t24 = Task.Factory.StartNew(() => detectOne(24, cc.nn, cc.sn, cc.ans[24], cc.pans[24], cc.getDenomination()));


            var taskList = new List<Task> { t00, t01, t02, t03, t04, t05, t06, t07, t08, t09, t10, t11 , t12 , t13 , t14 , t15 , t16 , t17 , t18 , t19 , t20 , t21 , t22 , t23,  t24   };
            Task.WaitAll( taskList.ToArray(), milliSecondsToTimeOut);
            //Get data from the detection agents

            for (int i = 0; i < 25; i++)
            {
                returnCoin.pastStatus[i] = lastDetectStatus[i];
            }//end for each detection agent

            returnCoin.setAnsToPansIfPassed();
            returnCoin.calculateHP();
            returnCoin.gradeCoin(); // sets the grade and figures out what the file extension should be (bank, fracked, counterfeit, lost
            returnCoin.calcExpirationDate();
            returnCoin.grade();

            return returnCoin;
        }//end detect coin


        public void detectOne(int i, int nn, int sn, String an, String pan, int d)
        {
            string urlAddress = "https://raida"+ i +".cloudcoin.global/service/detect?nn=" + nn + "&sn=" + sn + "&an=" + an + "&pan=" + pan + "&denomination=" + d + "&b=t";
           // Console.Out.Write(".");
            string data = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
           // request.ContinueTimeout = readTimeout;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
            DateTime before = DateTime.Now;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine( ex.Message );
            }


            DateTime after = DateTime.Now; TimeSpan ts = after.Subtract(before);
            long dms = ts.Milliseconds;
         //   Console.Out.Write(" " + dms + " ");
            if ( data.Contains("pass") )
            {
                lastDetectStatus[i] = "pass";
            }
            else if ( data.Contains("fail") && data.Length < 200)//less than 200 incase their is a fail message inside errored page
            {
                lastDetectStatus[i] = "fail";
            }
            else
            {
                lastDetectStatus[i] = "error";
            }

        }//end get HTML

      
        public CloudCoin fixCoin(CloudCoin brokeCoin)
        {

            brokeCoin.setAnsToPans();// Make sure we set the RAIDA to the cc ans and not new pans. 
            DateTime before = DateTime.Now;

            String fix_result = "";
            FixitHelper fixer;
            String[] trustedServerAns;
            int corner = 1;

            // For every guid, check to see if it is fractured
            for (int guid_id = 0; guid_id < 25; guid_id++)
            {
               // Console.WriteLine("Past Status for " + guid_id + ", " + brokeCoin.pastStatus[guid_id]);
            
                if (brokeCoin.pastStatus[guid_id] == "fail")
                { // This guid has failed, get tickets 


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Out.WriteLine("");
                    Console.WriteLine("Attempting to fix RAIDA " + guid_id);
                    Console.Out.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.White;

                    
                    fixer = new FixitHelper(guid_id, brokeCoin.ans);

                    trustedServerAns = new String[] { brokeCoin.ans[fixer.currentTriad[0]], brokeCoin.ans[fixer.currentTriad[1]], brokeCoin.ans[fixer.currentTriad[2]] };
                    corner = 1;
                    while (!fixer.finnished)
                    {
                        Console.WriteLine(" Using corner " + corner );
                        fix_result = "";
                        trustedServerAns  = new String[] { brokeCoin.ans[fixer.currentTriad[0]], brokeCoin.ans[fixer.currentTriad[1]], brokeCoin.ans[fixer.currentTriad[2]] };
                        get_tickets(fixer.currentTriad, trustedServerAns, brokeCoin.nn, brokeCoin.sn, brokeCoin.getDenomination());
                        // See if there are errors in the tickets                  
                        if (ticketStatus0 != "ticket" || ticketStatus1 != "ticket" || ticketStatus2 != "ticket")
                        {// No tickets, go to next triad corner 
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Out.WriteLine("");
                            Console.Out.WriteLine("Failed using corner " + corner);
                            Console.Out.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.White;   

                            //check for more fails. 
                            //   if (ticketStatus0 == "fail") { brokeCoin.pastStatus[ fixer.currentTriad[0] ] = "fail";  }//end if t0 fail
                            //  if (ticketStatus1 == "fail") { brokeCoin.pastStatus[ fixer.currentTriad[1] ] = "fail"; }//end if t1 fail
                            //   if (ticketStatus2 == "fail") { brokeCoin.pastStatus[ fixer.currentTriad[2] ] = "fail"; }//end if t2 fail
                            //Console.WriteLine("Done with corner " + corner);
                            corner++;
                            fixer.setCornerToCheck(corner);
                        
                        }
                        else
                        {
                            // Has three good tickets   
                            fix_result = this.agent[guid_id].fix(fixer.currentTriad, ticket1, ticket2, ticket3, brokeCoin.ans[guid_id]);
                            if (fix_result == "success")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Out.WriteLine("");
                                Console.Out.WriteLine("Successfully unfracked RAIDA " + guid_id );
                                Console.Out.WriteLine("");
                                Console.ForegroundColor = ConsoleColor.White;
                                brokeCoin.pastStatus[guid_id] = "pass";
                                fixer.finnished = true;
                                corner = 1;
                            }
                            else
                            { // command failed,  need to try another corner
                                corner++;
                                fixer.setCornerToCheck(corner);
                                if (fixer.finnished)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Out.WriteLine("");
                                    Console.Out.WriteLine("Failed using corner " + corner);
                                    Console.Out.WriteLine("");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Out.WriteLine("");
                                    Console.Out.WriteLine("Failed to unfrack RAIDA " + guid_id);
                                    Console.Out.WriteLine("");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }//end if else fix was successful
                        }//end if else one of the tickts has an error. 
                    }//end while fixer not finnihsed. 
                }// end if guid is fail
            }//end for all the guids

            DateTime after = DateTime.Now;
            TimeSpan ts = after.Subtract(before);
            Console.WriteLine("Time spent fixing RAIDA in milliseconds: " + ts.Milliseconds);

            brokeCoin.calculateHP();//how many fails did it get
            brokeCoin.gradeCoin();
            // sets the grade and figures out what the file extension should be (bank, fracked, counterfeit, lost
            brokeCoin.calcExpirationDate();
            brokeCoin.grade();
            return brokeCoin;

        }// end fix coin


        public void get_tickets(int[] triad, String[] ans, int nn, int sn, int denomination)
        {
            string[] returnTicketsStatus = { "error", "error", "error" };
            Console.Out.WriteLine(" Requesting Kerberus tickets from trusted servers " + triad[0] + ", " + triad[1] + " and " + triad[2]);
            getTicket(0, triad[0], nn, sn, ans[0], denomination);
            getTicket(1, triad[1], nn, sn, ans[1], denomination);
            getTicket(2, triad[2], nn, sn, ans[2], denomination);
            Console.Out.WriteLine(" Response: " + ticketStatus0 + ", " + ticketStatus1 + " and " + ticketStatus2);
        }//end get_tickets



        public void getTicket(int i, int raidaID, int nn, int sn, String an, int d)
        {
            string lastRequest  = "https://raida" + raidaID + ".cloudcoin.global/service/get_ticket?nn=" + nn + "&sn=" + sn + "&an=" + an + "&pan=" + an + "&denomination=" + d;
          //  Console.Out.Write("Request: " + lastRequest);
            string data = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(lastRequest);
            // request.ContinueTimeout = readTimeout;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
            DateTime before = DateTime.Now;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
            }
            catch (Exception ex)
            {
               // Console.Out.WriteLine(ex.Message);
            }
           // Console.Out.WriteLine("The data is " + data);
            if ( data.Contains("ticket"))
            {
                String[] KeyPairs = data.Split(',');
                String message = KeyPairs[3];
                int startTicket = ordinalIndexOf(message, "\"", 3) + 2;
                int endTicket = ordinalIndexOf(message, "\"", 4) - startTicket;
                string lastTicket = message.Substring(startTicket - 1, endTicket + 1);
                string lastTicketStatus = "ticket";
               // Console.Out.WriteLine(" RAIDA" + raidaID +": Ticket: " + lastTicket);
                switch (i)//Which ticket is this?
                {
                    case 0:
                        ticketStatus0 = lastTicketStatus;
                        ticket1 = lastTicket;
                 
                        break;
                    case 1:
                        ticketStatus1 = lastTicketStatus;
                        ticket2 = lastTicket;
                 
                        break;
                    case 2:
                        ticketStatus2 = lastTicketStatus;
                        ticket3 = lastTicket;
  
                        break;
                }//end switch
            }
            else if (data.Contains("fail"))
            {
                switch (i)//Which ticket is this?
                {
                    case 0:
                        ticketStatus0 = "fail";
                        ticket1 = "fail";

                        break;
                    case 1:
                        ticketStatus1 = "fail";
                        ticket2 = "fail";

                        break;
                    case 2:
                        ticketStatus2 = "fail";
                        ticket3 = "fail";

                        break;
                }//end switch
            }//end if //  Console.Out.Write("Response" + data);
        }//end get ticket



        /**
      * Method ordinalIndexOf used to parse cloudcoins. Finds the nth number of a character within a string
      *
      * @param str The string to search in
      * @param substr What to count in the string
      * @param n The nth number
      * @return The index of the nth number
      */
        public int ordinalIndexOf(string str, string substr, int n)
        {
            int pos = str.IndexOf(substr);
            while (--n > 0 && pos != -1)
            {
                pos = str.IndexOf(substr, (pos + 1));
            }
            return pos;
        }//end ordinal Index of
       
        /*
            working_nn = nn;
            working_sn = sn;
            working_ans = ans;
            working_getDenomination = denomination;
            working_triad = triad;

            ThreadStart detectThread0Start = new ThreadStart(fixThread0);
            ThreadStart detectThread1Start = new ThreadStart(fixThread1);
            ThreadStart detectThread2Start = new ThreadStart(fixThread2);


            Thread[] fixThread = new Thread[3];
            fixThread[0] = new Thread(detectThread0Start);
            fixThread[1] = new Thread(detectThread1Start);
            fixThread[2] = new Thread(detectThread2Start);


            foreach (Thread myThread in fixThread)
            {
                //Console.Out.WriteLine("Starting a get ticket thread.");
                myThread.Start();
                if (!myThread.Join(TimeSpan.FromSeconds(milliSecondsToTimeOut)))
                {
                    myThread.Abort();
                }//End if timeout. 
            }//End start each thread for each

            returnTicketsStatus[0] = agent[working_triad[0]].lastTicketStatus;
            returnTicketsStatus[1] = agent[working_triad[1]].lastTicketStatus;
            returnTicketsStatus[2] = agent[working_triad[2]].lastTicketStatus;

            return returnTicketsStatus;
        }//end detectCoin
        */

        public void fixThread0()
        {
            agent[working_triad[0]].get_ticket(working_nn, working_sn, working_ans[0], working_getDenomination);
           Console.Out.WriteLine(agent[working_triad[0]].lastRequest);
           Console.Out.WriteLine();
            if (agent[working_triad[0]].lastResponse.Contains("ticket"))
            {
                ticketStatus0 = "ticket";
                ticket1 = agent[working_triad[0]].lastTicket;
               // Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                ticketStatus0 = "fail";
                //Console.ForegroundColor = ConsoleColor.Red;
            }//end if ticket fail
          //  Console.Out.WriteLine("ticketStatus 0 " + ticketStatus0 + ", ticket 1 " + ticket1);
            Console.Out.WriteLine(agent[working_triad[0]].lastResponse);
            Console.Out.WriteLine();
           // Console.ForegroundColor = ConsoleColor.White;
        }
        public void fixThread1()
        {
            agent[working_triad[1]].get_ticket(working_nn, working_sn, working_ans[1], working_getDenomination);
            Console.Out.WriteLine(agent[working_triad[1]].lastRequest);
            Console.Out.WriteLine();
            if (agent[working_triad[1]].lastResponse.Contains("ticket"))
            {
                ticketStatus1 = "ticket";
                ticket2 = agent[working_triad[1]].lastTicket;
                //Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                ticketStatus1 = "fail";
               // Console.ForegroundColor = ConsoleColor.Red;
            }
           // Console.Out.WriteLine("ticketStatus 1 " + ticketStatus1 + ", ticket 2 " + ticket2);
            Console.Out.WriteLine(agent[working_triad[1]].lastResponse);
            Console.Out.WriteLine();
           // Console.ForegroundColor = ConsoleColor.White;
        }
        public void fixThread2()
        {
            agent[working_triad[2]].get_ticket(working_nn, working_sn, working_ans[2], working_getDenomination);
            Console.Out.WriteLine(agent[working_triad[2]].lastRequest);
            Console.Out.WriteLine();
            if (agent[working_triad[2]].lastResponse.Contains("ticket"))
            {
                ticketStatus2 = "ticket";
                ticket3 = agent[working_triad[2]].lastTicket;
               // Console.ForegroundColor = ConsoleColor.Green;
            }
            else 
            {
                ticketStatus2 = "fail";
              //  Console.ForegroundColor = ConsoleColor.Red;
            }
           Console.Out.WriteLine("ticketStatus 2 " + ticketStatus2 + ", ticket 3 " + ticket3);
            Console.Out.WriteLine(agent[working_triad[2]].lastResponse);
            //Console.Out.WriteLine();
           // Console.ForegroundColor = ConsoleColor.White;
        }


    }
}
