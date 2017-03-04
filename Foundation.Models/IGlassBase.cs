using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Foundation.Models
{
    public partial interface IGlassBase
    {
        [SitecoreInfo(SitecoreInfoType.Version)]
        int __Version { get; }

        [SitecoreInfo(SitecoreInfoType.Name)]
        string __Name { get; set; }

        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        string __DisplayName { get; set; }

        [SitecoreParent(InferType = true)]
        IGlassBase __Parent { get; set; }

        [SitecoreChildren(InferType = true)]
        IEnumerable<IGlassBase> __Children { get; set; }

        [SitecoreField("__Sortorder"),]
        string __Sortorder { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        string __Url { get; set; }
    }
}
