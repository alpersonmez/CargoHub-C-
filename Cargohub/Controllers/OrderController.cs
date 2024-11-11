using System.Text;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Services;

namespace Cargohub.Controllers;

[Route("Orders")]
public class OrderController : Controller
{
    private readonly IOrderService orderService;

    public OrderController(IOrderService _orderService)
    {
        orderService = _orderService;
    }


}