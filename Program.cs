using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.Reflection.Metadata;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;



namespace TechnologyOneTest

{
    public class HttpServer
    {
        public static HttpListener listener;
        public static int PORT = 8000;
        public static string url = $"http://localhost:{PORT}/";
        
        public static string pageData = new StreamReader("index.html").ReadToEnd();

        public static string[] ones = ["ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN"]; 
        public static string[] teens = ["ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"];
        public static string[] tens = ["TWENTY", "THRITY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINTY"];
        public static string[] powers = ["HUNDRED", "THOUSAND", "MILLION", "BILLION"]; 
        public static string NumToName(int val) { 
            //this should all probably be a switch case...............
            if (val == 0) {
                return "";
            }
            /* int[] n = [1000000000, 1000000, 1000];
            foreach (int i in n) { 
                if (val >= i) {
                    if (val % i == 0) {
                        return NumToName(val/i) + " " + powers[3];
                } else if (val - ((val/d) * d) < 100) {
                    return NumToName(val/d) + " " + powers[3] + " AND " + NumToName(val - ((val/d) * d));
                } else {
                    return NumToName(val / d) + " " + powers[3] + ", " + NumToName(val - ((val/d) * d));
                }
            }
            } */
            else if (val >= 1000000000) {
                if (val % 1000000000 == 0) {
                    return NumToName(val/1000000000) + " " + powers[3];
                } else if (val - ((val/1000000000) * 1000000000) < 100) {
                    return NumToName(val/1000000000) + " " + powers[3] + " AND " + NumToName(val - ((val/1000000000) * 1000000000));
                } else {
                    return NumToName(val / 1000000000) + " " + powers[3] + ", " + NumToName(val - ((val/1000000000) * 1000000000));
                }
            }
            
            else if (val >= 1000000) {
                if (val % 1000000 == 0) {
                    return NumToName(val/1000000) + " " + powers[2];
                } else if (val - ((val/1000000) * 1000000) < 100) {
                    return NumToName(val/1000000) + " " + powers[2] + " AND " + NumToName(val - ((val/1000000) * 1000000));
                } else {
                    return NumToName(val / 1000000) + " " + powers[2] + ", " + NumToName(val - ((val/1000000) * 1000000));
                }
            }

            else if (val >= 1000) {
                if (val % 1000 == 0) {
                    return NumToName(val/1000) + " " + powers[1];
                } else if (val - ((val/1000) * 1000) < 100) {
                    return NumToName(val/1000) + " " + powers[1] + " AND " + NumToName(val - ((val/1000) * 1000));
                } else {
                    return NumToName(val / 1000) + " " + powers[1] + ", " + NumToName(val - ((val/1000) * 1000));
                }
            }

            else if (val >= 100) {
                if (val % 100 == 0) {
                    return ones[(val / 100) - 1] + " " + powers[0];
                } else {
                return ones[(val / 100) - 1] + " " + powers[0] + " AND " + NumToName(val - ((val / 100) * 100)); 
                }
            }

            else if (val >= 20) {
                if (val % 10 != 0) //if tens only, does not need hyphen
                {   
                    return tens[(val / 10) - 2] + "-" + NumToName(val - ((val / 10) * 10));
                } else {
                    return tens[(val / 10) - 2];
                }
            }

            else if (0 < val && val <= 10) {
                return ones[val - 1];
            } 

            else if (10 < val && val < 20) {
                return teens[(val % 10 )- 1];
            }

        return ""; //if any other input, return empty 
        }    
        public static async Task HandleIncomingConnections() {
            bool running = true;

            while (running)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest req = context.Request;
                HttpListenerResponse res = context.Response;

                var inputStream = new StreamReader(req.InputStream).ReadToEnd();
                var clientInput = ""; //store client input as string TODO: input verification
                var clientOutput = ""; //process client input to desired format, returned on request
                double val; 
                int dollarsInt;
                int centsInt;
                //Write Request Header to Console
                Console.WriteLine();
                Console.WriteLine(req.RawUrl.ToString());
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);

                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/input"))
                {
                    clientInput = inputStream.Substring(5); //TODO: fix this to search for substring and remove rather than hard code num
                    Console.WriteLine("Recieved input: {0}", clientInput);
                    //Process Client Input 

                    //validate input: decimal number to two decimals (round)
                    try {
                        //remove all values except numeric and decimal place

                        //parse input as double 
                        val = double.Parse(clientInput); 
                        Console.WriteLine($"Successfully parsed input {val}");

                        //Format double value to integers Dollars and Cents
                        dollarsInt = (int) Math.Floor(val);
                        centsInt = (int) (Math.Round(val - dollarsInt, 2) * 100);

                        //Create Strings for Output
                        if (dollarsInt != 0) {
                            clientOutput += NumToName(dollarsInt) + " DOLLARS";
                        }

                        if (centsInt != 0) { 
                            if (clientOutput != "") {
                                clientOutput += " AND ";
                            }
                            clientOutput += NumToName(centsInt) + " CENTS";
                        }
                        Console.WriteLine(clientOutput);


                    } catch (FormatException e) {
                        Console.WriteLine($"Could not parse input '{clientInput}' ");   //message sent to console
                        clientOutput = "INVALID INPUT!";                                //message sent to webpage
                    }
                }

                //Response Info
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, clientOutput));    //formats html doc string with clientOutput 
                res.ContentType = "text/html";
                res.ContentEncoding = Encoding.UTF8;
                res.ContentLength64 = data.LongLength;

                //Output Resp
                await res.OutputStream.WriteAsync(data,0,data.Length);
                res.Close();
            }
        }
            public static void Main(string[] args)
        {
            //Start server
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine($"Listening on {url}");

            //Requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            //Close
            listener.Close();
        }
    }
}