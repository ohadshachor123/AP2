using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ImageService.Server;
using ImageService.Controller;
using ImageService.Modal;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Configuration;
using ImageService.Infrastructure;

namespace ImageService
{
    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };

    public partial class ImageService : ServiceBase
    {

        private ImageServer m_imageServer;          // The Image Server
        private IImageServiceModal modal;
        private IImageController controller;
        private EventLog eventLog1;
        private ILoggingService logging;

        public ImageService()
        {
            // INIT. Event Log
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
            // INIT. LogginService
            logging = new LoggingService();
            logging.MessageRecieved += AddToEventLogger;

            // INIT. server
            //INIT. Controller
            //INIT. modal
        }
		// Here You will Use the App Config!
        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {

        }

        private void InitializeComponent()
        {
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }
        private void AddToEventLogger(object sender, MessageRecievedEventArgs e)
        {
            EventLogEntryType type = EventLogEntryType.Information;
            if (e.Status == MessageTypeEnum.FAIL)
            {
                type = EventLogEntryType.FailureAudit;
            } else if (e.Status == MessageTypeEnum.WARNING)
            {
                type = EventLogEntryType.Warning;
            }
            eventLog1.WriteEntry(e.Message, type);
        }

    }


}
}
