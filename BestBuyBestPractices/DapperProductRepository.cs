using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
             _connection.Execute("INSERT INTO PRODUCTS (NAME, PRICE, CATEGORYID) VALUES(@name, @price, @categoryID);",
            new {name = name, price = price, categoryID = categoryID });
        }

        public IEnumerable<Product> GetAllProducts()
        {
           return _connection.Query<Product>("SELECT * FROM products;");
            
        }
        public void UpdateProductName(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET name = @updatedName WHERE ProductID = @productID;",
                new {productID = productID, updatedName = updatedName});
        }
        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE productID = @productID;",
                new {productID = productID});
            _connection.Execute("DELETE FROM sales WHERE productID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM products WHERE productID = @productID;",
                new { productID = productID });
        }
    }
    
   
     
    
}
