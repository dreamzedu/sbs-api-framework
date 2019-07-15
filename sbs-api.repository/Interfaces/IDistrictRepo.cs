using System;
using System.Collections.Generic;
using System.Text;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface IDistrictRepo
    {
        List<District> GetDistricts(int stateId);

        void InsertDistrict(string name, int stateId);

        void UpdateDistrict(int id, string name);

        void DeleteDistrict(int id);

    }
}
