using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;

namespace Repository.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByName(string categoryName);
    }
}
