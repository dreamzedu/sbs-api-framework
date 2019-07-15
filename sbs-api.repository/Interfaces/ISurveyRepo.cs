using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface ISurveyRepo
    {
        List<SurveyBasicDetail> GetSurveys(int startIndex, int count);
        List<SurveyBasicDetail> GetSurveysByDistrict(int districtId, int startIndex, int count);

        List<SurveyBasicDetail> GetSurveysByBlock(int blockId, int startIndex, int count);

        List<SurveyBasicDetail> GetSurveysByPanchayat(int panchayatId, int startIndex, int count);

        Survey GetSurvey(Int32 surveyId);

        void InsertSurvey(Survey survey);

        void UpdateSurvey(Survey survey);

        void DeleteSurvey(int surveyId);

        List<Int32> InsertSurveys(List<Survey> surveys);
    }
}
