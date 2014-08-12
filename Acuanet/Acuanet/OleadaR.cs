using CSL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Acuanet
{

   

    //esta clase registra una oleada
    class OleadaR 
    {
        CS461_HL_API reader = new CS461_HL_API();
        TrustedServer server = new TrustedServer();
        LecConfigXML cxml = new LecConfigXML();

        List<Lectura> lLec = new List<Lectura>();
        
        int id_oleada;
        string strConexion;

        //constructor
        public OleadaR()
        {
            id_oleada = 0;
            //prepara la conexion a la BD
           strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            // incia los servicios de la antena

        }

        //metodo para prender antena
        public bool prendeAntena()
        {
            setupReader();
            server.TagReceiveEvent += new TagReceiveEventHandler(this.AccessControl_TagReceiveEvent);
            return true;
        }

        //metodo que inicia captura 
        public bool iniciaCaptura(){


            return true;
        }

        //metodo que finaliza captura
        public bool finalizaCaptura(){


            return false;
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
            svr.ip = cxml.Text("","192.168.0.254");
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
                MessageBox.Show("Tag Recibido Evento recepción:" + tag.TagOrigId+" Tiempo:"+tag.Time);

                // se crea la clase que hace el trabajo de insertar lectura en multihilo
                InsertaLecturaE inlec = new InsertaLecturaE(tag, id_oleada, strConexion);
                Thread T = new Thread(inlec.insertaTag);
                T.Start();

            }
            else
            {
                
            }
        }


    }
}
