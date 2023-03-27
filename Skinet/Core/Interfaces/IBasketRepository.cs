using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        void AddItem(Basket basket, Product product, int quantity);
        void RemoveItem(Basket basket, int productId, int quantity);
        Task<int> SaveAsync();
        Task<Basket> RetrieveBasket();
    }
}
