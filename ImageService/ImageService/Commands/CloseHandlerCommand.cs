using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class CloseHandlerCommand : ICommand
    {
        public string Execute(string[] Args, out bool result)
        {
            try
            {
                string path = Args[0];
                string[] allHandlers = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
                string ans = "";
                foreach (string handler in allHandlers)
                {
                    if (handler != path)
                        ans += handler + ";";
                }
                ans = ans.Trim().TrimEnd(';');

                //open and change app config, used stack overflow for this.
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("Handler");
                config.AppSettings.Settings.Add("Handler", ans);
                config.Save(ConfigurationSaveMode.Minimal);
                // Force a reload of the changed section. This 
                // makes the new values available for reading.
                ConfigurationManager.RefreshSection("appSettings");
                //  this.imageServer.CloseServer();
                result = true;
                return "True";
            }
            catch (Exception e)
            {
                result = false;
                return e.Message;
            }
        }
    }
}
