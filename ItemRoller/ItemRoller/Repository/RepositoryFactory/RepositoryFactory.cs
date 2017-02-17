﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoller.Repository
{
    public static class RepositoryFactory
    {
        public static IRepository GetRepository(string type, IDataMapper mapper, IRepositoryLoader loader)
        {
            return new TableRepository();
        }
    }
}