using System;
using System.Collections.Generic;
using System.Text;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface IBlockRepo
    {
        List<Block> GetBlocks(int districtId);

        int InsertBlock(string name, int districtId);

        void UpdateBlock(int id, string name);

        void DeleteBlock(int id);

    }
}
