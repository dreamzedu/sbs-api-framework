using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class BlockRepo : IBlockRepo
    {
        private MySqlConnection con;
        public BlockRepo()
        {
            string conStr =  ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<Block> GetBlocks(int districtId)
        {
            List<Block> blocks = new List<Block>();
            MySqlCommand cmd = new MySqlCommand("select id, name from sbsdb.Block where district_id=@districtId", con);
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@districtId", Value = districtId, MySqlDbType = MySqlDbType.Int32 });
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Block  obj= new  Block();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.name = Convert.ToString(reader["name"]);
                blocks.Add(obj);
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }

            return blocks;
        }

        public int InsertBlock(string name, int districtId)
        {
            int id = 0;

            con.Open();
            MySqlCommand cmd = new MySqlCommand("insert into sbsdb.Block(name, district_id) values(@name, @districtId); SELECT LAST_INSERT_ID();", con);
            
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value=name, MySqlDbType =  MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@districtId", Value = districtId, MySqlDbType = MySqlDbType.Int32 });
            
            object obj = cmd.ExecuteScalar();

            if(obj != null && obj != DBNull.Value)
                Int32.TryParse(obj.ToString(), out id);

            if (con != null)
            {
                con.Close();
            }

            return id;
        }

        public void UpdateBlock(int id, string name)
        {
            MySqlCommand cmd = new MySqlCommand("update sbsdb.block set name=@name where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value = name, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });

            cmd.ExecuteNonQuery();

            if (con != null)
            {
                con.Close();
            }
        }

        public void DeleteBlock(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from sbsdb.block where id=@id", con);
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
