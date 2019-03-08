using LinqKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class EFExternsion
    {
        /// <summary>
        /// Generic wrapper for handling multiple result sets for EF. At a minimum, all properties of the specified 
        /// type must be returned in the resultset; otherwise, an exception will be thrown.
        /// 
        /// var results = new PricingDbContext()
        ///             .MultipleResults("sp_PricingDetails 1")
        ///             .With<PricingAnalysis>()
        ///             .With<PricingAnalysisLayer>()
        ///             .With<UnderlyingCoverage>()
        ///             .Execute();
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="storedProcedure">The stored procedure name with any parameters specified.</param>
        /// <returns></returns>
        public static MultipleResultSetWrapper MultipleResults(this DbContext db, string storedProcedure)
        {
            return new MultipleResultSetWrapper(db, storedProcedure);
        }

        public class MultipleResultSetWrapper
        {
            private readonly DbContext _db;
            private readonly string _storedProcedure;
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> _resultSets;

            public MultipleResultSetWrapper(DbContext db, string storedProcedure)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                _resultSets.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }

            public List<IEnumerable> Execute(params SqlParameter[] parameters)
            {
                var results = new List<IEnumerable>();

                using (var connection = _db.Database.Connection)
                {
                    connection.Open();                    
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    //command.CommandText = "EXEC " + _storedProcedure;
                    command.CommandText =  _storedProcedure;

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        command.Parameters.Add(parameters[i]);
                    }

                    if (_db.Database.CommandTimeout.HasValue)
                        command.CommandTimeout = _db.Database.CommandTimeout.GetValueOrDefault();

                    using (var reader = command.ExecuteReader())
                    {
                        var adapter = ((IObjectContextAdapter)_db);
                        foreach (var resultSet in _resultSets)
                        {
                            results.Add(resultSet(adapter, reader));
                            reader.NextResult();
                        }
                    }

                    return results;
                }
            }
        }

        public static Expression<Func<T, bool>> AndIf<T>(this ExpressionStarter<T> expression, Expression<Func<T, bool>> expr2)
        {
            if (expr2 != null)
            {
                return expression.And(expr2);
            }

            return expression;
        }

        /// <summary>
        /// Finds the primary key names for specified database type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCtx">The database context.</param>
        /// <returns></returns>
        public static string[] PrimaryKeyNames<T>(this DbContext dbCtx) where T : class
        {
            var objectSet = ((IObjectContextAdapter)dbCtx).ObjectContext.CreateObjectSet<T>();
            var keyNames = objectSet.EntitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();
            return keyNames;
        }

        /// <summary>
        /// Finds the primary key data types for specified database type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCtx">The database context.</param>
        /// <returns></returns>
        public static Type[] PrimaryKeyTypes<T>(this DbContext dbCtx) where T : class
        {
            var keyNames = dbCtx.PrimaryKeyNames<T>();
            var types = keyNames.Select(keyName => typeof(T).GetProperty(keyName).PropertyType).ToArray();
            return types;
        }

        /// <summary>
        /// Finds the primary key default values for specified database type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCtx">The database context.</param>
        /// <returns></returns>
        public static object[] PrimaryKeyDefaultValues<T>(this DbContext dbCtx) where T : class
        {
            var types = dbCtx.PrimaryKeyTypes<T>();
            var defaultValues = types.Select(type => type.IsValueType ? Activator.CreateInstance(type) : null).ToArray();
            return defaultValues;
        }

        public static IEnumerable<string> GetMappedTableName(this DbContext context, Type type)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            // Find the storage entity sets (tables) that the entity is mapped
            var tables = mapping
                .EntityTypeMappings.Single()
                .Fragments;

            // Return the table name from the storage entity set
            return tables.Select(f => (string)f.StoreEntitySet.MetadataProperties["Table"].Value ?? f.StoreEntitySet.Name);
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> itemList, Dictionary<string, int> fieldLength = null)
        {
            Type type = typeof(T);
            DataTable dataTable = new DataTable(type.Name);

            // Columns
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                string fieldName = propertyInfo.Name;
                Type propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                DataColumn dataColumn = new DataColumn(fieldName, propertyType);
                if (fieldLength?.ContainsKey(fieldName) == true)
                {
                    dataColumn.MaxLength = fieldLength[fieldName];
                }

                dataTable.Columns.Add(dataColumn);
            }

            if (itemList == null) return dataTable;

            // Rows
            foreach (T item in itemList)
            {
                DataRow row = dataTable.NewRow();
                foreach (DataColumn column in dataTable.Columns)
                {
                    row[column.ColumnName] = type.GetProperty(column.ColumnName).GetValue(item) ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}