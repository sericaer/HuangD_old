using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;

using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

public partial class MyGame
{
    public class Province : HuangDAPI.Province
    {
        public static Province[] All
        {
            get
            {
                return _All.ToArray();
            }
        }

        public Province(string name, string economy, string mainOfficeName)
        {
            _name = name;

            string[] anaylize = economy.Split('|');
            _economy = new {baseTax = Convert.ToInt32(anaylize[1]), levelName = anaylize[0]};
            //listStatus = new List<STATUS>();
            _mainOffice = mainOfficeName; 

            _All.Add(this);
        }

        public  Office mainOffice
        {
            get
            {
                return MyGame.Office.All.Where(x => x.name == _mainOffice).Single();
            }
        }

        public  string name
        {
            get
            {
                return _name;
            }
        }

        public  string economy
        {
            get
            {
                return _economy.levelName;
            }
        }

        public double income
        {
            get
            {
                return Math.Max(0, (from x in taxDetail()
                                    select x.Item2).Sum());
            }
        }

        public double reb
        {
            get
            {
                return Math.Max(0, (from x in rebDetail()
                                    select x.Item2).Sum());
            }
        }

        public List<Tuple<string, double>>  taxDetail()
        {
            List<Tuple<string, double>> result = new List<Tuple<string, double>>();

            result.Add(new Tuple<string, double>("TAX_BASE", baseTax));
            foreach(var elem in CountryFlags.current)
            {
                if (elem.funcAffectCountryTax != null)
                {
                    result.Add(new Tuple<string, double>(elem.Title(), elem.funcAffectCountryTax(baseTax)));
                }
            }
            foreach (var elem in DecisionProcess.current)
            {
                var decision = HuangDAPI.DECISION.All[elem.name];
                if (elem.state == DecisionProcess.ENUState.Published && decision.funcAffectCountryTax != null)
                {
                    result.Add(new Tuple<string, double>(decision._funcTitle(), decision.funcAffectCountryTax(baseTax)));
                }
            }
            return result;
        }

        public List<Tuple<string, double>>  rebDetail()
        {
            List<Tuple<string, double>> result = new List<Tuple<string, double>>();

            result.Add(new Tuple<string, double>("STABILITY_REFLECTION", (double)-Stability.current));

            foreach (var elem in CountryFlags.current)
            {
                if (elem.funcAffectCountryReb != null)
                {
                    result.Add(new Tuple<string, double>(elem.Title(), elem.funcAffectCountryReb(baseTax)));
                }
            }

            foreach (var elem in DecisionProcess.current)
            {
                var decision = HuangDAPI.DECISION.All[elem.name];
                if (elem.state == DecisionProcess.ENUState.Published && decision.funcAffectCountryReb != null)
                {
                    result.Add(new Tuple<string, double>(decision._funcTitle(), decision.funcAffectCountryReb(baseTax)));
                }
            }
            return result;
        }

        internal static void Initialize()
        {
            Type type = StreamManager.Types.Where(x => x.Name == "Provinces").Single();
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        double baseTax
        {
            get
            {
                return (double)_economy.baseTax;
            }
        }

        public Dictionary<string, Func<dynamic, dynamic>> ProvTaxEffects =  new Dictionary<string, Func<dynamic, dynamic>>();
        public Dictionary<string, Func<dynamic, dynamic>> ProvRebEffects =  new Dictionary<string, Func<dynamic, dynamic>>();

        [SerializeField]
        private string _name;
        //public List<STATUS> listStatus;

        [SerializeField]
        private dynamic _economy;

        [SerializeField]
        private string _mainOffice;

        [SerializeField]
        static List<Province> _All = new List<Province>();
    }
}

