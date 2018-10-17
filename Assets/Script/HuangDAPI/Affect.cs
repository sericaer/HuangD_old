using System;
using System.Reflection;

namespace HuangDAPI
{
    public class Affect : ReflectBase
    {
        public Func<int, int> funcAffectEmperorHeath = null;
        public Func<double, double> funcAffectCountryTax = null;
        public Func<double, double> funcAffectCountryReb = null;

        public Affect()
        {
            funcAffectEmperorHeath = AssocAffect<int, int>("affectEmperorHeath");
            funcAffectCountryTax = AssocAffect<double, double>("affectCountryTax");
            funcAffectCountryReb = AssocAffect<double, double>("affectCountryReb");
        }

        private Func<T, TResult> AssocAffect<T, TResult>(string methodName)
        {
            MethodInfo method = this.GetType().GetMethod(methodName);
            if (method != null)
            {
                return (Func<T, TResult>)(object)Delegate.CreateDelegate(typeof(Func<T, TResult>), this, method);
            }

            return null;
        }
    }
}

