using ecomerce.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ecomerce.Service
{
    public class OrderService
    {
        private ProductService productService = new ProductService();
        private readonly ecomerce.Models.MyStoreContext dbContext;
        public OrderService()
        {
            dbContext = new MyStoreContext();
        }

        internal void addNewCart(int id, int productID, int quantity, string address)
        {
            try
            {
                    Product product = dbContext.Products.FirstOrDefault(p => p.ProductId == productID);
    
    if (product != null)
    {
        Order o = new Order
        {
            CustomerId = id,
            ShipAddress = address,
        };
        o.OrderDetails.Add(new OrderDetail
        {
            ProductId = productID,
            Price = product.Price,
            Quantity = quantity,
            Product = product
        });
        
        dbContext.Orders.Add(o);
        dbContext.SaveChanges();
    }
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void addToCart(int productID, int orderID, int quantity)
        {
            try
            {
                OrderDetail odProduct = dbContext.OrderDetails.FirstOrDefault(od => od.Product.ProductId == productID && od.OrderId == orderID);
                if (odProduct != null)
                {
                    odProduct.Quantity += quantity;
                    dbContext.OrderDetails.Update(odProduct);
                }
                else
                {
                    Product product = dbContext.Products.FirstOrDefault(p => p.ProductId == productID);
                    OrderDetail detail = new OrderDetail
                    {
                        OrderId = orderID,
                        ProductId = product.ProductId,
                        Price = product.Price,
                        Quantity = quantity,
                    };
                    dbContext.OrderDetails.Add(detail);
                }
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal IList<OrderDetail> getCart(int id)
        {
            try
            {
                return dbContext.OrderDetails.Where(o => o.Order.CustomerId == id && o.Order.OrderDate == null).Include(od => od.Product).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //cart
        internal Order getCartOrder(int id)
        {
            try
            {
                return dbContext.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderDate == null && o.CustomerId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // 1 order history detail
        internal Order getOrder(int id)
        {
            try
            {
                return dbContext.Orders.Include(od => od.Customer).FirstOrDefault(o => o.OrderId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //order history
        internal IList<Order> getOrderCustomer(int id)
        {
            try
            {
                return dbContext.Orders.Where(o => o.CustomerId == id && o.OrderDate != null).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        // 1 order history detail with product detail
        internal IList<OrderDetail> getOrderDetails(int id)
        {
            try
            {
                return dbContext.OrderDetails.Where(o => o.OrderId == id).Include(od => od.Product).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // after buy
        internal void startOrder(int cusid, DateTime requireDate)
        {
            try
            {
                Order order = getCartOrder(cusid);
                order.OrderDate = DateTime.Now;
                order.RequiredDate = requireDate;

                var productIds = order.OrderDetails.Select(od => od.ProductId).ToList();
                var products = dbContext.Products.Where(p => productIds.Contains(p.ProductId)).ToList();
                foreach (var od in order.OrderDetails)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == od.ProductId);
                    product.Quantity -= od.Quantity;

                }
                dbContext.Orders.Update(order);
                dbContext.Products.UpdateRange(products);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        internal void removeItemInCart(int cusid, int id)
        {
            try
            {
                Order order = getCartOrder(cusid);
                OrderDetail orderDetail = dbContext.OrderDetails.FirstOrDefault(od => od.ProductId == id && od.OrderId == order.OrderId);
                dbContext.OrderDetails.Remove(orderDetail);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
