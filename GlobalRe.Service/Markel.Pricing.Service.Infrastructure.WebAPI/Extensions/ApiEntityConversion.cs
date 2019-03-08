using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ObjectMapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class ApiEntityConversion
    {
        public static BLL_CLASS ToBLLModel<API_CLASS, BLL_CLASS>(dynamic partialApiModel)
            where API_CLASS : BaseApiModel<BLL_CLASS>
            where BLL_CLASS : BaseChangeTrackingEntity
        {
            APIMessageException apiException = null;
            API_CLASS apiModel = (API_CLASS)Activator.CreateInstance(typeof(API_CLASS), new object[] { });
            List<string> changedFields = new List<string>();
            foreach (var item in partialApiModel.Children())
            {
                try
                {
                    ObjectExtensions.SetPropertyValue(apiModel, item.Name, item.Value);

                    // Translate Property Name from API_CLASS to BLL_CLASS
                    string propertyName = PropertyMapper.GetPropertyName<API_CLASS, BLL_CLASS>(item.Name);
                    changedFields.Add(propertyName);
                }
                catch (Exception)
                {
                    apiException = apiException ?? new IllegalArgumentAPIException("Invalid Field");
                    apiException.Add(item.Name, $"Error applying value to field '{item.Name}' on '{apiModel.GetType().Name}'.");
                }
            }

            if (apiException != null) throw apiException;

            BLL_CLASS model = apiModel.ToBLLModel();
            model.AddChangedFields(changedFields);
            return model;
        }

        public static IEnumerable<BLL_CLASS> ToBLLModels<API_CLASS, BLL_CLASS>(dynamic partialApiModel)
            where API_CLASS : BaseApiModel<BLL_CLASS>
            where BLL_CLASS : BaseChangeTrackingEntity
        {
            var models = new List<BLL_CLASS>();

            if (partialApiModel.Type == JTokenType.Array)
            {
                foreach (var item in partialApiModel.Children())
                {
                    models.Add(ApiEntityConversion.ToBLLModel<API_CLASS, BLL_CLASS>(item));
                }
            }
            else
            {
                models.Add(ApiEntityConversion.ToBLLModel<API_CLASS, BLL_CLASS>(partialApiModel));
            }          
            return models;
        }
    }
}
