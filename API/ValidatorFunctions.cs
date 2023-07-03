using System;
using System.Text.RegularExpressions;

namespace advanced.API
{
    public delegate bool RequiredValidDel(string fieldVal);
    public delegate bool StringLengthValidDel(string fieldVal, int min, int max);
    public delegate bool DateValidDel(string fieldVal, out DateTime validDateTime); //out ? 
    public delegate bool PatternMatchValidDel(string fieldVal, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare);

    // created delegates...methods have to follow this signiture

    public class ValidatorFunctions
	{
        private static RequiredValidDel _requiredValidDel = null;
        private static StringLengthValidDel _stringLengthValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchValidDel _patternMatchValidDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;

        // lazily initialised 

        public static RequiredValidDel RequiredFieldValidDel
        {
            get  // has to be used to access the property _requiredValidDel
            {
                if (_requiredValidDel == null)
                    _requiredValidDel = new RequiredValidDel(RequiredFieldValid);

                return _requiredValidDel;
            }
        }
        // when requiredValidDel method is called _requiredValid dell will have access to the
        // method requiredFieldValid  which checks if the string is null or not
        // DOING IT THIS WAY MEANS METHOD CAN BE CALLED ANYWHERE AND IT WILL DO THE CHECK IF ITS STRING OR NOT

        public static StringLengthValidDel StringLengthFieldValidDel
        {
            get
            {
                if (_stringLengthValidDel == null)
                    _stringLengthValidDel = new StringLengthValidDel(StringFieldLengthValid);

                return _stringLengthValidDel;
            }
        }

        /*  Once the RequiredFieldValidDel property is accessed and the delegate instance is created, subsequent calls
         *  to the property will not re-initialize the delegate. Instead, it will return the existing delegate instance.

        This means that once the delegate instance is created, it can be reused multiple times without needing to
        create a new instance every time the property is accessed. The delegate instance retains its state and behavior,
        allowing it to be called multiple times with different arguments to perform the validation logic. */


        public static DateValidDel DateFieldValidDel
        {
            get
            {
                if (_dateValidDel == null)
                    _dateValidDel = new DateValidDel(DateFieldValid);

                return _dateValidDel;
            }
        }
        public static PatternMatchValidDel PatternMatchValidDel
        {
            get
            {
                if (_patternMatchValidDel == null)
                    _patternMatchValidDel = new PatternMatchValidDel(FieldPatternValid);

                return _patternMatchValidDel;
            }
        }



        public static CompareFieldsValidDel FieldsCompareValidDel
        {
            get
            {
                if (_compareFieldsValidDel == null)
                    _compareFieldsValidDel = new CompareFieldsValidDel(FieldComparisonValid);

                return _compareFieldsValidDel;
            }
        }


        //DELEGATES UP TO HERE BELOW ARE METHODS BEING PASSED INTO 





        private static bool RequiredFieldValid(string fieldVal)
        {
            if (!string.IsNullOrEmpty(fieldVal))
                return true;

            return false;

        }

        private static bool StringFieldLengthValid(string fieldVal, int min, int max)
        {
            if (fieldVal.Length >= min && fieldVal.Length <= max)
                return true;

            return false;

        }

        private static bool DateFieldValid(string dateTime, out DateTime validDateTime)
        {
            if (DateTime.TryParse(dateTime, out validDateTime))
                return true;

            return false;

        }

        private static bool FieldPatternValid(string fieldVal, string regularExpressionPattern)
        {
            Regex regex = new Regex(regularExpressionPattern);

            if (regex.IsMatch(fieldVal))
                return true;

            return false;

        }

        private static bool FieldComparisonValid(string field1, string field2)
        {
            if (field1.Equals(field2))
                return true;

            return false;
        }



    }
}

