using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class StateRepo : IStateRepo
    {
        
        private MySqlConnection con;
        public StateRepo()
        {
            
            string conStr = ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<State> GetStates(int countryId)
        {
            List<State> states = new List<State>();
            MySqlCommand cmd = new MySqlCommand("select id, name from sbsdb.state where country_id=@countryId", con);
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@countryId", Value = countryId, MySqlDbType = MySqlDbType.Int32 });
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                State  obj= new  State();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.name = Convert.ToString(reader["name"]);
                states.Add(obj);
            }

            return states;
        }

        public void InsertState(string name, int countryId)
        {
            MySqlCommand cmd = new MySqlCommand("insert into sbsdb.state(name, country_id) values(@name, @countryId)", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value=name, MySqlDbType =  MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@countryId", Value = countryId, MySqlDbType = MySqlDbType.Int32 });
            cmd.ExecuteNonQuery();
        }

        public void UpdateState(int id, string name)
        {
            MySqlCommand cmd = new MySqlCommand("update sbsdb.state set name=@name where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value = name, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });

            cmd.ExecuteNonQuery();
        }

        public void DeleteState(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from sbsdb.state where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });
            cmd.ExecuteNonQuery();
        }
    }
}
