using System;
using System.Collections.Generic;
using System.Text;

using CSL;

namespace Acuanet
{
    //Clase para registra la lectura y el tiempo estima posicion y velocidad promedio
    class Lectura
    {
        public double rssi;
        public long tiempo;
        public int milis;

        public double tms;
        public double d_dist;
        public double a_dist;

        public bool bdatoc=false;
    }
}
