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
	/// <summary>HomePage</summary>
	[PublishedModel("homePage")]
	public partial class HomePage : PublishedContentModel, ITFooterContainer, ITHeaderContainer
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		public new const string ModelTypeAlias = "homePage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<HomePage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public HomePage(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// ContentPage
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tContentPage")]
		public virtual global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent TContentPage => this.Value<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent>(_publishedValueFallback, "tContentPage");

		///<summary>
		/// About footer
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tAboutFoot")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TAboutFooter> TAboutFoot => global::Umbraco.Cms.Web.Common.PublishedModels.TFooterContainer.GetTAboutFoot(this, _publishedValueFallback);

		///<summary>
		/// Contact us
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tContactUsFooter")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TContact> TContactUsFooter => global::Umbraco.Cms.Web.Common.PublishedModels.TFooterContainer.GetTContactUsFooter(this, _publishedValueFallback);

		///<summary>
		/// Quick links
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tQuickLinks")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TQuickLinks> TQuickLinks => global::Umbraco.Cms.Web.Common.PublishedModels.TFooterContainer.GetTQuickLinks(this, _publishedValueFallback);

		///<summary>
		/// Button
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tHeaderButton")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListItem<global::Umbraco.Cms.Web.Common.PublishedModels.TButton> THeaderButton => global::Umbraco.Cms.Web.Common.PublishedModels.THeaderContainer.GetTHeaderButton(this, _publishedValueFallback);

		///<summary>
		/// Links
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tHeaderLinks")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockListModel THeaderLinks => global::Umbraco.Cms.Web.Common.PublishedModels.THeaderContainer.GetTHeaderLinks(this, _publishedValueFallback);

		///<summary>
		/// Is Dark
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[ImplementPropertyType("tIsDark")]
		public virtual bool TIsDark => global::Umbraco.Cms.Web.Common.PublishedModels.THeaderContainer.GetTIsDark(this, _publishedValueFallback);

		///<summary>
		/// Language dropdown
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tLanguageDropdown")]
		public virtual string TLanguageDropdown => global::Umbraco.Cms.Web.Common.PublishedModels.THeaderContainer.GetTLanguageDropdown(this, _publishedValueFallback);

		///<summary>
		/// Logo
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.2.2+79d241a")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("tLogo")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops TLogo => global::Umbraco.Cms.Web.Common.PublishedModels.THeaderContainer.GetTLogo(this, _publishedValueFallback);
	}
}
