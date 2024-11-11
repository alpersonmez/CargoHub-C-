using System.Text;
using Microsoft.AspNetCore.Mvc;
using Cargohub.Services;

namespace Cargohub.Controllers;

[Route("Warehouses")]
public class WarehouseController : Controller
{
    private readonly IWarehouseService warehouseService;

    public WarehouseController(IWarehouseService _warehouseService)
    {
        warehouseService = _warehouseService;
    }


}