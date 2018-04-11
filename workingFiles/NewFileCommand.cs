using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal m_modal;

        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }

        public string Execute(string[] args, out bool result)
        {
            string path = args[0];
            string year = args[1];
            string month = args[2];
            m_modal.OutputSubfolder(year);
            m_modal.OutputSubfolder(year + "\\" + month);
            m_modal.ThumbnailSubfolder(year);
            m_modal.ThumbnailSubfolder(year + "\\" + month);
            return m_modal.MoveFile(path, year + "\\" + month, out result);
        }
    }
}
