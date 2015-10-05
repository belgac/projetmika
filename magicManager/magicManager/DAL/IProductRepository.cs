using magicManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magicManager.DAL
{
    interface IProductRepository : IDisposable
    {
        Product GetProduct();
        IEnumerable<Product> GetProductByExpansion(int idGame, string expansionName);
        Product GetProductById(int idProduct);
        void InsertProduct(Product product);
        void DeleteProduct(int idProduct);
        void UpdateProduct(Product product);
        void Save();
    }
}
