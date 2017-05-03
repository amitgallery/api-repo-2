/// <summary>
/// Experiment object contract
/// </summary>
[DataContract]
public class Experiment : EntityBase
{
	public Experiment()
	{
		this.EntityType = EntityType.Experiment;
	}

	[DataMember(Name = "node_count")]
	[AuthorizePropertyEdit("MLPropertiesPublisher,Administrator")]
	[IntrinsicProperty]
	public int? NodeCount { get; set; }

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "modules")]
	[AuthorizePropertyEdit("MLPropertiesPublisher,Administrator")]
	[IntrinsicProperty]
	public IList<string> Modules { get; set; }
}

/// <summary>
/// Tutorial type. Used for Azure Stream Analytics tutorials etc.
/// </summary>
[DataContract]
public class Tutorial : EntityBase
{
	public Tutorial()
	{
		this.EntityType = EntityType.Tutorial;
	}
}

/// <summary>
/// Collection type. Used for Collections of entities.
/// </summary>
[DataContract]
[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Naming is correct.")]
public class Collection : EntityBase
{
	public Collection()
	{
		this.EntityType = EntityType.Collection;
	}

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "items")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public IList<CollectionItemReference> Items { get; set; }

	[DataMember(Name = "item_count")]
	[AuthorizePropertyEdit("Administrator")]
	public int? ItemCount { get; set; }
}

/// <summary>
/// Solution accelerator entity contract.
/// </summary>
[DataContract]
public class SolutionAccelerator : EntityBase
{
	public SolutionAccelerator()
	{
		this.EntityType = EntityType.SolutionAccelerator;
	}

	/// <summary>
	/// The list of string identifiers of Azure service associated with the entity.
	/// </summary>
	[DataMember(Name = "related_azure_services")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	public IList<string> RelatedAzureServices { get; set; }
}

/// <summary>
/// Notebook object contract
/// </summary>
[DataContract]
public class Notebook : EntityBase
{
	public Notebook()
	{
		this.EntityType = EntityType.Notebook;
	}

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "languages")]
	[AuthorizePropertyEdit("MLPropertiesPublisher,Administrator")]
	[IntrinsicProperty]
	public IList<string> Languages { get; set; }
}

