using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetVersion
{
    public class dropBoxHandler
    {
        private string token = "mWYakKSUMVAAAAAAAAAAIK6p5iLF2C2gteymNZ0l9gSGH12GZtqwhQ0h9uevSMZg";
        public async Task downloadVersion()
        {
            DropboxClient dc = new DropboxClient(token);

                using (var response = await dc.Files.DownloadAsync("/version.gev"))
                {
                    byte[] buffer = await response.GetContentAsByteArrayAsync();
                    File.WriteAllBytes("version.gev", buffer);
                }
            dc.Dispose();
        }

        public async Task downloadZipFile()
        {
            using (DropboxClient dc = new DropboxClient(token))
            {
                using (var response = await dc.Files.DownloadAsync("/GEPRINT.zip"))
                {
                    byte[] buffer = await response.GetContentAsByteArrayAsync();
                    File.WriteAllBytes("GEPRINT.zip", buffer);
                }
            }
        }


        //TASK:
        // unzip file, move to AppFile, create shortcut to desktop
    }
}
