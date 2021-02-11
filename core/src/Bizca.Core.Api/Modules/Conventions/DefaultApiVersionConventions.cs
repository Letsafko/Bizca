namespace Bizca.Core.Api.Modules.Conventions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class DefaultApiVersionConvention : IApplicationModelConvention
    {
        private readonly string versionMatcher;
        public DefaultApiVersionConvention(string routeConstraint)
        {
            versionMatcher = "(/?[^/]+{.+:" + routeConstraint + "}/)";
        }

        public void Apply(ApplicationModel application)
        {
            foreach (ControllerModel applicationController in application.Controllers)
            {
                foreach (RouteAttribute route in GetAllRouteWithVersionAttribute(applicationController.Attributes))
                {
                    string defaultRoute = Regex.Replace(route.Template, versionMatcher, "/").Trim('/');
                    applicationController.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel
                        {
                            Template = defaultRoute
                        }
                    });
                }
            }
        }

        private IEnumerable<RouteAttribute> GetAllRouteWithVersionAttribute(IReadOnlyList<object> controllerAttributes)
        {
            foreach (object attr in controllerAttributes)
            {
                if (attr is RouteAttribute)
                {
                    var routeAttr = attr as RouteAttribute;

                    if (Regex.IsMatch(routeAttr.Template, versionMatcher))
                    {
                        yield return routeAttr;
                    }
                }
            }
        }

    }
}