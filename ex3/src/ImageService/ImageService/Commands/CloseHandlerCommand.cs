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
                // Calculating the new string representing the handlers list after the removal.
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
                ConfigurationManager.RefreshSection("appSettings");
                result = true;
                // Specifically here the return value does not really matter, so I just return "True"
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
