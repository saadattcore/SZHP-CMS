using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    interface IContact
    {
          long Contact_Id { get; set; }
          string City_Ar { get; set; }
          string City_En { get; set; }
          string Location_En { get; set; }
          string Location_Ar { get; set; }
          Nullable<double> Latitude { get; set; }
          Nullable<double> Longitude { get; set; }
          string Phone { get; set; }
          string Fax { get; set; }
          string Email { get; set; }
    }
}
