using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
namespace WebSiteBanHangNoiThat.Controllers
{

    public class LocalhostConstraint : IRouteConstraint
    {
        public bool Match
            (
                HttpContextBase httpContext,
                Route route,
                string parameterName,
                RouteValueDictionary values,
                RouteDirection routeDirection
            )
        {
            return httpContext.Request.IsLocal;
        }
    }
}