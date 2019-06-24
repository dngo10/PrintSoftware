using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD
{
    public class dropBoxHandler
    {

        public async Task downloadVersion()
        {
            DropboxClient dc = new DropboxClient("mWYakKSUMVAAAAAAAAAAIK6p5iLF2C2gteymNZ0l9gSGH12GZtqwhQ0h9uevSMZg");

                using (var response = await dc.Files.DownloadAsync("/version.gev"))
                {
                    byte[] buffer = await response.GetContentAsByteArrayAsync();
                    File.WriteAllBytes("abc.gev", buffer);
                }
            dc.Dispose();
        }

        private async Task downloadZipFile()
        {
            using (DropboxClient dc = new DropboxClient("mWYakKSUMVAAAAAAAAAAH1St48Vyn__pFebIu0Qr3lsHgbSq0NFsCA3WR4c8yKzy"))
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
