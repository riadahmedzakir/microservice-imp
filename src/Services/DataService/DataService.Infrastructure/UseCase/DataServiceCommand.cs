using DataService.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Application.UseCase
{
    class DataServiceCommand<T> : ICommandModel<T>
    {
        public Task DeleteItem(string CollectionName, string ItemId)
        {
            throw new NotImplementedException();
        }

        public Task InsertItem(string CollectionName, T Item)
        {
            throw new NotImplementedException();
        }

        public Task UpdatedItem(string CollectionName, T Item)
        {
            throw new NotImplementedException();
        }
    }
}
