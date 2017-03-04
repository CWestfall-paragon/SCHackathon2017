using System;
using System.Linq;
using System.Net;
using System.Xml;
using Foundation.Models.Models.sitecore.templates.WeaponX;
using Foundation.Models.Models.sitecore.templates.WeaponX.Folders;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;
using Terradue.ServiceModel.Syndication;

namespace Feature.WeaponX.Tasks
{
    public class RssFeedImporter
    {
        private Database _db { get; set; }

        private RSSFeedSettings Feedsettings { get; set; }

        public string SyncDatabase { get; set; }
        public string BlogFeedLocId { get; set; }
        public string BlogName { get; set; }

        public void Run()
        {
            //get setttings
            var masterDb = Database.GetDatabase("master");

            var item = masterDb.GetItem("/sitecore/system/Modules/RSSFeedSettings");
            SyncDatabase = item.Fields["SyncDatabase"].Value;

            Sitecore.Data.Fields.LinkField linkField = item.Fields["BlogLocation"];

            BlogFeedLocId = linkField.TargetID.ToString();
            if (string.IsNullOrEmpty(BlogFeedLocId))
            {
                return;
            }

            //create current month folder may not need this
            CreateFolderForMonth();

            var masterDB = Database.GetDatabase("master");

            Item[] feeditems = masterDB.SelectItems("/sitecore/system/modules//*[@@templatename='RSSFeedConfiguration']");

            if (feeditems.Any())
            {
                foreach (var feedItem in feeditems)// feedlist.RssFeeds)
                {
                    SyndicationFeed feed = null;

                    BlogName = feedItem.Name;

                    try
                    {
                        feed = RetrieveFeed(feedItem.Fields["BlogSource"].Value);
                    }
                    catch (Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error("There was an error retrieving the XML from " + BlogName + ": " + ex.Message,"RssFeedImport");
                    }

                    if (feed == null || !feed.Items.Any())
                    {
                        Sitecore.Diagnostics.Log.Error("Unable to process the " + BlogName + " feed, no valid data was retrieved.", "RssFeedImport");
                    }
                    else
                    {
                        try
                        {
                            ProcessDocuments(feed);
                        }
                        catch (Exception ex)
                        {
                            Sitecore.Diagnostics.Log.Error("There was an error retrieving the XML from " + BlogName + ": " + ex.Message, "RssFeedImport");
                        }
                    }
                }

            }
        }

        //syndication feed retrieval. uses a third party dll
        private SyndicationFeed RetrieveFeed(string url)
        {
            WebRequest request = WebRequest.Create(url);
            SyndicationFeed feed;

            using (WebResponse response = request.GetResponse())
            using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
            {
                feed = SyndicationFeed.Load(reader);
            }
            return feed;
        }

        //Read the Feed and put it in a model
        private void ProcessDocuments(SyndicationFeed documents)
        {
            string message = string.Format("Found {0} Documents within the " + BlogName + " Feed",
                documents.Items.Count());

            Sitecore.Diagnostics.Log.Error(message, "RssFeedImport");

            //start iterating through the feed
            foreach (var doc in documents.Items)
            {


                bool ismatch = false;

                bool blogfound = BlogExistsNoIndex(doc.Id);
                ismatch = blogfound == true;
                // }

                if (!ismatch)
                {
                    var pubsmodel = new BlogItem()
                    {
                        BlogTitle = doc.Title.Text,
                        BlogAbstract = doc.Summary.Text,
                        BlogSourceUrl = doc.Id,
                        BlogDisplayDate = doc.PublishDate.ToString(),
                        BlogID = doc.Id
                    };

                    ProcessNewBlogItem(pubsmodel);
                }

            }
        }

        //Save the Feed to Sitecore
        private void ProcessNewBlogItem(IBlogItem doc)
        {
            string message = string.Format("Creating New Item [docId:{0}] - \"{1}\"", doc.Id, doc.BlogTitle);

            Sitecore.Diagnostics.Log.Error(message, "RssFeedImport");

            IBlogItem blog = null;

            var masterDb = Database.GetDatabase("master");
            var item = masterDb.GetItem("/sitecore/system/Modules/RSSFeedSettings");
            Sitecore.Data.Fields.LinkField linkField = item.Fields["BlogLocation"];


            Guid folderTemplateId =
                IBlog_FolderConstants.TemplateId.Guid;


            DateTime rsspubdate = new DateTime();

            if (doc.BlogDisplayDate != null)
            {
                rsspubdate = System.Convert.ToDateTime(doc.BlogDisplayDate.ToString());
            };

            var year = rsspubdate.Year;
            var month = rsspubdate.Month;

            var rootItemId = new ID(new Guid(BlogFeedLocId));
            var parentPubItem = masterDb.GetItem(rootItemId);

            var yearFolder = BlogImportHelper.EnsureChildFolder(parentPubItem, year.ToString(), folderTemplateId);

            var monthFolder = BlogImportHelper.EnsureChildFolder(yearFolder, month.ToString("00"), folderTemplateId);
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                var itemname = ItemUtil.ProposeValidItemName(doc.BlogTitle);
                var blogItem = monthFolder.Add(itemname, new TemplateID(IBlogItemConstants.TemplateId));
                var db = Sitecore.Configuration.Factory.GetDatabase("master");
                if (db != null)
                {
                    if (blogItem != null)
                    {
                        blogItem.Fields.ReadAll();
                        blogItem.Editing.BeginEdit();
                        try
                        {
                            //Should be able to add name here
                            blogItem.Fields["BlogID"].Value = doc.BlogID;
                            blogItem.Fields["BlogTitle"].Value = doc.BlogTitle;
                            blogItem.Fields["BlogAbstract"].Value = doc.BlogAbstract;
                            blogItem.Fields["BlogDisplayDate"].Value = doc.BlogDisplayDate;
                            blogItem.Fields["BlogSourceUrl"].Value = doc.BlogSourceUrl;
                        }
                        catch (Exception ex)
                        {
                            Sitecore.Diagnostics.Log.Error("Error while creating the New Item: " + BlogName + ": " + ex.Message, "RssFeedImport");
                        }
                        blogItem.Editing.EndEdit();
                    }
                }
            }
            
        }

        private void CreateFolderForMonth()
        {
            Guid folderTemplateId =
                Foundation.Models.Models.sitecore.templates.WeaponX.Folders.IBlog_FolderConstants.TemplateId.Guid;
            var year = DateTime.Now.Year.ToString();
            var month = DateTime.Now.Month.ToString("00");

            var rootItemId = new ID(BlogFeedLocId);
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
        }

        public bool BlogExistsNoIndex(string blogid)
        {
            var db = Sitecore.Configuration.Factory.GetDatabase(SyncDatabase);
            string query = "/sitecore/content/Blogs//*[@BlogID = '" + blogid + "']";
            Item[] blogItem = db.SelectItems(query);

            if (blogItem.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    //Command to run this. Can be used with powershell, button, scheduled task
    public class RunExport : Command
    {
        public override void Execute(CommandContext context)
        {
            var runblogimport = new RssFeedImporter();
            runblogimport.Run();
        }

    }
}