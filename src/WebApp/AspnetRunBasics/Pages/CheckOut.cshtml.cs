using System;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
  public class CheckOutModel : PageModel
  {
    private readonly IBasketApi _basketApi;

    public CheckOutModel(IBasketApi basketApi)
    {
      _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
    }

    [BindProperty]
    public BasketCheckoutModel Order { get; set; }

    public BasketModel Cart { get; set; } = new BasketModel();

    public async Task<IActionResult> OnGetAsync()
    {
      var userName = "shockz";
      Cart = await _basketApi.GetBasket(userName);

      return Page();
    }

    public async Task<IActionResult> OnPostCheckOutAsync()
    {
      var userName = "shockz";
      Cart = await _basketApi.GetBasket(userName);

      if (!ModelState.IsValid)
      {
        return Page();
      }

      Order.UserName = userName;
      Order.TotalPrice = Cart.TotalPrice;

      await _basketApi.CheckoutBasket(Order);

      return RedirectToPage("Confirmation", "OrderSubmitted");
    }
  }
}