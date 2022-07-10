using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace Web_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LotController : ControllerBase
    {
        private readonly ILotService _lotService;
        private readonly IConfiguration _configuration;

        public LotController(IConfiguration configuration, ILotService lotService)
        {
            _lotService = lotService;
            _configuration = configuration;
        }

        /// <summary>
        /// Get all the lots with the title photo
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET api/lot/GetAllLotsWithPhoto
        /// 
        /// </remarks>
        /// <returns> A list of existed lot</returns>
        [HttpGet]
        public JsonResult GetAllLotsWithPhoto()
        {

            string query = @"SELECT  lots.id,lots.Title,lots.StartPrice,lots.StartDate,lots.EndDate, ph.PhotoSrc
                             FROM dbo.Lots AS lots
                             JOIN dbo.Photos AS ph 
                             ON ph.id = lots.PhotoId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(table);
        }

        /// <summary>
        /// Get all lots with relevant information
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET api/lot/get
        /// 
        /// </remarks>
        /// <returns> A list of existed lot</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not found
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        [ProducesResponseType(StatusCodes.Status200OK)] // Ok
        public async Task<ActionResult<IEnumerable<LotModel>>> Get()
        {
            IEnumerable<LotModel> lots;
            try
            {
                lots = await _lotService.GetAllAsync();
                if (lots == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok(lots);
        }

        /// <summary>
        /// Get All photos
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET api/lot/GetPhotos
        /// 
        /// </remarks>
        /// <returns> A list of existed photos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not found
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        [ProducesResponseType(StatusCodes.Status200OK)] // Ok
        public async Task<ActionResult<IEnumerable<PhotoModel>>> GetPhotos()
        {
            IEnumerable<PhotoModel> photos;
            try
            {
                photos = await _lotService.GetAllPhotosAsync();
                if (photos == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok(photos);
        }

        /// <summary>
        /// Get a photo according to the set group
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET api/lot/GetPhotoInGroup/id
        /// 
        /// </remarks>
        /// <param name="lotId">Lot id, the photo of which we want to receive</param>
        /// <returns> A list of lot photos</returns>
        [HttpGet("{lotId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not found
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        [ProducesResponseType(StatusCodes.Status200OK)] // Ok
        public async Task<ActionResult<IEnumerable<PhotoModel>>> GetPhotoInGroup(int lotId)
        {
            IEnumerable<PhotoModel> photos;
            try
            {
                photos = await _lotService.GetPhotosGroupByIdAsync(lotId);
                if (photos == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(photos);
        }

    }
}
