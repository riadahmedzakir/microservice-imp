using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Application.UseCase
{
    interface ICommandModel<T> where T : EntityBase
    {
        Task InsertItem(string CollectionName, T Item);
        Task UpdatedItem(string CollectionName, T Item);
        Task DeleteItem(string CollectionName, string ItemId);
    }
}
