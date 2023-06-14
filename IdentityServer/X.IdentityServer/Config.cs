// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace X.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catolog"){Scopes={"catalog_fullpermission"}},
             new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermisson"}},
              new ApiResource("resource_basket"){Scopes={"basket_fullpermisson"}},
               new ApiResource("resource_discount"){Scopes={"discount_fullpermisson"}},
                new ApiResource("resource_order"){Scopes={"order_fullpermisson"}},
                 new ApiResource("resource_payment"){Scopes={"payment_fullpermisson"}},
                  new ApiResource("resource_gateway"){Scopes={"gateway_fullpermisson"}},
                   new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
               new IdentityResources.Email(),
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResource(){Name="roles",DisplayName="Roles",Description="Users roles",UserClaims=new[]{ "role"} }
             };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
               new ApiScope("catalog_fullpermission"),
                new ApiScope("photo_stock_fullpermisson"),
                 new ApiScope("basket_fullpermisson"),
                  new ApiScope("discount_fullpermisson"),
                   new ApiScope("order_fullpermisson"),
                    new ApiScope("payment_fullpermisson"),
                     new ApiScope("gateway_fullpermisson"),
               new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
           new Client[]
            {
               new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId="WebMvcClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={ "catalog_fullpermission", "photo_stock_fullpermisson", IdentityServerConstants.LocalApi.ScopeName }
                },
               new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId="WebMvcClientForUser",
                    AllowOfflineAccess=true,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes=
                    {
                       "basket_fullpermisson","discount_fullpermisson","order_fullpermisson","payment_fullpermisson","gateway_fullpermisson",
                       IdentityServerConstants.StandardScopes.Email,
                       IdentityServerConstants.StandardScopes.Profile,
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.LocalApi.ScopeName,
                       IdentityServerConstants.StandardScopes.OfflineAccess,"roles"
                    },
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage=TokenUsage.ReUse
                }

            };
    }
}