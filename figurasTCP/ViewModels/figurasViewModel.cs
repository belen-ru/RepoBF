using figurasTCP.Models;
using figurasTCP.Services;
using figurasTCP.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace figurasTCP.ViewModels
{
   public class figurasViewModel:INotifyPropertyChanged
    {
        userServices servi;
        ClientServices client;
        Dispatcher dispatcher;

        private bool soyServidor;
        public bool  SoyServidor

        {
            get { return soyServidor; }
            set { soyServidor = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("soyServidor")); }
        }

        private figura fig;

        public figura Fig
        {
            get { return fig; }
            set { fig = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Fig")); }
        }

        private Usuario1 usu1;

        public  Usuario1 Usu1
        {
            get { return usu1; }
            set { usu1 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Usu1")); }
        }


        public ICommand EnviarFig { get; set; }
        public ICommand Iniciar{ get; set; }


        public figurasViewModel()
        {
            EnviarFig = new RelayCommand(Enviar);
            Iniciar = new RelayCommand(Inicar);
            dispatcher = Dispatcher.CurrentDispatcher;
            Usu1 = new Usuario1();
          
        }

        private void Servi_figuraRecibida(figura fig)
        {
            figuras.Add(fig);

        }

        private ObservableCollection<figura> figuras;

        public ObservableCollection<figura> Figuras
        {
            get { return figuras; }
            set { figuras = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Figuras")); }
        }

        


        public event PropertyChangedEventHandler PropertyChanged;

        public void Inicar()
        {
            Error = "";
            if(soyServidor==true)
            {
               
                servi = new userServices();
                servi.figuraRecibida += Servi_figuraRecibida1;
                Servidor viewServi = new Servidor();
                Figuras= new ObservableCollection<figura>();
                viewServi.DataContext = this;
                viewServi.ShowDialog();

            }
            else
            {
                if (!IPAddress.TryParse(Usu1.Ip, out IPAddress? address))
                {                    
                    Error = "¡Oh no! Ingresa una IP validaaaa";
                    return;

                }

                client = new ClientServices(Usu1.Ip,Usu1);

                Window1 viewClient = new Window1();
               Fig = new figura();
                viewClient.DataContext = this;
           
                viewClient.ShowDialog();
                
            }

        }

        private void Servi_figuraRecibida1(figura fig)
        {
            dispatcher.Invoke(() =>
            {
                figuras.Add(fig);
            });
            
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Error")); }
        }

             
       

        public void Enviar()
        {

            if (Fig.Alto < 1)
            {
                Error = "¡OYE! Olvidaste especificar el Alto de tu figura";
                return;
            }

            if (Fig.Ancho<1)
            {
                Error = "¡OYE! Olvidaste especificar el Ancho de tu figura";
                return;
            }

           

       

            client.Enviar(Fig);





        }

    }
}
