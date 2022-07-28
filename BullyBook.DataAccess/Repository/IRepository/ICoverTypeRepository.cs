using BullyBook.Models;

namespace BullyBook.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType obj);
    }
}
