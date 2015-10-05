using magicManager.Models.Expansions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magicManager.DAL
{
    interface IExpansionRepository : IDisposable
    {
        IEnumerable<Expansion> GetExpansion();
        IEnumerable<Expansion> GetExpansionByID(int idGame);
        void InsertExpansion(Expansion expansion);
        void DeleteExpansion(int idExpansion);
        void UpdateExpansion(Expansion expansion);
        void Save();
    }
}
