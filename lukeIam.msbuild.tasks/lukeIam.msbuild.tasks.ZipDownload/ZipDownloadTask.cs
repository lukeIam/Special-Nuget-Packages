using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Authentication;
using Ionic.Zip;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
namespace lukeIam.msbuild.tasks.ZipDownload
{
    public class ZipDownloadTask : Task
    {
        private const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
        private const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;

        [Required]
        public string Address { get; set; }

        [Required]
        public string TargetFolder { get; set; }

        public override bool Execute()
        {
            if (String.IsNullOrEmpty(Address) || String.IsNullOrEmpty(TargetFolder))
            {
                Log.LogError("Parameter must be not emty");
                return false;
            }

            if (!Directory.Exists(TargetFolder))
            {
                Directory.CreateDirectory(TargetFolder);
            }

            System.Net.ServicePointManager.SecurityProtocol = Tls12;

            using (WebClient wc = new WebClient())
            {
                byte[] downloadedData = null;

                try
                {
                    downloadedData = wc.DownloadData(Address);
                }
                catch (WebException e)
                {
                    Log.LogErrorFromException(e);
                    return false;
                }
                catch (NotSupportedException e)
                {
                    Log.LogErrorFromException(e);
                    return false;
                }

                try
                {
                    using (ZipInputStream zip = new ZipInputStream(new MemoryStream(downloadedData)))
                    {
                        ZipEntry zipEntry = zip.GetNextEntry();
                        while (zipEntry != null)
                        {
                            var name = zipEntry.FileName;
                            if (zipEntry.IsDirectory)
                            {
                                Directory.CreateDirectory(Path.Combine(TargetFolder, name));
                            }
                            else
                            {
                                var path = Path.Combine(TargetFolder, name);
                                var folder = Path.GetDirectoryName(path);
                                if (!String.IsNullOrEmpty(folder) && ! Directory.Exists(folder))
                                {
                                    Directory.CreateDirectory(folder);
                                }

                                FileStream writer = File.Create(path);
                                
                                byte[] data = new byte[4096];
                                int bytesRead = 1;
                                while (bytesRead > 0)
                                {
                                    bytesRead = zip.Read(data, 0, data.Length);
                                    writer.Write(data, 0, bytesRead);
                                }
                                writer.Close();
                            }

                            zipEntry = zip.GetNextEntry();
                        }
                    }
                }
                catch(BadReadException e)
                {
                    Log.LogErrorFromException(e);
                    return false;
                }
                catch (BadStateException e)
                {
                    Log.LogErrorFromException(e);
                    return false;
                }
                catch (ZipException e)
                {
                    Log.LogErrorFromException(e);
                    return false;
                }
                catch (Ionic.Zlib.ZlibException e)
                {
                    Log.LogErrorFromException(e);
                    return false;
                }
            }

            return true;
        }
    }
}
