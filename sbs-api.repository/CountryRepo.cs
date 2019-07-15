using System;
using System.Collections.Generic;
using System.Configuration;

using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class CountryRepo : ICountryRepo
    {
        
        private MySqlConnection con;
        public CountryRepo()
        {
            
            string conStr = ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();
            MySqlCommand cmd = new MySqlCommand("select * from sbsdb.country", con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Country  obj= new  Country();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.name = Convert.ToString(reader["name"]);
                countries.Add(obj);
            }

            return countries;
        }

        public void InsertCountry(string name)
        {
            MySqlCommand cmd = new MySqlCommand("insert into sbsdb.country(name) values(@name)",con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value=name, MySqlDbType =  MySqlDbType.String });
            cmd.ExecuteNonQuery();
        }

        public void UpdateCountry(int id, string name)
        {
            MySqlCommand cmd = new MySqlCommand("update sbsdb.country set name=@name where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value = name, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });

            cmd.ExecuteNonQuery();
        }

        public void DeleteCountry(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from sbsdb.country where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });
            cmd.ExecuteNonQuery();
        }
    }
}
