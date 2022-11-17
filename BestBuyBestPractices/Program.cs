using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace BestBuyBestPractices
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection connection = new MySqlConnection(connString);

            var repo = new DapperProductRepository(connection);

            var products = repo.GetAllProducts();

            foreach(var product in products)
            {
                Console.WriteLine($"{product.Name} {product.Price} {product.CategoryID}{product.OnSale}");
                Console.WriteLine();
            }

            var departmentRepo = new DapperDepartmentRepository(connection);

            var departments = departmentRepo.GetAllDepartments();

            foreach (var department in departments)
            {
                Console.WriteLine(department.DepartmentID);
                Console.WriteLine(department.Name);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}