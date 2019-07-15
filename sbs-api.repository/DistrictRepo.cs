using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class DistrictRepo : IDistrictRepo
    {
        
        private MySqlConnection con;
        public DistrictRepo()
        {
            
            string conStr = ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<District> GetDistricts(int stateId)
        {
            List<District> districts = new List<District>();
            MySqlCommand cmd = new MySqlCommand("select id, name from sbsdb.district where state_id=@stateId", con);
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@stateId", Value = stateId, MySqlDbType = MySqlDbType.Int32 });
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                District  obj= new  District();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.name = Convert.ToString(reader["name"]);
                districts.Add(obj);
            }

            return districts;
        }

        public void InsertDistrict(string name, int stateId)
        {
            MySqlCommand cmd = new MySqlCommand("insert into sbsdb.district(name, state_id) values(@name, @stateId)", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value=name, MySqlDbType =  MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@stateId", Value = stateId, MySqlDbType = MySqlDbType.Int32 });
            cmd.ExecuteNonQuery();
        }

        public void UpdateDistrict(int id, string name)
        {
            MySqlCommand cmd = new MySqlCommand("update sbsdb.district set name=@name where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value = name, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });

            cmd.ExecuteNonQuery();
        }

        public void DeleteDistrict(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from sbsdb.district where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });
            cmd.ExecuteNonQuery();
        }
    }
}
