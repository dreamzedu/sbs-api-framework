using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class AnalyticsRepo : IAnalyticsRepo
    {
        private MySqlConnection con;
        public AnalyticsRepo()
        {
            string conStr = ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<ResponsePercentage> GetBlockWiseReport(int blockId)
        {
            throw new NotImplementedException();
        }

        public List<ResponsePercentage> GetDistrictWiseReport(int districtId)
        {
            List<ResponsePercentage> analytics = new List<ResponsePercentage>();
            //MySqlCommand cmd = new MySqlCommand("select q.question_id, " +
            //                                    "(select count(q1.question_id) from sbsdb.questionnaire q1 inner join sbsdb.beneficiary b1 on b1.id = q1.beneficiary_id" +
            //                                    " inner join sbsdb.village v1 on b1.village_id = v1.id " +
            //                                    " inner join sbsdb.block bl1 on v1.block_id = bl1.id " +
            //                                    " inner join sbsdb.district d1 on d1.id = bl1.district_id where d1.id=" + districtId + " and q1.answer_id=1 and q1.question_id = q.question_id )" +
            //                                    "*100/count(q.question_id) response_percentage from sbsdb.questionnaire q inner join sbsdb.beneficiary b on b.id = q.beneficiary_id" +
            //                                    " inner join sbsdb.village v on b.village_id = v.id " +
            //                                    " inner join sbsdb.block bl on v.block_id = bl.id " +
            //                                    " inner join sbsdb.district d on d.id = bl.district_id where d.id="+ districtId + " group by q.question_id", con);

            MySqlCommand cmd =
                new MySqlCommand(
                    "select q.question_id, (select count(q1.question_id) from sbsdb.questionnaire q1 where q1.answer_id in (1,3) and q1.question_id = q.question_id)*100/count(q.question_id) response_percentage from sbsdb.questionnaire q group by q.question_id;", con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ResponsePercentage  obj= new ResponsePercentage();
                obj.questionId = Convert.ToInt32(reader["question_id"]);
                obj.responsePercentage = Convert.ToDecimal(reader["response_percentage"]);
                analytics.Add(obj);
            }

            return analytics;
        }

        public List<ResponsePercentage> GetPanchayatWiseReport(int panchayatId)
        {
            throw new NotImplementedException();
        }
    }
}
