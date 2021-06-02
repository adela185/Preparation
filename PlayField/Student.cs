using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField
{
    //This example plays with some inheritance as well
    public class Student
    {
        public Student()
        {
            Console.WriteLine("Constructor for master student called.");
            Clubs = new List<string>(1);
        }

        public Student(int age)
        {
            Console.WriteLine("Parameterized ctor called");
            this.age = age;
            Clubs = new List<string>(1);
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }


        private List<string> Clubs { get; set; }
        public int GPA { get; set; }


        public virtual void AssignClubs(string club)
        {
            Clubs.Add(club);
            Console.WriteLine("Club Added this Student");
        }

        public void viewClubs()
        {
            if (Clubs.Count >= 1)
                Console.WriteLine("Club: " + string.Join(", ", Clubs));
            else
                Console.WriteLine("No Clubs");
        }

        public static int totalMarks(int[] list)
        {
            int sum = 0;

            for(int i = 0; i < list.Length; i++)
            {
                sum += list[i];
            }

            return sum;
        }

        public static string allSubs(params string[] list)
        {
            StringBuilder br = new StringBuilder();
            
            foreach (var i in list)
            {
                br.Append(string.Format($"{i} "));
            }

            string subjects = br.ToString();
            return subjects;
        }

        public delegate string d(params string[] s);
        public delegate int t(params int[] i);
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Subjects: {allSubs("Math", "History", "English")} \nMarks: {totalMarks(new int[]{5, 2, 80})}");

            //Console.ReadLine();

            int[] arr = { 1, 2, 3 };
            Console.WriteLine(string.Join(",", arr));

            Student student1 = new Student();
            Console.WriteLine();
            Student student2 = new Student(19);
            Console.WriteLine();
            SpecialStudent student1S = new SpecialStudent();
            Console.WriteLine();
            Student student2S = new SpecialStudent(9);
            Console.WriteLine();

            student1.GPA = 2;
            student1.AssignClubs("Football");
            student1.viewClubs();
            student2.AssignClubs("Gardening");
            student2.AssignClubs("Coding");
            student2.viewClubs();

            Console.WriteLine();

            student1S.GPA = 4;
            student1S.AssignClubs("Underwater Basket Weaving");
            student1S.viewClubs();
            student2S.AssignClubs("Cultured Gentlemen's Club");


            Console.ForegroundColor = ConsoleColor.Cyan;
            d d1 = new d(allSubs);

            t t1 = delegate (int[] list)
            {
                int sum = 0;

                for (int i = 0; i < list.Length; i++)
                {
                    sum += list[i];
                }

                return sum;
            };

            Console.WriteLine(d1("Cooco"));
            Console.WriteLine(t1(1, 6, 3, 7));
            Console.WriteLine(message(d1));
            Console.WriteLine(messagePt2( delegate (string[] list) {
                StringBuilder br = new StringBuilder();

                foreach (var i in list)
                {
                    br.Append(string.Format($"{i} "));
                }

                string subjects = br.ToString();
                return subjects;
            }));
            Console.WriteLine("Lamba Ex: " + message((param) => { 
                Console.ForegroundColor = ConsoleColor.Blue;
                StringBuilder br = new StringBuilder();

                foreach (var i in param)
                {
                    br.Append(string.Format($"{i} "));
                }

                string subjects = br.ToString();
                return subjects; 
                 
            }));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.ReadLine();

            //Anonymous use of ctor; type is inferred
            //var stuAnonymous = new {GPA = 2, Clubs = new List<string>(), randomParam = "oogey boogey"};

        }

        public static string message(d mw)
        {
            Console.Write("Test: ");
            return mw("Mike", "Mark"); 
        }

        public static string messagePt2(d sd)
        {
            return sd("Sully");
        }
    }

    

    public class SpecialStudent : Student
    {
        public SpecialStudent()
        {
            Console.WriteLine("Child constructor called");
        }

        public SpecialStudent(int age):base(age)
        {
            Console.WriteLine("Parameterized Child ctor called");
        }

        public override void AssignClubs(string club)
        {
            Console.WriteLine("Special Students Don't Have Time For Clubs");
        }

        //If I wanted to bring in parent method body
        //public override void AssignClubs(string club)
        //{
        //    base.AssignClubs(club);
        //}
    }
}
