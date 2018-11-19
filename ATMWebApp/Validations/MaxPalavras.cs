using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATMWebApp.Validations
{
    public class MaxPalavrasAttribute : ValidationAttribute
    {
        private int num_max;

        // Constructor
        public MaxPalavrasAttribute(int max) : base()
        {
            num_max = max;
        }
        public override bool IsValid(object value)
        {
            string phrase = value as string;
            if (value != null)
            {
                string[] separatingChars = { " ", "\t" };
                string[] words = phrase.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > num_max) return false;
            }
            return true;
        }
    }
}