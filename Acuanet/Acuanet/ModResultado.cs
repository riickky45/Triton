using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace Acuanet
{
    class ModResultado
    {
        MySqlConnection dbConn;

        //crea la conexion y prepara para operaciones sobre los usuarios
        public ModResultado(string strConexion)
        {                       
            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();
        }

        public DataSet obtenDatos(int id_categoria)
        {

            string sqc = "";
            if (id_categoria > 0)
            {
                sqc = " AND resultado_final.id_categoria=" + id_categoria;
            }

            string sql = "SELECT posicion,participante.nombre,participante.numero,tiempo,fecha_hora_ini,fecha_hora_fin,oleadacat.categoria,oleadacat.oleada FROM resultado,participante,salida,oleadacat WHERE participante.id=resultado.id_participante AND salida.oleada=oleadacat.oleada AND oleadacat.categoria=participante.categoria  ORDER BY salida.fecha_hora_ini_local,oleadacat.categoria,tiempo_meta";
           // MessageBox.Show(sql);

            MySqlDataAdapter datad = new MySqlDataAdapter(sql, dbConn);
            DataSet dt = new DataSet();

            datad.Fill(dt);

            return dt;
        }

        // destructor libera la memoria y en este caso la conexión a la BD 
        ~ModResultado()
        {
            if (this.dbConn != null) dbConn.Close();
            dbConn = null;
        }
    }
}
