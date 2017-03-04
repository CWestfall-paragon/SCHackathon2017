using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foundation.Models.Models;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Feature.WeaponX.Tasks
{
    public static class TaskExtensions
    {
        public static Item ToItem(this IGlassBase entity)
        {
            if (entity == null) return null;

            return Context.Database.GetItem(new ID(entity.Id));
        }
    }
}