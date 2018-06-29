using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace SimpleLibrary.API.Models
{
    public abstract class ModelBase<T>
        where T : new()
    {
        public ModelBase()
        {
            Entity = new T();
        }

        public ModelBase(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity in base model class is null");

            Entity = entity;
        }

        #region Properities

        protected virtual T Entity { get; set; }

        #endregion


        #region Methods

        protected virtual void SetPropertyValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Entity.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Entity);

            if (!Equals(currentValue, value))
                propertyInfo.SetValue(Entity, value);
        }


        protected virtual TValue GetPropertyValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)Entity.GetType().GetProperty(propertyName).GetValue(Entity);
        }


        protected virtual void RegisterCollection<TModel, TEntity>(ObservableCollection<TModel> modelCollection, ICollection<TEntity> entityCollection)
        where TModel : ModelBase<TEntity> where TEntity : new()
        {
            modelCollection.CollectionChanged += (s, e) =>
            {
                entityCollection.Clear();

                foreach (var item in modelCollection)
                {
                    entityCollection.Add(item.Entity);
                }
            };
        }

        public T GetEntity() { return Entity; }
        #endregion


    }
}
