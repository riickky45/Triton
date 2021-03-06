﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Net;
using System.IO;

using CSL;

namespace Acuanet
{
    public partial class EscannerChip : Form
    {
        CS461_HL_API reader = new CS461_HL_API();
        TrustedServer server = new TrustedServer();
        LecConfigXML cxml = new LecConfigXML();

        string strConexion;
        ModParticipante modP;
        Object guiLock = new Object();


        public EscannerChip()
        {
            InitializeComponent();
        }


        private void escanerChip_Load(object sender, EventArgs e)
        {
            cargaConfig();


            strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            modP = new ModParticipante(strConexion);

            Application.DoEvents();

            if (reader.connect())
            {
                setupReader();
                reader.purgeAllTags();
                server.Start();
            }
            else
            {
                MessageBox.Show("Problemas en la configuración de la antena", "Problema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.DoEvents();

            server.TagReceiveEvent += new TagReceiveEventHandler(this.AccessControl_TagReceiveEvent);
        }

        //Metodo que carga la configuracion inicial de la antena
        private void cargaConfig()
        {
            lock (this)
            {
                reader.login_name = cxml.Text("ACUANET/Reader/Login/Name", "root");
                reader.login_password = cxml.Text("ACUANET/Reader/Login/Password", "csl2006");
                reader.http_timeout = cxml.Int16("ACUANET/SocketTimeout/Http", 30000);
                reader.api_log_level = reader.LogLevel(cxml.Text("ACUANET/Application/LogLevel", "Info"));
                reader.setURI(cxml.Text("ACUANET/Reader/URI", "http://192.168.25.248/"));

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

        delegate void actualizaPantalla_Delegate(TAG tag);

        // metodo para actualizar la pantalla con los datos del usuario parte de un tag y recupera la informacion desde la BD 
        // incluye proteccion de un solo hilo (uno solo pinta completamente la pantalla un hilo)
        private void actualizaPantalla(TAG tag)
        {

            if (InvokeRequired)
            {
                actualizaPantalla_Delegate task = new actualizaPantalla_Delegate(actualizaPantalla);
                BeginInvoke(task, new object[] { tag });
            }
            else
            {
                lock (guiLock)
                {
                    if (lbl_id_tag.Text.Equals(tag.TagOrigId) == false)
                    {
                        //MessageBox.Show("Tag Recibido Evento recepción:" + tag.TagOrigId + " Tiempo:" + tag.Time + " ms" + tag.ApiTimeStampUTC.Millisecond);

                        Participante par = modP.recuperaPxTag(tag.TagOrigId);
                        this.lbl_id_tag.Text = tag.TagOrigId;
                        this.lbl_nombre.Text = par.nombre;
                        this.lbl_numero.Text = par.snumero;
                        this.lbl_categoria.Text = par.categoria;
                        this.lbl_pais.Text = par.pais;
                        this.lbl_prueba.Text = par.prueba;
                        
                        
                    }
                }
            }
        }

        //configurador del lector y la antena 
        #region Configurador Reader
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
                MessageBox.Show("No es posible configurar el perfil de operación");
                return false;
            }


            //Setup Trusted Server
            SERVER_INFO svr = new SERVER_INFO();
            svr.id = "AccessControlDemoServer";
            svr.desc = "Access Control Demo Server";
            svr.ip = cxml.Text("ACUANET/Application/LocalIP", "192.168.25.1");
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
        #endregion

        //Metodo para interceptar el evento de llegada de un tag
        public void AccessControl_TagReceiveEvent(object sender, TagReceiveEventArgs e)
        {
            if (e.rxTag != null)
            {
                actualizaPantalla((TAG)e.rxTag);
            }
            else
            {

            }
        }


       
        //metodo que intercepta el cierre de la ventan y para al servidor
        private void escanerChip_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }


    }
}
