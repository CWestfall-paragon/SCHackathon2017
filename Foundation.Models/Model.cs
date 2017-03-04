






















#pragma warning disable 1591
#pragma warning disable 0108
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Team Development for Sitecore.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;   
using System.Collections.Generic;   
using System.Linq;
using System.Text;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Fields;
using Sitecore.Globalization;
using Sitecore.Data;




namespace Foundation.Models.Models
{

	public partial interface IGlassBase{
		
		[SitecoreId]
		Guid Id{ get; }

		[SitecoreInfo(SitecoreInfoType.Language)]
        Language Language{ get; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        int Version { get; }

	}

	public abstract partial class GlassBase : IGlassBase{
		
		[SitecoreId]
		public virtual Guid Id{ get; private set;}

		[SitecoreInfo(SitecoreInfoType.Language)]
        public virtual Language Language{ get; private set; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        public virtual int Version { get; private set; }

		[SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string Url { get; private set; }
	}
}

namespace Foundation.Models.Models.sitecore.templates.WeaponX.Folders
{


 	/// <summary>
	/// IBlog_Feed_Folder Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/Folders/Blog Feed Folder</para>	
	/// <para>ID: 123fc896-d5af-4f5c-a053-bb875b7de86c</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBlog_Feed_FolderConstants.TemplateIdString )] //, Cachable = true
	public partial interface IBlog_Feed_Folder : IGlassBase 
	{
			
	}


	public static partial class IBlog_Feed_FolderConstants{

			public const string TemplateIdString = "123fc896-d5af-4f5c-a053-bb875b7de86c";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "Blog Feed Folder";

			

	}

	
	/// <summary>
	/// Blog_Feed_Folder
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/Folders/Blog Feed Folder</para>	
	/// <para>ID: 123fc896-d5af-4f5c-a053-bb875b7de86c</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBlog_Feed_FolderConstants.TemplateIdString)] //, Cachable = true
	public partial class Blog_Feed_Folder  : GlassBase, IBlog_Feed_Folder 
	{
	   
			
	}

}
namespace Foundation.Models.Models.sitecore.templates.WeaponX
{


 	/// <summary>
	/// IRSSFeedConfiguration Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/RSSFeedConfiguration</para>	
	/// <para>ID: 47ec005d-f89f-4695-8efa-35585cbf244d</para>	
	/// </summary>
	[SitecoreType(TemplateId=IRSSFeedConfigurationConstants.TemplateIdString )] //, Cachable = true
	public partial interface IRSSFeedConfiguration : IGlassBase 
	{
			
					/// <summary>
					/// The BlogSource field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 4c8949f5-4c4a-4c84-b74a-848c2cac9926</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IRSSFeedConfigurationConstants.BlogSourceFieldName)]
					string BlogSource  {get; set;}
			
			
	}


	public static partial class IRSSFeedConfigurationConstants{

			public const string TemplateIdString = "47ec005d-f89f-4695-8efa-35585cbf244d";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "RSSFeedConfiguration";

		
			
			public static readonly ID BlogSourceFieldId = new ID("4c8949f5-4c4a-4c84-b74a-848c2cac9926");
			public const string BlogSourceFieldName = "BlogSource";
			
			

	}

	
	/// <summary>
	/// RSSFeedConfiguration
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/RSSFeedConfiguration</para>	
	/// <para>ID: 47ec005d-f89f-4695-8efa-35585cbf244d</para>	
	/// </summary>
	[SitecoreType(TemplateId=IRSSFeedConfigurationConstants.TemplateIdString)] //, Cachable = true
	public partial class RSSFeedConfiguration  : GlassBase, IRSSFeedConfiguration 
	{
	   
		
				/// <summary>
				/// The BlogSource field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 4c8949f5-4c4a-4c84-b74a-848c2cac9926</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IRSSFeedConfigurationConstants.BlogSourceFieldName)]
				public virtual string BlogSource  {get; set;}
					
			
	}

}
namespace Foundation.Models.Models.sitecore.templates.WeaponX
{


 	/// <summary>
	/// IBlogRoot Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/BlogRoot</para>	
	/// <para>ID: 7148addc-03df-49d0-9f56-2ed720149ab5</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBlogRootConstants.TemplateIdString )] //, Cachable = true
	public partial interface IBlogRoot : IGlassBase 
	{
			
	}


	public static partial class IBlogRootConstants{

			public const string TemplateIdString = "7148addc-03df-49d0-9f56-2ed720149ab5";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "BlogRoot";

			

	}

	
	/// <summary>
	/// BlogRoot
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/BlogRoot</para>	
	/// <para>ID: 7148addc-03df-49d0-9f56-2ed720149ab5</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBlogRootConstants.TemplateIdString)] //, Cachable = true
	public partial class BlogRoot  : GlassBase, IBlogRoot 
	{
	   
			
	}

}
namespace Foundation.Models.Models.sitecore.templates.WeaponX
{


