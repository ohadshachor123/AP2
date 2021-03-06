﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Configuration;
using ImageService.Commands;
using ImageService.FilesModal;
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
        private Logging.IlogService logger;
        private Server server;
        private IController controller;
        private IImageModal modal;

        public ImageService()
        {
            
            string output = ConfigurationManager.AppSettings["OutputDir"];
            int size = int.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
            string[] pathsToListen = ConfigurationManager.AppSettings["Handler"].Split(';');
            string sourceName = ConfigurationManager.AppSettings["SourceName"];
            string logName = ConfigurationManager.AppSettings["LogName"];
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(sourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    sourceName, logName);
            }
            eventLog1.Source = sourceName;
            eventLog1.Log = logName;

            logger = new Logging.LoggingService();
            logger.MessageRecieved += NewEventLogEntry;

            modal = new ImageModal(output, size);
            controller = new Controller(modal, logger);
            server = new Server(logger, controller);
            foreach(string path in pathsToListen)
            {
                server.AddPath(path);
            }
        }

        private void NewEventLogEntry(object sender, Logging.MessageReceivedArgs args)
        {
            string message = args.Message;
            Logging.MessageType status = args.Status;
            EventLogEntryType entryType = EventLogEntryType.Information;
            if(status == Logging.MessageType.FAIL)
            {
                entryType = EventLogEntryType.FailureAudit;
            } else if (status == Logging.MessageType.WARNING)
            {
                entryType = EventLogEntryType.Warning;
            }
            eventLog1.WriteEntry(message, entryType);
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.  
            logger.Log("Start pending...");
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            logger.Log("Starting....");
            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            logger.Log("Finishing....");
            server.Stop();
        }

        [DllImport("advapi32.dll", SetLastError= true)]  
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
    }
}
