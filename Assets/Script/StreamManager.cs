﻿using System;
using System.Reflection; 
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using HuangDAPI;
//using Mono.CSharp;

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
        csharpLoader.logWriter = new CSharpCompiler.UnityLogTextWriter();

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
        foreach(string filename in csvNames)
        {
            uiDesc.AddCSVFile(filename);
        }

		string[] fileName = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
        CSharpCompiler.ScriptBundleLoader.ScriptBundle bd = csharpLoader.LoadAndWatchScriptsBundle(fileName);
       
        Type[] types = bd.assembly.GetTypes();

        LoadName(types);
        LoadEvent(types);
        LoadDecision(types);
        LoadDefines(types);

        Debug.Log(string.Format("*****************End Load mod {0}********************", path));
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
        Type economyDefineType = types.Where(x => x.Name == "ECONOMY_DEFINE").Single();
        FieldInfo[] fields = economyDefineType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.NonPublic);
        ECONOMY.BASE_TAX = fields.Where(x => x.Name == "BASE_TAX").Single().GetValue(null) as int ;
        ECONOMY.PROV_LOW = fields.Where(x => x.Name == "PROV_LOW").Single().GetValue(null);
        ECONOMY.PROV_HIGH = fields.Where(x => x.Name == "PROV_HIGH").Single().GetValue(null);
        ECONOMY.PROV_MID = fields.Where(x => x.Name == "PROV_MID").Single().GetValue(null);
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
        IEnumerable<Type> EmumType = null;
        EmumType = types.Where(x => x.Name == "PersonName");
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