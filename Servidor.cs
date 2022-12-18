using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Servidor{
 
    public static string data = null;
    public static int numero1 = 12;
    public static int numero2 = 8;
    public static int total;
    public static string funcion = null;

    public static void Escucha(){
        
        byte[] bytes = new Byte[1024];

        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
  
        Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try{
            listener.Bind(localEndPoint);
            listener.Listen(10);

            while (true){
                Console.WriteLine(" ");
                Console.WriteLine("Esperando conexiones..."); 
                Socket handler = listener.Accept();
                data = null;
 
                while (true){
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("!") > -1){
                        Console.WriteLine("Mensaje del Cliente: {0}", data);

                        byte[] msg = Encoding.ASCII.GetBytes("Conexion Establecida Correctamente");
                        handler.Send(msg);

                        funcion = Sumar().ToString();

                        byte[] msg2 = Encoding.ASCII.GetBytes("La suma de 12 + 8 es: "+ funcion);
                        handler.Send(msg2);

                        break;
                    }
                }
 
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

        } catch (Exception e){
            Console.WriteLine(e.ToString());
        }
    }

    public static int Sumar(){
        total = numero1 + numero2;
        return total;
    }

    public static int Main(String[] args){
        Escucha();
        return 0;
    }
}
