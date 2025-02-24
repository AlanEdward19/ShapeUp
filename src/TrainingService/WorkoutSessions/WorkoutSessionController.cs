using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainingService.WorkoutSessions;

[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
public class WorkoutSessionController : ControllerBase
{
    
}