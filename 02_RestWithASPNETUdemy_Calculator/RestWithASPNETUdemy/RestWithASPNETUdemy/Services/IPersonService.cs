using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Services
{
    public interface IPersonService
    {
        Person Create(Person person);

        Person Update(Person person);

        Person FindByID(long id);

        void Delete(long id);

        List<Person> FindAll();

    }
}
