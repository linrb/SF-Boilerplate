
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SF.Core.Common
{
    public class AssemblyProvider : IAssemblyProvider
    {
        protected ILogger<AssemblyProvider> logger;

        public Func<Assembly, bool> IsCandidateAssembly { get; set; }
        public Func<Library, bool> IsCandidateCompilationLibrary { get; set; }

        public AssemblyProvider(IServiceProvider serviceProvider)
        {
            this.logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<AssemblyProvider>();
            this.IsCandidateAssembly = assembly =>
             /* !assembly.FullName.StartsWith("Microsoft.") && !assembly.FullName.Contains("SF.WebHost") &&*/ assembly.FullName.StartsWith("SF.");
            this.IsCandidateCompilationLibrary = library =>
              library.Name != "NETStandard.Library" && !library.Name.StartsWith("Microsoft.") && !library.Name.StartsWith("System.");
        }

        public IEnumerable<Assembly> GetAssemblies(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();

            assemblies.AddRange(this.GetAssembliesFromPath(path));
            //  assemblies.AddRange(this.GetAssembliesFromDependencyContext());
            return assemblies;
        }

        public IEnumerable<ModuleInfo> GetModules(string path)
        {
            IList<ModuleInfo> modules = new List<ModuleInfo>();

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                this.logger.LogInformation("Discovering and loading assemblies from path '{0}'", path);
                var moduleRootFolder = new DirectoryInfo(path);
                var moduleFolders = moduleRootFolder.GetDirectories();

                foreach (var moduleFolder in moduleFolders)
                {
                    var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                    if (!binFolder.Exists)
                    {
                        continue;
                    }

                    foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                    {
                        Assembly assembly;
                        try
                        {
                            assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                        }
                        catch (FileLoadException)
                        {
                            // Get loaded assembly
                            assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                            if (assembly == null)
                            {
                                throw;
                            }
                        }

                        //过滤非主模块库，如SF.Module.Backend.Data
                        if (assembly.FullName.Split(',')[0].Equals(moduleFolder.Name))
                        {
                            modules.Add(new ModuleInfo
                            {
                                Name = moduleFolder.Name,
                                Assembly = assembly,
                                Path = moduleFolder.FullName
                            });
                        }
                    }
                }
            }

            return modules;

        }

        private IEnumerable<Assembly> GetAssembliesFromPath(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();
            var binFolder = new DirectoryInfo(path);
            if (!binFolder.Exists)
            {
                this.logger.LogWarning("Discovering and loading assemblies from path '{0}' skipped: path not found", binFolder);
                return assemblies;
            }
            this.logger.LogInformation("Discovering and loading assemblies from path '{0}'", binFolder);
            foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
            {
                Assembly assembly;
                try
                {
                    assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                }
                catch (FileLoadException)
                {
                    // Get loaded assembly
                    assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                    if (assembly == null)
                    {
                        throw;
                    }
                }
                if (this.IsCandidateAssembly(assembly) && !assemblies.Contains(assembly))
                {
                    assemblies.Add(assembly);
                    this.logger.LogInformation("Assembly '{0}' is discovered and loaded", assembly.FullName);
                }

            }


            return assemblies;
        }

        private IEnumerable<Assembly> GetAssembliesFromDependencyContext()
        {
            List<Assembly> assemblies = new List<Assembly>();

            this.logger.LogInformation("Discovering and loading assemblies from DependencyContext");

            foreach (CompilationLibrary compilationLibrary in DependencyContext.Default.CompileLibraries)
            {
                if (this.IsCandidateCompilationLibrary(compilationLibrary))
                {
                    Assembly assembly = null;

                    try
                    {
                        assembly = Assembly.Load(new AssemblyName(compilationLibrary.Name));
                        assemblies.Add(assembly);
                        this.logger.LogInformation("Assembly '{0}' is discovered and loaded", assembly.FullName);
                    }

                    catch (Exception e)
                    {
                        this.logger.LogWarning("Error loading assembly '{0}'", compilationLibrary.Name);
                        this.logger.LogInformation(e.ToString());
                    }
                }
            }

            return assemblies;
        }


    }
    public class AssemblyLoader : AssemblyLoadContext
    {
        private string folderPath;

        public AssemblyLoader(string folderPath)
        {
            this.folderPath = folderPath;
        }
        // Not exactly sure about this
        protected override Assembly Load(AssemblyName assemblyName)
        {
            //var deps = DependencyContext.Default;
            //var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
            //var assembly = Assembly.Load(new AssemblyName(res.First().Name));
            //return assembly;

            var deps = DependencyContext.Default;
            var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
            if (res.Count > 0)
            {
                return Assembly.Load(new AssemblyName(res.First().Name));
            }
            //else
            //{
            //    var apiApplicationFileInfo = new FileInfo($"{folderPath}{Path.DirectorySeparatorChar}{assemblyName.Name}.dll");
            //    if (File.Exists(apiApplicationFileInfo.FullName))
            //    {
            //        var asl = new AssemblyLoader(apiApplicationFileInfo.DirectoryName);
            //        return asl.LoadFromAssemblyPath(apiApplicationFileInfo.FullName);
            //    }
            //}
            return Assembly.Load(assemblyName);
        }


    }
}