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

        bool bantenaE;
        string sTag;


        //constructor
        public FormaRegistro()
        {
            bantenaE = false;
            InitializeComponent();

        }

        //metodo para configurar la antena al momento de carga
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
                MessageBox.Show("Error no posible la conexión con el Lector");
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
            profile.modulation_profile = cxml.Text("ACUANET/Reader/ModulationProfile", "Profile0");
            profile.population = cxml.Int16("ACUANET/Reader/PopulationEstimation", 10);
            profile.session_no = cxml.Int16("ACUANET/Reader/Session", 1);
            profile.ant1_power = cxml.Text("ACUANET/Reader/Antennas/Ant1/Power", "30.00");
            profile.ant2_power = cxml.Text("ACUANET/Reader/Antennas/Ant2/Power", "30.00");
            profile.ant3_power = cxml.Text("ACUANET/Reader/Antennas/Ant3/Power", "30.00");
            profile.ant4_power = cxml.Text("ACUANET/Reader/Antennas/Ant4/Power", "30.00");
            profile.ant1_enable = cxml.Boolean("ACUANET/Reader/Antennas/Ant1/Enabled", false);
            profile.ant2_enable = cxml.Boolean("ACUANET/Reader/Antennas/Ant2/Enabled", false);
            profile.ant3_enable = cxml.Boolean("ACUANET/Reader/Antennas/Ant3/Enabled", false);
            profile.ant4_enable = cxml.Boolean("ACUANET/Reader/Antennas/Ant4/Enabled", false);
            profile.window_time = cxml.Int16("ACUANET/Reader/DuplicationElimination/Time", 1000);
            profile.trigger = cxml.Text("ACUANET/Reader/DuplicationElimination/Method", "Autonomous Time Trigger");
            profile.capture_mode = "Time Window";
            profile.tagModel = cxml.Text("ACUANET/Reader/TagIC", "GenericTID32");
            profile.memoryBank = cxml.Text("ACUANET/Reader/AdditionalMemoryBank", "None");
            profile.antennaPortScheme = cxml.Text("ACUANET/Reader/DuplicationElimination/AntennaPortScheme", "true");

            if (reader.setOperProfile_TxPowers(profile) == false)
            {
                System.Console.WriteLine("No es posible configurar el perfil de operación");
                return false;
            }


            //Setup Trusted Server
            SERVER_INFO svr = new SERVER_INFO();
            svr.id = "AccessControlDemoServer";
            svr.desc = "Access Control Demo Server";
            svr.ip = cxml.Text("ACUANET/Application/LocalIP", "0.0.0.0");
            svr.server_port = cxml.Text("ACUANET/Application/ServerPort", "9090");
            svr.mode = "Listening Port on Server Side";
            svr.enable = true;
            

            if (reader.setServerID(svr) == false)
            {
                if (reader.modServerID(svr) == false)
                {
                    MessageBox.Show("Problema al configurar el servidor de confianza");
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
            trigger.capture_point += cxml.Boolean("ACUANET/Reader/Antennas/Ant1/Enabled", false) ? "1" : "";
            trigger.capture_point += cxml.Boolean("ACUANET/Reader/Antennas/Ant2/Enabled", false) ? "2" : "";
            trigger.capture_point += cxml.Boolean("ACUANET/Reader/Antennas/Ant3/Enabled", false) ? "3" : "";
            trigger.capture_point += cxml.Boolean("ACUANET/Reader/Antennas/Ant4/Enabled", false) ? "4" : "";

            if (reader.addTriggeringLogic(trigger) == false)
            {
                trigger.mode = "Read Any Tags";     //For firmware 2.0.9, 2.0.10
                if (reader.addTriggeringLogic(trigger) == false)
                {
                    MessageBox.Show("Problema al configurar la logica del disparador");
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
                sTag = tag.TagOrigId;
                labelCodigoP.Text = sTag;
                MessageBox.Show("Tag Recibido Evento recepción:" + tag.TagOrigId + " Tiempo:" + tag.Time + " ms" + tag.ApiTimeStampUTC.Millisecond);

            }
            else
            {
               MessageBox.Show("Tag Receive Event received: None");
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
        private void btnAgegarR_Click(object sender, EventArgs e)
        {

            //se crea el participante "en memoria"
            Participante par = new Participante();

            //se asignana los valores 
            par.nombre = text_nombre.Text;
            par.snumero = txt_numero.Text;
            par.sclub = txt_club.Text;
            par.direcc = txt_direccion.Text;
            par.email = txt_email.Text;
            par.id_tag = sTag;

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



        //Metodo que carga la configuracion inicial de la antena
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

        //Metodo que enciende la captura o la apaga
        private void btnEncenderA_Click(object sender, EventArgs e)
        {

            if (!bantenaE)
            {
                reader.purgeAllTags();
                server.Start();
                bantenaE = true;
                btnEncenderA.Text = "Apagar Antena";
            }
            else
            {
               server.Stop();
                bantenaE = false;
                btnEncenderA.Text = "Encender Antena";
            }

        }

       


    }



}
