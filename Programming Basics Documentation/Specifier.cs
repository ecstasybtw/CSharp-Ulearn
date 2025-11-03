using System;
using System.Linq;
using System.Reflection;

namespace Documentation
{
    public class Specifier<T> : ISpecifier
    {
        Type targetType = typeof(T);

        public string GetApiDescription()
        {
            return targetType.GetCustomAttributes(true)
                             .OfType<ApiDescriptionAttribute>()
                             .Select(descAttr => descAttr.Description)
                             .FirstOrDefault();
        }

        public string[] GetApiMethodNames()
        {
            return targetType.GetMethods()
                             .Where(methodInfo => methodInfo.IsPublic && methodInfo.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
                             .Select(methodInfo => methodInfo.Name)
                             .ToArray();
        }

        public string GetApiMethodDescription(string methodName)
        {
            return targetType.GetMethod(methodName)?
                             .GetCustomAttributes(true)
                             .OfType<ApiDescriptionAttribute>()
                             .Select(descAttr => descAttr.Description)
                             .FirstOrDefault();
        }

        public string[] GetApiMethodParamNames(string methodName)
        {
            return targetType.GetMethod(methodName)?
                             .GetParameters()
                             .Select(parameterInfo => parameterInfo.Name)
                             .ToArray();
        }

        public string GetApiMethodParamDescription(string methodName, string paramName)
        {
            return targetType.GetMethod(methodName)?
                             .GetParameters()
                             .FirstOrDefault(parameterInfo => parameterInfo.Name == paramName)?
                             .GetCustomAttributes(true)
                             .OfType<ApiDescriptionAttribute>()
                             .Select(descAttr => descAttr.Description)
                             .FirstOrDefault();
        }

        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var parameterInfo = targetType.GetMethod(methodName)?
                                        .GetParameters()
                                        .FirstOrDefault(p => p.Name == paramName);

            return CreateParamDescription(paramName, parameterInfo);
        }

        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
        {
            var methodInfo = targetType.GetMethod(methodName);
            if (methodInfo == null || !methodInfo.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
                return null;

            var methodDescription = new ApiMethodDescription
            {
                MethodDescription = new CommonDescription(methodName, GetApiMethodDescription(methodName)),
                ParamDescriptions = GetApiMethodParamNames(methodName)
                                    .Select(param => GetApiMethodParamFullDescription(methodName, param))
                                    .ToArray()
            };

            var returnParamDescription = CreateParamDescription(null, methodInfo.ReturnParameter, out bool hasReturnMeta);

            if (hasReturnMeta)
                methodDescription.ReturnDescription = returnParamDescription;

            return methodDescription;
        }

        // --- Вспомогательные методы ---

        private ApiParamDescription CreateParamDescription(string paramName, ParameterInfo parameterInfo)
        {
            var paramDesc = new ApiParamDescription
            {
                ParamDescription = new CommonDescription(paramName)
            };

            if (parameterInfo == null)
                return paramDesc;

            ApplyMetadata(parameterInfo, paramDesc, out _);
            return paramDesc;
        }

        private ApiParamDescription CreateParamDescription(string paramName, ParameterInfo parameterInfo, out bool hasMetadata)
        {
            var paramDesc = new ApiParamDescription
            {
                ParamDescription = new CommonDescription(paramName)
            };

            hasMetadata = ApplyMetadata(parameterInfo, paramDesc, out _);
            return paramDesc;
        }

        private bool ApplyMetadata(ParameterInfo parameterInfo, ApiParamDescription description, out bool hasAny)
        {
            hasAny = false;

            if (TryApplyDescription(parameterInfo, description))
                hasAny = true;

            if (TryApplyIntValidation(parameterInfo, description))
                hasAny = true;

            if (TryApplyRequired(parameterInfo, description))
                hasAny = true;

            return hasAny;
        }

        private bool TryApplyDescription(ParameterInfo parameterInfo, ApiParamDescription description)
        {
            var attr = parameterInfo.GetCustomAttributes(true)
                                    .OfType<ApiDescriptionAttribute>()
                                    .FirstOrDefault();

            if (attr != null)
            {
                description.ParamDescription.Description = attr.Description;
                return true;
            }

            return false;
        }

        private bool TryApplyIntValidation(ParameterInfo parameterInfo, ApiParamDescription description)
        {
            var attr = parameterInfo.GetCustomAttributes(true)
                                    .OfType<ApiIntValidationAttribute>()
                                    .FirstOrDefault();

            if (attr != null)
            {
                description.MinValue = attr.MinValue;
                description.MaxValue = attr.MaxValue;
                return true;
            }

            return false;
        }

        private bool TryApplyRequired(ParameterInfo parameterInfo, ApiParamDescription description)
        {
            var attr = parameterInfo.GetCustomAttributes(true)
                                    .OfType<ApiRequiredAttribute>()
                                    .FirstOrDefault();

            if (attr != null)
            {
                description.Required = attr.Required;
                return true;
            }

            return false;
        }
    }
}
