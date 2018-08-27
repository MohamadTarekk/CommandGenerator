using Renci.SshNet;
using System;

namespace CommandGenerator
{
    class SSHConnection
    {
        private DataBaseReference dbr = new DataBaseReference();
       
        public bool Connection(String SSHIp, String Username, String Password)
        {
            try
            {
                using (var client = new SshClient(SSHIp, Username, Password))
                {
                    client.Connect();
                    client.RunCommand(Comm("test"));       //*Running Command Function will be Here *********************************************************
                    client.Disconnect();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }

        }

        public void Excute(string name, string command)
        {
            string[] data = dbr.AccessNetworkElement(name);
            try
            {
                using (var client = new SshClient(data[0], data[1], data[2]))
                {
                    client.Connect();
                    client.RunCommand(command);       //*Running Command Function will be Here *********************************************************
                    client.Disconnect();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }


        public String Comm(String Command)
        {

            //* I Am Wiating The Command Came from Excel File Sheet and added To The Array To run in The Above Starred Function 

            return Command;

        }

    }
}
