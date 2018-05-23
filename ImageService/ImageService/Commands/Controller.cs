using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.FilesModal;
using ImageService.Logging;

namespace ImageService.Commands
{
    public class Controller : IController
    {
        private IImageModal modal;
        private Dictionary<int, ICommand> commands;

        public Controller(IImageModal modal, IlogService logger)
        {
            this.modal = modal;
            // Creating the dictionary of commands.
            commands = new Dictionary<int, ICommand>()
            {
                {(int)CommandEnum.NewFileCommand, new NewFileCommand(modal) },
                {(int)CommandEnum.GetConfig, new GetConfigCommand() },
                {(int)CommandEnum.AllLogs, new AllLogsCommand(logger) }
            };
        }

        // Converts the commandID to a command object, and executes it.
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            ICommand command;
            if (commands.TryGetValue(commandID, out command))
            {
                return command.Execute(args, out resultSuccesful);
            }
            else
            {
                resultSuccesful = false;
                return "Command does not exist.";
            }
        }
    }
}