 	/// <summary>
	/// IRSSFeedSettings Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/RSSFeedSettings</para>	
	/// <para>ID: 836ce55b-d8a3-47ce-972c-c82289debfe5</para>	
	/// </summary>
	[SitecoreType(TemplateId=IRSSFeedSettingsConstants.TemplateIdString )] //, Cachable = true
	public partial interface IRSSFeedSettings : IGlassBase 
	{
			
					/// <summary>
					/// The BlogLocation field.
					/// <para></para>
					/// <para>Field Type: General Link</para>		
					/// <para>Field ID: b6b56b24-e4ae-4e8d-b3a4-7e5992761d11</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IRSSFeedSettingsConstants.BlogLocationFieldName)]
					Link BlogLocation  {get; set;}
			
			
					/// <summary>
					/// The MaxDaysBack field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: dd947fa9-753e-4d0f-ae2c-e44746a33264</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IRSSFeedSettingsConstants.MaxDaysBackFieldName)]
					string MaxDaysBack  {get; set;}
			
			
					/// <summary>
					/// The SyncDatabase field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 23ca3371-e903-4b8e-bdd9-a06372c03197</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IRSSFeedSettingsConstants.SyncDatabaseFieldName)]
					string SyncDatabase  {get; set;}
			
			
					/// <summary>
					/// The UserName field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: b141b63b-b7a8-480b-8b6b-1fe392c912ff</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IRSSFeedSettingsConstants.UserNameFieldName)]
					string UserName  {get; set;}
			
			
	}


	public static partial class IRSSFeedSettingsConstants{

			public const string TemplateIdString = "836ce55b-d8a3-47ce-972c-c82289debfe5";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "RSSFeedSettings";

		
			
			public static readonly ID BlogLocationFieldId = new ID("b6b56b24-e4ae-4e8d-b3a4-7e5992761d11");
			public const string BlogLocationFieldName = "BlogLocation";
			
		
			
			public static readonly ID MaxDaysBackFieldId = new ID("dd947fa9-753e-4d0f-ae2c-e44746a33264");
			public const string MaxDaysBackFieldName = "MaxDaysBack";
			
		
			
			public static readonly ID SyncDatabaseFieldId = new ID("23ca3371-e903-4b8e-bdd9-a06372c03197");
			public const string SyncDatabaseFieldName = "SyncDatabase";
			
		
			
			public static readonly ID UserNameFieldId = new ID("b141b63b-b7a8-480b-8b6b-1fe392c912ff");
			public const string UserNameFieldName = "UserName";
			
			

	}

	
	/// <summary>
	/// RSSFeedSettings
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/RSSFeedSettings</para>	
	/// <para>ID: 836ce55b-d8a3-47ce-972c-c82289debfe5</para>	
	/// </summary>
	[SitecoreType(TemplateId=IRSSFeedSettingsConstants.TemplateIdString)] //, Cachable = true
	public partial class RSSFeedSettings  : GlassBase, IRSSFeedSettings 
	{
	   
		
				/// <summary>
				/// The BlogLocation field.
				/// <para></para>
				/// <para>Field Type: General Link</para>		
				/// <para>Field ID: b6b56b24-e4ae-4e8d-b3a4-7e5992761d11</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IRSSFeedSettingsConstants.BlogLocationFieldName)]
				public virtual Link BlogLocation  {get; set;}
					
		
				/// <summary>
				/// The MaxDaysBack field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: dd947fa9-753e-4d0f-ae2c-e44746a33264</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IRSSFeedSettingsConstants.MaxDaysBackFieldName)]
				public virtual string MaxDaysBack  {get; set;}
					
		
				/// <summary>
				/// The SyncDatabase field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 23ca3371-e903-4b8e-bdd9-a06372c03197</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IRSSFeedSettingsConstants.SyncDatabaseFieldName)]
				public virtual string SyncDatabase  {get; set;}
					
		
				/// <summary>
				/// The UserName field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: b141b63b-b7a8-480b-8b6b-1fe392c912ff</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IRSSFeedSettingsConstants.UserNameFieldName)]
				public virtual string UserName  {get; set;}
					
			
	}

}
namespace Foundation.Models.Models.sitecore.templates.WeaponX
{


 	/// <summary>
	/// IBlogItem Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/BlogItem</para>	
	/// <para>ID: d032f66d-dc04-43b9-b782-311fdc17a9ac</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBlogItemConstants.TemplateIdString )] //, Cachable = true
	public partial interface IBlogItem : IGlassBase 
	{
			
