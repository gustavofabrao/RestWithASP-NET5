using RestWithASPNETUdemy.Data.VO; 

namespace RestWithASPNETUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO PersonVO);

        PersonVO Update(PersonVO PersonVO);

        PersonVO FindByID(long id);

        void Delete(long id);

        List<PersonVO> FindAll(); 
    }
}
