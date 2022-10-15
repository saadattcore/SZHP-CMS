using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SZHPCMS.Common
{
    public static class AutoMapperUtil
    {
        public static List<TDestination> GetList<TSource, TDestination>(List<TSource> before)
        {
            //  Mapper.CreateMap<TSource, TDestination>();

            if (before.Count > 0)            
                AutoMapperUtil.IgnoreProperties<TSource, TDestination>(before[0]);           
            else
                Mapper.CreateMap<TSource, TDestination>();

            var after = Mapper.Map<List<TSource>, List<TDestination>>(before);
            return after;
        }
        public static TDestination Get<TSource, TDestination>(TSource before)
        {
            // Mapper.CreateMap<TSource, TDestination>();

            AutoMapperUtil.IgnoreProperties<TSource, TDestination>(before);

            var after = Mapper.Map<TSource, TDestination>(before);
            return after;
        }

        private static void IgnoreProperties<TSource, TDestination>(TSource source)
        {
            Type t = typeof(TSource);

            PropertyInfo[] properties = t.GetProperties();
            bool found = false;

            for (int i = 0; i < properties.Length; i++)
            {
                switch (properties[i].PropertyType.FullName)
                {
                    case "System.Web.HttpPostedFileBase":
                    case "ITJS.Models.DocumentViewModel":
                    case "DataContract.Implementation.DocumentModel":

                        try
                        {
                            Mapper.CreateMap<TSource, TDestination>().ForMember(properties[i].Name, opt => opt.Ignore());
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        
                        found = true;
                        break;

                    default:
                        break;
                }
            }

            if (!found)
                Mapper.CreateMap<TSource, TDestination>();

        }
    }
}
