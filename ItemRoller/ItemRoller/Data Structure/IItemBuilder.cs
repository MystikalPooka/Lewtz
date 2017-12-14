using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoller.Data_Structure
{
    public interface IItemBuilder
    {
        /// <summary>
        /// Build the item. returns true if successful.
        /// </summary>
        /// <returns>
        /// True if successful. False if not.
        /// </returns>
        bool Build(Item item);
    }
}
