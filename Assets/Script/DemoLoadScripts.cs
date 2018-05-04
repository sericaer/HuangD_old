using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

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

            synchronizedInvoke = new DeferredSynchronizeInvoke();

            loader = new CSharpCompiler.ScriptBundleLoader(synchronizedInvoke);
            loader.logWriter = new CSharpCompiler.UnityLogTextWriter();
            loader.createInstance = (Type t) =>
            {
                if (typeof(Component).IsAssignableFrom(t)) return this.gameObject.AddComponent(t);
                else return System.Activator.CreateInstance(t);
            };
            loader.destroyInstance = (object instance) =>
            {
                if (instance is Component) Destroy(instance as Component);
            };
        }
        void Update()
        {
            synchronizedInvoke.ProcessQueue();
        }
        void OnGUI()
        {
            var sourceFolder = Application.streamingAssetsPath;
            int num = 0;
            var files = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (!file.EndsWith(".meta"))
                {
                    if (num > 20) break;
                    num++;
                    var shortPath = file.Substring(sourceFolder.Length);
                    if (loaded.Contains(file))
                    {
                        GUILayout.Label("Loaded: " + shortPath);
                    }
                    else
                    {
                        if (GUILayout.Button("Load: " + shortPath))
                        {
                            CSharpCompiler.ScriptBundleLoader.ScriptBundle aa = loader.LoadAndWatchScriptsBundle(new[] { file });

                            foreach (var type in aa.assembly.GetTypes())
                            {
                                if (type.BaseType == typeof(NATIVE.EVENT))
                                {
                                    NATIVE.EVENT event1 = (NATIVE.EVENT)Activator.CreateInstance(type);
                                    event1.Precondition();
                                    Debug.Log("Test");
                                }
                            }
                            loaded.Add(file);
                        }
                    }
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
