using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class UserRepo : IUserRepo
    {
        
        private MySqlConnection con;
        public UserRepo()
        {
            
            string conStr = ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<User> GetUsers()
        {
            List<User> Users = new List<User>();
            MySqlCommand cmd = new MySqlCommand("select distinct(u.id), u.name, u.phone, u.userid, u.password, ur.role_id, r.name role from sbsdb.User u inner join sbsdb.user_roles ur on u.id = ur.user_id inner join sbsdb.role r on r.id = ur.role_id", con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                User  obj= new  User();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.name = Convert.ToString(reader["name"]);
                obj.phone = Convert.ToString(reader["phone"]);
                obj.roles = new List<Role>() {new Role(){id = Convert.ToInt32(reader["role_id"]), name= Convert.ToString(reader["role"])}};
                obj.userid = Convert.ToString(reader["userid"]);
                obj.password = Convert.ToString(reader["password"]);

                Users.Add(obj);
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }
            return Users;
        }

        public User GetUser(int id)
        {
            User user = new User();
            MySqlCommand cmd = new MySqlCommand("select id, name, phone, userid, password, email from sbsdb.User where id=@id", con);
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });

            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                user.id = Convert.ToInt32(reader["id"]);
                user.name = Convert.ToString(reader["name"]);
                user.phone = Convert.ToString(reader["phone"]);
                user.email = Convert.ToString(reader["email"]);
                user.userid = Convert.ToString(reader["userid"]);
                user.password = Convert.ToString(reader["password"]);
            }

            reader.Close();

            cmd = new MySqlCommand("select r.id, r.name from sbsdb.role r inner join sbsdb.user_roles ur on r.id = ur.role_id and ur.user_id = @id", con);
            cmd.Parameters.Add(new MySqlParameter(){ ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Role r = new Role();
                r.id = Convert.ToInt32(reader["id"]);
                r.name = Convert.ToString(reader["name"]);
                user.roles.Add(r);
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }
            return user;
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();
            MySqlCommand cmd = new MySqlCommand("select id, name from sbsdb.Role", con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Role obj = new Role();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.name = Convert.ToString(reader["name"]);
                roles.Add(obj);
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }
            return roles;
        }

        public IEnumerable<UserRole> GetUserRolesMapping()
        {
            List<UserRole> roles = new List<UserRole>();
            MySqlCommand cmd = new MySqlCommand("select * from sbsdb.user_roles", con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                UserRole obj = new UserRole();
                obj.user_id = Convert.ToInt32(reader["user_id"]);
                obj.role_id = Convert.ToInt32(reader["role_id"]);
                roles.Add(obj);
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }
            return roles;
        }

        public int InsertUser(User user)
        {
            int id=0;
            StringBuilder sb = new StringBuilder();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("select id from sbsdb.user where userid=@userid or phone=@phone", con);
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@phone", Value = user.phone, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@userid", Value = user.userid, MySqlDbType = MySqlDbType.String });

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                throw new ValidationException("User with same username or phone already exists");
            }

            cmd = new MySqlCommand();
            sb.Append(
                "insert into sbsdb.User(name, phone, email, userid, password) values(@name, @phone, @email, @userid, @password); ");

            foreach (var role in user.roles)
            {
                sb.Append(
                    "insert into sbsdb.user_roles(role_id, user_id) values(@roleid" + role.id +
                    ", (select id from sbsdb.user where userid=@userid)); ");
                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@roleid" + role.id, Value = role.id, MySqlDbType = MySqlDbType.Int32});

            }

            cmd.CommandText = sb.ToString();
            cmd.Connection = con;

            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value=user.name, MySqlDbType =  MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@phone", Value = user.phone, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@email", Value = user.email == null?"": user.email, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@userid", Value = user.userid, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@password", Value = user.password, MySqlDbType = MySqlDbType.String });

            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand("select id from sbsdb.user where userid=@userid and phone=@phone");
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@userid", Value = user.userid, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@phone", Value = user.phone, MySqlDbType = MySqlDbType.String });
            object obj = cmd.ExecuteScalar();

            if (obj != null && obj != DBNull.Value)
                Int32.TryParse(obj.ToString(), out id);

            if (con != null)
            {
                con.Close();
            }

            return id;
        }

        public void UpdateUser(User user)
        {
            StringBuilder sb = new StringBuilder();

            con.Open();

            MySqlCommand cmd = new MySqlCommand();
            sb.Append(
                "update sbsdb.User set name= @name, phone = @phone, email=@email, password=@password where id=@id; ");

            foreach (var role in user.roles)
            {
                sb.Append(
                    "delete from sbsdb.user_roles where user_id=@id; insert into sbsdb.user_roles(role_id, user_id) values(@roleid" + role.id +
                    ", @id); ");
                cmd.Parameters.Add(new MySqlParameter()
                { ParameterName = "@roleid" + role.id, Value = role.id, MySqlDbType = MySqlDbType.Int32 });

            }

            cmd.CommandText = sb.ToString();
            cmd.Connection = con;

            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value = user.name, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@phone", Value = user.phone, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@email", Value = user.email == null ? "" : user.email, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@password", Value = user.password, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = user.id, MySqlDbType = MySqlDbType.Int32 });

            cmd.ExecuteNonQuery();

            if (con != null)
            {
                con.Close();
            }
        }

        public void DeleteUser(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from sbsdb.User_roles where user_id=@id; delete from sbsdb.User where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });
            cmd.ExecuteNonQuery();
            if (con != null)
            {
                con.Close();
            }
        }
    }

    
}
