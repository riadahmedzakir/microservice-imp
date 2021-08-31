using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IRowLevelSecurity
    {
        public string[] RolesAllowedToRead { get; set; }
        public string[] IdsAllowedToRead { get; set; }
        public string[] RolesAllowedToWrite { get; set; }
        public string[] IdsAllowedToWrite { get; set; }
        public string[] RolesAllowedToUpdate { get; set; }
        public string[] IdsAllowedToUpdate { get; set; }
        public string[] RolesAllowedToDelete { get; set; }
        public string[] IdsAllowedToDelete { get; set; }
    }
}
