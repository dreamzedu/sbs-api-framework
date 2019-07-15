using System;
using System.Collections.Generic;
using System.Text;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface IAnalyticsRepo
    {
        List<ResponsePercentage> GetDistrictWiseReport(int districtId);

        List<ResponsePercentage> GetBlockWiseReport(int blockId);

        List<ResponsePercentage> GetPanchayatWiseReport(int panchayatId);
    }
}
