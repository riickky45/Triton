using System;
using System.Collections.Generic;
using System.Text;

namespace Acuanet
{
    partial class GResultados
    {
        //metodo que checa el buen orden de los datos, borra items que no aportan buen orden
        public void marcaLecturasBOrden()
        {
            foreach (Resultado r in lRes)
            {
                double rssi_b = r.aLec[0].rssi;
                foreach (Lectura lec in r.aLec)
                {
                    if (lec.rssi >= rssi_b)
                    {
                        lec.bdatoc = true;
                        rssi_b = lec.rssi;
                    }
                    else
                    {
                       // r.aLec.Remove(lec);
                    }
                }

                for (int i = 0; i < r.aLec.Count; i++)
                {
                    if (r.aLec[i].bdatoc == false)
                    {
                        r.aLec.Remove(r.aLec[i]);
                    }
                }


                    r.cantidad_aLec = r.aLec.Count;
                this.trabajo_rea++;
            }




        }


        //metodo que asigna el Tiempo de Cruce de Meta cuando solo hay una lectura de un participante 
        private void estimaTCM2(Resultado r)
        {
            Lectura lec = r.aLec[0];
            r.tc_meta = (lec.tiempo + lec.a_dist / this.obtenVelMasCercano(r));
            r.tc_meta_local = r.tc_meta + r.tiempo_ini_local + r.milis_ini_local / 1000;
            this.trabajo_rea++;

        }


        //metodo que obtiene la velocidad promedio del participante mas cercano en la misma categoria
        private double obtenVelMasCercano(Resultado r)
        {
            double vel = 0;
            double dist = -1;
            double[] res;

            for (int i = 0; i < lRes.Count; i++)
            {
                Resultado rb = lRes[i];
                if (rb.categoria.Equals(r.categoria) && rb.cantidad_aLec > 1 && !rb.Equals(r))
                {

                    res = this.obtenDgV(rb.aLec, r.aLec[0]);


                    if (dist == -1)
                    {
                        dist = res[0];
                        vel = res[1];

                    }
                    else if (dist > res[0])
                    {

                        dist = res[0];
                        vel = res[1];
                    }
                }
            }

            return vel;
        }


        //método que obtiene la minima distancia generalizada entre un Listado de lecturas y una lectura
        private double[] obtenDgV(List<Lectura> alec, Lectura lec)
        {
            double[] res = new double[2];

            res[0] = this.obtenDg(alec[0], lec);
            double daux = res[0];

            for (int i = 0; i < alec.Count; i++)
            {

                daux = this.obtenDg(alec[i], lec);

                if (daux < res[0])
                {
                    res[0] = daux;
                    if (i >= 1)
                    {

                        if (this.obtenDg(alec[i - 1], lec) < this.obtenDg(alec[i + 1], lec))
                        {
                            res[1] = this.obtenVel(alec[i - 1], alec[i]);
                        }
                        else
                        {
                            if (i < alec.Count - 1)
                            {
                                res[1] = this.obtenVel(alec[i + 1], alec[i]);
                            }
                            else
                            {
                                res[1] = this.obtenVel(alec[i - 1], alec[i]);
                            }
                        }

                    }
                    else
                    {
                        res[1] = this.obtenVel(alec[i + 1], alec[i]);
                    }
                }

            }

            return res;
        }


        //metodo que calcula la Distancia genralizada entre 2 lecturas
        private double obtenDg(Lectura lref1, Lectura lref2)
        {
            return Math.Pow(lref1.d_dist - lref2.d_dist, 2) +Math.Pow(lref1.tiempo - lref2.tiempo, 2);
        }


        //metodo que calcula la velocidad promedio del intervalo entre 2 lecturas
        private double obtenVel(Lectura lref1, Lectura lref2)
        {
            return Math.Abs((lref1.a_dist - lref2.a_dist) / (lref1.tiempo - lref2.tiempo));
        }


        //método que determina el valor maximo de rssi y el tiempo y la minima distancia puede ser no utilizado
        private void CalculaMax()
        {

            foreach (Resultado r in this.lRes)
            {
                bool bpv = true;
                double rssi_max = 0;
                double tms_max = 0;
                foreach (Lectura lec in r.aLec)
                {
                    if (bpv)
                    {
                        rssi_max = lec.rssi;
                        tms_max = lec.tms;
                        bpv = false;
                    }
                    else
                    {
                        if (rssi_max < lec.rssi)
                        {
                            rssi_max = lec.rssi;
                            tms_max = lec.tms;
                        }
                    }
                }

                r.rssi_max = rssi_max;
                r.tms_max = tms_max;
                r.d_min = estimaDist(r.rssi_max);
            }

        }

    }
}
