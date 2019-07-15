using System;
using System.Collections.Generic;
using System.Text;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface ICountryRepo
    {
        List<Country> GetCountries();

        void InsertCountry(string name);

        void UpdateCountry(int id, string name);

        void DeleteCountry(int id);

    }
}