/// <summary>
/// Base class for catalog entities, like scripts or experiments. Contains all common properties.
/// </summary>
[DataContract]
[KnownType(typeof(Experiment))]
[KnownType(typeof(Competition))]
[KnownType(typeof(Api))]
[KnownType(typeof(Tutorial))]
[KnownType(typeof(Collection))]
[KnownType(typeof(SolutionAccelerator))]
[KnownType(typeof(Notebook))]
public class EntityBase : TenantItemBase
{
	[DataMember(Name = "entity_type")]
	public EntityType EntityType { get; set; }

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <remarks>
	/// Called description in the Studio UX.
	/// </remarks>
	[DataMember(Name = "name")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public string Name { get; set; }

	/// <remarks>
	/// Called project goal in the Studio UX.
	/// </remarks>
	[DataMember(Name = "summary")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public string Summary { get; set; }

	/// <remarks>
	/// Called long description in the Studio UX.
	/// </remarks>
	[DataMember(Name = "description")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public string Description { get; set; }

	[DataMember(Name = "image_url")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public string ImageLink { get; set; }

	[IgnoreDataMember]
	public Uri ImageUrl
	{
		get
		{
			return string.IsNullOrEmpty(this.ImageLink) ? null : new Uri(this.ImageLink);
		}
	}

	/// <summary>
	/// Gets or sets the content of the image during create or update operations.
	/// </summary>
	[DataMember(Name = "upload_image_data")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public UploadContent UploadImageData { get; set; }

	[DataMember(Name = "created_at")]
	[AuthorizePropertyEdit("Administrator")]
	public DateTimeOffset CreatedTime { get; set; }

	[DataMember(Name = "updated_at")]
	[AuthorizePropertyEdit("Administrator")]
	public DateTimeOffset LastModifiedTime { get; set; }

	/// <summary>
	/// Gets or sets the content of entity. This will always return the latest version of 
	/// the entity. For other versions query the history collection of the entity.
	/// </summary>
	[DataMember(Name = "content")]
	public Storage Content { get; set; }

	/// <summary>
	/// Gets or sets the content data during create operations.
	/// </summary>
	[DataMember(Name = "upload_content_data")]
	public UploadContent UploadContentData { get; set; }

	[DataMember(Name = "featured")]
	[AuthorizePropertyEdit("FeaturedContentAdministrator,Administrator")]
	public bool? IsFeatured { get; set; }

	[DataMember(Name = "hidden")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	public bool? IsHidden { get; set; }

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "tags")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public IList<string> Tags { get; set; }

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "algorithms")]
	[AuthorizePropertyEdit("MLPropertiesPublisher,Administrator")]
	[IntrinsicProperty]
	public IList<string> Algorithms { get; set; }

	[DataMember(Name = "author")]
	public Author Author { get; set; }

	[DataMember(Name = "rating")]
	public double? Rating { get; set; }

	[DataMember(Name = "comment_count")]
	[AuthorizePropertyEdit("UserActivityPublisher,Administrator")]
	public int? CommentCount { get; set; }

	[DataMember(Name = "download_count")]
	[AuthorizePropertyEdit("UserActivityPublisher,Administrator")]
	public int? DownloadCount { get; set; }

	[DataMember(Name = "view_count")]
	[AuthorizePropertyEdit("UserActivityPublisher,Administrator")]
	public int? ViewCount { get; set; }

	[DataMember(Name = "share_count")]
	[AuthorizePropertyEdit("UserActivityPublisher,Administrator")]
	public int? ShareCount { get; set; }

	/// <summary>
	/// Gets or sets the trending value. Higher value means more trending
	/// </summary>
	[DataMember(Name = "trending")]
	[AuthorizePropertyEdit("UserActivityPublisher,Administrator")]
	public double? Trending { get; set; }

	/// <summary>
	/// Gets or sets the quality value. Higher value means better quality
	/// </summary>
	[DataMember(Name = "quality")]
	[AuthorizePropertyEdit("FeaturedContentAdministrator,Administrator")]
	public double? Quality { get; set; }

	/// <summary>
	/// Gets or sets the freshness value. Higher value means more fresh
	/// </summary>
	[DataMember(Name = "freshness")]
	[AuthorizePropertyEdit("Administrator")]
	public double? Freshness { get; set; }

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "hyperlinks")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public IList<Hyperlink> Hyperlinks { get; set; }

	[DataMember(Name = "action_hyperlink")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public Hyperlink ActionHyperlink { get; set; }

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "slugs")]
	[AuthorizePropertyEdit("Administrator")]
	public IList<string> Slugs { get; set; }

	/// <remarks>
	/// This is a virtual property, calculated by the server during a GET for a single item.  It cannot be set from the REST API
	/// </remarks>
	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "in_collections")]
	public IList<Collection> InCollections { get; set; }
}


/// <summary>
/// Base class for all tenant based objects in the catalog.
/// </summary>
[DataContract]
public abstract class TenantItemBase : ItemBase
{
	[DataMember(Name = "tenant_id")]
	public string TenantId { get; set; }

	[DataMember(Name = "community_id")]
	public string CommunityId { get; set; }

	[DataMember(Name = "index_eid")]
	[AuthorizePropertyEdit("Administrator")]
	public string IndexEid { get; set; }
}


[DataContract]
public class Api : EntityBase
{
	public Api()
	{
		this.EntityType = EntityType.Api;
	}

	[DataMember(Name = "demo_hyperlink")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public Hyperlink DemoHyperlink { get; set; }

	[DataMember(Name = "quick_start_hyperlink")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public Hyperlink QuickStartHyperlink { get; set; }

	[DataMember(Name = "ml_api_type")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	public string MLApiType { get; set; }

	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "plans")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public IList<ApiPlan> Plans { get; set; } 
}

/// <summary>
/// Represents an api plan 
/// </summary>
[DataContract]
public class ApiPlan
{
	[DataMember(Name = "id")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	public string Id { get; set; }

	[DataMember(Name = "name")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public string Name { get; set; }

	[DataMember(Name = "description")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public string Description { get; set; }

	/// <summary>
	/// Is the plan free
	/// </summary>
	[DataMember(Name = "is_free")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public bool? IsFree { get; set; }
	
	/// <summary>
	/// Usually for paid offers, sign-up will happen outside of the gallery context
	/// </summary>
	[DataMember(Name = "action_hyperlink")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public Hyperlink ActionHyperlink { get; set; }

	/// <summary>
	/// List of quotas, associated with this plan 
	/// </summary>
	[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is required for de-serialization.")]
	[DataMember(Name = "quotas")]
	[AuthorizePropertyEdit("EntityOwner,EntityContributor,Administrator")]
	[IntrinsicProperty]
	public IList<ApiPlanQuota> Quotas { get; set; } 
}