using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class ImageController : IImageController
    {
        private ImageModal imageModal;
        private Dictionary<int, ICommand> idToCommand;

        public ImageController()
        {
            imageModal = new ImageModal();
            idToCommand = new Dictionary<int, ICommand>();
        }

        public void ParseAndExecute(CommandData data)
        {
            int id = data.Id;
            ICommand commandToExecute = idToCommand[id];
            commandToExecute.Execute(imageModal, data.Args);
        }
    }
}
