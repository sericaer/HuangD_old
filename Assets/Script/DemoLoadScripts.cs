using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace TEST
{ 

    public class DemoLoadScripts : MonoBehaviour
    {
        public bool loadInBackground = true;
        public bool doStream = false;

        List<string> loaded = new List<string>();

        DeferredSynchronizeInvoke synchronizedInvoke;
        CSharpCompiler.ScriptBundleLoader loader;
        void Start()
        {
            Debug.Log("Start");

            //synchronizedInvoke = new DeferredSynchronizeInvoke();

            //loader = new CSharpCompiler.ScriptBundleLoader(null);
            //loader.logWriter = new CSharpCompiler.UnityLogTextWriter();

            //string[] subDir = Directory.GetDirectories(Application.streamingAssetsPath);

            //foreach(string dirname in subDir)
            //{
            //    string infoPath = dirname + "/info.txt";
            //    if (!File.Exists(infoPath))
            //    {
            //        continue;
            //    }

            //    LoadMod(dirname);
            //}

            //loader.createInstance = (Type t) =>
            //{
            //    if (typeof(Component).IsAssignableFrom(t)) return this.gameObject.AddComponent(t);
            //    else return System.Activator.CreateInstance(t);
            //};
            //loader.destroyInstance = (object instance) =>
            //{
            //    if (instance is Component) Destroy(instance as Component);
            //};
        }
        void Update()
        {
            //synchronizedInvoke.ProcessQueue();
        }
        void OnGUI()
        {
        //    var sourceFolder = Application.streamingAssetsPath;
        //    int num = 0;
        //    var files = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);
        //    foreach (var file in files)
        //    {
        //        if (!file.EndsWith(".meta"))
        //        {
        //            if (num > 20) break;
        //            num++;
        //            var shortPath = file.Substring(sourceFolder.Length);
        //            if (loaded.Contains(file))
        //            {
        //                GUILayout.Label("Loaded: " + shortPath);
        //            }
        //            else
        //            {
        //                if (GUILayout.Button("Load: " + shortPath))
        //                {
        //                    CSharpCompiler.ScriptBundleLoader.ScriptBundle aa = loader.LoadAndWatchScriptsBundle(new[] { file });

        //                    foreach (var type in aa.assembly.GetTypes())
        //                    {
        //                        if (type.BaseType == typeof(NATIVE.EVENT))
        //                        {
        //                            NATIVE.EVENT event1 = (NATIVE.EVENT)Activator.CreateInstance(type);
        //                            event1.Precondition();
        //                            Debug.Log("Test");
        //                        }
        //                    }
        //                    loaded.Add(file);
        //                }
        //            }
        //        }
        //    }
        }

        private void LoadMod(string path)
        {
            string[] fileName = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
            CSharpCompiler.ScriptBundleLoader.ScriptBundle bd = loader.LoadAndWatchScriptsBundle(fileName);

            Type[] types = bd.assembly.GetTypes();

            LoadName(types);
        }

        private void LoadName(Type[] types)
        {

            AnaylizePersonName(types);
            AnaylizeYearName(types);
            AnaylizeDynastyName(types);

        }

        private void AnaylizeDynastyName(Type[] types)
        {
            Type type = types.Where(x => x.Name == "DynastyName").First();
            object obj = Activator.CreateInstance(type);
            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.Name == "name")
                {
                    string[] first = (string[])field.GetValue(obj);
                    Debug.Log(string.Join(",", first));
                }
            }
        }

        private void AnaylizeYearName(Type[] types)
        {
            Type type = types.Where(x => x.Name == "YearName").First();
            object obj = Activator.CreateInstance(type);
            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.Name == "name")
                {
                    string[] first = (string[])field.GetValue(obj);
                    Debug.Log(string.Join(",", first));
                }
            }
        }

        private void AnaylizePersonName(Type[] types)
        {
            Type PersonNameType = types.Where(x => x.Name == "PersonName").First();
            object obj = Activator.CreateInstance(PersonNameType);
            foreach (FieldInfo field in PersonNameType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.Name == "family")
                {
                    string[] first = (string[])field.GetValue(obj);
                    Debug.Log(string.Join(",", first));
                }
                if (field.Name == "given")
                {
                    string[] second = (string[])field.GetValue(obj);
                    Debug.Log(string.Join(",", second));
                }
            }
        }
    }
}

namespace NATIVE
{
    public class EVENT
    {
        public virtual bool Precondition()
        {
            return false;
        }
    }
}
