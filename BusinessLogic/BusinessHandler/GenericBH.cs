using DataAccess.CommonRespository;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessHandler
{
    public class GenericBH
    {
        private readonly IUnitOfWork _uow;
        public GenericBH()
        {
            _uow = new UnitOfWork();
        }

        public List<RowStatusModel> GetRowStatusList()
        {
            return _uow.CommonRepository.GetAll().Select(x => new RowStatusModel()
            {

                RowStatusId = x.Row_Status_Id,
                TitleEnglish = x.Name_En,
                TitleArabic = x.Name_Ar,
                CreatedDate = x.Created_Date

            }).ToList();
        }
    }
}
