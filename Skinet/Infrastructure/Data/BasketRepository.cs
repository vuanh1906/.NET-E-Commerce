using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreContext _context;

        public BasketRepository(StoreContext context)
        {
            _context = context;
        }
        public void AddItem(Basket basket, Product product, int quantity)
        {
            basket.AddItem(product, quantity);
        }

        public void RemoveItem(Basket basket, int productId, int quantity)
        {
            basket.RemoveItem(productId, quantity);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<Basket> RetrieveBasket()
        {
            throw new NotImplementedException();
        }
    }
}
