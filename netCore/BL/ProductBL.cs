using IBL;
using IBL.Data;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BL
{
    public class ProductBL : GenericBL<Guid, Product>, IProductBL
    {
        public ProductBL(IProductDAL data):base(data)
        {

        }
        public override IResponse<Guid> Add(Product obj)
        {
            Guid id = Guid.NewGuid();
            obj.Id = id;
            return base.Add(obj);
        }
        PropertyInfo _propertyInfoIDField;
        protected override PropertyInfo IdField
        {
            get
            {
                if (_propertyInfoIDField == null)
                {

                    _propertyInfoIDField = typeof(Product).GetProperty("Id");
                }
                return _propertyInfoIDField;
            }
        }
        protected override Tuple<bool, IEnumerable<string>> ValidateEntity(Product obj)
        {
            bool isValid = true;
            List<String> validationMessages = new List<string>();
            if(string.IsNullOrWhiteSpace(obj.Name) || obj.Name.Length>100)
            {
                isValid = false;
                validationMessages.Add("Field Name is empty or too long");
            }
            if (obj.Price<=0)
            {
                isValid = false;
                validationMessages.Add("Price cannot be less or equal 0");
            }

            return new Tuple<bool, IEnumerable<string>>(isValid, validationMessages);
        }
    }
}
