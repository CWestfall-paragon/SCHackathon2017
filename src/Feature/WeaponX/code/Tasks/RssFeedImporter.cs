using System;
using System.Linq;
using Foundation.Models.Models.sitecore.templates.WeaponX;
using Glass.Mapper.Sc;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Feature.WeaponX.Tasks
{
    public class RssFeedImporter
    {
        private Database _db { get; set; }

        private RSSFeedSettings feedsettings { get; set; }

        public void Run()
        {
            //get setttings
            feedsettings = BlogImportHelper.RssFeedSettings();

            if (feedsettings == null)
            {
                return;
            }

            //create month folder
            CreateFolderForMonth();

            var masterDB = Database.GetDatabase("master");
            SitecoreContext repository = new SitecoreContext(masterDB);
        }

        private void CreateFolderForMonth()
        {
            Guid folderTemplateId =
                Foundation.Models.Models.sitecore.templates.WeaponX.Folders.IBlog_FolderConstants.TemplateId.Guid;
            var year = DateTime.Now.Year.ToString();
            var month = DateTime.Now.Month.ToString("00");

            var rootItemId = new ID(feedsettings.BlogLocation.TargetId.ToString());
            var masterDb = Database.GetDatabase("master");
            var parentPubItem = masterDb.GetItem(rootItemId);

            var yearFolder = BlogImportHelper.EnsureChildFolder(parentPubItem, year, folderTemplateId);

            var monthFolder = BlogImportHelper.EnsureChildFolder(yearFolder, month, folderTemplateId);
        }

        public class BlogImportHelper
        {
            public static Item EnsureChildFolder(Item parent, string name, Guid folderTemplateId)
            {
                using (new Sitecore.SecurityModel.SecurityDisabler())
                {
                    if (folderTemplateId == Guid.Empty)
                        folderTemplateId = TemplateIDs.Folder.Guid;
                    return parent.Children.FirstOrDefault(x => x.Name == name) ??
                           parent.Add(name, new TemplateID(new ID(folderTemplateId)));
                }
            }

            public static RSSFeedSettings RssFeedSettings()
            {
                var masterDb = Database.GetDatabase("master");
                var context = new SitecoreContext(masterDb);
                var rssFeedSettings = new RSSFeedSettings();
                rssFeedSettings = context.GetItem<RSSFeedSettings>("/sitecore/system/Modules/RSSFeedSettings");
                return rssFeedSettings;
            }
        }
    }
}