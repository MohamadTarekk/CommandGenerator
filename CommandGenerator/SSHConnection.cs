using Renci.SshNet;
using System;

namespace CommandGenerator
{
    class SSHConnection
    {
        
        private void Connect_Click(object sender, EventArgs e)
        {

        }

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




        public String Comm(String Command)
        {

            //* I Am Wiating The Command Came from Excel File Sheet and added To The Array To run in The Above Starred Function 

            return Command;

        }

    }
}
