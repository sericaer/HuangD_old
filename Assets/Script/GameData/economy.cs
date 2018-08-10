using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public partial class MyGame
{
    public class Economy : SerializeManager
    {
        public static Dictionary<string, int> IncomeDetail()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (var p in MyGame.Province.All)
            {
                Dictionary <string, int> provTaxDetail = p.taxDetail();
                result.Add(p.name, provTaxDetail.Values.Sum());
            }

            return result;
        }

        public static Dictionary<string, int> DecomeDetail()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            result = result.Union(Military.GetConsumer()).ToDictionary(arg => arg.Key, arg => arg.Value);
            return result;
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

            desc += HuangDAPI.UI.Format("DECOME_PER_MONTH") + ":" + -decome.Values.Sum() + "\n";
            foreach (var elem in decome)
            {
                desc += "\t" + HuangDAPI.UI.Format(elem.Key) + ":" + -elem.Value + "\n";
            }

            desc += HuangDAPI.UI.Format("NET_INCOME_PER_MONTH") + ":" + (income.Values.Sum() - decome.Values.Sum());
            return desc;
        }

        public static void UpDate()
        {
            current += NetInCome();
        }

        [SerializeField]
        public static int current = Probability.GetRandomNum(60, 90);
    }
}
