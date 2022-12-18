using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Cliente{

    public static void EnviaMSJ(){
         
        byte[] bytes = new byte[1024];

        try{ 
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try{
                sender.Connect(remoteEP);

                Console.WriteLine("Conexion exitosa al servidor: {0}", sender.RemoteEndPoint.ToString());
 
                byte[] msg = Encoding.ASCII.GetBytes("EJECUTAR!");
 
                int bytesSent = sender.Send(msg);
 
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("Servidor: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                int bytesRec2 = sender.Receive(bytes);
                Console.WriteLine("Servidor: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec2));

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            } catch (ArgumentNullException ane) {
                Console.WriteLine("Error: {0}", ane.ToString());
            } catch (SocketException se) {
                Console.WriteLine("Error: {0}", se.ToString());
            } catch (Exception e) {
                Console.WriteLine("Error: {0}", e.ToString());
            }

        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args){
        EnviaMSJ();
        return 0;
    }
}
