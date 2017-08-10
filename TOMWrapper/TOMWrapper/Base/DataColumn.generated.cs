
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
///             Represents a column in a Table that gets data from an external data source.
///             </summary>
	[TypeConverter(typeof(DynamicPropertyConverter))]
	public partial class DataColumn: Column
			, IClonableObject
	{
	    protected internal new TOM.DataColumn MetadataObject { get { return base.MetadataObject as TOM.DataColumn; } internal set { base.MetadataObject = value; } }

/// <summary>Gets or sets the SourceColumn property of the current DataColumn object.</summary><returns>A String containing the SourceColumn property of the current DataColumn object.</returns>
		[DisplayName("Source Column")]
		[Category("Options"),Description(@"Gets or sets the SourceColumn property of the current DataColumn object."),IntelliSense("The Source Column of this DataColumn.")]
		public string SourceColumn {
			get {
			    return MetadataObject.SourceColumn;
			}
			set {
				var oldValue = SourceColumn;
				if (oldValue == value) return;
				bool undoable = true;
				bool cancel = false;
				OnPropertyChanging(Properties.SOURCECOLUMN, value, ref undoable, ref cancel);
				if (cancel) return;
				MetadataObject.SourceColumn = value;
				if(undoable) Handler.UndoManager.Add(new UndoPropertyChangedAction(this, Properties.SOURCECOLUMN, oldValue, value));
				OnPropertyChanged(Properties.SOURCECOLUMN, oldValue, value);
			}
		}
		private bool ShouldSerializeSourceColumn() { return false; }


		public static DataColumn CreateFromMetadata(TOM.DataColumn metadataObject, bool init = true) {
			var obj = new DataColumn(metadataObject, init);
			if(init) 
			{
				obj.InternalInit();
				obj.Init();
			}
			return obj;
		}


		/// <summary>
		/// Creates a new DataColumn and adds it to the parent Table.
		/// Also creates the underlying metadataobject and adds it to the TOM tree.
		/// </summary>
		public static DataColumn CreateNew(Table parent, string name = null)
		{
			var metadataObject = new TOM.DataColumn();
			metadataObject.Name = parent.Columns.GetNewName(string.IsNullOrWhiteSpace(name) ? "New DataColumn" : name);

			var obj = new DataColumn(metadataObject);

			parent.Columns.Add(obj);
			
			obj.Init();

			return obj;
		}


		/// <summary>
		/// Creates an exact copy of this DataColumn object.
		/// </summary>
		public DataColumn Clone(string newName = null, bool includeTranslations = true, Table newParent = null) {
		    Handler.BeginUpdate("Clone DataColumn");

			// Create a clone of the underlying metadataobject:
			var tom = MetadataObject.Clone() as TOM.DataColumn;


			// Assign a new, unique name:
			tom.Name = Parent.Columns.MetadataObjectCollection.GetNewName(string.IsNullOrEmpty(newName) ? tom.Name + " copy" : newName);
				
			// Create the TOM Wrapper object, representing the metadataobject (but don't init until after we add it to the parent):
			var obj = CreateFromMetadata(tom, false);

			// Add the object to the parent collection:
			if(newParent != null) 
				newParent.Columns.Add(obj);
			else
    			Parent.Columns.Add(obj);

			obj.InternalInit();
			obj.Init();

			// Copy translations, if applicable:
			if(includeTranslations) {
				// TODO: Copy translations of child objects

				obj.TranslatedNames.CopyFrom(TranslatedNames);
				obj.TranslatedDescriptions.CopyFrom(TranslatedDescriptions);
				obj.TranslatedDisplayFolders.CopyFrom(TranslatedDisplayFolders);
			}
				
			// Copy perspectives:
			obj.InPerspective.CopyFrom(InPerspective);

            Handler.EndUpdate();

            return obj;
		}

		TabularNamedObject IClonableObject.Clone(string newName, bool includeTranslations, TabularNamedObject newParent) 
		{
			return Clone(newName);
		}

	
        internal override void RenewMetadataObject()
        {
            Handler.WrapperLookup.Remove(MetadataObject);
            MetadataObject = MetadataObject.Clone() as TOM.DataColumn;
            Handler.WrapperLookup.Add(MetadataObject, this);
        }

		public Table Parent { 
			get {
				return Handler.WrapperLookup[MetadataObject.Parent] as Table;
			}
		}



		/// <summary>
		/// CTOR - only called from static factory methods on the class
		/// </summary>
		protected DataColumn(TOM.DataColumn metadataObject, bool init = true) : base(metadataObject)
		{
			if(init) InternalInit();
		}

		private void InternalInit()
		{
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

}
