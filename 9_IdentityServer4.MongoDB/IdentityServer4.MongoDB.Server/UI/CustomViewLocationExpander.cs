using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Server.UI
{
    public class CustomViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            yield return "~/UI/{1}/Views/{0}.cshtml";
            yield return "~/UI/SharedViews/{0}.cshtml";
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
