//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v13.2.2+79d241a
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	// Mixin Content Type with alias "tFooterContainer"
	/// <summary>Footer Container</summary>
	public partial interface ITFooterContainer : IPublishedContent
	{
		/// <summary>About footer</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TAboutFooter> TAboutFoot { get; }

		/// <summary>Contact us</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TContact> TContactUsFooter { get; }

		/// <summary>Quick links</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TQuickLinks> TQuickLinks { get; }
	}

	/// <summary>Footer Container</summary>
	[PublishedModel("tFooterContainer")]
	public partial class TFooterContainer : PublishedContentModel, ITFooterContainer
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		public new const string ModelTypeAlias = "tFooterContainer";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<TFooterContainer, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public TFooterContainer(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// About footer
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tAboutFoot")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TAboutFooter> TAboutFoot => GetTAboutFoot(this, _publishedValueFallback);

		/// <summary>Static getter for About footer</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TAboutFooter> GetTAboutFoot(ITFooterContainer that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TAboutFooter>>(publishedValueFallback, "tAboutFoot");

		///<summary>
		/// Contact us
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tContactUsFooter")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TContact> TContactUsFooter => GetTContactUsFooter(this, _publishedValueFallback);

		/// <summary>Static getter for Contact us</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TContact> GetTContactUsFooter(ITFooterContainer that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TContact>>(publishedValueFallback, "tContactUsFooter");

		///<summary>
		/// Quick links
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tQuickLinks")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TQuickLinks> TQuickLinks => GetTQuickLinks(this, _publishedValueFallback);

		/// <summary>Static getter for Quick links</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TQuickLinks> GetTQuickLinks(ITFooterContainer that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TQuickLinks>>(publishedValueFallback, "tQuickLinks");
	}
}
