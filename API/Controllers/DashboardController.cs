﻿using Application.Repository;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        [AllowAnonymous]
        [HttpGet("TopSellingProducts")] 
        public async Task<IActionResult> GetTopSellingProducts()
        {
            var products = await _dashboardRepository.GetTopSellingProductsAsync();
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("LowStockItems")]
        public async Task<IActionResult> GetLowStockItems()
        {
            var products = await _dashboardRepository.GetLowStockItemsAsync();
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("RevenueProfitCost")]
        public async Task<IActionResult> GetRevenueProfitCost()
        {
            var data = await _dashboardRepository.GetRevenueProfitCost();
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpGet("DailyRevenueProfitCostForWeeks")]
        public async Task<IActionResult> GetDailyRevenueProfitCostForWeeks()
        {
            var data = await _dashboardRepository.GetDailyRevenueProfitCostForWeeks();
            return Ok(data);
        }
    }
}
