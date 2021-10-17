using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using AthensLibrary.Service.Interface;

namespace AthensLibrary.Service.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IRepository<Author> _authorRepository;
        private readonly IServiceFactory _serviceFactory;

        public AuthorService(IUnitofWork unitofWork, IServiceFactory serviceFactory)
        {
            _unitofWork = unitofWork;
            _authorRepository = unitofWork.GetRepository<Author>();
            _serviceFactory = serviceFactory;
        }

        public Task<Author> Login()
        {
            throw new NotImplementedException();
        }

        public Task<Author> UpdateAuthor()
        {
            throw new NotImplementedException();
        }
    }
}
