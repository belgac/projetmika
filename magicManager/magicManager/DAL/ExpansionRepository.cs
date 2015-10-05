using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using magicManager.Models.Expansions;
using Newtonsoft.Json;
using magicManager.Models;

namespace magicManager.DAL
{
    public class ExpansionRepository : IExpansionRepository
    {
        RequestHelper essai = new RequestHelper();

        public void DeleteExpansion(int idExpansion)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Expansion> GetExpansion()
        {
            string text = essai.ExpansionRequest();
            if (text == null) return null;
            RootExpansion root = JsonConvert.DeserializeObject<RootExpansion>(text);
            return root.expansion as IEnumerable<Expansion>;
        }

        public IEnumerable<Expansion> GetExpansionByID(int idGame)
        {
            string text = essai.ExpansionByIdRequest(idGame);
            if (text == null) return null;
            RootExpansion root = JsonConvert.DeserializeObject<RootExpansion>(text);
            return root.expansion as IEnumerable<Expansion>;
        }

        public void InsertExpansion(Expansion expansion)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateExpansion(Expansion expansion)
        {
            throw new NotImplementedException();
        }
    }
}