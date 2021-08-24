using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

using identity.API.Data.Feature;
using identity.API.Entities;

namespace identity.API.Repositories.IdentityService
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            IEnumerable<Claim> UserRoleClaims = context.HttpContext.User.FindAll("Roles");
            List<string> UserRoles = new List<string> { };
            List<string> FeatureIds = new List<string> { };

            IFeatureContext _featureContext = context.HttpContext.RequestServices.GetService(typeof(IFeatureContext)) as IFeatureContext;

            ControllerActionDescriptor controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string ServiceName = controllerActionDescriptor.ControllerTypeInfo.FullName;
            string ControllerName = controllerActionDescriptor.ControllerName;
            string ActionName = controllerActionDescriptor.ActionName;

            foreach (Claim role in UserRoleClaims)
            {
                UserRoles.Add(role.Value);
            }

            var featureRoleMapfilter = Builders<FeatureRoleMaps>.Filter.In(item => item.RoleName, UserRoles.ToArray());
            List<FeatureRoleMaps> featureRoleMaps = await _featureContext.FeatureRoleMaps
                .Find(featureRoleMapfilter)
                .ToListAsync();

            if (featureRoleMaps.Count == 0)
            {
                context.Result = new ForbidResult();
            }

            foreach (FeatureRoleMaps roleMap in featureRoleMaps)
            {
                FeatureIds.Add(roleMap.FeatureId);
            }

            var featureEndpointMapBuilder = Builders<FeatureEndpointMaps>.Filter;
            var featureEndpointMapfilter = featureEndpointMapBuilder.In(item => item.FeatureId, FeatureIds.ToArray())
                & featureEndpointMapBuilder.Eq(item => item.Service, ServiceName)
                & featureEndpointMapBuilder.Eq(item => item.Controller, ControllerName)
                & featureEndpointMapBuilder.Eq(item => item.Action, ActionName);

            FeatureEndpointMaps featureEndpointMap = await _featureContext.FeatureEndpointMaps
                .Find(featureEndpointMapfilter)
                .FirstOrDefaultAsync();

            if (featureEndpointMap == null)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
