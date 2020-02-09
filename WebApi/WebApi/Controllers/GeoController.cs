using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonTools;
using loadMaxmind.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public GeoController(ApplicationContext db)
        {
            _db = db;
        }

        // GET: api/Geo?ip=213.135.141.85
        [HttpGet]
        public Ipv4bloc Get(string ip)
        {
            uint uintIp = ip.IpToUint32();
            Ipv4bloc ipInfo = _db.Ipv4bloc.Where(x => x.IpMax >= uintIp && x.IpMin <= uintIp).FirstOrDefault();
            
            return ipInfo;
        }
    }
}
