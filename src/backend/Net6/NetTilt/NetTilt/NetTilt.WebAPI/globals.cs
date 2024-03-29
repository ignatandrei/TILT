﻿global using AMSWebAPI;
global using Generated;
global using Microsoft.AspNetCore.Http.Json;
global using Microsoft.EntityFrameworkCore;
global using NetCore2BlocklyNew;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using NetTilt.WebAPI;
global using HealthChecks.UI.Client;
global using HealthChecks.UI.Core;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using NetTilt.Auth;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Azure.Monitor.OpenTelemetry.Exporter;
global using OpenTelemetry.Resources;
global using OpenTelemetry.Trace;
global using NetTilt.Logic;
global using Lib.AspNetCore.ServerTiming;

global using Hellang.Middleware.ProblemDetails;
global using System.Diagnostics;


[assembly: AMS_Base.VersionReleased(Name = "FutureRelease", ISODateTime = "9999-04-16", recordData = AMS_Base.RecordData.Merges)]
[assembly: AMS_Base.VersionReleased(Name = "Ang_Instrumentation,CountTilts,TypedReactive", ISODateTime = "2022-10-13", recordData = AMS_Base.RecordData.Merges)]
[assembly: AMS_Base.VersionReleased(Name = "ProgrammersData", ISODateTime = "2022-09-29", recordData = AMS_Base.RecordData.Merges)]
[assembly: AMS_Base.VersionReleased(Name = "Licences", ISODateTime = "2022-09-14", recordData = AMS_Base.RecordData.Merges)]
[assembly: AMS_Base.VersionReleased(Name = "AddTimeZone for Today TILT -and more improvements", ISODateTime = "2022-08-17", recordData = AMS_Base.RecordData.Merges)]
[assembly: AMS_Base.VersionReleased(Name = "ServerTiming,Export", ISODateTime = "2022-06-07", recordData = AMS_Base.RecordData.Merges)]
[assembly: AMS_Base.VersionReleased(Name = "MVP", ISODateTime = "2022-05-09", recordData = AMS_Base.RecordData.Merges)]
[assembly: AMS_Base.VersionReleased(Name = "Calendar,help,streak", ISODateTime = "2022-05-25", recordData = AMS_Base.RecordData.Merges)]
