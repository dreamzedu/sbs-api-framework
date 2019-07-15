using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using sbs_api.models;
using sbs_api.repository.Interfaces;

namespace sbs_api.repository
{
    public class DataVersionRepo : IDataVersionRepo
    {
        private MySqlConnection con;
        public DataVersionRepo()
        {
            string conStr =  ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<DataVersion> GetDataVersions()
        {
            List<DataVersion> list = new List<DataVersion>();
            MySqlCommand cmd = new MySqlCommand("select * from sbsdb.data_version", con);

            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DataVersion  obj= new  DataVersion();
                obj.version = Convert.ToInt32(reader["version"]);
                obj.tbl_name = Convert.ToString(reader["tbl_name"]);
                list.Add(obj);
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }

            return list;
        }

    }
}
