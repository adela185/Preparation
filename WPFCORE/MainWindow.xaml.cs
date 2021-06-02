using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCORE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Employee/*string*/> Employees { get; set; }
        public MainWindow()
        {
            //Employees = new List<string>();
            //Employees.Add("Alex");
            //Employees.Add("John");
            //Employees.Add("Mark");
            //Employees.Add("Macy");

            var db = new DatabaseContext();
            //Once Ran Once You Don't need this code again since db is already created
            //db.Database.EnsureCreated();
            //if(db.Employees?.Count() == 0)
            //{
            //    db.Employees.Add(new Employee() { Name = "Mark", Age = 40 });
            //    db.Employees.Add(new Employee() { Name = "Natalie", Age = 39 });
            //    db.Employees.Add(new Employee() { Name = "Mike", Age = 29 });
            //    db.SaveChanges();
            //}

            Employees = db.Employees.ToList();
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
