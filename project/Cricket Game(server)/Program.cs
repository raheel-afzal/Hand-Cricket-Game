using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cricket_Game_server_
{
    class Program
    {
        static TcpClient p1, p2;
        static void Main(string[] args)
        {

            
            int target = 0;
          //  TcpListener server = new TcpListener(System.Net.IPAddress.Parse("192.168.210.251") ,9999);
            TcpListener server = new TcpListener(9999);
            server.Start();
            Console.WriteLine("Server Started");
            string sign = "1";
            for (int i = 1; i <= 2; i++)
            {    
                TcpClient c = server.AcceptTcpClient();
                if (i == 1)
                    p1 = c;
                else
                    p2 = c;
            }

            BinaryFormatter fobj = new BinaryFormatter();
            fobj.Serialize(p1.GetStream(), "1");
            fobj.Serialize(p2.GetStream(), "0");
            string pmsg = "";
            while (true)
            {


                pmsg = (string)fobj.Deserialize(p1.GetStream());
                fobj.Serialize(p2.GetStream(), pmsg);
                pmsg = (string)fobj.Deserialize(p2.GetStream());
                string[] response = pmsg.Split(',');
                if (response[2] == "5")
                {

                    target = int.Parse(response[1].ToString()) + 1;

                    fobj.Serialize(p1.GetStream(), "0" + "," + "0" + "," + "0" + "," + (string)target.ToString());
                    fobj.Serialize(p2.GetStream(), "1" + "," + "0" + "," + "0" + "," + (string)target.ToString());
                    
                   


                    while (true)
                    {
                       
                        pmsg = (string)fobj.Deserialize(p2.GetStream());
                        fobj.Serialize(p1.GetStream(), pmsg);
                        pmsg = (string)fobj.Deserialize(p1.GetStream());

                        response = pmsg.Split(',');

                        if ((int.Parse(response[1])) > target)
                        {
                            fobj.Serialize(p2.GetStream(), "win" + "," + "0");
                            
                            fobj.Serialize(p1.GetStream(), "lose" + ", " + "0");
                        }
                        else if (response[2] == "5"&& ((int.Parse(response[1]))-1)== (target-1))
                        {
                            fobj.Serialize(p2.GetStream(), "Draw" + "," + "0");
                            
                            fobj.Serialize(p1.GetStream(), "Draw" + "," + "0");
                        }
                        else if (response[2] == "5" && (int.Parse(response[1]))<target)
                        {

                            fobj.Serialize(p2.GetStream(), "lose" + "," + "0");
                           
                            fobj.Serialize(p1.GetStream(), "win" + "," + "0");

                           
                        }
                      
                        else { fobj.Serialize(p2.GetStream(), pmsg); }
                    }

                }
                else
                {
                   
                    fobj.Serialize(p1.GetStream(), pmsg);
                }


            }
        }







        }

    }


