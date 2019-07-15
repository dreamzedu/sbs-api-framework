using System;
using System.Collections.Generic;
using System.Text;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface IUserRepo
    {
        List<User> GetUsers();

        User GetUser(int id);

        int InsertUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int id);

        List<Role> GetAllRoles();

        IEnumerable<UserRole> GetUserRolesMapping();
    }
}
