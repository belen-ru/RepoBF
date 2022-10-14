using figurasTCP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace figurasTCP.Services
{
    internal class ClientServices
    {

        TcpClient cliente = new TcpClient();
        public ClientServices(string ip, Usuario1 usuario)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 55551);
            cliente.Connect(ipe);                
            Thread.Sleep(1000);
         

           
        }

        public void Enviar(figura figu)
        {
            try
            {
                var json = JsonConvert.SerializeObject(figu);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                var stream = cliente.GetStream();
                stream.Write(buffer, 0, buffer.Length);

            }
            catch (Exception)
            {
                cliente.Close();
            }

        }


        public void Cerrar()
        {
            cliente.Close();
        }
    }
}
