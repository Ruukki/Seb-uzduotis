using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Model;

namespace DAL
{
    public class DbMethods
    {

        public T Save<T>(T model) where T : class, BaseModel
        {
            using (var context = new MyDbContext())
            {
                context.Set<T>().Attach(model);
                context.Set<T>().Add(model);

                context.SaveChanges();
                return model;
            }
        }

        public T Get<T>(Expression<Func<T, bool>> predicate, bool include = false)
            where T : class, BaseModel
        {
            T item = null;
            using (var context = new MyDbContext())
            {
                var set = context.Set<T>();
                if (include)
                {
                    var tables = GetTables(context);
                    var properties = typeof(T).GetProperties();

                    var tableNames = tables.Select(x => x.Name).Intersect(properties.Select(y => y.Name));
                    DbQuery<T> query = null;
                    foreach (var table in tableNames)
                    {
                        query = set.Include(table);
                    }
                    item = query?.FirstOrDefault(predicate);
                }
                else
                {
                    item = set.FirstOrDefault(predicate);
                }
                return item;
            }
        }

        public IList<T> GetList<T>(Expression<Func<T, bool>> predicate, bool include = false)
            where T : class, BaseModel
        {
            IList<T> item = null;
            using (var context = new MyDbContext())
            {
                var set = context.Set<T>();

                item = set.Where(predicate).ToList();
                return item;
            }
        }

        public IList<T> GetAll<T>(bool include = false)
            where T : class, BaseModel
        {
            IList<T> item = null;
            using (var context = new MyDbContext())
            {
                item = context.Set<T>().ToList();
            }
            return item;
        }

        private IEnumerable<EntityType> GetTables(IObjectContextAdapter context)
        {
            ObjectContext objContext = ((IObjectContextAdapter)context).ObjectContext;
            MetadataWorkspace workspace = objContext.MetadataWorkspace;
            IEnumerable<EntityType> tables = workspace.GetItems<EntityType>(DataSpace.SSpace);
            return tables;
        }

    }
}
