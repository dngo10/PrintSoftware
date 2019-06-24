using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace upDateProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "mWYakKSUMVAAAAAAAAAAIK6p5iLF2C2gteymNZ0l9gSGH12GZtqwhQ0h9uevSMZg";
            using (DropboxClient dbx = new DropboxClient(token)) {
                byte[] buffer = File.ReadAllBytes("version.gev");
                Upload(dbx, "", "version.gev", buffer).Wait();
                buffer = File.ReadAllBytes("GEPRINT.zip");
                Upload(dbx, "", "GEPRINT.zip", buffer).Wait();
                dbx.Dispose();
            }
        }

        static async Task Upload(DropboxClient dbx, string folder, string file, byte[] buffer)
        {
            using (var mem = new MemoryStream(buffer))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    mode: WriteMode.Overwrite.Instance, body: mem);
                Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
            }
        }
    }
}
