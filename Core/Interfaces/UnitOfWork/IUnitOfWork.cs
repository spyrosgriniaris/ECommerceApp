using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // IDisposable disposes the context when the transaction is over
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
         
    }
}