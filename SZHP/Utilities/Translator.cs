using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Models;
using DataContract.Implementation;
using System.Reflection;
using SZHPCMS.Common;

namespace SZHPCMS.Utilities
{
    public class Translator
    {
        public static List<FormDocumentViewModel> TranslateToViewFormDoc(List<FormDocumentModel> source)
        {
            if (source == null)
                return new List<FormDocumentViewModel>();

            List<FormDocumentViewModel> result = new List<FormDocumentViewModel>();

            foreach (FormDocumentModel item in source)
            {
                FormDocumentViewModel vmFormDoc = new FormDocumentViewModel();

                vmFormDoc.NameEn = item.NameEn;
                vmFormDoc.NameAr = item.NameAr;
                vmFormDoc.DocumentName = item.DocumentName;

                result.Add(vmFormDoc);
            }

            return result;
        }

        public static TDestination TranslateObject<TSource,TDestination>(TSource source)
        {

            try
            {
                if (source == null)
                    throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

                TDestination target = (TDestination)Activator.CreateInstance(typeof(TDestination));

                Type sourceType = source.GetType();

                List<PropertyInfo> sourceProperties = sourceType.GetProperties().ToList();

                foreach (PropertyInfo property in sourceProperties)
                {
                    if (!property.PropertyType.IsClass || property.PropertyType.IsPrimitive || property.PropertyType.Name == typeof(String).Name)
                    {
                        PropertyInfo targetPropertyToSet = target.GetType().GetProperty(property.Name);

                        if (targetPropertyToSet == null)
                            continue;

                        targetPropertyToSet.SetValue(target, property.GetValue(source, null), null);
                    }
                }

                return target;
            }
            catch (Exception ex)
            {                
                throw ex;
            }     

        }

        public static List<TDestination> TranslateList<TSource, TDestination>(List<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            List<TDestination> list = new List<TDestination>();

            foreach (var item in source)
            {
                TDestination objTarget = Translator.TranslateObject<TSource,TDestination>(item);

                list.Add(objTarget);
            }

            return list;
        }

    }
}