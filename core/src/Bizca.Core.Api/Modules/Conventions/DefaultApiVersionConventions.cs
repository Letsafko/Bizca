namespace Bizca.Core.Api.Modules.Conventions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class DefaultApiVersionConvention : IApplicationModelConvention
    {
        private readonly string _versionMatcher;

        public DefaultApiVersionConvention(string routeConstraint)
        {
            _versionMatcher = "(/?[^/]+{.+:" + routeConstraint + "}/)";
        }

        public void Apply(ApplicationModel application)
        {
            foreach (ControllerModel applicationController in application.Controllers)
            foreach (RouteAttribute route in GetAllRouteWithVersionAttribute(applicationController.Attributes))
            {
                var defaultRoute = Regex.Replace(route.Template, 
                    _versionMatcher, 
                    "/")
                    .Trim('/');
                
                applicationController
                    .Selectors
                    .Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel { Template = defaultRoute }
                    });
            }
        }

        private IEnumerable<RouteAttribute> GetAllRouteWithVersionAttribute(IEnumerable<object> controllerAttributes)
        {
            foreach (object attr in controllerAttributes)
                if (attr is RouteAttribute routeAttr)
                {
                    if (Regex.IsMatch(routeAttr.Template, _versionMatcher)) 
                        yield return routeAttr;
                }
        }
    }
}