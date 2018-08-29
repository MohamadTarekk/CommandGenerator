using Renci.SshNet;
using System;

namespace CommandGenerator
{
    class SSHConnection
    {
        private DataBaseReference dbr = new DataBaseReference();
       
        public bool TestConnection(string SSHIp, string Username, string Password)
        {
            try
            {
                bool res = false;
                using (var client = new SshClient(SSHIp, Username, Password))
                {
                    client.Connect();
                    res = client.IsConnected;
                    client.Disconnect();
                }
                return res;
            }
            catch (Exception ex)
            {
                FileParser.LogException(ex);
                return false;
            }

        }

        public bool Excute(string name, string command)
        {
            string[] data = dbr.AccessNetworkElement(name);
            string IP = data[0];
            string username = data[1];
            string password = data[2];
            try
            {
                using (var client = new SshClient(IP, username, password))
                {
                    client.Connect();
                    var cmd = client.RunCommand(command);
                    FileParser.SaveResult(name, IP, cmd.CommandText, cmd.Result);
                    client.Disconnect();
                    return true;
                }
            }
            catch (Exception ex)
            {
                FileParser.LogException(ex);
                return false;
            }
        }
    }
}
