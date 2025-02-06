using ecomerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecomerce.Service
{
    public class CategoryService
    {
        private readonly ecomerce.Models.MyStoreContext dbContext;
        public CategoryService()
        {
            dbContext = new MyStoreContext();
        }

        public SelectList getAllCategory()
        {
            return new SelectList(dbContext.Categories, "CategoryId", "CategoryName");
        }

        public IList<Category> getAllCategories()
        {
            try
            {
                return dbContext.Categories.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal bool InsertCategory(Category category)
        {
            try
            {
                if (dbContext.Categories.Any(cate => cate.CategoryName.Equals(category.CategoryName)))
                {
                    return false;
                }
                dbContext.Categories.Add(category);
                dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal Category getCategory(int? id)
        {
            try
            {
                return dbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }


        internal bool updateCategory(Category category)
        {
            try
            {
                if (dbContext.Categories.Any(cate => cate.CategoryId != category.CategoryId && cate.CategoryName.Equals(category.CategoryName)))
                {
                    return false;
                }
                dbContext.Attach(category).State = EntityState.Modified;
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
