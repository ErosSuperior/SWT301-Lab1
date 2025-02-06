using ecomerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecomerce.Service
{
    public class SupplierService
    {
        private readonly ecomerce.Models.MyStoreContext dbContext;
        public SupplierService()
        {
            dbContext = new MyStoreContext();
        }

        public SelectList getAllSuppliers()
        {
            return new SelectList(dbContext.Suppliers, "SupplierId", "CompanyName");
        }

        public IList<Supplier> getAllSupplier()
        {
            try
            {
                return dbContext.Suppliers.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal bool insertSupplier(Supplier supplier)
        {
            try
            {
                if (dbContext.Suppliers.Any(cate => cate.CompanyName.Equals(supplier.CompanyName)))
                {
                    return false;
                }
                dbContext.Suppliers.Add(supplier);
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        internal Supplier getSupplier(int? id)
        {
            try
            {
                return dbContext.Suppliers.FirstOrDefault(c => c.SupplierId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        internal bool updateSupplier(Supplier supplier)
        {
            try
            {
                if (dbContext.Suppliers.Any(cate =>cate.SupplierId != supplier.SupplierId && cate.CompanyName.Equals(supplier.CompanyName)))
                {
                    return false;
                }
                dbContext.Attach(supplier).State = EntityState.Modified;
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
