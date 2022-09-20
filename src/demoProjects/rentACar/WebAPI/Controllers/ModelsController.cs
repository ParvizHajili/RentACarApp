using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModel;
using Application.Features.Models.Queries.GetListModelByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };
            ModelListModel result =await Mediator.Send(getListModelQuery);


            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListModelByDynamicQuery getListByDynamicModelQuery = new GetListModelByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };
            ModelListModel result = await Mediator.Send(getListByDynamicModelQuery);

            return Ok(result);
        }
    }
}


//        {
//  "sort": [
//    {
//      "field": "name",
//      "dir": "asc"
//    }
//  ],
//  "filter": {
//    "field": "name",
//    "operator": "eq",
//    "value": "Series 5",
//    "logic": "or",
//    "filters": [
//{
//      "field": "dailyPrice",
//    "operator": "gte",
//    "value": "600"
//}
//    ]
//  }
//}
