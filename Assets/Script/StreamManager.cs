using System;
using System.Reflection; 
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using HuangDAPI;
using System.Text.RegularExpressions;


#if NET_4_6
#else
using Mono.CSharp;
#endif

public class StreamManager
{
	public class DynastyName
	{
		public string GetRandom()
		{
			int i = Probability.GetRandomNum(0, names.Count - 1);
			return names[i];
		}

		internal List<string> names = new List<string> ();
	}


	public class YearName
	{
		public string GetRandom()
		{
			int i = Probability.GetRandomNum(0, names.Count-1);

            int j = i;
            while (j == i)
            {
                j = Probability.GetRandomNum(0, names.Count - 1);
            }

			return names[i] + names[j];
		}

        internal List<string> names = new List<string>();
    }


	public class PersonName
	{
		public string GetRandomMale()
		{
			int i = Probability.GetRandomNum(0, family.Count - 1);
			int j = Probability.GetRandomNum(0, given.Count - 1);

			return family [i] + given [j];
		}

		public string GetRandomFemale()
		{
			int i = Probability.GetRandomNum(0, family.Count - 1);
			int j = Probability.GetRandomNum(0, givenfemale.Count - 1);

			return family [i] + givenfemale [j];
		}

		internal List<string> family = new List<string> ();
		internal List<string> given = new List<string> ();
		internal List<string> givenfemale = new List<string> ();
	}

    public class UIDesc
    {
        public UIDesc(string envLang)
        {
			this.envLang = envLang;
			dict = new Dictionary<Tuple<string, string>, string>();
        }

        public void AddCSVFile(string path)
        {
			var newdict = Tools.CSV.Anaylze(path);
			dict = dict.Union(newdict).ToDictionary(k => k.Key, v => v.Value);
        }

		public string Get(string key)
		{
#if UNITY_EDITOR_OSX
			return key;
#endif
            string[] keys = key.Split('|');

            string result = "";
            foreach(string k in keys)
            {
                if (!dict.ContainsKey(new Tuple<string, string>(k, envLang)))
                {
                    result += k;
                    continue;
                }

                result += dict[new Tuple<string, string>(k, envLang)];
            }

			return result;
		}

		private string envLang;
		private Dictionary<Tuple<string, string>, string> dict;
    }

	private StreamManager ()
	{
        csharpLoader = new CSharpCompiler.ScriptBundleLoader(null);
        csharpLoader.actLog = CSharpCompiler.UnityLogTextWriter.Log;

        string[] subDir = Directory.GetDirectories(Application.streamingAssetsPath);
        foreach (string dirname in subDir)
        {
            string infoPath = dirname + "/info.txt";
            if (!File.Exists(infoPath))
            {
                continue;
            }

            LoadMod(dirname);
        }
	}
    
    private void LoadMod(string path)
    {
        Debug.Log(string.Format("*****************Start Load mod {0}********************", path));
        string[] csvNames = Directory.GetFiles(path + "/static", "*.csv");
        foreach (string filename in csvNames)
        {
            uiDesc.AddCSVFile(filename);
        }

        string[] defineSourceCodes = GenerateByDefine(path + "/define");

        List<string> sourceCodes = defineSourceCodes.ToList();
        foreach(string filename in Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories))
        {
            //string script = ScriptPerProcess(File.ReadAllText(filename));
            string script = File.ReadAllText(filename);
            sourceCodes.Add(script);
        }

        CSharpCompiler.ScriptBundleLoader.IScriptBundle bd = csharpLoader.LoadAndWatchSourceBundle(sourceCodes.ToArray());

        Types = bd.assembly.GetTypes();

        LoadName(Types);
        LoadEvent(Types);
        LoadDecision(Types);
        LoadDefines(Types);

