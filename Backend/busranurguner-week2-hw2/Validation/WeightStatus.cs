using diet_center.Abstract;
using diet_center.RequestModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diet_center
{
    public class WeighStatus : ValidationAttribute
    {
        private int v;

        public WeighStatus(int v)
        {
            this.v = v;
        }

        public WeighStatus(int v, string ErrorMessage)
        {
            this.v = v;
            this.ErrorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            Model2 patientModel = (Model2)validationContext.ObjectInstance;

            if (patientModel.Type == (int)PatientType.Thin)
            {
                if (patientModel.Index < 18)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.MemberName));
                }
            }
            
            if (patientModel.Type == (int)PatientType.Obese)
            {
                if ( patientModel.Index > 30)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.MemberName));
                }
            }
            



            return ValidationResult.Success;
        }

    }

    }
