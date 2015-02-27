using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using XPence.DbAccess.Interfaces;
using XPence.Framework.XmlSerialization;
using XPence.Models;
using DataType = XPence.Models.DataType;

namespace XPence.DbAccess.Implementations
{
    /// <summary>
    ///     SimulinkContext is responsible for interacting with data as objects and
    ///     manages the entity objects during run time, which includes populating objects
    ///     with data from the SQLite database, change tracking, and persisting data to the database.
    /// </summary>
    public class SimulinkContext : DbContext, IContext
    {
        #region fields

        /// <summary>
        ///     Flag to determines whether the attribute is enabled for auditing.
        /// </summary>
        private readonly bool isAuditEnabled;

        /// <summary>
        ///     Prepare the list of entries for the undo feature.
        ///     List of snap data which contains the entity state and original value.
        /// </summary>
        private readonly Stack<IEnumerable<SnapData>> undoList = new Stack<IEnumerable<SnapData>>();

        /// <summary>
        ///     Prepare the list of entries for the redo feature.
        ///     List of snap data which contains the entity state and original value.
        /// </summary>
        private readonly Stack<IEnumerable<SnapData>> redoList = new Stack<IEnumerable<SnapData>>();

        /// <summary>
        ///     Prepare the list of entries for the revert feature.
        ///     List of snap data which contains the entity state and original value.
        /// </summary>
        private readonly Stack<IEnumerable<SnapData>> revertList = new Stack<IEnumerable<SnapData>>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimulinkContext" /> class.
        ///     It is needed for the context detect changes  and Enabling for the auditing.
        /// </summary>
        public SimulinkContext() 
        {
            this.isAuditEnabled = true;
            ObjectContext.SavingChanges += this.OnSavingChanges;
        }

        #endregion

        #region Model builder

