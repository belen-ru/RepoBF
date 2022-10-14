using System;
using System.Collections.Generic;
using System.Text;

namespace figurasTCP.Models
{
   public class figura
    {

        //DATOS DEL DIBUJO
        public int Alto { get; set; }
        public int Ancho { get; set; }

        public string Color { get; set; } = "#FFFFFF";


        //POSICION DE LA FORMA
        public int Px { get; set; }
        public int Py { get; set; }
    }
}
