using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Application.Interface
{
    public interface ICommandModel<T>
    {
        Task InsertItem(string CollectionName, T Item);
        Task UpdatedItem(string CollectionName, T Item);
        Task DeleteItem(string CollectionName, string ItemId);
    }
}
