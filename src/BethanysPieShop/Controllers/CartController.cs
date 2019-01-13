using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    public class CartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCart _shoppingCart;

        public CartController(IPieRepository pieRepository, ShoppingCart shoppingCart)
        {
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
        }

        // GET: api/Cart
        [HttpGet]
        public IActionResult Get()
        {
            // return Json(_shoppingCart.GetShoppingCartItems());
            return Ok(new{pies = _shoppingCart.GetShoppingCartItems(), count = _shoppingCart.GetShoppingCartCount(), countT = _shoppingCart.GetShoppingCartCountTotal(), total = Decimal.Round(_shoppingCart.GetShoppingCartTotal(), 1) });

        }
        

        // GET: api/Cart/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
        //    return RedirectToAction("ShoppingCart", _userManager.Users)
            // return RedirectToAction("Index", "ShoppingCart");
            return Json(new {result = "Redirect", url = Url.Action("Index", "ShoppingCart")});
            // return Redirect("http://localhost:5000/ShoppingCart");
        }

        // POST: api/Cart/5
        // [HttpPost("{id}")]
        // public IActionResult Post(int id)
        // {
        //     var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == id);

        //     if (selectedPie != null)
        //     {
        //         _shoppingCart.AddToCart(selectedPie, 1);
        //     }

        //     return Ok(new { });
        //     // return PartialView("_ShoppingCart");
        // }

        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] int amount)
        {
            var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == id);

            string product = new StreamReader(Request.Body).ReadToEnd();

            if (selectedPie != null)
            {
                _shoppingCart.AddToCart(selectedPie, amount);
                return Ok(new { });
            }
            return NotFound(id);
            // return PartialView("_ShoppingCart");
        }
        // PUT: api/Cart/5
        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == id);

            if (selectedPie != null)
            {
                _shoppingCart.RemoveFromCart(selectedPie);
                return Ok(new {});
            }
            return NotFound(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == id);

            if (selectedPie != null)
            {
                _shoppingCart.RemoveAllFromCart(selectedPie);
                return Ok(new {});
            }
            return NotFound(id);
        }
    }
}
