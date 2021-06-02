using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADO.NET_Class_Library_Ex
{
    [ToolboxData("<{0}:DataAnnotationValidator runat=\"server\"></{0}:DataAnnotationValidator>")]
    public class DataAnnotationValidator : BaseValidator
    {
        #region Properties

        public string SourceTypeName { get; set; }
        public string PropertyName { get; set; }

        #endregion

        #region Methods

        //public void Diagnosis()
        //{
        //    Process p = Process.GetCurrentProcess();
        //    string assemblyName = p.ProcessName;
        //}

        protected override bool EvaluateIsValid()
        {
            Type source = GetValidatedType();
            PropertyInfo property = GetValidatedProperty(source);

            string value = GetControlValidationValue(ControlToValidate);

            foreach (var attribute in property.GetCustomAttributes(typeof(ValidationAttribute), true).OfType<ValidationAttribute>())
            {
                if (!attribute.IsValid(value))
                {
                    ErrorMessage = attribute.ErrorMessage;
                    return false;
                }
            }
            return true;
        }

        private PropertyInfo GetValidatedProperty(Type source)
        {
            PropertyInfo property = source.GetProperty(PropertyName, BindingFlags.Public | BindingFlags.Instance);

            if(property == null)
            {
                throw new InvalidOperationException($"Validated Property does not exist");
            }
            return property;
        }

        private Type GetValidatedType()
        {
            if (string.IsNullOrEmpty(SourceTypeName))
            {
                throw new InvalidOperationException("Null SourceTypeName can't be validated.");
            }

            Type validatedType = Type.GetType(SourceTypeName);
            if (validatedType == null)
            {
                throw new InvalidOperationException($"Invalid SourceTypeName: {SourceTypeName}");
            }

            return validatedType;
        }

        #endregion
    }
}
