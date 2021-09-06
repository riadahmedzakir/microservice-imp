using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace DataService.Application.Interface
{
    public interface IQueryModel<T>
    {
        Task<IReadOnlyList<T>> GetCollection(string CollectionName, string queryString, string fields, int pageNumber, int pageSize);
    }
}