					/// <summary>
					/// The BlogAbstract field.
					/// <para></para>
					/// <para>Field Type: Multi-Line Text</para>		
					/// <para>Field ID: bd243774-92b6-4c55-9f6c-105b6207e95f</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IBlogItemConstants.BlogAbstractFieldName)]
					string BlogAbstract  {get; set;}
			
			
					/// <summary>
					/// The BlogDisplayDate field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 69eb7c3b-4624-4035-a883-4f2f20877f59</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IBlogItemConstants.BlogDisplayDateFieldName)]
					string BlogDisplayDate  {get; set;}
			
			
					/// <summary>
					/// The BlogID field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 4f7f8b09-60ae-42e1-973e-556ddd4223d1</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IBlogItemConstants.BlogIDFieldName)]
					string BlogID  {get; set;}
			
			
					/// <summary>
					/// The BlogSourceUrl field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 60be106e-5c2b-4f1b-86d8-4e3352a64d40</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IBlogItemConstants.BlogSourceUrlFieldName)]
					string BlogSourceUrl  {get; set;}
			
			
					/// <summary>
					/// The BlogTitle field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 686b5276-62f0-4e1d-81d1-1bee7c197856</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IBlogItemConstants.BlogTitleFieldName)]
					string BlogTitle  {get; set;}
			
			
	}


	public static partial class IBlogItemConstants{

			public const string TemplateIdString = "d032f66d-dc04-43b9-b782-311fdc17a9ac";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "BlogItem";

		
			
			public static readonly ID BlogAbstractFieldId = new ID("bd243774-92b6-4c55-9f6c-105b6207e95f");
			public const string BlogAbstractFieldName = "BlogAbstract";
			
		
			
			public static readonly ID BlogDisplayDateFieldId = new ID("69eb7c3b-4624-4035-a883-4f2f20877f59");
			public const string BlogDisplayDateFieldName = "BlogDisplayDate";
			
		
			
			public static readonly ID BlogIDFieldId = new ID("4f7f8b09-60ae-42e1-973e-556ddd4223d1");
			public const string BlogIDFieldName = "BlogID";
			
		
			
			public static readonly ID BlogSourceUrlFieldId = new ID("60be106e-5c2b-4f1b-86d8-4e3352a64d40");
			public const string BlogSourceUrlFieldName = "BlogSourceUrl";
			
		
			
			public static readonly ID BlogTitleFieldId = new ID("686b5276-62f0-4e1d-81d1-1bee7c197856");
			public const string BlogTitleFieldName = "BlogTitle";
			
			

	}

	
	/// <summary>
	/// BlogItem
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/BlogItem</para>	
	/// <para>ID: d032f66d-dc04-43b9-b782-311fdc17a9ac</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBlogItemConstants.TemplateIdString)] //, Cachable = true
	public partial class BlogItem  : GlassBase, IBlogItem 
	{
	   
		
				/// <summary>
				/// The BlogAbstract field.
				/// <para></para>
				/// <para>Field Type: Multi-Line Text</para>		
				/// <para>Field ID: bd243774-92b6-4c55-9f6c-105b6207e95f</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IBlogItemConstants.BlogAbstractFieldName)]
				public virtual string BlogAbstract  {get; set;}
					
		
				/// <summary>
				/// The BlogDisplayDate field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 69eb7c3b-4624-4035-a883-4f2f20877f59</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IBlogItemConstants.BlogDisplayDateFieldName)]
				public virtual string BlogDisplayDate  {get; set;}
					
		
				/// <summary>
				/// The BlogID field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 4f7f8b09-60ae-42e1-973e-556ddd4223d1</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IBlogItemConstants.BlogIDFieldName)]
				public virtual string BlogID  {get; set;}
					
		
				/// <summary>
				/// The BlogSourceUrl field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 60be106e-5c2b-4f1b-86d8-4e3352a64d40</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IBlogItemConstants.BlogSourceUrlFieldName)]
				public virtual string BlogSourceUrl  {get; set;}
					
		
				/// <summary>
				/// The BlogTitle field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 686b5276-62f0-4e1d-81d1-1bee7c197856</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IBlogItemConstants.BlogTitleFieldName)]
				public virtual string BlogTitle  {get; set;}
					
			
	}

}
namespace Foundation.Models.Models.sitecore.templates.WeaponX.Folders
{


 	/// <summary>
	/// IBlog_Folder Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/Folders/Blog Folder</para>	
	/// <para>ID: f83c525c-6816-490f-8e32-0d720f5f24fb</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBlog_FolderConstants.TemplateIdString )] //, Cachable = true
	public partial interface IBlog_Folder : IGlassBase 
	{
			
	}


	public static partial class IBlog_FolderConstants{

			public const string TemplateIdString = "f83c525c-6816-490f-8e32-0d720f5f24fb";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "Blog Folder";

			

	}

	
	/// <summary>
	/// Blog_Folder
	/// <para></para>
	/// <para>Path: /sitecore/templates/WeaponX/Folders/Blog Folder</para>	
	/// <para>ID: f83c525c-6816-490f-8e32-0d720f5f24fb</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBlog_FolderConstants.TemplateIdString)] //, Cachable = true
	public partial class Blog_Folder  : GlassBase, IBlog_Folder 
	{
	   
			
	}

}
