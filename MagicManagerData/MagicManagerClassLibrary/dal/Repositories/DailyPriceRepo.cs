using MagicManager.dal.Interfaces;
using MagicManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicManager.dal.Repositories
{
    public class DailyPriceRepo : GenericRepository<MagicManagerDataEntities, DailyPrice>, IDailyPrices
    {
    }
}