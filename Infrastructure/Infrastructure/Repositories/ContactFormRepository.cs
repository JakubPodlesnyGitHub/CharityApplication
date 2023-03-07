using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class ContactFormRepository : BaseRepository<ContactForm>, IContactFormRepository
    {
        public ContactFormRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
        }
    }
}