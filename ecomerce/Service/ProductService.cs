using ecomerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ecomerce.Service
{
    public class ProductService
    {
        private readonly ecomerce.Models.MyStoreContext dbContext;
        public ProductService()
        {
            dbContext = new MyStoreContext();
        }

        internal async Task<IList<Product>> getAllProductsAsync()
        {
            try
            {
                return await dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        internal Product? getProductID(int? id)
        {
            try
            {
                return dbContext.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefault(m => m.ProductId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal async Task<IList<Product>> getProductsAsync(string? strSearch, double? firstInput, double? secondInput)
        {
            try
            {
                var list = dbContext.Products.Where(p => p.Display == true && p.Quantity > 0).Include(p => p.Category).Include(p => p.Supplier).ToList();
                if (!string.IsNullOrEmpty(strSearch)) list = list.Where(p => p.ProductName.ToUpper().Contains(strSearch.ToUpper())).ToList();
                if (firstInput != null) list = list.Where(p => p.Price >= firstInput && p.Price <= secondInput).ToList();

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal async Task insertProductAsync(Product product)
        {
            try
            {
                dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal async Task updateProductAsync(Product product)
        {
            dbContext.Attach(product).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
