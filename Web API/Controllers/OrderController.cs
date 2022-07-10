﻿using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Web_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IOrderService _orderService;
        public OrderController(IConfiguration configuration, IOrderService orderService)
        {
            _orderService = orderService;
            _configuration = configuration;
        }

        /// <summary>
        /// Get All orders
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET api/order/get
        /// 
        /// </remarks>
        /// <returns> A list of existed orders</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not found
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        [ProducesResponseType(StatusCodes.Status200OK)] // Ok
        public async Task<ActionResult<IEnumerable<OrderModel>>> Get()
        {
            IEnumerable<OrderModel> orders;
            try
            {
                orders = await _orderService.GetAllAsync();
                if (orders == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(orders);
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET api/order/GetById/id
        /// 
        /// </remarks>
        /// <returns> Order with the desired id </returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not found
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        [ProducesResponseType(StatusCodes.Status200OK)] // Ok
        public async Task<ActionResult<OrderModel>> GetById(int id)
        {
            OrderModel model;
            try
            {
                model = await _orderService.GetByIdAsync(id);

                if (model == null)
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(model);
        }



    }
}
