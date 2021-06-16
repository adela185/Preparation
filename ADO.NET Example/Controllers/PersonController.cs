using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ADO.NET_Example
{
    public class DictionaryPersonRepository
    {
        int nextID = 0;
        Dictionary<int, Person> persons = new Dictionary<int, Person>();

        public IEnumerable<Person> Get()
        {
            return persons.Values.OrderBy(person => person.PersonID);
        }

        public bool TryGet(int id, out Person person)
        {
            return persons.TryGetValue(id, out person);
        }

        public Person Add(Person person)
        {
            person.PersonID = nextID++;
            persons[person.PersonID] = person;
            return person;
        }

        public bool Delete(int id)
        {
            return persons.Remove(id);
        }

        public bool Update(Person person)
        {
            bool update = persons.ContainsKey(person.PersonID);
            persons[person.PersonID] = person;
            return update;
        }
    }

    public class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonController : ApiController
    {
        List<Person> db = new List<Person> {
                new Person {PersonID=0, FirstName="Joe", LastName="Mama"},
                new Person {PersonID=1, FirstName="Doe", LastName="Slough"},
                new Person {PersonID=2, FirstName="Foe", LastName="Sho"}
            };

        DictionaryPersonRepository repository;

        public PersonController()
        {
            repository = new DictionaryPersonRepository();
            repository.Add(new Person { FirstName = "Joe", LastName = "Mama" });
        }
        public PersonController(DictionaryPersonRepository repository)
        {
            this.repository = repository;
        }

        // GET api/<controller>
        public IEnumerable<Person> Get()
        {
            return repository.Get();
        }

        // GET api/<controller>/5
        public Person Get(int id)
        {
            //var p = from person in db
            //        where person.PersonID == id
            //        select person;

            //return p.Single();

            Person person;
            if (!repository.TryGet(id, out person))
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return person;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Person value)
        {
            Person person = repository.Add(value);
            return this.Request.CreateResponse(HttpStatusCode.Created, person);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] Person value)
        {
            bool status = repository.Update(value);
            if (!status)
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            if (!repository.Delete(id))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}