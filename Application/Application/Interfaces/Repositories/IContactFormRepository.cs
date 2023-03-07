using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IContactFormRepository : IBaseRepository<ContactForm>
    {
        //tutaj jakieś metody specjalne dla Account ,które wykonują operacje CRUD
    }
}