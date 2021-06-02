using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField
{
    public class LINQPlaying
    {
        public void QueryStringArray()
        {
            string[] dogs = {"K 9", "Brian Griffin",
            "Scooby Doo", "Old Yeller", "Rin Tin Tin",
            "Benji", "Charlie B. Barkin", "Lassie",
            "Snoopy"};

            var dogSpaces = from dog in dogs
                           where dog.Contains(" ")
                           orderby dog ascending
                           select dog;

            foreach (var dog in dogSpaces)
            {
                Console.WriteLine(dog);
            }

            Console.WriteLine();
        }

        public int[] QueryIntArray()
        {
            int[] nums = { 5, 10, 15, 20, 25, 30, 35 };

            var gt20 = from num in nums
                       where num > 20
                       orderby num
                       select num;

            foreach (var num in gt20)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine();

            Console.WriteLine($"Type: {gt20.GetType()}");

            var listGT20 = gt20.ToList<int>();
            var arrayGT20 = gt20.ToArray<int>();

            nums[0] = 40;

            foreach (var num in gt20)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine();

            return arrayGT20;
        }

        public void QueryArrayList()
        {
            ArrayList famAnimals = new ArrayList()
            {
                new Animal("Heidi", 18, .8),

                new Animal
                {
                    Name = "Shrek",
                    Height = 4,
                    Weight = 130
                },

                new Animal
                {
                    Name = "Congo",
                    Height = 3.8,
                    Weight = 90
                }
            };

            var famAnimalsEnum = famAnimals.OfType<Animal>();

            var smFamAnimals = from animal in famAnimalsEnum
                               where animal.Weight <= 90
                               orderby animal.Name
                               select animal;

            foreach (var animal in smFamAnimals)
            {
                Console.WriteLine($"{ animal.Name} weighs { animal.Weight}");
            }
            Console.WriteLine();
            
        }

        public void QueryCollection()
        {
            var animalList = new List<Animal>()
            {
                new Animal{Name = "German Shepard",
                Weight = 77,
                Height = 25},

                new Animal
                {
                    Name = "Chihuahua",
                    Height = 7,
                    Weight = 4.4
                },

                new Animal
                {
                    Name = "Saint Bernard",
                    Height = 30,
                    Weight = 200
                }
            };

            var bigDogs = from dog in animalList
                          where dog.Height >= 25 && dog.Weight >= 70
                          orderby dog.Name
                          select dog;

            foreach (var dog in bigDogs)
            {
                Console.WriteLine(dog);
            }
            Console.WriteLine();
        }

        public void QueryAnimalData()
        {
            Animal[] animals = new[]
            {
                new Animal
                {
                    Name = "German Shepherd",
                    Height = 25,
                    Weight = 77,
                    AnimalID = 1
                },
                new Animal
                {
                    Name = "Chihuahua",
                    Height = 7,
                    Weight = 4.4,
                    AnimalID = 2
                },
                new Animal
                {
                    Name = "Saint Bernard",
                    Height = 30,
                    Weight = 200,
                    AnimalID = 3
                },
                new Animal
                {
                    Name = "Pug",
                    Height = 12,
                    Weight = 16,
                    AnimalID = 1
                },
                new Animal
                {
                    Name = "Beagle",
                    Height = 15,
                    Weight = 23,
                    AnimalID = 2
                }
            };

            Owner[] owners = new[]
            {
                new Owner{Name = "Doug Parks",
                OwnerID = 1},
                new Owner{Name = "Sally Smith",
                OwnerID = 2},
                new Owner{Name = "Paul Brooks",
                OwnerID = 3}
            };

            var nameHeight = from a in animals
                             select new
                             {
                                 a.Name,
                                 a.Height
                             };

            Array arrNameHeight = nameHeight.ToArray();

            foreach (var i in arrNameHeight)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            var innerJoin = from animal in animals
                            join owner in owners on animal.AnimalID equals owner.OwnerID
                            select new
                            {
                                OwnerName = owner.Name,
                                AnimalName = animal.Name
                            };

            foreach (var i in innerJoin)
            {
                Console.WriteLine($"{i.OwnerName} owns a {i.AnimalName}");
            }
            Console.WriteLine();

            var groupJoin = from owner in owners
                            orderby owner.OwnerID
                            join animal in animals on owner.OwnerID equals animal.AnimalID into ownerGroup
                            select new
                            {
                                Owner = owner.Name,
                                Animals = from animal in ownerGroup
                                          orderby animal.Name
                                          select animal
                            };

            foreach (var ownerGroup in groupJoin)
            {
                Console.WriteLine(ownerGroup.Owner);
                foreach (var animal in ownerGroup.Animals)
                {
                    Console.WriteLine($"- {animal.Name}");
                }
            }
        }
    }

    class Animal
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int AnimalID { get; set; }

        public Animal(string name = "No Name", double weight = 0, double height = 0)
        {
            Name = name;
            Weight = weight;
            Height = height;
        }

        public override string ToString()
        {
            return $"{Name} weighs {Weight}lbs, and is {Height} inches tall.";
        }
    }

    class Owner
    {
        public string Name { get; set; }
        public int OwnerID { get; set; }
    }
}
