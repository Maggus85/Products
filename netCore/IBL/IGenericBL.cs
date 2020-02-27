using IBL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    public interface IGenericBL<Tkey,TObject> where TObject:class
    {
        IResponse<IEnumerable<TObject>> GetAll();
        IResponse<TObject>  Get(Tkey id);
        IResponse<Tkey> Add(TObject obj);
        IResponse<TObject> Update(TObject obj);
        IResponse<bool> Delete(Tkey id);
    }
}
