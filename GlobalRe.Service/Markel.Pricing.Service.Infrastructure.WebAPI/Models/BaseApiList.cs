using Markel.Pricing.Service.Infrastructure.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    public abstract class BaseApiList<BLL_CONTAINER, BLL_ITEM, ITEM> : BaseApiModel<BLL_CONTAINER>, IList<ITEM>
        where BLL_CONTAINER : BaseMessageEntity
        where BLL_ITEM : BaseMessageEntity
        where ITEM : BaseApiModel<BLL_ITEM>
    {
        protected IList<ITEM> Items;

        public BaseApiList() { Items = new List<ITEM>(); }

        public BaseApiList(BLL_CONTAINER model) : base(model) { }

        protected override bool HasValue()
        {
            return Items != null;
        }

        public override BLL_CONTAINER ToBLLModel()
        {
            // Validations
            if (!HasValue()) throw new NoContentAPIException();

            // Conversion
            BLL_CONTAINER model = (BLL_CONTAINER)Activator.CreateInstance(typeof(BLL_CONTAINER), new object[] { Items.Select(i => i.ToBLLModel()).ToList() });
            return model;
        }

        #region IList

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return Items.IsReadOnly;
            }
        }

        public ITEM this[int index]
        {
            get
            {
                return Items[index];
            }

            set
            {
                Items[index] = value;
            }
        }

        public int IndexOf(ITEM item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, ITEM item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public void Add(ITEM item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(ITEM item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(ITEM[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITEM item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<ITEM> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion IList
    }
}
