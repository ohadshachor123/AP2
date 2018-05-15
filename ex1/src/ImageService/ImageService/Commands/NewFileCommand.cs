using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class NewFileCommand : ICommand
    {
        private FilesModal.IImageModal modal;

        public NewFileCommand(FilesModal.IImageModal modal)
        {
            this.modal = modal;
        }

        // Executes the command. Args: path, year, month(of the photo's creation/take time)
        public string Execute(string[] args, out bool result)
        {
            string path = args[0];
            string year = args[1];
            string month = args[2];
            return modal.MoveFile(path, year, month, out result);
        }
    }
}
