using InventoryManagement.Web.Contracts;
using InventoryManagement.Web.Data;

namespace InventoryManagement.Web.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
