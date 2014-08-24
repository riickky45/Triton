using System;
using System.Collections.Generic;
using System.Text;

namespace Acuanet
{
    class Resultado
    {
        public int id_participante;
        public int id_categoria;
        public string id_tag;

        public List<Lectura> aLec;
        public int cantidad_aLec = 0;

        //ultima lectura
        public double rssi_max;
        public double tms_max;
        public double d_min;

        //tiempo de cruce de la meta
        public decimal tc_meta;

        public bool blecb = true;
    }
}
