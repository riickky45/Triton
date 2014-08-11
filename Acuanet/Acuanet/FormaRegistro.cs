using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CSL;

namespace Acuanet
{
    public partial class FormaRegistro : Form
    {

        CS461_HL_API reader = new CS461_HL_API();
        TrustedServer server = new TrustedServer();
        LecConfigXML cxml = new LecConfigXML();

        //constructor
        public FormaRegistro()
        {




            InitializeComponent();

        }


        private void formaRegistro_Load(object sender, EventArgs e)
        {
            cargaConfiguracion();

            Application.DoEvents();

            if (reader.connect())
            {
                setupReader();
            }
            else
            {

            }
            Application.DoEvents();

            server.TagReceiveEvent += new TagReceiveEventHandler(this.AccessControl_TagReceiveEvent);
        }



        //configurador del lector y la antena 
        private bool setupReader()
        {
            if (reader.connect() == false)
            {
                System.Console.WriteLine("Error no posible la conexión");
                return false;
            }


            //Disable all events
            System.Collections.ArrayList eventList;
            eventList = reader.listEvent();
            if (eventList != null)
            {
                foreach (EVENT_INFO e in eventList)
                {
                    reader.enableEvent(e.id, false);
                }
            }

            //Setup Operation Profile
            OPERATION_PROFILE profile = new OPERATION_PROFILE();

            profile.profile_id = "Default Profile";
            profile.profile_enable = true;
            profile.modulation_profile = "Profile0";
            profile.population = 10;
            profile.session_no = 1;
            profile.ant1_power = cxml.Text("ACUANET/Reader/Antennas/Ant1/Power", "30.00");
            profile.ant2_power = cxml.Text("ACUANET/Reader/Antennas/Ant2/Power", "30.00");
            profile.ant3_power = cxml.Text("ACUANET/Reader/Antennas/Ant3/Power", "30.00");
            profile.ant4_power = cxml.Text("ACUANET/Reader/Antennas/Ant4/Power", "30.00");
            profile.ant1_enable = true;
            profile.ant2_enable = false;
            profile.ant3_enable = false;
            profile.ant4_enable = false;
            profile.window_time = 1000;
            profile.trigger = "Autonomous Time Trigger";
            profile.capture_mode = "Time Window";
            profile.tagModel = "GenericTID32";
            profile.memoryBank = "None";
            profile.antennaPortScheme = "true";

            if (reader.setOperProfile_TxPowers(profile) == false)
            {
                System.Console.WriteLine("No es posible configurar el perfil de operación");
                return false;
            }


            //Setup Trusted Server
            SERVER_INFO svr = new SERVER_INFO();
            svr.id = "AccessControlDemoServer";
            svr.desc = "Access Control Demo Server";
            svr.ip = "0.0.0.0"; //aqui debe ir el ip de la maquina que escucha
            svr.server_port = "9090";
            svr.mode = "Listening Port on Server Side";
            svr.enable = true;

            if (reader.setServerID(svr) == false)
            {
                if (reader.modServerID(svr) == false)
                {
                    System.Console.WriteLine("Problema al configurar el servidor de confianza");
                    return false;
                }
            }

            //Setup Triggering Logic
            reader.delTriggeringLogic("AccessControlDemoLogic");

            TRIGGER_INFO trigger = new TRIGGER_INFO();
            trigger.id = "AccessControlDemoLogic";
            trigger.desc = "Access Control Demo";
            trigger.mode = "Read Any Tags (any ID, 1 trigger per tag)"; //For firmware 2.1.0 or later
            trigger.capture_point = "";
            trigger.capture_point += true ? "1" : "";
            trigger.capture_point += false ? "2" : "";
            trigger.capture_point += false ? "3" : "";
            trigger.capture_point += false ? "4" : "";

            if (reader.addTriggeringLogic(trigger) == false)
            {
                trigger.mode = "Read Any Tags";     //For firmware 2.0.9, 2.0.10
                if (reader.addTriggeringLogic(trigger) == false)
                {
                    System.Console.WriteLine("Problema al configurar la logica del disparador");
                    return false;
                }
            }

            //Setup Resultant Action
            reader.delResultantAction("AccessControlDemoAction");

            RESULTANT_ACTION_INFO action1 = new RESULTANT_ACTION_INFO();
            action1.id = "AccessControlDemoAction";
            action1.desc = "Access Control Demo";
            if (profile.trigger.Equals("Autonomous Time Trigger") == true)
            {
                action1.mode = "Instant Alert to Server";
            }
            else
            {
                action1.mode = "Batch Alert to Server";
            }
            action1.server_id = svr.id;
            action1.report_id = "Default Report";

            if (reader.addResultantAction(action1) == false)
            {

                System.Console.WriteLine("Falla al poner accion resultado");
                return false;
            }

            //Setup Event
            reader.delEvent("AccessControlDemoEvent");

            EVENT_INFO eventInfo = new EVENT_INFO();
            eventInfo.id = "AccessControlDemoEvent";
            eventInfo.desc = "Access Control Demo";
            eventInfo.profile = profile.profile_id;
            eventInfo.trigger = trigger.id;
            eventInfo.action = action1.id;
            eventInfo.log = false;
            eventInfo.enable = true;
            eventInfo.enabling = "Always On";
            eventInfo.disabling = "Never Stop";

            if (reader.addEvent(eventInfo) == false)
            {

                System.Console.WriteLine("Falla al poner evento");
                return false;
            }




            return true;
        }

