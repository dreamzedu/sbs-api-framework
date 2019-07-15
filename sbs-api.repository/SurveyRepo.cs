using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class SurveyRepo : ISurveyRepo
    {
        
        private MySqlConnection con;
        public SurveyRepo()
        {
            
            string conStr = ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

       
        public List<SurveyBasicDetail> GetSurveys(int startIndex, int count)
        {
            List<SurveyBasicDetail> Surveys = new List<SurveyBasicDetail>();
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                con.Open();
                cmd.Connection = con;

                cmd.CommandText = "select count(*) from sbsdb.survey";
                object result = cmd.ExecuteScalar();
                int totalRecords = int.Parse(result.ToString());
                int fetchCount = count;

                if (totalRecords < startIndex + count - 1)
                {
                    fetchCount = totalRecords - startIndex + 1;
                }

                cmd.CommandText =
                    "select * from (select * from (select s.id, s.benef_name, s.benef_father, s.created_location, s.latitude, s.longitude, v.name `panchayat`, bl.name `block`, d.name `district`, u.name `created_by` from sbsdb.survey s " +
                    "inner join sbsdb.panchayat v on s.panchayat_id= v.id " +
                    "inner join sbsdb.block bl on v.block_id = bl.id " +
                    "left join sbsdb.user u on u.id=s.created_by " +
                    "inner join sbsdb.district d on d.id = bl.district_id order by s.benef_name asc limit " +
                    (startIndex + count - 1) + ") a order by benef_name desc limit " + fetchCount +
                    ") b order by benef_name asc;";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SurveyBasicDetail obj = new SurveyBasicDetail();
                    obj.id = reader.GetString("id");
                    obj.benfName = reader.GetString("benef_name");
                    obj.benfHead = reader.GetString("benef_father");
                    obj.panchayat = reader.GetString("panchayat");
                    obj.createdBy = reader.GetString("created_by");
                    obj.createdAtLocation = reader.GetString("created_location");
                    obj.latitude = reader.GetString("latitude");
                    obj.longitude = reader.GetString("longitude");

                    Surveys.Add(obj);
                }
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
            }

            return Surveys;
        }

        public List<SurveyBasicDetail> GetSurveysByDistrict(int districtId, int startIndex, int count)
        {
            List<SurveyBasicDetail> Surveys = new List<SurveyBasicDetail>();
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                con.Open();
                cmd.Connection = con;

                cmd.CommandText = "select count(*) from sbsdb.survey";
                object result = cmd.ExecuteScalar();
                int totalRecords = int.Parse(result.ToString());
                int fetchCount = count;

                if (totalRecords < startIndex + count - 1)
                {
                    fetchCount = startIndex + count - totalRecords;
                }

                cmd.CommandText =
                    "select * from (select * from (select s.id, s.benef_name, s.benef_father, s.created_location, s.latitude, s.longitude, v.name panchayat, bl.name block, d.name district, u.name created_by from sbsdb.survey s " +
                    "inner join sbsdb.panchayat v on s.panchayat_id= v.id " +
                    "inner join sbsdb.block bl on v.block_id = bl.id " +
                    "left join sbsdb.user u on u.id=s.created_by " +
                    "inner join sbsdb.district d on d.id = bl.district_id where d.id=@districtId order by s.benef_name asc limit " +
                    (startIndex + count - 1) + ") a order by benef_name desc limit " + fetchCount +
                    ") b order by benef_name asc;";

                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@districtId", Value = districtId, MySqlDbType = MySqlDbType.Int32});
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SurveyBasicDetail obj = new SurveyBasicDetail();
                    obj.id = reader.GetString("id");
                    obj.benfName = reader.GetString("benef_name");
                    obj.benfHead = reader.GetString("benef_father");
                    obj.panchayat = reader.GetString("panchayat");
                    obj.createdBy = reader.GetString("created_by");
                    obj.createdAtLocation = reader.GetString("created_location");
                    obj.block = reader.GetString("block");
                    obj.latitude = reader.GetString("latitude");
                    obj.longitude = reader.GetString("longitude");
                    Surveys.Add(obj);
                }
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
            }

            return Surveys;
        }

        public List<SurveyBasicDetail> GetSurveysByBlock(int blockId, int startIndex, int count)
        {
            List<SurveyBasicDetail> Surveys = new List<SurveyBasicDetail>();

            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                con.Open();
                cmd.Connection = con;

                cmd.CommandText = "select count(*) from sbsdb.survey";
                object result = cmd.ExecuteScalar();
                int totalRecords = int.Parse(result.ToString());
                int fetchCount = count;

                if (totalRecords < startIndex + count - 1)
                {
                    fetchCount = startIndex + count - totalRecords;
                }

                cmd.CommandText =
                    "select * from (select * from (select s.id, s.benef_name, s.benef_father, s.created_location, s.latitude, s.longitude, v.name panchayat, bl.name block, d.name district, u.name created_by from sbsdb.survey s " +
                    "inner join sbsdb.panchayat v on s.panchayat_id= v.id " +
                    "inner join sbsdb.block bl on v.block_id = bl.id " +
                    "left join sbsdb.user u on u.id=s.created_by " +
                    "inner join sbsdb.district d on d.id = bl.district_id where bl.id=@blockId order by s.benef_name asc limit "
                    + (startIndex + count - 1) + ") a order by benef_name desc limit " + fetchCount +
                    ") b order by benef_name asc;";

                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@blockId", Value = blockId, MySqlDbType = MySqlDbType.Int32});
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SurveyBasicDetail obj =
                        new SurveyBasicDetail();
                    obj.id = reader.GetString("id");
                    obj.benfName = reader.GetString("benef_name");
                    obj.benfHead = reader.GetString("benef_father");
                    obj.panchayat = reader.GetString("panchayat");
                    obj.createdBy = reader.GetString("created_by");
                    obj.createdAtLocation = reader.GetString("created_location");
                    obj.latitude = reader.GetString("latitude");
                    obj.longitude = reader.GetString("longitude");
                    Surveys.Add(obj);
                }
            }
            finally

            {
                if (con != null)
                {
                    con.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
            }

            return Surveys;
        }

        public List<SurveyBasicDetail> GetSurveysByPanchayat(int panchayatId, int startIndex, int count)
        {
            List<SurveyBasicDetail> Surveys = new List<SurveyBasicDetail>();
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand();
            con.Open();

            try
            {
                cmd.Connection = con;

                cmd.CommandText = "select count(*) from sbsdb.survey";
                object result = cmd.ExecuteScalar();
                int totalRecords = int.Parse(result.ToString());
                int fetchCount = count;

                if (totalRecords < startIndex + count - 1)
                {
                    fetchCount = startIndex + count - totalRecords;
                }

                cmd.CommandText =
                    "select * from (select * from (select s.id, s.benef_name, s.benef_father, s.created_location, s.latitude, s.longitude, v.name panchayat, bl.name block, d.name district, u.name created_by from sbsdb.survey s " +
                    "inner join sbsdb.panchayat v on s.panchayat_id= v.id " +
                    "inner join sbsdb.block bl on v.block_id = bl.id " +
                    "left join sbsdb.user u on u.id=s.created_by " +
                    "inner join sbsdb.district d on d.id = bl.district_id where v.id=@panchayatId order by s.benef_name asc limit " +
                    (startIndex + count - 1) + ") a order by benef_name desc limit " + fetchCount +
                    ") b order by benef_name asc;";

                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@panchayatId", Value = panchayatId, MySqlDbType = MySqlDbType.Int32});
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SurveyBasicDetail obj = new SurveyBasicDetail();
                    obj.id = reader.GetString("id");
                    obj.benfName = reader.GetString("benef_name");
                    obj.benfHead = reader.GetString("benef_father");
                    obj.panchayat = reader.GetString("panchayat");
                    obj.createdBy = reader.GetString("created_by");
                    obj.createdAtLocation = reader.GetString("created_location");
                    obj.latitude = reader.GetString("latitude");
                    obj.longitude = reader.GetString("longitude");
                    Surveys.Add(obj);
                }
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
            }

            return Surveys;
        }

        public Survey GetSurvey(Int32 surveyId)
        {
            Survey survey = new Survey();
            MySqlDataReader reader = null;

            try
            {
                MySqlCommand cmd = new MySqlCommand(
                    "select s.benef_name, s.benef_father, s.benef_adhar, s.benef_member_count, s.created_location, s.latitude, s.longitude, v.name panchayat, bl.name block, d.name district, u.name created_by from sbsdb.survey s " +
                    "inner join sbsdb.panchayat v on s.panchayat_id= v.id " +
                    "inner join sbsdb.block bl on v.block_id = bl.id " +
                    "left join sbsdb.user u on u.id=s.created_by " +
                    "inner join sbsdb.district d on d.id = bl.district_id where s.id=@surveyId;", con);


                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@surveyId", Value = surveyId, MySqlDbType = MySqlDbType.Int32});
                con.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Beneficiary obj = new Beneficiary();
                    obj.name = Convert.ToString(reader["benef_name"]);
                    obj.fatherOrHusbandName = Convert.ToString(reader["benef_father"]);
                    obj.adhar = Convert.ToString(reader["benef_adhar"]);
                    obj.memberCount = Convert.ToInt32(reader["benef_member_count"]);

                    survey.beneficiary = obj;
                    survey.panchayat = Convert.ToString(reader["panchayat"]);
                    survey.block = Convert.ToString(reader["block"]);
                    survey.district = Convert.ToString(reader["district"]);
                    survey.created_by = Convert.ToString(reader["created_by"]);
                    survey.created_location = Convert.ToString(reader["created_location"]);
                }

                reader.Close();


                cmd = new MySqlCommand(
                    "select question_id, answer_id from sbsdb.questionnaire where survey_id=@surveyId",
                    con);
                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@surveyId", Value = surveyId, MySqlDbType = MySqlDbType.Int32});
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    QuestionResponse q = new QuestionResponse();
                    q.questionId = Convert.ToInt32(reader["question_id"]);
                    q.answerId = Convert.ToInt32(reader["answer_id"]);
                    survey.questionnaries.Add(q);
                }
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
            }

            return survey;
        }

        public void InsertSurvey(Survey survey)
        {
            InsertSurvey(survey, null);
        }

        private void InsertSurvey(Survey survey, MySqlTransaction trns)
        {
            int surveyId = 0;

            MySqlCommand cmd = new MySqlCommand();

            if (trns == null)
            {
                con.Open();
                trns = con.BeginTransaction();
            }

            try
            {
                cmd.CommandText = "select id from sbsdb.survey where benef_adhar='"+ survey.beneficiary.adhar+"';";
                cmd.Connection = con;
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    throw new ValidationException("Survey already exists for beneficiary with adhar no: " +
                                                  survey.beneficiary.adhar);
                }

                StringBuilder cmdText = new StringBuilder(
                    "insert into sbsdb.survey(benef_name, benef_father, benef_adhar, benef_member_count, panchayat_id, latitude, longitude, created_by) "
                    + "values(@name, @fatherOrHusbandName, @adhar, @memberCount, @panchayatId,@latitude, @longitude, @created_by); select id from sbsdb.survey where benef_adhar=@adhar;");


                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@name", Value = survey.beneficiary.name, MySqlDbType = MySqlDbType.String});
                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@fatherOrHusbandName", Value = survey.beneficiary.fatherOrHusbandName,
                    MySqlDbType = MySqlDbType.String
                });
                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@adhar", Value = survey.beneficiary.adhar, MySqlDbType = MySqlDbType.String});
                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@memberCount", Value = survey.beneficiary.memberCount,
                    MySqlDbType = MySqlDbType.Int32
                });
                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@panchayatId", Value = survey.panchayat, MySqlDbType = MySqlDbType.Int32});
                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@latitude", Value = survey.latitude, MySqlDbType = MySqlDbType.String});
                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@longitude", Value = survey.longitude, MySqlDbType = MySqlDbType.String});
                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@created_by", Value = survey.created_by, MySqlDbType = MySqlDbType.Int32});


                cmd.CommandText = cmdText.ToString();

                object obj = cmd.ExecuteScalar();

                surveyId = int.Parse(obj.ToString());

                string[] p = new string[2];

                cmdText = new StringBuilder();

                for (int i = 0; i < survey.questionnaries.Count; i++)
                {
                    QuestionResponse q = survey.questionnaries[i];

                    p[0] = "@qId" + i;
                    p[1] = "@ansId" + i;

                    cmdText.Append("insert into sbsdb.questionnaire(question_id, answer_id, survey_id) values(" +
                                   string.Join(",", p) + ", @surveyId); ");
                    cmd.Parameters.Add(new MySqlParameter()
                        {ParameterName = p[0], Value = q.questionId, MySqlDbType = MySqlDbType.Int32});
                    cmd.Parameters.Add(new MySqlParameter()
                        {ParameterName = p[1], Value = q.answerId, MySqlDbType = MySqlDbType.Int32});

                }

                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@surveyId", Value = surveyId, MySqlDbType = MySqlDbType.Int32});

                cmd.CommandText = cmdText.ToString();

                cmd.ExecuteNonQuery();

                trns.Commit();

            }
            catch (Exception e)
            {
                trns.Rollback();
                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<Int32> InsertSurveys(List<Survey> surveys)
        {
            List<Int32> resultIds = new List<Int32>();
           
            foreach (var survey in surveys)
            {
                try
                {
                    InsertSurvey(survey, null);
                    resultIds.Add(survey.id);
                }
                catch (ValidationException e)
                {
                    // throw only if none of the record is saved
                    if (resultIds.Count <= 0)
                    {
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    
                }
            }
            
            return resultIds;
        }

        public void UpdateSurvey(Survey survey)
        {
            StringBuilder cmdText = new StringBuilder("delete from sbsdb.questionnaire where survey_id=@surveyId; delete from sbsdb.survey where id=@surveyId; ");
           
            MySqlCommand cmd = new MySqlCommand();

            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@surveyId", Value = survey.id, MySqlDbType = MySqlDbType.Int32 });
            
            cmd.CommandText = cmdText.ToString();
            cmd.Connection = con;

            con.Open();

            using (MySqlTransaction trns = con.BeginTransaction())
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    trns.Rollback();
                    if (con != null)
                    {
                        con.Close();
                    }
                    throw e;
                }

                // Transaction will be committed in this method
                InsertSurvey(survey, trns);
            }
        }

        public void DeleteSurvey(Int32 surveyId)
        {
            MySqlCommand cmd = new MySqlCommand("delete from sbsdb.questionnaire where survey_id=@surveyId; delete from sbsdb.survey where id=@surveyId; ", con);
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@surveyId", Value = surveyId, MySqlDbType = MySqlDbType.Int32 });

            con.Open();
            cmd.ExecuteNonQuery();
            if (con != null)
            {
                con.Close();
            }
        }
    }
}
