using ItemRoller.Data_Structure;
using ItemRoller.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LewtzGUI.Data_Access
{
    public abstract class RepositoryLoader
    {
        public TableRepository LoadRepository(string folderPath, IDataMapper mapper)
        {
            return new TableRepository();
        }
    }
}
