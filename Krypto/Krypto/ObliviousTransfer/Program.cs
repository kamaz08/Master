using ObliviousTransfer.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ObliviousTransfer.Model;
using System.Numerics;
using System.Threading;

namespace ObliviousTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Count() < 4)
            {
                Console.Out.WriteLine("4 args: isSerwer (0,1), Ip (127.0.0.1), port, datafile");
                return;
            }
            bool IsSerwer = args[0] == "1";
            var ip = args[1];
            var port = Int32.Parse(args[2]);
            var datafile = args[3];

            if (IsSerwer)
            {
                Console.Out.WriteLine($"Running serwer at {ip} : {port}");
                Serwer(ip, port, datafile);
            }
            else
            {
                Console.Out.WriteLine($"Connecting to serwer at {ip} : {port}");
                Client(ip, port, datafile);
            }
        }
        public static List<string> ReadFile(string file)
        {
            Console.Out.WriteLine($"Reading file {file}");
            var result = new List<string>();
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                    result.Add(sr.ReadLine());
            }
            Console.WriteLine($"Read {result.Count} lines");
            return result;
        }

        public static void Serwer(string ip, int port, string file)
        {
            var pesellist = ReadFile(file);

            var server = new TcpListener(IPAddress.Parse(ip), port);
            server.Start();
            Console.Out.WriteLine("Serwer running\nWaiting for Client");
            var client = server.AcceptTcpClient();
            Console.Out.WriteLine("Client Connection");
            List<string> cliendata;
            var stream = client.GetStream();

            using (var wr = new BinaryReader(stream))
            {
                var data1 = wr.ReadString();
                cliendata = JsonConvert.DeserializeObject<List<string>>(data1);
            }
            Console.Out.WriteLine($"Read {cliendata.Count}");

            var data = new List<byte[]>();
            for(int i =0; i < cliendata.Count; i++)
            {
                data.Add(new byte[32]);
                data[i][0] = (byte) (pesellist.Any(x => x == cliendata[i]) ? 1 : 0);
            }

            var test = new OneOutOfN(data.Count, data);
            var cipher = test.GetCipher();
            var keyList = test.GetKeys();
            client = server.AcceptTcpClient();
            using (var wr = new BinaryWriter(client.GetStream()))
            {
                var cip = JsonConvert.SerializeObject(cipher);
                wr.Write(cip);
            }

            for (int i = 0; i < keyList.Count; i++)
            {
                var elGamal = new ElGamal();

                var priv = new BellareMicali();
                //var pub = new BellareMicali();

                priv.ElGamalModel = elGamal.GenerateElGamalModel();
                var elpubmodel = new ElGamalModel
                {
                    P = priv.ElGamalModel.P,
                    G = priv.ElGamalModel.G
                };
                client = server.AcceptTcpClient();
                using (var wr = new BinaryWriter(client.GetStream()))
                {
                    var data1 = JsonConvert.SerializeObject(elpubmodel);
                    wr.Write(data1);
                }
                priv.C = elGamal.GenerateC(priv.ElGamalModel);
                client = server.AcceptTcpClient();
                using (var wr = new BinaryWriter(client.GetStream()))
                {
                    var data1 = JsonConvert.SerializeObject(priv.C);
                    wr.Write(data1);
                }
                client = server.AcceptTcpClient();
                List<BigInteger> keys;
                using (var wr = new BinaryReader(client.GetStream()))
                {
                    var data1 = wr.ReadString();
                    keys = JsonConvert.DeserializeObject<List<BigInteger>>(data1);
                }
                if (!priv.CheckKeys(keys))
                {
                    Console.WriteLine("Error K0 * K1 != C");
                    return;
                }

                var cipher1 = priv.EncryptData(keys, new List<byte[]> { keyList[i].K0, keyList[i].K1 }, new BigIntegerRandomGenerator());
                client = server.AcceptTcpClient();
                using (var wr = new BinaryWriter(client.GetStream()))
                {
                    var data1 = JsonConvert.SerializeObject(cipher1);
                    wr.Write(data1);
                }
            }


        }

        public static void Client(string ip, int port, string file)
        {
            var test = new OneOutOfN(0, null);
            var pesellist = ReadFile(file);
            var client = new TcpClient();
            client.Connect(IPAddress.Parse(ip), port);
            Console.Out.WriteLine("Client running\nWaiting for Serwer");
            //NetworkStream stream = client.GetStream();
            Console.Out.WriteLine("Serwer Connection");

            Console.Out.WriteLine("Send List");
            var stream = client.GetStream();
            using (var wr = new BinaryWriter(client.GetStream()))
            {
                var data = JsonConvert.SerializeObject(pesellist);
                wr.Write(data);
            }
            client = new TcpClient();
            client.Connect(IPAddress.Parse(ip), port);
            List<byte[]> cipher;
            using (var wr = new BinaryReader(client.GetStream()))
            {
                var data1 = wr.ReadString();
                cipher = JsonConvert.DeserializeObject<List<byte[]>>(data1);
            }

            Console.WriteLine("number of client");
            int a = Int32.Parse(Console.In.ReadLine());
            int tempa = a;
            var keys = new byte[32];
            for (int i = 0; i < 4; i++)
            {
                bool isZero = tempa % 2 == 0;
                tempa /= 2;

                var elGamal = new ElGamal();
                var pub = new BellareMicali();
                ElGamalModel ElGamalModel;
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                using (var wr = new BinaryReader(client.GetStream()))
                {
                    var data1 = wr.ReadString();
                    ElGamalModel = JsonConvert.DeserializeObject<ElGamalModel>(data1);
                }

                pub.ElGamalModel = new ElGamalModel
                {
                    P = ElGamalModel.P,
                    G = ElGamalModel.G
                };

                BigInteger C;
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                using (var wr = new BinaryReader(client.GetStream()))
                {
                    var data1 = wr.ReadString();
                    C = JsonConvert.DeserializeObject<BigInteger>(data1);
                }

                pub.C = C;

                pub.IsZero = isZero;

                pub.K = elGamal.GenerateC(pub.ElGamalModel);
                var keysPair = pub.GenerateKeyPairPublic();
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                using (var wr = new BinaryWriter(client.GetStream()))
                {
                    var data = JsonConvert.SerializeObject(keysPair);
                    wr.Write(data);
                }

                List<ElGamalData> cipher1;
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                using (var wr = new BinaryReader(client.GetStream()))
                {
                    var data1 = wr.ReadString();
                    cipher1 = JsonConvert.DeserializeObject<List<ElGamalData>>(data1);
                }
                var result = pub.DecryptData(cipher1);
                keys = test.xor(test.sha(result), keys);
            }
            Console.WriteLine($"{pesellist.Count} {a}");
            Console.WriteLine(keys[0]);
            Console.WriteLine($"{pesellist[a]} {(test.xor(cipher[a], keys)[0] == 0 ? "not " : "")}is on the list");

        }
    }
}
