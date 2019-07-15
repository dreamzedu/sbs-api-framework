using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbs_api.models;

namespace sbs_api.repository.Interfaces
{
    public interface IDataVersionRepo
    {
        List<DataVersion> GetDataVersions();
    }
}