        //Metodo para interceptar el evento de llegada de un tag
        public void AccessControl_TagReceiveEvent(object sender, TagReceiveEventArgs e)
        {
            if (e.rxTag != null)
            {
                TAG tag = (TAG)e.rxTag;
                //esta salida deberia de mostrar el tag y el tiempo (que es un int)
                // System.Console.WriteLine("Tag Recibido Evento recepcion:" + tag.TagOrigId + " Tiempo:" + tag.Time);
                MessageBox.Show("Tag Recibido Evento recepcion:" + tag.TagOrigId + " Tiempo:" + tag.Time + " ms" + tag.ApiTimeStampUTC.Millisecond);

            }
            else
            {
                System.Console.WriteLine("Tag Receive Event received: None");
            }
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //este metodo recupera los datos y los manda a insertar a la BD
        private void button3_Click(object sender, EventArgs e)
        {

            //se crea el particiapnete "en memoria"
            Participante par = new Participante();

            //se asignana los valores 
            par.nombre = text_nombre.Text;
            par.snumero = txt_numero.Text;
            par.sclub = txt_club.Text;
            par.direcc = txt_direccion.Text;

            //se prepara la conexion a la BD
            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");
            //MessageBox.Show(strConexion);
            ModParticipante modp = new ModParticipante(strConexion);

            //se crea el particiapante
            modp.crearP(par);

        }




        private void cargaConfiguracion()
        {
            lock (this)
            {
                reader.login_name = cxml.Text("ACUANET/Reader/Login/Name", "root");
                reader.login_password = cxml.Text("ACUANET/Reader/Login/Password", "csl2006");
                reader.http_timeout = cxml.Int16("ACUANET/SocketTimeout/Http", 30000);
                reader.api_log_level = reader.LogLevel(cxml.Text("ACUANET/Application/LogLevel", "Info"));
                reader.setURI(cxml.Text("ACUANET/Reader/URI", "http://192.168.25.208/"));

                server.api_log_level = reader.LogLevel(cxml.Text("ACUANET/Application/LogLevel", "Info"));
                try
                {
                    server.tcp_port = int.Parse(cxml.Text("ACUANET/Application/ServerPort", "9090"));
                }
                catch
                {
                    server.tcp_port = 9090;
                }
            }
        }


    }



}
