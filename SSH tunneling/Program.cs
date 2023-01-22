using Renci.SshNet;
using SshNet;
using nsoftware;
using System.Net;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using SSH_tunneling;

namespace MyNamespace
{
    public class Program
    {
        public static SshClient client;
        public static ForwardedPortDynamic port;
        
        public static void Main(string[] args)
        {            
            start("ssh server ip", "username", "password");
            Console.WriteLine($"ip : 127.0.0.1 , port : 35421");
            Console.WriteLine("enter any key to exit.");
            Console.ReadLine();
        }
        public static void start(string server, string user, string password, int serverport = 22)
        {
            client = new SshClient(server, user, password);
            client.KeepAliveInterval = new TimeSpan(0, 0, 5);
            client.ConnectionInfo.Timeout = new TimeSpan(0, 0, 20);
            client.Connect();

            if (client.IsConnected)
            {
                try
                {

                    port = new ForwardedPortDynamic("127.0.0.1", 35421);
                    client.AddForwardedPort(port);
                    port.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}