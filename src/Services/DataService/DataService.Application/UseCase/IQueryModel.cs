using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace DataService.Application.UseCase
{
    public interface IQueryModel<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetCollection(string CollectionName);
    }
}
