using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateForm
{
    public class dropBoxHandler
    {
        string key = "mWYakKSUMVAAAAAAAAAAIK6p5iLF2C2gteymNZ0l9gSGH12GZtqwhQ0h9uevSMZg";
        public async Task downloadVersion()
        {
            DropboxClient dc = new DropboxClient(key);

            using (var response = await dc.Files.DownloadAsync("/version.gev"))
            {
                byte[] buffer = await response.GetContentAsByteArrayAsync();
                File.WriteAllBytes("abc.gev", buffer);
            }
            dc.Dispose();
        }

        private async Task downloadZipFile()
        {
            using (DropboxClient dc = new DropboxClient(key))
            {
                using (var response = await dc.Files.DownloadAsync("GEprintUpdate" + "/" + "GEPRINT.zip"))
                {
                    Console.WriteLine(await response.GetContentAsStringAsync());
                }
            }
        }


        //TASK:
        // unzip file, move to AppFile, create shortcut to desktop
    }
}
