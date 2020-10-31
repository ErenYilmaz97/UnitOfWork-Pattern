using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Results;
using Entities.Entities;

namespace Core.ApiServices
{
    public interface IProductApiService
    {
        Task<IDataResult<List<Product>>> GetAll();
    }
}