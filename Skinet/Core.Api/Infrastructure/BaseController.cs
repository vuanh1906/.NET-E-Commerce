using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Core.Api.Infrastructure
{
    [Authorize]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected int UserId { get { return Convert.ToInt32(User?.Claims.Where(c => c.Type == "userId").FirstOrDefault()?.Value); } }
    }
}