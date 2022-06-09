using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {   

        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            int cont = 0;
            while(cont < 8)
            {
                Person person = MockPerson(cont);
                persons.Add(person);
                cont = cont + 1;
            }

            return persons;
        } 

        public Person FindByID(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Gustavo",
                LastName = "Abrão",
                Address = "Ribeirão Preto",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int cont)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "FirstName " + cont,
                LastName = "LastName" + cont,
                Address = "Some Address" + cont,
                Gender = "Gender" + cont
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
