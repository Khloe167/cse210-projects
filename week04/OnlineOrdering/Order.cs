using System.Collections.Generic;
using System.Text;

namespace OnlineOrdering
{
    public class Order
    {
        private List<Product> _products = new List<Product>();
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public double CalculateTotalCost()
        {
            double productTotal = 0;
            foreach (Product product in _products)
            {
                productTotal += product.CalculateTotalCost();
            }

            double shippingCost = _customer.IsInUSA() ? 5.00 : 35.00;
            return productTotal + shippingCost;
        }

        public string GetPackingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("PACKING LABEL:");
            foreach (Product product in _products)
            {
                sb.AppendLine($" - Product Name: {product.GetName()} | ID: {product.GetProductId()}");
            }
            return sb.ToString().TrimEnd();
        }

        public string GetShippingLabel()
        {
            return $"SHIPPING LABEL:\n{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
        }
    }
}