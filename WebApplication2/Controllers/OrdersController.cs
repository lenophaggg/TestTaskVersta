using System.Diagnostics;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Services;
using WebApplication2.ViewModels;
using System.Globalization;

namespace WebApplication2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly AppDbContext _context;
        private readonly IOrderNumberGenerator _orderNumberGenerator;

        public OrdersController(ILogger<OrdersController> logger, AppDbContext context, IOrderNumberGenerator orderNumberGenerator)
        {
            _logger = logger;
            _context = context;
            _orderNumberGenerator = orderNumberGenerator;
        }

        public async Task<IActionResult> List(int page = 1, string? searchQuery = null)
        {
            const int PageSize = 1000;

            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower(); 
                query = query.Where(o =>
                    o.Ordernumber.ToLower().Contains(searchQuery));
            }

            var totalOrders = await query.CountAsync();

            var totalPages = (int)Math.Ceiling(totalOrders / (double)PageSize);

            if (page < 1 || (page > totalPages && totalPages > 0))
            {
                return RedirectToAction("List", new { page = 1, searchQuery });
            }

            var orders = await query
                .OrderByDescending(o => o.Pickupdate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)                
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery; 

            return View(orders);
        }

        public IActionResult Create()
        {
            return View(new OrderViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var order = new Order
            {
                Ordernumber = _orderNumberGenerator.GenerateOrderNumber(),
                Sendercity = viewModel.Sendercity,
                Senderaddress = viewModel.Senderaddress,
                Recipientcity = viewModel.Recipientcity,
                Recipientaddress = viewModel.Recipientaddress,
                Cargoweight = Math.Round(viewModel.Cargoweight, 2),
                Pickupdate = viewModel.Pickupdate,
                Createdat = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = order.Ordernumber });
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Ordernumber == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
