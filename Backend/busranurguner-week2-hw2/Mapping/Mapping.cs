using diet_center.Abstract;
using diet_center.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace diet_center.Mapping
{
    public static class Mapping
    {

        public static List<PatientModel> ToPatientResponse(this List<Patient> patients)
        {
            List<PatientModel> result = new List<PatientModel>();

            for (int i = 0; i < patients.Count; i++)
            {
                result.Add(new PatientModel
                {
                    Id = patients[i].Id,
                    Height = patients[i].Height,
                    Weight = patients[i].Weight,
                    Index = (int)patients[i].Height/patients[i].Weight,
                    Type = (PatientType)patients[i].Type,
                    Summary = patients[i].Summary,
                    

                });
            }

            return result;
        }
    }
}
