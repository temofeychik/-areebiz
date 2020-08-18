using CareebizExam.WebApi.Services.GoogleMaps;
using CareebizExam.WebApi.Services.Pdf;
using CareebizExam.WebApi.Services.Rectangle;
using CareebizExam.WebApi.Services.Zip;
using CareebizExam.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CareebizExam.WebApi.Controllers
{
    [Route("api/rectangle")]
    [ApiController()]
    public class RectangleController : Controller
    {
        private readonly IRectangleService _rectangleService;
        private readonly IGoogleMapsService _googleMapsService;
        private readonly IPdfService _pdfService;
        private readonly IZipService _zipService;

        public RectangleController(IRectangleService rectangleService, 
            IGoogleMapsService googleMapsService, 
            IPdfService pdfService,
            IZipService zipService)
        {
            _rectangleService = rectangleService ?? throw new ArgumentNullException(nameof(rectangleService));
            _googleMapsService = googleMapsService ?? throw new ArgumentNullException(nameof(googleMapsService));
            _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));
            _zipService = zipService ?? throw new ArgumentNullException(nameof(zipService));
        }

        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RectangleViewModel>))]
        public async Task<IActionResult> GetAllRectangles()
        {
            var result = await _rectangleService.GetAll();         

            return Ok(result);
        }

        [HttpPost("add")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestObjectResult))]
        public async Task<IActionResult> AddRectangle([FromBody]RectangleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _rectangleService.AddAsync(model);

            if (result !=  null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("update")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestObjectResult))]
        public async Task<IActionResult> UpdateRectangle([FromBody]RectangleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _rectangleService.UpdateAsync(model);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}/remove")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestObjectResult))]
        public async Task<IActionResult> UpdateRectangle([FromRoute]int id)
        {
            if (id <= 0)
                return BadRequest("Id field should be greater than 0.");

            var result = await _rectangleService.RemoveAsync(id);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }


        [HttpPost("export")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FileContentResult))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestObjectResult))]
        public async Task<IActionResult> Export([FromBody]IEnumerable<ExportRectanglesViewModel> rectangles)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Stream> pdfStreams = new List<Stream>();
            foreach (var r in rectangles)
            {
                var rect = await _rectangleService.Get(r.Id);
                
                if(rect != null)
                {
                    var strm = await _googleMapsService.GetMap(rect, r.ImageWidth, r.ImageHeight, r.Zoom);
                    pdfStreams.Add(_pdfService.ImageToPdf(strm, r.ImageWidth));
                }
            }
            var zipStream = await _zipService.MakeZip(pdfStreams.ToArray());

            return File(zipStream, "application/octet-stream", "map.zip");

        }


    }
}
