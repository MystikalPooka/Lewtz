using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoller.Repository
{
    public interface IRepository
    {
        void Create();
        //should be type
        void Retrieve();
        void Update();
        void Delete();
        void Save();
        void Load();
    }
}
