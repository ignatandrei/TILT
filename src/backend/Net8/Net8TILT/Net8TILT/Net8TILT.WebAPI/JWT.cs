namespace Net8TILT.WebAPI;

public static class JWT
{
    public static void AddAuth(this WebApplicationBuilder builder)
    {
        var key = builder.Configuration["MySettings:secretToken"];
        builder.Services.AddAuthorization(options =>

            options.AddPolicy("CustomBearer", policy =>
            {
                policy.AuthenticationSchemes.Add("CustomBearer");
                policy.RequireAuthenticatedUser();
            }));
        builder.Services.AddAuthentication()
                   .AddJwtBearer("CustomBearer", options =>
                   {

                       options.RequireHttpsMetadata = false;
                       options.SaveToken = true;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                           ValidateIssuer = false,
                           ValidateAudience = false
                       };
                       options.Events = new JwtBearerEvents()
                       {
                           OnMessageReceived = ctx =>
                           {
                               if (!(ctx?.Request?.Headers?.ContainsKey("Authorization") ?? true))
                               {
                                   ctx.NoResult();
                                   return Task.CompletedTask;
                               };
                               var auth = ctx.Request.Headers["Authorization"].ToString();
                               if (string.IsNullOrEmpty(auth))
                               {
                                   ctx.NoResult();
                                   return Task.CompletedTask;
                               }
                               if (!auth.StartsWith("CustomBearer ", StringComparison.OrdinalIgnoreCase))
                               {
                                   ctx.NoResult();
                                   return Task.CompletedTask;
                               }

                               ctx.Token = auth.Substring("CustomBearer ".Length).Trim();
                               return Task.CompletedTask;

                           }
                       };
                       //x.Events.OnTokenValidated=
                   });

    }

}
