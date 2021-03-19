using System;

namespace CleanCode
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Degisken İsimleri
            DateTime yyymmd = new DateTime(2017, 11, 10, 9, 5, 0); // false
            DateTime mevcuttarih = new DateTime(2017, 11, 10, 9, 5, 0);  //true
            #endregion

            #region Boolean Karşılaştırmalar - Deger Atama
            bool buyukKucuk = true;

            if (buyukKucuk == true) // false
            if (buyukKucuk) // true

             bool sayi;   //false
             if(sayi < 0)
             {
                sayi = false;
             }
             else
             {
                sayi = true;
             }

            bool sayi = sayi > 0; //true
            #endregion

            #region Pozitif Ol
            if (!IsNotLoggedIn) //false

            if (LoggedIn) //true


                    #endregion

            #region Ternary If
            bool y;
            if (x == 42)
            {
                y= true;
            }
            else
            {
                y= false;
            }

            bool y= (x == 42) ? true : false;


            #endregion

            #region Strongly Type
            if (status == "success") //false

            if (status == Status.Success)//true
                    #endregion

            #region Karmaşık Koşullar
            if (employee.age > 55
            && employee.yearsEmplyed > 10
            && employee.isRetired)
            {
                     
            }

            bool eligibleForPension = employee.age > minRetirementAge
                       && employee.yearEmployed > minPensionEmploymentYears
                       && emplyee.isRetired;
            #endregion

            #region Karmaşıklığı azaltmak

            if (salary < 3000) return 0; //false

            else if (salary < 6000) return 3;

            else if (salary < 10000) return 7;

            else if (salary < 15000) return 15;


            return Repository.getTaxRate(salary); //true
            #endregion


        }
    }
}
