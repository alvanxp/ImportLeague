using System;

namespace ImportLeague.Core.Interfaces.Repository
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
