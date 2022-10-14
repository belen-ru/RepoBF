using figurasTCP.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using Newtonsoft.Json;

namespace figurasTCP.Services
{
    internal class userServices
    {
        TcpListener servidor;

        public userServices()
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 55551);
            servidor = new TcpListener(ipe);
            Thread hilo = new Thread(new ThreadStart(Escuchar));
            hilo.Start();
        }

        public void Escuchar()
        {
              if(servidor!=null)
            {
                servidor.Start();

                while (servidor.Server.IsBound)
                {
                    TcpClient concect = servidor.AcceptTcpClient();
                    Thread hiloRecibir = new Thread(new ParameterizedThreadStart(Recibir));
                    hiloRecibir.Start(concect);


                }
            }

        }

        public void Recibir(Object usuario)
        {
            TcpClient usu = (TcpClient)usuario;
            while (usu.Connected)
            {
                if (usu.Available > 0)
                {
                    byte[] buffer = new byte[usu.Available];
                    var stream = usu.GetStream();
                    stream.Read(buffer, 0, buffer.Length);
                    var paq = JsonConvert.DeserializeObject<figura>(Encoding.UTF8.GetString(buffer));

                    if (paq != null)
                    {
                        figuraRecibida.Invoke(paq);
                    }

                }
            }
        }

        public event Action<figura> figuraRecibida;






    }
}
