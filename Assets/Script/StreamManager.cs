using System;
using System.Reflection; 
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using System.Linq;

using HuangDAPI;

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
        string[] csvNames = Directory.GetFiles(path + "/static", "*.csv");
        foreach(string filename in csvNames)
        {
            cvsDict.Add(filename, new Cvs(filename));
        }

        string[] fileName = Directory.GetFiles(path, "*.cs");
        CSharpCompiler.ScriptBundleLoader.ScriptBundle bd = csharpLoader.LoadAndWatchScriptsBundle(fileName);

        Type[] types = bd.assembly.GetTypes();

        LoadName(types);
        LoadEvent(types);
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
            eventDict.Add(type.Name, (EVENT_HD)Activator.CreateInstance(type));
        }
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
    public static Dictionary<string, Tools.Cvs> cvsDict = new Dictionary<string, Cvs>();

#pragma warning disable 414
    private static StreamManager wInst = new StreamManager();
#pragma warning restore

    private CSharpCompiler.ScriptBundleLoader csharpLoader;
}