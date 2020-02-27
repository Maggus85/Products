using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IDAL
{
    public interface IGenericDAL<TTKey, TValue> where TValue:class
    {
        PropertyInfo IdField { get; set; }
        IEnumerable<TValue> GetAll();
        TValue Get(TTKey id);
        TValue Create(TValue obj);
        TValue Update(TValue obj);
        bool Delete(TTKey id);
    }
}
