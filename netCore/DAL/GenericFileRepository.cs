using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Reflection;
using IDAL;

namespace DAL
{
    public abstract class GenericFileRepository<TKey, TValue>: IGenericDAL<TKey, TValue> where TValue:class
    {
        public PropertyInfo IdField { get; set; }
        private string _path;
        public GenericFileRepository(string path)
        {
            _path = path;
        }
        private IDictionary<TKey, TValue> _data;
        private IDictionary<TKey, TValue> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new Dictionary<TKey, TValue>();
                    if (File.Exists(_path))
                    {
               
                        string content = File.ReadAllText(_path);
                        var vals = JsonConvert.DeserializeObject<IEnumerable<TValue>>(content).ToList();
                        vals.ForEach(x =>  _data.Add((TKey)IdField.GetValue(x), x ));
                    }
                }
                return _data;
            }
        }
        public virtual IEnumerable<TValue> GetAll()
        {
            return Data.Values;
        }
        public virtual TValue Get(TKey id)
        {
            TValue val;
            if(Data.TryGetValue(id,out val))
            {
                return val;
            }
            return null;
        }
        private void SaveFile()
        {
          var content=  JsonConvert.SerializeObject(Data.Values);
            File.WriteAllText(_path, content);
            _data = null;
        }
        public virtual TValue Create(TValue obj)
        {

            Data.Add((TKey)IdField.GetValue(obj), obj);
            SaveFile();
            return obj;
        }
        public virtual TValue Update(TValue obj)
        {
            var key = (TKey)IdField.GetValue(obj);
            Data[key] = obj;
            SaveFile();
            return obj;
        }
        public virtual bool Delete(TKey id)
        {
            Data.Remove(id);
            SaveFile();
            return true;
        }
    }
}
