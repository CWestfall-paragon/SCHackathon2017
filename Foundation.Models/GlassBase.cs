using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;

namespace Foundation.Models
{
    public partial class GlassBase : IGlassBase
    {

        public virtual ItemUri __Uri { get; set; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        public virtual int __Version
        {
            get
            {
                return __Uri == null ? 0 : __Uri.Version.Number;
            }
        }

        [SitecoreInfo(SitecoreInfoType.Name)]
        public virtual string __Name { get; set; }

        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        public virtual string __DisplayName { get; set; }

        [SitecoreParent(InferType = true)]
        public virtual IGlassBase __Parent { get; set; }

        [SitecoreChildren(InferType = true)]
        public virtual IEnumerable<IGlassBase> __Children { get; set; }

        [SitecoreField("__Sortorder"),]
        public virtual string __Sortorder { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string __Url { get; set; }
    }
}
