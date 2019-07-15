using System;
using System.Collections.Generic;
using System.Text;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface IPanchayatRepo
    {
        List<Panchayat> GetPanchayats(int blockId);

        List<Panchayat> GetPanchayats();

        int InsertPanchayat(string name, int blockId);

        void UpdatePanchayat(int id, string name);

        void DeletePanchayat(int id);

    }
}
