﻿using MagicManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicManager.dal.Repositories
{
    public class ProductRepo : GenericRepository<MagicManagerDataEntities, Product>, IProducts
    {

    }
}