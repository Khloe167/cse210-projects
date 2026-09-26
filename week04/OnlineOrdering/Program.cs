using System;
using System.Collections.Generic;

namespace OnlineOrdering
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>();

            // ----------------------------------------------------
            // Order 1: Domestic USA Customer
            // ----------------------------------------------------
            Address address1 = new Address("123 Main St", "Springfield", "OR", "USA");
            Customer customer1 = new Customer("John Doe", address1);
            Order order1 = new Order(customer1);

            order1.AddProduct(new Product("Wireless Mouse", "WM-101", 24.99, 2));
            order1.AddProduct(new Product("Mechanical Keyboard", "MK-202", 79.99, 1));
            order1.AddProduct(new Product("Mouse Pad", "MP-303", 12.50, 1));
            orders.Add(order1);

            // ----------------------------------------------------
            // Order 2: International Customer (Nigeria)
            // ----------------------------------------------------
            Address address2 = new Address("10B Mussco Street, Magodo", "Lagos", "Lagos State", "Nigeria");
            Customer customer2 = new Customer("Chloe Agbah", address2);
            Order order2 = new Order(customer2);

            order2.AddProduct(new Product("HD Monitor 27\"", "MON-404", 199.99, 1));
            order2.AddProduct(new Product("USB-C Hub", "HUB-505", 34.99, 2));
            orders.Add(order2);

            // ----------------------------------------------------
            // Output Order Details
            // ----------------------------------------------------
            int orderCount = 1;
            foreach (Order order in orders)
            {
                Console.WriteLine($"==================================================");
                Console.WriteLine($"ORDER #{orderCount++}");
                Console.WriteLine($"==================================================");
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Total Price: ${order.CalculateTotalCost():F2}");
                Console.WriteLine();
            }
        }
    }
}