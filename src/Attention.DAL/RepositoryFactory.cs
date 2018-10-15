using Attention.DAL.Repositories;
using System;

namespace Attention.DAL
{
    public class RepositoryFactory
    {
        private readonly IServiceProvider _provider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        public BingRepository GetBingRepository()
        {
            return _provider.GetService(Type.GetType(nameof(BingRepository))) as BingRepository;
        }
    }
}
