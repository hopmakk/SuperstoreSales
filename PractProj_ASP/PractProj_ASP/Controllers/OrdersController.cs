using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PractProj_ASP.Models.Entities;

namespace PractProj_ASP.Controllers
{
    //[ApiController]
    [Route("/api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly ApplicationContext _context;

        public OrdersController(ApplicationContext context)
        {
            _context = context;
        }

        public class OrderSummary
        {
            public string Date { get; set; }
            public double Values { get; set; }
        }


        [HttpGet("orderQuantity")] // Путь: /api/orders/orderQuantity
        public IActionResult GetOrderQuantity()
        {
            var orderSummaries = _context.Orders
                .GroupBy(o => o.OrderDate.ToString().Substring(0, 7))
                .OrderBy(g => g.Key)
                .Select(g => new OrderSummary
                {
                    Date = g.Key,
                    Values = g.Average(o => o.OrderQuantity)
                })
                .ToList();

            return Ok(orderSummaries);
        }

        [HttpGet("Sales")]
        [HttpGet]
        public IActionResult GetSales()
        {
            var orderSummaries = _context.Orders
                .GroupBy(o => o.OrderDate.ToString().Substring(0, 7))
                .OrderBy(g => g.Key)
                .Select(g => new OrderSummary
                {
                    Date = g.Key,
                    Values = g.Average(o => o.Sales)
                })
                .ToList();

            return Ok(orderSummaries);
        }


        [HttpGet("Profit")]
        public IActionResult GetProfit()
        {
            var orderSummaries = _context.Orders
                .GroupBy(o => o.OrderDate.ToString().Substring(0, 7))
                .OrderBy(g => g.Key)
                .Select(g => new OrderSummary
                {
                    Date = g.Key,
                    Values = g.Average(o => o.Profit)
                })
                .ToList();

            return Ok(orderSummaries);
        }


        [HttpGet("LastMonth")]
        public string GetLastMonth()
        {
            DateTime? lastOrderDate = _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Select(o => o.OrderDate)
                .FirstOrDefault();

            DateTime Dt = Convert.ToDateTime(lastOrderDate);
            string formattedDate = Dt.Year.ToString() + "-" + Dt.Month.ToString("D2");
            return formattedDate;
        }


        [HttpGet("MonthStat")]
        public IActionResult GetMonthStat(string month)
        {
            // переводим month в DateTime
            DateTime selectedMonth;
            if (!DateTime.TryParseExact(month, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedMonth))
            {
                return BadRequest("Invalid month format");
            }

            // Вычисляем начало и конец месяца
            DateTime startDate = new DateTime(selectedMonth.Year, selectedMonth.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Запрос
            var monthlyData = new
            {
                TotalOrders = _context.Orders.Count(o => o.OrderDate >= startDate && o.OrderDate <= endDate),
                TotalProfit = Math.Round(_context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).Sum(o => o.Profit),4),
                TotalSales = Math.Round(_context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).Sum(o => o.Sales), 4)
            };

            return Ok(monthlyData);
        }


        [HttpGet("CustomerSegmentCount")]
        public IActionResult GetCustomerSegmentCount()
        {
            var customerSegmentCounts = _context.Orders
                .GroupBy(o => o.CustomerSegment)
                .Select(g => new { CustomerSegment = g.Key, Count = g.Count() })
                .ToList();

            return Ok(customerSegmentCounts);
        }


        [HttpGet("CustomerSegmentCount/{year}")]
        public IActionResult GetCustomerSegmentCount(int year)
        {
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime endDate = startDate.AddYears(1).AddDays(-1);

            var customerSegmentCounts = _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .GroupBy(o => o.CustomerSegment)
                .Select(g => new { CustomerSegment = g.Key, Count = g.Count() })
                .ToList();

            return Ok(customerSegmentCounts);
        }


        [HttpGet("DistinctYears")]
        public List<string> GetDistinctYears()
        {
            var distinctYears = _context.Orders
                .Select(o => Convert.ToDateTime(o.OrderDate).Year.ToString())
                .Distinct()
                .ToList();

            List<string> save = new List<string>();

            foreach (var item in distinctYears)
            {
                if (save.Contains(item))
                    continue;
                save.Add(item);
            }

            return save;
        }
    }
}
