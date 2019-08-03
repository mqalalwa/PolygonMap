using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PolygonMap.Domain.ApiModels;
using PolygonMap.Domain.Supervisor;

using Microsoft.EntityFrameworkCore;
using PolygonMap.Domain.Entities;
using PolygonMap.Domain.Repositories;

using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json.Linq;
using PolygonMap.API.Utitlites;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PolygonMap.API.Controllers
{
    [Route("api/[controller]")]
    public class PolygonMapController : Controller
    {

        private readonly IPolygonMapSupervisor _polygonMapSupervisor;

        public PolygonMapController(IPolygonMapSupervisor polygonMapSupervisor)
        {
            _polygonMapSupervisor = polygonMapSupervisor;
        }
        
        [HttpGet("[action]")]
        [Produces(typeof(List<ShapeApiModel>))]
        public async Task<ActionResult<List<ShapeApiModel>>> GetAllShapeAsync()
        {
            try
            {
                return new ObjectResult(await _polygonMapSupervisor.GetAllShapeAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("[action]/{id}")]
        [Produces(typeof(ShapeApiModel))]
        public async Task<ActionResult<ShapeApiModel>> GetShapeByIdAsync(int id)
        {
            try
            {
                var Shape = await _polygonMapSupervisor.GetShapeByIdAsync(id);
                if (Shape == null)
                {
                    return NotFound();
                }

                return Ok(Shape);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("[action]")]
        [Produces(typeof(List<PolygonApiModel>))]
        public async Task<ActionResult<List<PolygonApiModel>>> GetPolygonsByListOfIdsAsync([FromQuery] List<int> ids)
        {
            try
            {
                var Polygons = await _polygonMapSupervisor.GetPolygonsByListOfIdsAsync(ids);
                if (Polygons == null)
                {
                    return NotFound();
                }          
                return Polygons.ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("[action]/{id}/{RegonName}/{newCenterLng}/{newCenterlat}")]
        [Produces(typeof(PolygonApiModel))]
        public async Task<ActionResult<PolygonApiModel>> CalPointsWithNewCenterAsync(int id, string RegonName, float newCenterlat, float newCenterLng)
        {
            try
            {
                var Shape = await _polygonMapSupervisor.GetShapeByIdAsync(id);
                if (Shape == null)
                {
                    return NotFound();
                }
                var newPolygonApiModel = CalDisplacementPoints(Shape, RegonName, newCenterlat, newCenterLng); 
                var polygon= await _polygonMapSupervisor.AddPolygonAsync(newPolygonApiModel);                
                return Ok(polygon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }               

        [HttpPut("[action]/{id}")]
        [Produces(typeof(PolygonApiModel))]
        public async Task<ActionResult<PolygonApiModel>> UpdatePolygonByIdAsync(int id, [FromBody] PolygonApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _polygonMapSupervisor.GetPolygonByIdAsync(id) == null)
                {
                    return NotFound();
                }
                var shape = await _polygonMapSupervisor.GetShapeByIdAsync(input.ShapeID);
                if(shape == null)
                {
                    return NotFound();
                }
                var newPolygonApiModel = CalDisplacementPoints(shape,input.Name, input.RealLatitude, input.RealLongitude);
                newPolygonApiModel.PolygonID = id;
                if (await _polygonMapSupervisor.UpdatePolygonAsync(newPolygonApiModel))
                {
                    return Ok(newPolygonApiModel);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("[action]/{id}")]
        [Produces(typeof(void))]
        public async Task<ActionResult> DeletePolygonByIdAsync(int id)
        {
            try
            {
                if (await _polygonMapSupervisor.GetPolygonByIdAsync(id) == null)
                {
                    return NotFound();
                }

                if (await _polygonMapSupervisor.DeletePolygonAsync(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("export")]
        public async Task<ActionResult> ExportToPDF([FromQuery] List<int> ids)
        {           
            string folderPath = @"C:\PDF";
            string zipPath = @"pdf.zip";

            var polygons =await GetPolygonsByListOfIdsAsync(ids);            
            foreach (var polygon in polygons.Value) 
            {
                CreatePDFFile(polygon, folderPath);
            }
            var zipFile = CreateZipFile(zipPath, folderPath);
            return File(zipFile, "application/octet-stream", "Ploygon.zip");
        }
        private IList<PointApiModel> DisplacementPoint(ShapeApiModel shape, float lat, float lng)
        {
            foreach (PointApiModel point in shape.Points)
            {
                point.Latitude += lat;
                point.Longitude += lng;
            }
            return shape.Points;
        }
        private void CreatePDFFile(PolygonApiModel polygon, string folderPath)
        {
            PdfContentByte cb = null;
            Document doc = null;
            var points = JsonConvert.DeserializeObject<List<Point>>(polygon.Points);
            points = ConvertCordinations(points);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            try
            {
                doc = new Document();
                var writer = PdfWriter.GetInstance(doc, new FileStream(folderPath + "/" + polygon.Name + "-" + polygon.PolygonID + ".pdf", FileMode.Create));
                doc.Open();

                cb = writer.DirectContent;
                int index = 0;
                foreach (var point in points)
                {
                    if (doc.PageSize.Height > point.Latitude && doc.PageSize.Width > point.Longitude)
                    {
                        if (index == 0)
                            cb.MoveTo(point.Longitude, doc.PageSize.Height-point.Latitude);
                        else
                            cb.LineTo(point.Longitude, doc.PageSize.Height - point.Latitude);
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                cb.ClosePath();
                cb.Stroke();
                doc.Close();
            }
        }
        private FileStream CreateZipFile(string zipPath, string folderPath)
        {
            if (System.IO.File.Exists(zipPath))
                System.IO.File.Delete(zipPath);

            ZipFile.CreateFromDirectory(folderPath, zipPath);
            var zipFile = new FileStream(zipPath, FileMode.Open, FileAccess.Read);
            return zipFile;
        }
        private List<Point> ConvertCordinations(List<Point> points)
        {
            var googleMapApiConverter = new GoogleMapsAPIProjection(12);
            

            foreach (var point in points)
            {
                var newPoint = googleMapApiConverter.FromCoordinatesToPixel(new System.Drawing.PointF(point.Longitude, point.Latitude));
                point.Latitude = newPoint.Y ;
                point.Longitude = newPoint.X;
            }
            double minHeight = points.Min(a => a.Latitude);
            double minWidth = points.Min(a => a.Longitude);
            foreach (var point in points)
            {
                point.Latitude = point.Latitude - (float)minHeight;
                point.Longitude = point.Longitude - (float)minWidth;
            }
            return points;
        }
        private PolygonApiModel CalDisplacementPoints(ShapeApiModel shape, string RegonName, float newCenterlat, float newCenterLng)
        {
            shape.Points = DisplacementPoint(shape, newCenterlat - shape.FixedLatitude,
                                        newCenterLng - shape.FixedLongitude);
            var newPolygonApiModel = new PolygonApiModel()
            {
                ShapeID = shape.ShapeID,
                Name = RegonName,
                RealLatitude = newCenterlat,
                RealLongitude = newCenterLng,
                Points = JsonConvert.SerializeObject(shape.Points, Formatting.Indented)
            };
            return newPolygonApiModel;
        }
    }
}
