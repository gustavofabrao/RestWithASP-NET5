using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);

        Person Update(Person person);

        Person FindByID(long id);

        void Delete(long id);

        List<Person> FindAll();

        bool Exists(long id);

    }
}
