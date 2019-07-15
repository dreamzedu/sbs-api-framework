using System;
using System.Collections.Generic;
using System.Text;

namespace sbs_api.models
{
    public class Role
    {
        public int id;
        public string name;
        public string description;
        public List<Permission> permissions = new List<Permission>();
    }
}
