using IDAL;
using Model;
using System;

namespace DAL
{
    public class ProductDAL : GenericFileRepository<Guid, Product>,IProductDAL
    {
        public ProductDAL(string path):base(path)
        {

        }
        public override Product Create(Product obj)
        {
            Guid id = Guid.NewGuid();
            obj.Id = id;

            return base.Create(obj);
        }
    }
}
