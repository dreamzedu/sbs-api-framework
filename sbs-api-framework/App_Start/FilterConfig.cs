using System.Web;
using System.Web.Mvc;
using sbs_api_framework.Filters;

namespace sbs_api_framework
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
        }
    }
}
