using System;
using System.Collections.Generic;

namespace PlayField
{
    class EventsExMain
    {
        public event EventHandler<CustomerEventArgument> CustomerAdded;

        public static void Main(string[] args)
        {
            GenericsEx<int> genricIntObj = new GenericsEx<int>();
            genricIntObj.Print(10);
            GenericsEx<string> gernicStringObj = new GenericsEx<string>();
            gernicStringObj.Print("I'm not getting these events.");

            EventsExMain obj = new EventsExMain();
            obj.CustomerAdded += Obj_CustomerAdded;
            Customer newCustomer = new Customer { CustID = 12, CustName = "John" };
            obj.AddCustomer(newCustomer);


            Console.ReadLine();
        }

        public static void Obj_CustomerAdded(object sender, CustomerEventArgument e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Customer {e.CustomerName} added at store {e.Store}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void AddCustomer(Customer cust)
        {
            Console.WriteLine("Adding a customer.");
            System.Threading.Thread.Sleep(2000);

            this.CustomerAdded(this, new CustomerEventArgument
            {
                CustomerName = cust.CustName,
                Store = "ABC Store"
            });
        }
    }

    public class CustomerEventArgument
    {
        public string CustomerName { get; set; }
        public string Store { get; set; }
    }

    public class Customer
    {
        public int CustID { get; set; }
        public string CustName { get; set; }
    }

    public class GenericsEx<T>
    {
        public void Print(T param)
        {
            Console.WriteLine($"Parameter: {param}");
        }
    }
}
