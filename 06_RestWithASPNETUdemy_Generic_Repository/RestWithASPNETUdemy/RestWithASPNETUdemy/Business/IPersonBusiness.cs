using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);

        Person Update(Person person);

        Person FindByID(long id);

        void Delete(long id);

        List<Person> FindAll(); 
    }
}
