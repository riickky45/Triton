using System;
using System.Collections.Generic;
using System.Text;

using CSL;

namespace Acuanet
{
    //Clase para registra la lectura y el tiempo estima posicion y velocidad promedio
    class Lectura
    {
        public decimal rssi;
        public long tiempo;
        public int milis;

        public decimal tms;
        public decimal d_dist;
        public decimal a_dist;

        public bool bdatoc=false;
    }
}
