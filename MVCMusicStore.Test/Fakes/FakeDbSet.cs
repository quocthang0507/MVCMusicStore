using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace MVCMusicStore.Test.Fakes
{

	// This coe is mostly copy/pasted from an implementation posted here:
	// http://refactorthis.wordpress.com/2011/05/31/mock-faking-dbcontext-in-entity-framework-4-1-with-a-generic-repository/
	// http://refactorthis.wordpress.com/2011/11/30/generic-repository-fake-idbset-implementation-update-find-method-identity-key/


	public class FakeDbSet<T> : IDbSet<T> where T : class
	{
		HashSet<T> _data;
		IQueryable _query;

		public FakeDbSet()
		{
			GetKeyProperties();
			_data = new HashSet<T>();
			_query = _data.AsQueryable();
		}

		private List<PropertyInfo> _keyProperties;

		public virtual T Find(params object[] keyValues)
		{
			if (keyValues.Length != _keyProperties.Count)
				throw new ArgumentException("Incorrect number of keys passed to find method");

			IQueryable<T> keyQuery = this.AsQueryable<T>();
			for (int i = 0; i < keyValues.Length; i++)
			{
				var x = i; // nested linq
				keyQuery = keyQuery
				   .Where(entity => _keyProperties[x].GetValue(entity, null).Equals(keyValues[x]));
			}

			return keyQuery.SingleOrDefault();
		}

		private void GetKeyProperties()
		{
			_keyProperties = new List<PropertyInfo>();
			PropertyInfo[] properties = typeof(T).GetProperties();
			foreach (PropertyInfo property in properties)
			{
				foreach (Attribute attribute in property.GetCustomAttributes(true))
				{
					if (attribute is KeyAttribute)
					{
						_keyProperties.Add(property);
					}
				}
			}
		}

		public void Add(T item)
		{
			_data.Add(item);
		}

		public void Remove(T item)
		{
			_data.Remove(item);
		}

		public void Attach(T item)
		{
			_data.Add(item);
		}

		public void Detach(T item)
		{
			_data.Remove(item);
		}

		Type IQueryable.ElementType
		{
			get { return _query.ElementType; }
		}

		System.Linq.Expressions.Expression IQueryable.Expression
		{
			get { return _query.Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return _query.Provider; }
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		T IDbSet<T>.Add(T entity)
		{
			_data.Add(entity);
			return entity;
		}

		T IDbSet<T>.Attach(T entity)
		{
			_data.Add(entity);
			return entity;
		}

		public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
		{
			throw new NotImplementedException();
		}

		public virtual T Create()
		{
			throw new NotImplementedException();
		}

		public virtual System.Collections.ObjectModel.ObservableCollection<T> Local
		{
			get { throw new NotImplementedException(); }
		}

		T IDbSet<T>.Remove(T entity)
		{
			_data.Remove(entity);
			return entity;
		}
	}
}
