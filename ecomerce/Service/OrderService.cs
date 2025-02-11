using ecomerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ecomerce.Service
{
    public class OrderService
    {
        private readonly ProductService productService = new ProductService();
        private readonly MyStoreContext dbContext;

        public OrderService()
        {
            dbContext = new MyStoreContext();
        }

        internal void addNewCart(int id, int productID, int quantity, string address)
        {
            try
            {
                Product product = dbContext.Products.FirstOrDefault(p => p.ProductId == productID);

                if (product == null)
                {
                    throw new Exception("Product not found.");
                }

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
            catch (Exception ex)
            {
                throw new Exception("Error adding new cart: " + ex.Message);
            }
        }

        internal void addToCart(int productID, int orderID, int quantity)
        {
            try
            {
                OrderDetail odProduct = dbContext.OrderDetails
                    .FirstOrDefault(od => od.Product.ProductId == productID && od.OrderId == orderID);

                if (odProduct != null)
                {
                    odProduct.Quantity += quantity;
                    dbContext.OrderDetails.Update(odProduct);
                }
                else
                {
                    Product product = dbContext.Products.FirstOrDefault(p => p.ProductId == productID);

                    if (product == null)
                    {
                        throw new Exception("Product not found.");
                    }

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
            catch (Exception ex)
            {
                throw new Exception("Error adding to cart: " + ex.Message);
            }
        }

        internal IList<OrderDetail> getCart(int id)
        {
            try
            {
                return dbContext.OrderDetails
                    .Where(o => o.Order.CustomerId == id && o.Order.OrderDate == null)
                    .Include(od => od.Product)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving cart: " + ex.Message);
            }
        }

        internal Order getCartOrder(int id)
        {
            try
            {
                return dbContext.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefault(o => o.OrderDate == null && o.CustomerId == id)
                    ?? throw new Exception("Cart order not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving cart order: " + ex.Message);
            }
        }

        internal Order getOrder(int id)
        {
            try
            {
                return dbContext.Orders
                    .Include(od => od.Customer)
                    .FirstOrDefault(o => o.OrderId == id)
                    ?? throw new Exception("Order not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving order: " + ex.Message);
            }
        }

        internal IList<Order> getOrderCustomer(int id)
        {
            try
            {
                return dbContext.Orders
                    .Where(o => o.CustomerId == id && o.OrderDate != null)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customer orders: " + ex.Message);
            }
        }

        internal IList<OrderDetail> getOrderDetails(int id)
        {
            try
            {
                return dbContext.OrderDetails
                    .Where(o => o.OrderId == id)
                    .Include(od => od.Product)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving order details: " + ex.Message);
            }
        }

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
                    if (product == null)
                    {
                        throw new Exception($"Product with ID {od.ProductId} not found.");
                    }
                    product.Quantity -= od.Quantity;
                }

                dbContext.Orders.Update(order);
                dbContext.Products.UpdateRange(products);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error starting order: " + ex.Message);
            }
        }

        internal void removeItemInCart(int cusid, int id)
        {
            try
            {
                Order order = getCartOrder(cusid);
                OrderDetail orderDetail = dbContext.OrderDetails
                    .FirstOrDefault(od => od.ProductId == id && od.OrderId == order.OrderId);

                if (orderDetail != null)
                {
                    dbContext.OrderDetails.Remove(orderDetail);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception($"Order detail not found for product ID {id}.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing item from cart: " + ex.Message);
            }
        }
    }
}
