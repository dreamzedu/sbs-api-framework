using System;
using System.Collections.Generic;
using System.Text;

namespace sbs_api.models
{
    public class User
    {
        public int id;
        public string name;
        public string phone;
        public string email;
        public string userid;
        public string password;

        public List<Role> roles = new List<Role>();
    }
}
