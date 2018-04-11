using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.FilesModal;
namespace ImageService.Commands
{
    public class Controller : IController
    {
        private IImageModal modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;

        public Controller(IImageModal modal)
        {
            this.modal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>()
            {
                {(int)CommandEnum.NewFileCommand, new NewFileCommand(modal) }
            };
        }
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
                return null;
            }
        }
    }
}
}
