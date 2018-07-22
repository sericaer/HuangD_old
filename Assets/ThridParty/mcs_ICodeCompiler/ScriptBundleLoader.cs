using System.Collections.Generic;
using System.Reflection;
using System.CodeDom.Compiler;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System;

namespace CSharpCompiler
{
    public class ScriptBundleLoader
    {
        public Func<Type, object> createInstance = (Type type) => { return Activator.CreateInstance(type); };
        public Action<object> destroyInstance = delegate { };

        //public TextWriter logWriter = Console.Out;
        public Action<string> actLog = null;

        ISynchronizeInvoke synchronizedInvoke;
        List<IScriptBundle> allFilesBundle = new List<IScriptBundle>();

        public ScriptBundleLoader(ISynchronizeInvoke synchronizedInvoke)
        {
            //this.synchronizedInvoke = synchronizedInvoke;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSources"></param>
        /// <returns>true on success, false on failure</returns>
        public IScriptBundle LoadAndWatchScriptsBundle(IEnumerable<string> fileSources)
        {
            var bundle = new ScriptBundleFiles(this, fileSources);
            allFilesBundle.Add(bundle);
            return bundle;
        }

        public IScriptBundle LoadAndWatchSourceBundle(IEnumerable<string> sourceCodes)
        {
            var bundle = new SourceCodeBundle(this, sourceCodes);
            allFilesBundle.Add(bundle);
            return bundle;
        }

        /// <summary>
        /// Manages a bundle of files which form one assembly, if one file changes entire assembly is recompiled.
        /// </summary>
        /// 

        public interface IScriptBundle
        {
            Assembly assembly { get; }
        }

        public class ScriptBundleFiles : IScriptBundle
        {
            public Assembly assembly 
            {
                get
                {
                    return _assembly;
                }
            }


            Assembly _assembly;
            IEnumerable<string> filePaths;
            //List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();
            //List<object> instances = new List<object>();
            ScriptBundleLoader manager;

            string[] assemblyReferences;
            public ScriptBundleFiles(ScriptBundleLoader manager, IEnumerable<string> filePaths)
            {
                this.filePaths = filePaths.Select(x => Path.GetFullPath(x));
                this.manager = manager;

                var domain = System.AppDomain.CurrentDomain;
                this.assemblyReferences = domain
                    .GetAssemblies()
                    .Where(a => !(a is System.Reflection.Emit.AssemblyBuilder) && !string.IsNullOrEmpty(a.Location))
                    .Select(a => a.Location)
                    .ToArray();

                manager.actLog("loading " + string.Join(", ", filePaths.ToArray()));
                CompileFiles();
                //CreateFileWatchers();
                //CreateNewInstances();
            }

            void CompileFiles()
            {
                filePaths = filePaths.Where(x => File.Exists(x)).ToArray();

                var options = new CompilerParameters();
                options.GenerateExecutable = false;
                options.GenerateInMemory = true;
                options.ReferencedAssemblies.AddRange(assemblyReferences);

                var compiler = new CodeCompiler();

                var result = compiler.CompileAssemblyFromFileBatch(options, filePaths.ToArray());

                foreach (var err in result.Errors)
                {
                    manager.actLog(err.ToString());
                }

                this._assembly = result.CompiledAssembly;
            }
            //void CreateFileWatchers()
            //{
            //    foreach (var filePath in filePaths)
            //    {
            //        FileSystemWatcher watcher = new FileSystemWatcher();
            //        fileSystemWatchers.Add(watcher);
            //        watcher.Path = Path.GetDirectoryName(filePath);
            //        /* Watch for changes in LastAccess and LastWrite times, and 
            //           the renaming of files or directories. */
            //        watcher.NotifyFilter = NotifyFilters.LastWrite
            //           | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            //        watcher.Filter = Path.GetFileName(filePath);

            //        // Add event handlers.
            //        watcher.Changed += new FileSystemEventHandler((object o, FileSystemEventArgs a) => { Reload(recreateWatchers: false); });
            //        //watcher.Created += new FileSystemEventHandler((object o, FileSystemEventArgs a) => { });
            //        watcher.Deleted += new FileSystemEventHandler((object o, FileSystemEventArgs a) => { Reload(recreateWatchers: false); });
            //        watcher.Renamed += new RenamedEventHandler((object o, RenamedEventArgs a) =>
            //        {
            //            filePaths = filePaths.Select(x =>
            //            {
            //                if (x == a.OldFullPath) return a.FullPath;
            //                else return x;
            //            });
            //            Reload(recreateWatchers: true);
            //        });
            //        watcher.SynchronizingObject = manager.synchronizedInvoke;
            //        // Begin watching.
            //        watcher.EnableRaisingEvents = true;
            //    }
            //}
            //void StopFileWatchers()
            //{
            //    foreach (var w in fileSystemWatchers)
            //    {
            //        w.EnableRaisingEvents = false;
            //        w.Dispose();
            //    }
            //    fileSystemWatchers.Clear();
            //}
            //void Reload(bool recreateWatchers = false)
            //{
            //    manager.logWriter.WriteLine("reloading " + string.Join(", ", filePaths.ToArray()));
            //    StopInstances();
            //    CompileFiles();
            //    CreateNewInstances();
            //    if (recreateWatchers)
            //    {
            //        StopFileWatchers();
            //        CreateFileWatchers();
            //    }
            //}
            //void CreateNewInstances()
            //{
            //    if (assembly == null) return;
            //    foreach (var type in assembly.GetTypes())
            //    {
            //        manager.synchronizedInvoke.Invoke((System.Action)(() =>
            //        {
            //            instances.Add(manager.createInstance(type));
            //        }), null);
            //    }
            //}
            //void StopInstances()
            //{
            //    foreach (var instance in instances)
            //    {
            //        manager.synchronizedInvoke.Invoke((System.Action)(() =>
            //        {
            //            manager.destroyInstance(instance);
            //        }), null);
            //    }
            //    instances.Clear();
            //}
        }

        public class SourceCodeBundle : IScriptBundle
        {
            public Assembly assembly
            {
                get
                {
                    return _assembly;
                }
            }

            Assembly _assembly;
            IEnumerable<string> filePaths;
            //List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();
            //List<object> instances = new List<object>();
            //ScriptBundleLoader manager;

            string[] assemblyReferences;
            public SourceCodeBundle(ScriptBundleLoader manager, IEnumerable<string> sourceCodes)
            {
                //this.manager = manager;

                var domain = System.AppDomain.CurrentDomain;
                this.assemblyReferences = domain
                    .GetAssemblies()
                    .Where(a => !(a is System.Reflection.Emit.AssemblyBuilder) && !string.IsNullOrEmpty(a.Location))
                    .Select(a => a.Location)
                    .ToArray();

                var options = new CompilerParameters();
                options.GenerateExecutable = false;
                options.GenerateInMemory = true;
                options.ReferencedAssemblies.AddRange(assemblyReferences);

                var compiler = new CodeCompiler();

                var result = compiler.CompileAssemblyFromSourceBatch(options, sourceCodes.ToArray());

                foreach (var err in result.Errors)
                {
                    manager.actLog(err.ToString());
                }

                this._assembly = result.CompiledAssembly;
            }
        }
    }

}
