using System;
using System.Linq;
using System.Collections.Generic;

public partial class MyGame
{
    public static class Economy 
    {
        public static Dictionary<string, int> IncomeDetail()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            //foreach (var p in MyGame.Provinces.Enumer)
            //{
            //    Dictionary <string, int> provTaxDetail = p.taxDetail();
            //    result.Add(p.name, provTaxDetail.Values.Sum());
            //}

            return result;
        }

        public static Dictionary<string, int> DecomeDetail()
        {
            return new Dictionary<string, int>();
        }

        public static int NetInCome()
        {
            var income = IncomeDetail();
            var decome = DecomeDetail();

            return income.Values.Sum() - decome.Values.Sum();
        }

        public static string Desc()
        {
            var income = IncomeDetail();
            var decome = DecomeDetail();

            string desc = HuangDAPI.UI.Format("INCOME_PER_MONTH") + ":" + income.Values.Sum() + "\n";
            foreach (var elem in income)
            {
                desc += "\t" + HuangDAPI.UI.Format(elem.Key) + ":" + elem.Value + "\n";
            }

            desc += HuangDAPI.UI.Format("DECOME_PER_MONTH") + ":" + decome.Values.Sum() + "\n";
            foreach (var elem in decome)
            {
                desc += "\t" + HuangDAPI.UI.Format(elem.Key) + ":" + elem.Value + "\n";
            }

            desc += HuangDAPI.UI.Format("NET_INCOME_PER_MONTH") + ":" + (income.Values.Sum() - decome.Values.Sum());
            return desc;
        }

        public static void UpDate()
        {
            current += NetInCome();
        }

        public static int current;
    }
}
