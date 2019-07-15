using System;
using System.Collections.Generic;
using System.Text;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface IStateRepo
    {
        List<State> GetStates(int countryId);

        void InsertState(string name, int countryId);

        void UpdateState(int id, string name);

        void DeleteState(int id);

    }
}
