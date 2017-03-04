using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using Foundation.Models.Models.sitecore.templates.WeaponX;
using Foundation.Models.Models.sitecore.templates.WeaponX.Folders;
using Glass.Mapper;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
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
            // Feedsettings = BlogImportHelper.RssFeedSettings();
            var masterDb = Database.GetDatabase("master");
            // var context = new SitecoreContext(masterDb);

            var item = masterDb.GetItem("/sitecore/system/Modules/RSSFeedSettings");
            SyncDatabase = item.Fields["SyncDatabase"].Value;
            //Not working
            Sitecore.Data.Fields.LinkField linkField = item.Fields["BlogLocation"];

            BlogFeedLocId = linkField.TargetID.ToString();
            //if (Feedsettings == null)
            //{
            //    return;
            //}
            //else
            //{
            //    SyncDatabase = Feedsettings.SyncDatabase;
            //}

            //create month folder may not need this
            CreateFolderForMonth();

            var masterDB = Database.GetDatabase("master");
            //SitecoreContext repository = new SitecoreContext(masterDB);

            Item[] feeditems = masterDB.SelectItems("/sitecore/system/modules//*[@@templatename='RSSFeedConfiguration']");
            
            var feedlist = new RssFeedItems();

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

        private void ProcessDocuments(SyndicationFeed documents)
        {
            string message = string.Format("Found {0} Documents within the " + BlogName + " Feed",
                documents.Items.Count());

            Sitecore.Diagnostics.Log.Error(message, "RssFeedImport");

            //start iterating through the feed
            foreach (var doc in documents.Items)
            {

                string title = doc.Title.Text;
                //id is the url of the blog
                //var publicationfound = GetPublication(doc.Id);

                //  bool ismatch = publicationfound != null && publicationfound.BlogId != "";

                bool ismatch = false;
               // if (!ismatch)
               // {
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

        private void ProcessNewBlogItem(IBlogItem doc)
        {
            string message = string.Format("Creating New Item [docId:{0}] - \"{1}\"", doc.Id, doc.BlogTitle);

            Sitecore.Diagnostics.Log.Error(message, "RssFeedImport");

            IBlogItem blog = null;

            Guid folderTemplateId =
                Feedsettings.BlogLocation.TargetId;

            DateTime rsspubdate = new DateTime();

            if (doc.BlogDisplayDate != null)
            {
                rsspubdate = System.Convert.ToDateTime(doc.BlogDisplayDate.ToString());
            };

            var year = rsspubdate.Year;
            var month = rsspubdate.Month;

            var rootItemId = new ID(new Guid("{D032F66D-DC04-43B9-B782-311FDC17A9AC}"));
            var masterDb = Database.GetDatabase("master");
            var parentPubItem = masterDb.GetItem(rootItemId);

            var yearFolder = BlogImportHelper.EnsureChildFolder(parentPubItem, year.ToString(), folderTemplateId);

            var monthFolder = BlogImportHelper.EnsureChildFolder(yearFolder, month.ToString("00"), folderTemplateId);

            try
            {
                blog = new BlogItem()
                {
                    //Should be able to add name here
                    BlogID = doc.BlogID,
                    BlogTitle = doc.BlogTitle,
                    BlogAbstract = doc.BlogAbstract,
                    BlogDisplayDate = doc.BlogDisplayDate,
                    BlogSourceUrl = doc.BlogSourceUrl
                };
            }
            catch (Exception ex)
            {

                Sitecore.Diagnostics.Log.Error("Error while creating the New Item: " + BlogName + ": " + ex.Message, "RssFeedImport");
            }

            if (blog != null)
                PublishSaveItem(blog, monthFolder, false);
        }

        private void PublishSaveItem(IBlogItem blogItem, Item foldertoplace = null, bool isUpdate = false)
        {
            //Not sure why had this before, but we may not have to do the same thing.
            //var publication = new ImportedPublication()
            //{
            //    __Name = ItemUtil.ProposeValidItemName(pubCoverage.Title),
            //    __DisplayName = pubCoverage.Title,
            //    PostedRssDateAndTime = pubCoverage.PostedRssDateAndTime,
            //    Date = pubCoverage.PostedRssDateAndTime,
            //    Title = pubCoverage.Title,
            //    Abstract = pubCoverage.Abstract,
            //    SourceUrl = pubCoverage.SourceUrl,
            //    BlogId = pubCoverage.BlogId,
            //    PublicationTypes = pubCoverage.PublicationTypes,
            //    RelatedServices = pubCoverage.RelatedServices,
            //    BlogSource = pubCoverage.BlogSource,
            //    Source = pubCoverage.Source
            //};

            var db = Sitecore.Configuration.Factory.GetDatabase("master");

            var service = new SitecoreService(db);
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                //_taskResults.LogActivity(new TaskResultsActivity()
                //{
                //    Message = "Saving Changes",
                //    Code = MessageCode.Debug
                //});

                //if (isUpdate)
                //{
                //    service.Save(pubCoverage);
                //    //QueryHelper.UpdateItemInIndex(publication.ToItem());
                //    // service.Create(foldertoplace, pubCoverage);
                //}
                //else
                //{
                    //service.Create<ImportedPublication, PublicationFolderGlass>(service.Cast<PublicationFolderGlass>(foldertoplace), publication);
                    service.Create<IBlogItem, Blog_Folder>(service.Cast<Blog_Folder>(foldertoplace), blogItem);
                    //QueryHelper.AddItemToIndex(publication.ToItem());
               // }
                //service.Save(pubCoverage);

                // pubCoverage.Save();

                //publish publication
                //CreateAuthorList(publication);

                Item scItem = blogItem.ToItem();
                scItem.Editing.BeginEdit();

                //Set workflow
                try
                {
                    //TODO put workflows in model
                    //scItem.Fields["__Workflow state"].Value = "{42E5512D-A8CC-4991-B6D1-94D3A453AC84}";
                    //OneNorth.Sitecore7.Client.Model.SitecoreSystem.Workflows.PublishItemwithAggregatesOnly.ItemIds.Publish.ToString();
                    //scItem.Fields["__Workflow state"].Value = OneNorth.Sitecore7.Client.Model.SitecoreSystem.Workflows.PublishItemwithSubItems.Publish.PublishConstants.Id;
                    //Looks like with Sitecore 8.1 it will not publish with workflow attached.
                    //scItem.Fields["__Workflow state"].Value = "";
                    //scItem.Fields["AuthorInfo"].Value = InsightHelper.GetAuthorListSubExpressItem(publication);
                    scItem.Fields["Name"].Value = blogItem.BlogTitle;
                }
                finally
                {
                    scItem.Editing.EndEdit();
                }

                scItem.Editing.BeginEdit();
                try
                {
                    if (foldertoplace != null)
                    {
                        scItem.MoveTo(foldertoplace);
                    }
                }
                finally
                {
                    scItem.Editing.EndEdit();
                }

                PublishItem(scItem);
            }
        }

        private void PublishItem(Sitecore.Data.Items.Item item)
        {
            // The publishOptions determine the source and target database,
            // the publish mode and language, and the publish date
            var publishingTargetItems = Sitecore.Publishing.PublishManager.GetPublishingTargets(_db);
            foreach (var publishingTarget in publishingTargetItems)
            {
                SecurityDisabler securitydisabler = new SecurityDisabler();
                var useDatabaseName = publishingTarget["Target Database"];
                var webDatabase = Database.GetDatabase(useDatabaseName);

                Sitecore.Publishing.PublishOptions publishOptions =
                    new Sitecore.Publishing.PublishOptions(item.Database,
                        Database.GetDatabase(useDatabaseName),
                        Sitecore.Publishing.PublishMode.Full,
                        item.Language,
                        System.DateTime.Now); // Create a publisher with the publishoptions
                Sitecore.Publishing.Publisher publisher = new Sitecore.Publishing.Publisher(publishOptions);

                // Choose where to publish from
                publisher.Options.RootItem = item;

                // Publish children as well?
                publisher.Options.Deep = true;

                // Do the publish!
                publisher.Publish();
                SecurityEnabler securityenabler = new SecurityEnabler();
                //var indexauthors = typeof (Website.Logic.ComputedFields.AttachedFiltersComputedField);

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

            //public static RSSFeedSettings RssFeedSettings()
            //{
            //    var masterDb = Database.GetDatabase("master");
            //   // var context = new SitecoreContext(masterDb);
                
            //    var rssFeedSettings = new RSSFeedSettings();
            //    var item = masterDb.GetItem("/sitecore/system/Modules/RSSFeedSettings");
            //    SyncDatabase = item.Fields["SyncDatabase"].Value;
            //    //if (item.TemplateID == IRSSFeedSettingsConstants.TemplateId)
            //    //{
            //    //    rssFeedSettings = item.CastTo<RSSFeedSettings>();
            //    //}
            //    //rssFeedSettings = masterDb.GetItem("/sitecore/system/Modules/RSSFeedSettings").CastTo<RSSFeedSettings>();  //context.GetItem<RSSFeedSettings>("/sitecore/system/Modules/RSSFeedSettings");
            //    return rssFeedSettings;
            //}

        }

        public bool BlogExistsNoIndex(string blogid)
        {
            var db = Sitecore.Configuration.Factory.GetDatabase(SyncDatabase);
            //do this differently?
            string query = "/sitecore/content/Home/Blogs//*[@BlogId = '" + blogid + "']";
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

       
        public class RssFeedItems
        {
            //[SitecoreQuery("/sitecore/system/Modules//*[@@templateid='47EC005D-F89F-4695-8EFA-35585CBF244D']", IsRelative = true)]
            [SitecoreQuery("/sitecore/system/modules//*[@@templatename='RSSFeedConfiguration']", IsRelative = true)]
            public virtual IEnumerable<RSSFeedConfiguration> RssFeeds { get; set; }
        }
    }
    public class RunExport : Command
    {
        public override void Execute(CommandContext context)
        {
            var runblogimport = new RssFeedImporter();
            runblogimport.Run();
        }

    }
}