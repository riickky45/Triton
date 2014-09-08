using System;
using System.Collections.Generic;
using System.Text;

namespace Acuanet
{
    class Resultado
    {
        public int id_participante;
        public string snumero;
        public string nombre;
        public string categoria;
        public string oleada;
        public string id_tag;

        public List<Lectura> aLec;
        public int cantidad_aLec = 0;

        //ultima lectura
        public double rssi_max;
        public double tms_max;
        public double d_min;

        //tiempo local de la categoria
        public long tiempo_ini_local;
        public int milis_ini_local;


        //tiempo de cruce de la meta
        public double tc_meta;

        //tiempo de cruce de la meta local
        public double tc_meta_local;


        //variables que se calculan e imprimen en BD
        public string stiempo;
        public string sfecha_hora_ini;
        public string sfecha_hora_fin;

        public double ts_ini;
        public int milis_fin;

        public DateTime dt_ini;
        public DateTime dt_fin;


        //checado de bool
        public bool blecb = true;
    }
}
