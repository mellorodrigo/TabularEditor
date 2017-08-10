
// Code generated by a template
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using TabularEditor.PropertyGridUI;
using TabularEditor.UndoFramework;
using System.Drawing.Design;
using TOM = Microsoft.AnalysisServices.Tabular;
namespace TabularEditor.TOMWrapper
{
  
	/// <summary>
///             Defines an open connection to an external data source for import, refresh, or DirectQuery operations on a Tabular <see cref="T:TabularEditor.TOMWrapper.Model" />.
///             </summary>
	[TypeConverter(typeof(DynamicPropertyConverter))]
	public abstract partial class DataSource: TabularNamedObject
			, IDescriptionObject
			, IAnnotationObject
	{
	    protected internal new TOM.DataSource MetadataObject { get { return base.MetadataObject as TOM.DataSource; } internal set { base.MetadataObject = value; } }

        [Browsable(true),NoMultiselect,Category("Translations and Perspectives"),Description("The collection of Annotations on this object."),Editor(typeof(AnnotationCollectionEditor), typeof(UITypeEditor))]
		public AnnotationCollection Annotations { get; private set; }
		public string GetAnnotation(int index) {
			return MetadataObject.Annotations[index].Value;
		}
		public string GetAnnotation(string name) {
		    return MetadataObject.Annotations.ContainsName(name) ? MetadataObject.Annotations[name].Value : null;
		}
		public void SetAnnotation(int index, string value, bool undoable = true) {
			var name = MetadataObject.Annotations[index].Name;
			SetAnnotation(name, value, undoable);
		}
		public string GetNewAnnotationName() {
			return MetadataObject.Annotations.GetNewName("New Annotation");
		}
		public void SetAnnotation(string name, string value, bool undoable = true) {
			if(name == null) name = GetNewAnnotationName();

			if(value == null) {
				// Remove annotation if set to null:
				RemoveAnnotation(name, undoable);
				return;
			}

			if(GetAnnotation(name) == value) return;
			bool undoable2 = true;
			bool cancel = false;
			OnPropertyChanging(Properties.ANNOTATIONS, name + ":" + value, ref undoable2, ref cancel);
			if (cancel) return;

			if(MetadataObject.Annotations.Contains(name)) {
				// Change existing annotation:
				var oldValue = GetAnnotation(name);
				MetadataObject.Annotations[name].Value = value;
				if (undoable) Handler.UndoManager.Add(new UndoAnnotationAction(this, name, value, oldValue));
				OnPropertyChanged(Properties.ANNOTATIONS, name + ":" + oldValue, name + ":" + value);
			} else {
				// Add new annotation:
				MetadataObject.Annotations.Add(new TOM.Annotation{ Name = name, Value = value });
				if (undoable) Handler.UndoManager.Add(new UndoAnnotationAction(this, name, value, null));
				OnPropertyChanged(Properties.ANNOTATIONS, null, name + ":" + value);
			}

		}
		public void RemoveAnnotation(string name, bool undoable = true) {
			if(MetadataObject.Annotations.Contains(name)) {
				// Get current value:
				bool undoable2 = true;
				bool cancel = false;
				OnPropertyChanging(Properties.ANNOTATIONS, name + ":" + GetAnnotation(name), ref undoable2, ref cancel);
				if (cancel) return;

				var oldValue = MetadataObject.Annotations[name].Value;
				MetadataObject.Annotations.Remove(name);

				// Undo-handling:
				if (undoable) Handler.UndoManager.Add(new UndoAnnotationAction(this, name, null, oldValue));
				OnPropertyChanged(Properties.ANNOTATIONS, name + ":" + oldValue, null);
			}
		}
		public int GetAnnotationsCount() {
			return MetadataObject.Annotations.Count;
		}
		public IEnumerable<string> GetAnnotations() {
			return MetadataObject.Annotations.Select(a => a.Name);
		}

		/// <summary>
///             Gets or sets an accessor specifying the Description property of the body of the object.
///             </summary><returns>An String accessor specifying the Description property of the body of the object.</returns>
		[DisplayName("Description")]
		[Category("Basic"),Description(@"Gets or sets an accessor specifying the Description property of the body of the object."),IntelliSense("The Description of this DataSource.")][Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Description {
			get {
			    return MetadataObject.Description;
			}
			set {
				var oldValue = Description;
				if (oldValue == value) return;
				bool undoable = true;
				bool cancel = false;
				OnPropertyChanging(Properties.DESCRIPTION, value, ref undoable, ref cancel);
				if (cancel) return;
				MetadataObject.Description = value;
				if(undoable) Handler.UndoManager.Add(new UndoPropertyChangedAction(this, Properties.DESCRIPTION, oldValue, value));
				OnPropertyChanged(Properties.DESCRIPTION, oldValue, value);
			}
		}
		private bool ShouldSerializeDescription() { return false; }
/// <summary>Gets or sets an accessor specifying the type of data source providing data to the object.</summary><returns>An accessor specifying the type of data source providing data to the object.</returns>
		[DisplayName("Type")]
		[Category("Other"),Description(@"Gets or sets an accessor specifying the type of data source providing data to the object."),IntelliSense("The Type of this DataSource.")]
		public TOM.DataSourceType Type {
			get {
			    return MetadataObject.Type;
			}
			
		}
		private bool ShouldSerializeType() { return false; }

		public Model Parent { 
			get {
				return Handler.WrapperLookup[MetadataObject.Parent] as Model;
			}
		}



		/// <summary>
		/// CTOR - only called from static factory methods on the class
		/// </summary>
		protected DataSource(TOM.DataSource metadataObject, bool init = true) : base(metadataObject)
		{
			if(init) InternalInit();
		}

		private void InternalInit()
		{
			
			// Create indexer for annotations:
			Annotations = new AnnotationCollection(this);
		}



		internal override void Undelete(ITabularObjectCollection collection) {
			base.Undelete(collection);
			Reinit();
			ReapplyReferences();
		}

		public override bool Browsable(string propertyName) {
			switch (propertyName) {
				case Properties.PARENT:
					return false;
				
				default:
					return base.Browsable(propertyName);
			}
		}

    }


	/// <summary>
	/// Collection class for DataSource. Provides convenient properties for setting a property on multiple objects at once.
	/// </summary>
	public partial class DataSourceCollection: TabularObjectCollection<DataSource, TOM.DataSource, TOM.Model>
	{
		public override TabularNamedObject Parent { get { return Model; } }
		public DataSourceCollection(string collectionName, TOM.DataSourceCollection metadataObjectCollection, Model parent) : base(collectionName, metadataObjectCollection)
		{
		}

		internal override void Reinit() {
			var ixOffset = 0;
			for(int i = 0; i < Count; i++) {
				var item = this[i];
				Handler.WrapperLookup.Remove(item.MetadataObject);
				item.MetadataObject = Model.MetadataObject.DataSources[i + ixOffset] as TOM.DataSource;
				Handler.WrapperLookup.Add(item.MetadataObject, item);
				item.Collection = this;
			}
			MetadataObjectCollection = Model.MetadataObject.DataSources;
			foreach(var item in this) item.Reinit();
		}

		internal override void ReapplyReferences() {
			foreach(var item in this) item.ReapplyReferences();
		}

		/// <summary>
		/// Calling this method will populate the DataSourceCollection with objects based on the MetadataObjects in the corresponding MetadataObjectCollection.
		/// </summary>
		public override void CreateChildrenFromMetadata()
		{
			// Construct child objects (they are automatically added to the Handler's WrapperLookup dictionary):
			foreach(var obj in MetadataObjectCollection) {
				switch((obj as TOM.DataSource).Type) {
					case TOM.DataSourceType.Provider: ProviderDataSource.CreateFromMetadata(obj as TOM.ProviderDataSource).Collection = this; break;
#if CL1400
					case TOM.DataSourceType.Structured: StructuredDataSource.CreateFromMetadata(obj as TOM.StructuredDataSource).Collection = this; break;
#endif
				}
			}
		}

		[Description("Sets the Description property of all objects in the collection at once.")]
		public string Description {
			set {
				if(Handler == null) return;
				Handler.UndoManager.BeginBatch(UndoPropertyChangedAction.GetActionNameFromProperty("Description"));
				this.ToList().ForEach(item => { item.Description = value; });
				Handler.UndoManager.EndBatch();
			}
		}

		public override string ToString() {
			return string.Format("({0} {1})", Count, (Count == 1 ? "DataSource" : "DataSources").ToLower());
		}
	}
}
