using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

using MySql.Data.MySqlClient;
using sbs_api.models;
namespace sbs_api.repository
{
    public class QuestionRepo : IQuestionRepo
    {
        
        private MySqlConnection con;
        public QuestionRepo()
        {
            
            string conStr = ConfigurationManager.ConnectionStrings[Constants.CONNECTION_STRING_NAME].ToString();
            con = new MySqlConnection(conStr);
        }

        public List<QuestionGroup> GetGroupWiseQuestions()
        {
            List<QuestionGroup> questionGroups = new List<QuestionGroup>(){new QuestionGroup()};
            List<Question> questions = new List<Question>();
            Dictionary<int, string> groups = new Dictionary<int, string>();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from sbsdb.headings;", con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                groups.Add((int)reader["id"], reader["text"].ToString());
            }
            reader.Close();
            
            cmd = new MySqlCommand("select q.id, q.text, possible_answers, h.id heading_id from question q left outer join headings h on h.id = q.heading_id order by q.index desc", con);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Question  obj= new  Question();
                obj.id = Convert.ToInt32(reader["id"]);
                obj.text = Convert.ToString(reader["text"]);

                //if (reader["possible_answers"] != DBNull.Value)
                //{
                //    obj.possible_answers = new List<int>();
                //    List<string> answers = Convert.ToString(reader["possible_answers"]).Split(',').ToList();
                //    foreach (var val in answers)
                //    {
                //        obj.possible_answers.Add(Convert.ToInt32(val));
                //    }
                //}
                obj.possible_answers = Convert.ToString(reader["possible_answers"]);

                if (reader["heading_id"] == DBNull.Value)
                    obj.headingId = null; 
                else
                    obj.headingId = Convert.ToInt32(reader["heading_id"]);
                questions.Add(obj);
            }

            int? lastHeading = questions[0].headingId;
            if (questions[0].headingId != null)
            {
                questionGroups[0].text = groups[questions[0].headingId.Value];
            }
            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].headingId != lastHeading)
                {
                    lastHeading = questions[i].headingId;
                    if (questions[i].headingId != null)
                        questionGroups.Add(new QuestionGroup() {text = groups[questions[i].headingId.Value]});
                    else
                        questionGroups.Add(new QuestionGroup());
                }

                questionGroups[questionGroups.Count -1].questions.Add(questions[i]);
            }
            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }
            return questionGroups;
        }

        public List<PossibleAnswers> GetPossibleAnswers()
        {
            List<PossibleAnswers> answers = new List<PossibleAnswers>();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from sbsdb.possible_answers;", con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                answers.Add( new PossibleAnswers(){ id = (int)reader["id"], text = reader["text"].ToString()});
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }

            return answers;
        }

        public IEnumerable<Question> GetQuestions()
        {
            List<Question> questions = new List<Question>();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from sbsdb.question;", con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Question q = new Question();

                q.id = (int) reader["id"];
                q.text = reader["text"].ToString();
                if (reader["heading_id"] != null && reader["heading_id"] != DBNull.Value)
                {
                    q.headingId = (Convert.ToInt32(reader["heading_id"]));
                }

                q.index = Convert.ToInt32(reader["index"]);
                //string[] strAnswers = Convert.ToString(reader["possible_answers"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //q.possible_answers = new List<int>();
                //foreach (var ans in strAnswers)
                //{
                //    q.possible_answers.Add(Convert.ToInt32(ans));
                //}
                q.possible_answers = Convert.ToString(reader["possible_answers"]);
                q.isactive = Convert.ToInt32(reader["isactive"]);
                questions.Add(q);
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }

            return questions;
        }

        public IEnumerable<Heading> GetQuestionHeadings()
        {
            List<Heading> headings = new List<Heading>();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from sbsdb.headings;", con);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Heading q = new Heading();

                q.id = (int)reader["id"];
                q.text = reader["text"].ToString();
                headings.Add(q);
            }

            if (con != null)
            {
                con.Close();
            }
            if (reader != null)
            {
                reader.Close();
            }

            return headings;

        }


        public void InsertQuestion(Question q)
        {
            MySqlCommand cmd = new MySqlCommand("insert into sbsdb.Question(text, possibleAnswers, heading, index) values(@text, @possibleAnswers, @heading, @index)", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value=q.text, MySqlDbType =  MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@possibleAnswers", Value = q.possible_answers, MySqlDbType = MySqlDbType.String });
            if (q.headingId == null)
                cmd.Parameters.Add(new MySqlParameter() {ParameterName = "@heading", Value = DBNull.Value});
            else
                cmd.Parameters.Add(new MySqlParameter()
                    {ParameterName = "@heading", Value = q.headingId.Value, MySqlDbType = MySqlDbType.Int32});

            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@index", Value = q.index, MySqlDbType = MySqlDbType.Int32 });
            cmd.ExecuteNonQuery();

            if (con != null)
            {
                con.Close();
            }
        }

        public void UpdateQuestion(int id, Question q)
        {
            MySqlCommand cmd = new MySqlCommand("update sbsdb.question set text=@name, possible_answers=@possibleAnswers, heading_id=@headingId, index=@index where id=@id", con);
            con.Open();
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@name", Value = q.text, MySqlDbType = MySqlDbType.String });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@possibleAnswers", Value = q.possible_answers, MySqlDbType = MySqlDbType.String });
            if (q.headingId == null)
                cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@heading", Value = DBNull.Value });
            else
                cmd.Parameters.Add(new MySqlParameter()
                    { ParameterName = "@heading", Value = q.headingId.Value, MySqlDbType = MySqlDbType.Int32 });

            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@index", Value = q.index, MySqlDbType = MySqlDbType.Int32 });
            cmd.Parameters.Add(new MySqlParameter() { ParameterName = "@id", Value = id, MySqlDbType = MySqlDbType.Int32 });


            cmd.ExecuteNonQuery();


            if (con != null)
            {
                con.Close();
            }
        }

        public void DeleteQuestion(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from sbsdb.question where id=@id", con);
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
