using dotNetCoreMVC.Models;
using dotNetCoreMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepo _pieRepo;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(IPieRepo pieRepo, ShoppingCart shoppingCart)
        {
            _pieRepo = pieRepo;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var vm = new ShoppingCartVM
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(vm);
        }
        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var pieToAdd = _pieRepo.GetPieById(pieId);
            if (pieToAdd != null) _shoppingCart.AddToCart(pieToAdd, 1);
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var pieToRemove = _pieRepo.GetPieById(pieId);
            if(pieToRemove != null) _shoppingCart.RemoveFromCart(pieToRemove);
            return RedirectToAction("Index");
        }

    }
}
