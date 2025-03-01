using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using YouTubeWebAPI.Middlewares.Interfaces;
using YouTubeWebAPI.Models.Endpoints;
namespace YouTubeWebAPI.Middlewares.Providers
{
    public class RouteProvider : IRouteProvider
    {
        private readonly IActionDescriptorCollectionProvider? _actionDescriptorCollectionProvider;
        public RouteProvider(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }
        public IEnumerable<UrlMetadata> GetRoutes()
        {
            var routes = new List<UrlMetadata>();
            var actionDescriptors = _actionDescriptorCollectionProvider?.ActionDescriptors.Items;
            foreach (var actionDescriptor in actionDescriptors)
            {
                if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    var routeMetadata = new UrlMetadata
                    {
                        ControllerName = controllerActionDescriptor.ControllerName,
                        ActionName = controllerActionDescriptor.ActionName,
                        HttpMethod = controllerActionDescriptor.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods?.FirstOrDefault(),
                        RouteTemplate = controllerActionDescriptor.AttributeRouteInfo?.Template
                    };
                    var parameters = controllerActionDescriptor.MethodInfo.GetParameters();
                    foreach (var parameter in parameters)
                    {
                        routeMetadata?.MethodParameterName?.Add(parameter.Name);
                        routeMetadata?.MethodParameterType?.Add(parameter.ParameterType.Name);   
                    }
                    routes.Add(routeMetadata ?? new UrlMetadata());
                }
            }
            List<UrlMetadata> urls = routes.Where(w => w.MethodParameterName.Count() >= 2).ToList();
            return urls;
        }
    }
}