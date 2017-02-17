using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoller.Repository.RepositoryFactory
{
    public class RepositoryFactory
    {
        public IRepository GetRepository(string type, IDataMapper mapper, IRepositoryLoader loader)
        {


            return new TableRepository();
        }
    }
}
