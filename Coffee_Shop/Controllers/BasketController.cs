using Coffee_Shop.Data;
using Coffee_Shop.Models;
using Coffee_Shop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coffee_Shop.Controllers;

public class BasketController : Controller
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;



    public BasketController(IRepository<Order> orderRepository, IRepository<Product> productRepository, ApplicationDbContext context)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
       
        
    }

    // GET Admin
    public  IActionResult GetAllOrders()
    {
        var orders = Models.User.Basket.ToList();
        return View(orders);
    }


    [HttpGet]
    public async Task<IActionResult> AddToBasket(int id)
    {
        var order = new Order();
        var product = await _productRepository.GetById(id);
        order.Product = product;
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket(int id, string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || !phone.StartsWith("+380"))
        {
            // Тут можна обробити помилку або повідомити користувача про некоректне значення phone
            return BadRequest("Некоректний номер телефону.");
        }
        var product = await _productRepository.GetById(id);

        var orderAdd = new Order()
        {
            Name = User.Identity.Name,
            Phone = phone,
            Product = product
        };
        _orderRepository.Add(orderAdd);
        Models.User.Basket.Add(orderAdd);

        return RedirectToAction("Index", "Product");

    }


    public IActionResult GetOrdersByUser()
    {
        var orders = Models.User.Basket.Where(x => x.Name == User.Identity.Name).ToList();
        return View(orders);
    }


    

    [HttpPost]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var orderRemove = await _orderRepository.GetById(id);

        _orderRepository.Delete(orderRemove);

        Models.User.Basket = Models.User.Basket.Where(x => x.Id != id).ToList();
        return RedirectToAction("GetOrdersByUser", "Basket");
    }


    [HttpPost]
    public async Task<IActionResult> AcceptOrder(int id)
    {
        var orderRemove = await _orderRepository.GetById(id);
        _orderRepository.Delete(orderRemove);
        Models.User.Basket = Models.User.Basket.Where(x => x.Id != id).ToList();
        return RedirectToAction("GetAllOrders", "Basket");
    }
}