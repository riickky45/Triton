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

        public double rssi_max;
        public double tms_max;
        public double d_min;

        public double tc_meta;

        public bool blecb = true;
    }
}