        Debug.Log(string.Format("*****************End Load mod {0}********************", path));
    }

    private string[] GenerateByDefine(string path)
    {
        List<string> defineSourceCodes = new List<string>();

        string[] fileNames = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
        CSharpCompiler.ScriptBundleLoader.IScriptBundle bd = csharpLoader.LoadAndWatchScriptsBundle(fileNames);

        Type[] types = bd.assembly.GetTypes();

        string officeSourceCode = GenerateOfficeCode(types);
        Debug.Log(officeSourceCode);

        string factionSourceCode = GenerateFactionCode(types);
        Debug.Log(factionSourceCode);

        string hougongSourceCode = GenerateHougongCode(types);
        Debug.Log(hougongSourceCode);

        string provinceSourceCode = GenerateProvinceCode(types);
        Debug.Log(provinceSourceCode);

        defineSourceCodes.Add(officeSourceCode);
        defineSourceCodes.Add(factionSourceCode);
        defineSourceCodes.Add(hougongSourceCode);
        defineSourceCodes.Add(provinceSourceCode);

        return defineSourceCodes.ToArray();
    }

    private string GenerateHougongCode(Type[] types)
    {
        Type officeDefineType = types.Where(x => x.Name == "HOUGONG_DEFINE").Single();

        var fields = new List<Tuple<string, Type, Type, List<object>>>();
        foreach (var eHougong in Enum.GetValues(officeDefineType))
        {
            FieldInfo field = eHougong.GetType().GetField(eHougong.ToString());
            HougongAttr attribute = Attribute.GetCustomAttribute(field, typeof(HougongAttr)) as HougongAttr;

            fields.Add(new Tuple<string, Type, Type, List<object>>(eHougong.ToString(), typeof(HuangDAPI.Hougong), typeof(MyGame.Hougong), new List<object> { eHougong.ToString(), attribute.group }));
        }

        CodeDomGen sourceCodeCreater = new CodeDomGen("Hougongs", fields);
        return sourceCodeCreater.Create();
    }

    private string GenerateOfficeCode(Type[] types)
    {
        Type officeDefineType = types.Where(x => x.Name == "OFFICE_DEFINE").Single();

        var fields = new List<Tuple<string, Type, Type, List<object>>>();
        foreach (var eoffice in Enum.GetValues(officeDefineType))
        {
            FieldInfo field = eoffice.GetType().GetField(eoffice.ToString());
            OfficeAttr attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttr)) as OfficeAttr;

            fields.Add(new Tuple<string, Type, Type, List<object>> (eoffice.ToString(), typeof(HuangDAPI.Office), typeof(MyGame.Office), new List<object>{eoffice.ToString(), attribute.Power, attribute.group}));
        }

        CodeDomGen sourceCodeCreater = new CodeDomGen("Offices", fields);
        return sourceCodeCreater.Create();
    }

    private string GenerateFactionCode(Type[] types)
    {
        Type officeDefineType = types.Where(x => x.Name == "FACTION_DEFINE").Single();

        var fields = new List<Tuple<string, Type, Type, List<object>>>();
        foreach (var efaction in Enum.GetValues(officeDefineType))
        {

            fields.Add(new Tuple<string, Type, Type, List<object>>(efaction.ToString(), typeof(HuangDAPI.Faction), typeof(MyGame.Faction), new List<object> { efaction.ToString()}));
        }

        CodeDomGen sourceCodeCreater = new CodeDomGen("Factions", fields);
        return sourceCodeCreater.Create();
    }

    private string GenerateProvinceCode(Type[] types)
    {
        Type provDefineType = types.Where(x => x.Name == "PROVINCE_DEFINE").Single();

        var fields = new List<Tuple<string, Type, Type, List<object>>>();
        foreach (var eprov in Enum.GetValues(provDefineType))
        {
            FieldInfo field = eprov.GetType().GetField(eprov.ToString());
            ProvinceAttribute attribute = Attribute.GetCustomAttribute(field, typeof(ProvinceAttribute)) as ProvinceAttribute;

            FieldInfo ecofield = attribute.economy.GetType().GetField(attribute.economy.ToString());
            ProvEconmoyLevelAttr Levelattr = Attribute.GetCustomAttribute(ecofield, typeof(ProvEconmoyLevelAttr)) as ProvEconmoyLevelAttr;

            string econmoy = attribute.economy + "|" + Levelattr.baseTax;
            //dynamic econmoy = new { baseTax = Levelattr.baseTax, levelName = attribute.economy.ToString() };

            fields.Add(new Tuple<string, Type, Type, List<object>>(eprov.ToString(), 
                                                                   typeof(HuangDAPI.Province), 
                                                                   typeof(MyGame.Province), 
                                                                   new List<object> { eprov.ToString(), econmoy, attribute.mainOffice.ToString() }));
        }

        string[] namespaces = { "native" };
        CodeDomGen sourceCodeCreater = new CodeDomGen("Provinces", fields, namespaces);
        return sourceCodeCreater.Create();
    }

    private void LoadName(Type[] types)
    {
        AnaylizePersonName(types);
        AnaylizeYearName(types);
        AnaylizeDynastyName(types);
    }

    private void LoadEvent(Type[] types)
    {
        Type[] EventTypes = types.Where(x => x.BaseType == typeof(EVENT_HD)).ToArray() ;
        foreach(Type type in EventTypes)
        {
            EVENT_HD ie = Activator.CreateInstance(type) as EVENT_HD;
            ie.SetMemento();

            eventDict.Add(type.Name, ie);
        }

        Debug.Log("Load event count:" + eventDict.Count);
    }

    private void LoadDecision(Type[] types)
    {
        Type[] DecisonTypes = types.Where(x => x.BaseType == typeof(DECISION)).ToArray();
        foreach (Type type in DecisonTypes)
        {
            DECISION decision = Activator.CreateInstance(type) as DECISION;
            //decision.SetMemento();

            decisionDict.Add(type.Name, decision);
        }

        Debug.Log("Load decision count:" + decisionDict.Count);
    }

    private void LoadDefines(Type[] types)
    {
        //Type economyDefineType = types.Where(x => x.Name == "ECONOMY_DEFINE").Single();
        //FieldInfo[] fields = economyDefineType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.NonPublic);
        //ECONOMY.BASE_TAX = fields.Where(x => x.Name == "BASE_TAX").Single().GetValue(null) as int ;
        //ECONOMY.PROV_LOW = fields.Where(x => x.Name == "PROV_LOW").Single().GetValue(null);
        //ECONOMY.PROV_HIGH = fields.Where(x => x.Name == "PROV_HIGH").Single().GetValue(null);
        //ECONOMY.PROV_MID = fields.Where(x => x.Name == "PROV_MID").Single().GetValue(null);

        //officeDefineType = types.Where(x => x.Name == "OFFICE_DEFINE").Single();
    }

    private void AnaylizeDynastyName(Type[] types)
    {
        Type type = types.Where(x => x.Name == "DynastyName").First();
        object obj = Activator.CreateInstance(type);

        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        FieldInfo nameField = fields.Where(x => x.Name == "name").First();
        dynastyName.names.AddRange((string[])nameField.GetValue(obj));
    }

    private void AnaylizeYearName(Type[] types)
    {
        Type type = types.Where(x => x.Name == "YearName").First();
        object obj = Activator.CreateInstance(type);

        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        FieldInfo nameField = fields.Where(x => x.Name == "name").First();
        yearName.names.AddRange((string[])nameField.GetValue(obj));
    }

    private void AnaylizePersonName(Type[] types)
    {
        IEnumerable<Type> EmumType = types.Where(x => x.Name == "PersonName");
        if (EmumType.Count() == 0)
        {
            return;
        }

        Type PersonNameType = EmumType.First();
        object obj = Activator.CreateInstance(PersonNameType);

        FieldInfo[] fields = PersonNameType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        IEnumerable<FieldInfo> EmumField = fields.Where(x => x.Name == "family");
        if(EmumField.Count() != 0)
        {
            FieldInfo familyField = EmumField.First();
            personName.family.AddRange((string[])familyField.GetValue(obj));
        }

        EmumField = fields.Where(x => x.Name == "given");
        if (EmumField.Count() != 0)
        {
            FieldInfo givenField = EmumField.First();
            personName.given.AddRange((string[])givenField.GetValue(obj));
        }

        EmumField = fields.Where(x => x.Name == "givenfemale");
        if (EmumField.Count() != 0)
        {
            FieldInfo givenfemaleField = EmumField.First();
            personName.givenfemale.AddRange((string[])givenfemaleField.GetValue(obj));
        }
    }

    private string ScriptPerProcess(string v)
    {
        v = v.Replace("\t", "    ");
        Regex r = new Regex(@"((?:^|\n)\s*)(EVENT_.*)(\n\s*{)");

        bool bFlag = false;
        string mcs = r.Replace(v, new MatchEvaluator((Match match) =>
                                                              {
             string result = match.Groups[1].Value + match.Groups[2].Value.Insert(0, "class ") + ":EVENT_HD"  + match.Groups[3].Value;
                                                                  bFlag = true;
                                                                  return result;
                                                         }));
        r = new Regex(@"((?:^|\n)\s*)(OPTION_.*)(\n\s*{)");
        mcs = r.Replace(mcs, new MatchEvaluator((Match match) =>
        {
            string result = match.Groups[1].Value + match.Groups[2].Value.Insert(0, "class ") + ":Option" + match.Groups[3].Value;
            bFlag = true;
            return result;
        }));

        r = new Regex(@"((?:^|\n)\s*)(Precondition.*)(\n\s*{)");
        mcs = r.Replace(mcs, new MatchEvaluator((Match match) =>
        {
            string result = match.Groups[1].Value + match.Groups[2].Value.Insert(0, "void ") + "(ref dynamic Precondition)" + match.Groups[3].Value;
            bFlag = true;
            return result;
        }));

        r = new Regex(@"((?:^|\n)\s*)(Select.*)(\n\s*{)");
        mcs = r.Replace(mcs, new MatchEvaluator((Match match) =>
        {
            string result = match.Groups[1].Value + match.Groups[2].Value.Insert(0, "void ") + "(dynamic Precondition, ref string nxtEvent, ref object param)" + match.Groups[3].Value;
            bFlag = true;
            return result;
        }));

        r = new Regex(@"((?:^|\n)\s*)(Desc.*)(\n\s*{)");
        mcs = r.Replace(mcs, new MatchEvaluator((Match match) =>
        {
            string result = match.Groups[1].Value + match.Groups[2].Value.Insert(0, "void ") + "(dynamic Precondition, ref string Desc)" + match.Groups[3].Value;
            bFlag = true;
            return result;
        }));

        r = new Regex(@"((?:^|\n)\s*)(COUNTRY_FLAG.*)(\n\s*{)");
        mcs = r.Replace(mcs, new MatchEvaluator((Match match) =>
        {
            string result = match.Groups[1].Value + match.Groups[2].Value.Insert(0, "class ") + ":COUNTRY_FLAG<" + match.Groups[2].Value + ">"+  match.Groups[3].Value;
            bFlag = true;
            return result;
        }));

        r = new Regex(@"((?:^|\n)\s*)(EFFECT.*)(\n\s*{)");
        mcs = r.Replace(mcs, new MatchEvaluator((Match match) =>
        {
            string result = match.Groups[1].Value + match.Groups[2].Value.Insert(0, "void ") + "(ref dynamic EFFECT)" + match.Groups[3].Value;
            bFlag = true;
            return result;
        }));

        if (bFlag)
        {
            Debug.Log(mcs);
        }
        return mcs;

    }

    private void LoadUIDesc()
    {
        //ItfZhoujun value = luaenv.Global.Get<string, ItfZhoujun> ("ZHOUJ");

        //Type ty = value.GetType();
        //PropertyInfo[] pros = ty.GetProperties();
        //foreach (PropertyInfo p in pros)
        //{
        //    UIDictionary.Add(p.Name, (string)p.GetValue(value, null));
        //}
    }
    //public static Type officeDefineType = null;

    public static Type[] Types;

    public static Type OfficesType = null;
    public static Type FactionsType = null;

	public  static DynastyName dynastyName = new DynastyName();
	public  static YearName yearName = new YearName();
	public  static PersonName personName = new PersonName();

    public  static Dictionary<string, string> UIDictionary = new Dictionary<string, string>();

    public static Dictionary<string, EVENT_HD> eventDict = new Dictionary<string, EVENT_HD>();
    public static Dictionary<string, DECISION> decisionDict = new Dictionary<string, DECISION>();
    public static Dictionary<string, string> cvsDict = new Dictionary<string, string>();

    public static UIDesc uiDesc = new UIDesc("CHI");

    public static class ECONOMY
    {
        public static int BASE_TAX;
        public static double PROV_LOW;
        public static double PROV_HIGH;
    }
#pragma warning disable 414
    private static StreamManager wInst = new StreamManager();
#pragma warning restore

    private CSharpCompiler.ScriptBundleLoader csharpLoader;
}