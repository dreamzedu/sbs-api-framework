using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class PanchayatRepo : IPanchayatRepo
    {
        
        private MySqlConnection con;
        public PanchayatRepo()
        {
            
            string conStr = ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<Panchayat> GetPanchayats(int blockId)
        {
            List<Panchayat> panchayats = new List<Panchayat>();
            MySqlCommand cmd = new MySqlCommand("select id, name from sbsdb.Panchayat where block_id=@blockId", con);
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@blockId", Value = blockId, MySqlDbType = MySqlDbType.Int32 });
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Panchayat  obj= new  Panchayat();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.name = Convert.ToString(reader["name"]);
                panchayats.Add(obj);
            }
            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }

            return panchayats;
        }


        public List<Panchayat> GetPanchayats()
        {
            List<Panchayat> panchayats = new List<Panchayat>();
            MySqlCommand cmd = new MySqlCommand("select * from sbsdb.panchayat", con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Panchayat obj = new Panchayat();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.name = Convert.ToString(reader["name"]);
                obj.block_id = Convert.ToInt32(reader["block_id"]);
                panchayats.Add(obj);
            }
            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }

            return panchayats;
        }

        public int InsertPanchayat(string name, int blockId)
        {
            int id=0;
            MySqlCommand cmd = new MySqlCommand("insert into sbsdb.panchayat(name, block_id) values(@name, @blockId); SELECT LAST_INSERT_ID();", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value=name, MySqlDbType =  MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@blockId", Value = blockId, MySqlDbType = MySqlDbType.Int32 });
            object obj = cmd.ExecuteScalar();

            if (obj != null && obj != DBNull.Value)
                Int32.TryParse(obj.ToString(), out id);

            if (con != null)
            {
                con.Close();
            }

            return id;
        }

        public void UpdatePanchayat(int id, string name)
        {
            MySqlCommand cmd = new MySqlCommand("update sbsdb.panchayat set name=@name where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value = name, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });

            cmd.ExecuteNonQuery();

            if (con != null)
            {
                con.Close();
            }
        }

        public void DeletePanchayat(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from sbsdb.panchayat where id=@id", con);
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
