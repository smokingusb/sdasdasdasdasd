using Renci.SshNet;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WgConfigHelper
{
    public class SshUploader
    {
        private readonly string host;
        private readonly string user;
        private readonly string password;

        public SshUploader(string host, string user, string password)
        {
            this.host = host;
            this.user = user;
            this.password = password;
        }

        public Task UploadFileAsync(string localPath, string remotePath)
        {
            return Task.Run(() =>
            {
                using (var client = new SshClient(host, user, password))
                using (var sftp = new Renci.SshNet.SftpClient(host, user, password))
                {
                    client.Connect();
                    sftp.Connect();
                    using (var fs = File.OpenRead(localPath))
                    {
                        sftp.UploadFile(fs, remotePath, true);
                    }
                    sftp.Disconnect();
                    client.Disconnect();
                }
            });
        }

        // For advanced: execute a command (be careful with root and passwords)
        public string ExecuteCommand(string command)
        {
            using var client = new SshClient(host, user, password);
            client.Connect();
            var result = client.RunCommand(command);
            client.Disconnect();
            if (!string.IsNullOrEmpty(result.Error))
                throw new Exception(result.Error);
            return result.Result;
        }
    }
}