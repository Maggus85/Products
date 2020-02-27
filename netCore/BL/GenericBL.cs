using BL.Data;
using IBL;
using IBL.Data;
using IDAL;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BL
{
    public abstract class GenericBL<TKey, TObject>: IGenericBL<TKey, TObject> where TObject : class
    {
        protected abstract  PropertyInfo IdField { get; }
        IGenericDAL<TKey, TObject> _data;
        public GenericBL(IGenericDAL<TKey,TObject> data)
        {
            data.IdField = IdField;
            _data = data;
        }
        public virtual IResponse<TKey> Add(TObject obj)
        {
            var validationResult = ValidateEntity(obj);
            if (validationResult.Item1)
            {
                var added = _data.Create(obj);
                if (added != null)
                {
                    return new Response<TKey>(ActionStatus.Success, (TKey)IdField.GetValue(added));
                }

                return new Response<TKey>(ActionStatus.Error);
            }
            return new Response<TKey>(ActionStatus.ValidationError, default(TKey), validationResult.Item2);

        }

        public virtual IResponse<bool> Delete(TKey id)
        {
            try
            {
                var existing = Get(id);
                if (existing.Data == null)
                {
                    return new Response<bool>(ActionStatus.ValidationError, false, $"Cannot found object with id {id}");
                }

                bool result= _data.Delete(id);
                if(result)
                {
                    return new Response<bool>(ActionStatus.Success,true);
                }
                return new Response<bool>(ActionStatus.Error);
            }
            catch(Exception ex)
            {
                return new Response<bool>(ActionStatus.Error);
            }
        }

        public virtual IResponse<TObject> Get(TKey id)
        {
            try
            {
                var existing = _data.Get(id);
                if (existing == null)
                {

                    return new Response<TObject>(ActionStatus.ValidationError, null, $"Cannot found object with id {id}");
                }
                return new Response<TObject>(ActionStatus.Success, existing);
            }
            catch (Exception ex)
            {
                return new Response<TObject>(ActionStatus.Error);
            }
         

        }

        public virtual IResponse<IEnumerable<TObject>> GetAll()
        {
            try
            {
                var existing = _data.GetAll();
                return new Response<IEnumerable<TObject>>(ActionStatus.Success, existing);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<TObject>>(ActionStatus.Error);
            }
         
        }

        public virtual IResponse<TObject> Update(TObject obj)
        {

            try
            {
                var id = (TKey)IdField.GetValue(obj);
                var existing = _data.Get(id);
                if (existing == null)
                {

                    return new Response<TObject>(ActionStatus.ValidationError, null, $"Cannot found object with id {id}");
                }
                var validationResult = ValidateEntity(obj);
                if(validationResult.Item1)
                {
                    TObject updated = _data.Update(obj);
                    return new Response<TObject>(ActionStatus.Success, updated);
                }
                return new Response<TObject>(ActionStatus.ValidationError, null, validationResult.Item2);
               
            }
            catch (Exception ex)
            {
                return new Response<TObject>(ActionStatus.Error);
            }
        }
        protected virtual Tuple<bool,IEnumerable<string>> ValidateEntity(TObject obj)
        {
            return new Tuple<bool, IEnumerable<string>>(true, new List<string>());
        }
    }
}
