using System;

namespace CommandGenerator
{
    class Queries
    {
        public String ShowUsers()
        {
            return "select * from Users";
        }
        public String ShowNetworks()
        {
            return "select * from Network";
        }
        public String AddUser(String Username, String Password, Boolean Is_Admin)
        {

            return "Insert Into Users(Username,Password,Is_Admin) Values('" + Username + "','" + Password + "','" + Is_Admin + "')";


        }
        public String AddNetwork(String Username, String Password, String Ip, String Name)
        {
            return "Insert Into Network(Username,Password,Is_Admin) Values('" + Username + "','" + Password + "','" + Ip + "','" + Name + "')";
        }

        public String DeleteUser(String Username)
        {
            return "Delete From User Where Username='" + Username + "'";
        }
        public String DeleteNetwork(String Name)
        {
            return "Delete From User Where Name='" + Name + "'";
        }
        public String Login(String Username, String Password)
        {
            return "Select count(*)  from Users Where Username = '" + Username + "' and Password = '" + Password + "' ";
        }
        public String SearchNetwork(String Name)
        {
            return "Select count(*)  from Network Where Name = '" + Name + "' ";
        }
        public String GetIp(String Name)
        {
            return "Select Ip from Network Where Name = '" + Name + "'";
        }
        public String GetUsername(String Name)
        {
            return "Select Username from Network Where Name = '" + Name + "'";
        }
        public String GetPassword(String Name)
        {
            return "Select Password from Network Where Name = '" + Name + "'";
        }



    }



}