        /// <summary>
        ///     Gets or sets the collection of all Audits in the context.
        /// </summary>
        public DbSet<Audit> Audits { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Components in the context.
        /// </summary>
        public DbSet<Component> Components { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ComponentDependencies in the context.
        /// </summary>
        public DbSet<ComponentDependency> ComponentDependencies { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ComponentFunctions in the context.
        /// </summary>
        public DbSet<ComponentFunction> ComponentFunctions { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ComponentRoles in the context.
        /// </summary>
        public DbSet<ComponentRole> ComponentRoles { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ComponentStates in the context.
        /// </summary>
        public DbSet<ComponentState> ComponentStates { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all DataMappings in the context.
        /// </summary>
        public DbSet<DataMapping> DataMappings { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all DataTypes in the context.
        /// </summary>
        public DbSet<DataType> DataTypes { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Directions in the context.
        /// </summary>
        public DbSet<Direction> Directions { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Components in the context.
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ExecutionModes in the context.
        /// </summary>
        public DbSet<ExecutionMode> ExecutionModes { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all FunctionInstances in the context.
        /// </summary>
        public DbSet<FunctionInstance> FunctionInstances { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all FunctionParameters in the context.
        /// </summary>
        public DbSet<FunctionParameter> FunctionParameters { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Groups in the context.
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all InstanceRoles in the context.
        /// </summary>
        public DbSet<InstanceRole> InstanceRoles { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Layers in the context.
        /// </summary>
        public DbSet<Layer> Layers { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ListParameters in the context.
        /// </summary>
        public DbSet<ListParameter> ListParameters { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all MappingModes in the context.
        /// </summary>
        public DbSet<MappingMode> MappingModes { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all MappingTriggers in the context.
        /// </summary>
        public DbSet<MappingTrigger> MappingTriggers { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Nodes in the context.
        /// </summary>
        public DbSet<Node> Nodes { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all NodeRoles in the context.
        /// </summary>
        public DbSet<NodeRole> NodeRoles { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Parameters in the context.
        /// </summary>
        public DbSet<Parameter> Parameters { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ParameterAttributes in the context.
        /// </summary>
        public DbSet<ParameterAttribute> ParameterAttributes { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ParameterLists in the context.
        /// </summary>
        public DbSet<ParameterList> ParameterLists { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all ParameterMappings in the context.
        /// </summary>
        public DbSet<ParameterMapping> ParameterMappings { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Roles in the context.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        ///     Gets or sets the collection of all Transformations in the context.
        /// </summary>
        public DbSet<Transformation> Transformations { get; set; }

        /// <summary>
        ///     Gets facilities for querying and working with entity data as objects.
        ///     It is necessary to raise event when changes are saved to the data source.
        /// </summary>
        private ObjectContext ObjectContext
        {
            get { return (this as IObjectContextAdapter).ObjectContext; }
        }

        #endregion

        #region IContext Implementation

        /// <summary>
        ///     Take snapshot of each changes by storing entries.
        ///     Allows to backward and forward any changes with the DBContext.
        /// </summary>
        public void TakeSnapshot()
        {
            this.redoList.Clear();
            if (!Configuration.AutoDetectChangesEnabled)
            {
                ChangeTracker.DetectChanges();
            }

            IEnumerable<DbEntityEntry> entries =
                ChangeTracker.Entries()
                    .Where(
                        e =>
                            e.State == EntityState.Added || e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted);
            {
                var entrySnapList = new List<SnapData>();
                foreach (DbEntityEntry entry in entries)
                {
                    if (entry.Entity != null && !this.undoList.Any(v => v.Any(s => s.Entity.Equals(entry.Entity))))
                    {
                        entrySnapList.Add(new SnapData
                        {
                            OrginalValue = (entry.State == EntityState.Deleted || entry.State == EntityState.Modified)
                                ? (entry.OriginalValues != null)
                                    ? entry.OriginalValues.ToObject()
                                    : entry.GetDatabaseValues()
                                : null,
                            Value =
                                (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                                    ? entry.CurrentValues.ToObject()
                                    : null,
                            State = entry.State,
                            Entity = entry.Entity
                        });
                    }
                }

                if (entrySnapList.Count > 0)
                {
                    this.undoList.Push(entrySnapList.AsEnumerable());
                    this.revertList.Push(entrySnapList.AsEnumerable());
                }
            }
        }

        /// <summary>
        ///     Undo the last action, which is performed.
        /// </summary>
        public void Undo()
        {
            if (this.CanUndo())
            {
                bool previousContiguration = Configuration.AutoDetectChangesEnabled;
                Configuration.AutoDetectChangesEnabled = false;
                IEnumerable<SnapData> entries = this.undoList.Pop();
                {
                    SnapData[] snapDatas = entries as SnapData[] ?? entries.ToArray();
                    this.redoList.Push(snapDatas);
                    foreach (SnapData snap in snapDatas)
                    {
                        DbEntityEntry currentEntry = Entry(snap.Entity);
                        if (snap.State == EntityState.Modified)
                        {
                            currentEntry.CurrentValues.SetValues(snap.OrginalValue);
                            DbPropertyValues databaseValues = currentEntry.GetDatabaseValues();
                            currentEntry.OriginalValues.SetValues(databaseValues);
                        }
                        else if (snap.State == EntityState.Deleted)
                        {
                            DbPropertyValues databaseValues = currentEntry.GetDatabaseValues();
                            currentEntry.OriginalValues.SetValues(databaseValues);
                        }

                        currentEntry.State = EntityState.Unchanged;
                    }
                }

                Configuration.AutoDetectChangesEnabled = previousContiguration;
            }
        }

        /// <summary>
        ///     Redo undoes the last Undo action.
        /// </summary>
        public void Redo()
        {
            if (this.CanRedo())
            {
                bool previousContiguration = Configuration.AutoDetectChangesEnabled;
                Configuration.AutoDetectChangesEnabled = false;
                IEnumerable<SnapData> entries = this.redoList.Pop();
                SnapData[] snapDatas = entries as SnapData[] ?? entries.ToArray();
                if (null != entries && snapDatas.Any())
                {
                    foreach (SnapData snap in snapDatas)
                    {
                        DbEntityEntry currentEntry = Entry(snap.Entity);

                        if (snap.State == EntityState.Modified)
                        {
                            currentEntry.CurrentValues.SetValues(snap.Value);
                            currentEntry.OriginalValues.SetValues(snap.OrginalValue);
                        }
                        else if (snap.State == EntityState.Deleted)
                        {
                            DbPropertyValues databaseValues = currentEntry.GetDatabaseValues();
                            currentEntry.OriginalValues.SetValues(databaseValues);
                        }

                        currentEntry.State = snap.State;
                    }
                }

                Configuration.AutoDetectChangesEnabled = previousContiguration;
            }
        }

        /// <summary>
        ///     Undo the last saved changes from the database.
        /// </summary>
        public void Revert()
        {
            if (this.CanRevert())
            {
                bool previousContiguration = Configuration.AutoDetectChangesEnabled;
                Configuration.AutoDetectChangesEnabled = false;
                IEnumerable<SnapData> entries = this.revertList.Pop();
                {
                    SnapData[] snapDatas = entries as SnapData[] ?? entries.ToArray();
                    foreach (SnapData snap in snapDatas)
                    {
                        DbEntityEntry currentEntry = Entry(snap.Entity);
                        Type entityType = currentEntry.Entity.GetType();

                        if (snap.State == EntityState.Modified)
                        {
                            currentEntry.CurrentValues.SetValues(snap.OrginalValue);
                            DbPropertyValues databaseValues = currentEntry.GetDatabaseValues();
                            currentEntry.OriginalValues.SetValues(databaseValues);
                            this.SaveChanges();
                        }
                        else if (snap.State == EntityState.Deleted)
                        {
                            Set(entityType).Add(currentEntry.Entity);
                            currentEntry.State = EntityState.Added;
                            this.SaveChanges();
                        }
                        else if (snap.State == EntityState.Added)
                        {
                            Set(entityType).Remove(currentEntry.Entity);
                            currentEntry.State = EntityState.Deleted;
                            this.SaveChanges();
                        }

                        currentEntry.State = EntityState.Unchanged;
                    }
                }

                Configuration.AutoDetectChangesEnabled = previousContiguration;

                this.ClearRevertList();
            }
        }

        /// <summary>
        ///     Can execute the undo feature.
        /// </summary>
        /// <returns>True or false</returns>
        public bool CanUndo()
        {
            return this.undoList.Count > 0;
        }

        /// <summary>
        ///     Can execute the redo feature.
        /// </summary>
        /// <returns>True or false</returns>
        public bool CanRedo()
        {
            return this.redoList.Count > 0;
        }

        /// <summary>
        ///     Can execute the revert feature.
        /// </summary>
        /// <returns>True or false.</returns>
        public bool CanRevert()
        {
            return this.revertList.Count > 0;
        }

        /// <summary>
        ///     Commit all changes
        /// </summary>
        /// <returns>
        ///     1 when the changes could be saved successfully or
        ///     0 when the changes could not be saved successfully
        /// </returns>
        public int Commit()
        {
            if (ChangeTracker.Entries().Any(IsChanged))
            {
                int result = SaveChanges();
                this.ClearSnapshots();
                return result;
            }

            return 0;
        }

        /// <summary>
        ///     This method is called when the model for a derived context has been initialized,
        ///     but before the model has been locked down and used to initialize the context.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasMany(t => t.Components).WithMany(t => t.Roles).Map(m =>
            {
                m.ToTable("Component_role");
                m.MapLeftKey("Component");
                m.MapRightKey("Role");
            });

            modelBuilder.Entity<Role>().HasMany(t => t.Nodes).WithMany(t => t.Roles).Map(m =>
            {
                m.ToTable("Node_Role");
                m.MapLeftKey("Node");
                m.MapRightKey("Role");
            });
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        ///     Gets the entity change state.
        /// </summary>
        /// <param name="entity">The Entity properties.</param>
        /// <returns>True or false.</returns>
        private static bool IsChanged(DbEntityEntry entity)
        {
            return IsStateEqual(entity, EntityState.Added) ||
                   IsStateEqual(entity, EntityState.Deleted) ||
                   IsStateEqual(entity, EntityState.Modified);
        }

        /// <summary>
        ///     Check the entity state.
        /// </summary>
        /// <param name="entity">The Entity properties.</param>
        /// <param name="state">State of the changes.</param>
        /// <returns>True or false.</returns>
        private static bool IsStateEqual(DbEntityEntry entity, EntityState state)
        {
            return (entity.State & state) == state;
        }

        /// <summary>
        ///     Clear the revert list.
        /// </summary>
        private void ClearRevertList()
        {
            this.revertList.Clear();
        }

        /// <summary>
        ///     clear all redo and undo lists.
        /// </summary>
        private void ClearSnapshots()
        {
            this.redoList.Clear();
            this.undoList.Clear();
        }

        #endregion

        #region AuditLog Implementation

        /// <summary>
        ///     SaveChanges event handler.
        ///     Persists all Audits updates to the data source.
        /// </summary>
        /// <param name="sender">The object sender that raised the event.</param>
        /// <param name="e">The event argument of the object and contains the event data.</param>
        private void OnSavingChanges(object sender, EventArgs e)
        {
            if (this.isAuditEnabled)
            {
                IEnumerable<DbEntityEntry> changeEntries =
                    ChangeTracker.Entries().Where(p => p.State == EntityState.Added
                                                       || p.State == EntityState.Deleted
                                                       || p.State == EntityState.Modified);

                foreach (DbEntityEntry entity in changeEntries)
                {
                    foreach (Audit audit in this.CreateAuditRecordsForChanges(entity))
                    {
                        this.Audits.Add(audit);
                    }
                }
            }
        }

        /// <summary>
        ///     Create audit records for the changes.
        /// </summary>
        /// <param name="entry">
        ///     Enables to access entity state,
        ///     current and original values of all the property of the given entity.
        /// </param>
        /// <returns>List of the Audit entries</returns>
        private IEnumerable<Audit> CreateAuditRecordsForChanges(DbEntityEntry entry)
        {
            var result = new List<Audit>();
            DateTime auditTime = DateTime.UtcNow;

            // Get the Table name by attribute
            var tableAttr =
                entry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as
                    TableAttribute;
            string tableName = tableAttr != null ? tableAttr.Name : entry.Entity.GetType().Name;

            // Find Primiray key.
            string keyName =
                entry.Entity.GetType()
                    .GetProperties()
                    .Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any())
                    .Name;

            if (entry.State == EntityState.Added)
            {
                result.Add(new Audit
                {
                    AuditDateInUtc = auditTime,
                    AuditState = AuditState.Added,
                    TableName = tableName,
                    RecordId = entry.CurrentValues.GetValue<object>(keyName).ToString(),
                    NewValue = DataSerializer.EntityToXmlString(entry.CurrentValues.ToObject())
                });
            }
            else if (entry.State == EntityState.Deleted)
            {
                result.Add(new Audit
                {
                    AuditDateInUtc = auditTime,
                    AuditState = AuditState.Deleted,
                    TableName = tableName,
                    RecordId = entry.OriginalValues.GetValue<object>(keyName).ToString(),
                    NewValue = DataSerializer.EntityToXmlString(entry.OriginalValues.ToObject().ToString())
                });
            }
            else if (entry.State == EntityState.Modified)
            {
                foreach (string propertyName in entry.OriginalValues.PropertyNames)
                {
                    if (!object.Equals(entry.OriginalValues.GetValue<object>(propertyName), entry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        result.Add(new Audit
                        {
                            AuditDateInUtc = auditTime,
                            AuditState = AuditState.Modified,
                            TableName = tableName,
                            RecordId = entry.OriginalValues.GetValue<object>(keyName).ToString(),
                            ColumnName = propertyName,
                            OriginalValue = entry.OriginalValues.GetValue<object>(propertyName) == null
                                ? null
                                : entry.OriginalValues.GetValue<object>(propertyName).ToString(),
                            NewValue = entry.CurrentValues.GetValue<object>(propertyName) == null
                                ? null
                                : entry.CurrentValues.GetValue<object>(propertyName).ToString()
                        });
                    }
                }
            }

            return result;
        }

        #endregion
    }
}