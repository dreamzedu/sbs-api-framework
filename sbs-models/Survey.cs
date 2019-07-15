using System;
using System.Collections.Generic;
using System.Text;

namespace sbs_api.models
{
    public class Survey
    {
        public Int32 id;

        public string district;

        public string block;

        public string panchayat;

        public string created_by;

        public string latitude;

        public string longitude;

        public Beneficiary beneficiary;

        public List<QuestionResponse> questionnaries = new List<QuestionResponse>();

        public string created_location;
    }
}
