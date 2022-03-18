using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StendenCafe.Controllers
{
    [Route("api/[controller]")]
    // ApiController attribute allows for automatic model validation
    [ApiController]
    public abstract class MyControllerBase : ControllerBase
    {
    }
}
