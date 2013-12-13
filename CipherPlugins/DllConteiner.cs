using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ClassLibraryLFSRCipher;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace CipherPlugins
{
    public class DllConteiner
    {
        public List<string> allTypes;
        public List<string> algorithmNames;
        public List<string> assemblyFileNames;
        public AppDomain domain;
        ComboBox cb_chooseAlgorithm;

        public DllConteiner(object sender)
        {
            cb_chooseAlgorithm = sender as ComboBox;
            allTypes = new List<string>();            
            assemblyFileNames = new List<string>();
            algorithmNames = new List<string>();
        }

        public void LoadDll()
        {
            domain = CreateDomain(Directory.GetCurrentDirectory());
            IEnumerable<ICiphers> algorithms = EnumerateAlgorithm(domain);
            foreach (ICiphers algorithm in algorithms)
                cb_chooseAlgorithm.Items.Add(algorithm.AlgorithmName);
        }

        public IEnumerable<ICiphers> EnumerateAlgorithm(AppDomain domain)
        {
            IEnumerable<string> fileNames = Directory.EnumerateFiles(domain.BaseDirectory, "*.dll");
            if (fileNames != null)
            {
                foreach (string assemblyFileName in fileNames)
                {
                    foreach (string typeName in GetTypes(assemblyFileName, typeof(ICiphers), domain))
                    {
                        System.Runtime.Remoting.ObjectHandle handle;
                        try
                        {
                            object[] args = { GetBytePassword("asas")};
                            handle = domain.CreateInstanceFrom(assemblyFileName, typeName, true, 0, null, args, null, null);//создание наследника
                        }
                        catch (MissingMethodException)
                        {
                            continue;
                        }

                        object obj = handle.Unwrap();
                        ICiphers algorithm = (ICiphers)obj;
                        //allAlgorithm.Add(algorithm);
                        algorithmNames.Add(algorithm.AlgorithmName);
                        allTypes.Add(typeName);
                        assemblyFileNames.Add(assemblyFileName);
                        yield return algorithm;
                    }
                }
            }
        }

        private IEnumerable<string> GetTypes(string assemblyFileName, Type interfaceFilter, AppDomain domain)
        {
            Assembly asm = domain.Load(AssemblyName.GetAssemblyName(assemblyFileName));
            Type[] types = asm.GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterface(interfaceFilter.Name) != null)
                {

                    yield return type.FullName;
                }
            }
        }

        public int GetAlgorithmIndex(string name)
        {
            int result = 0;
            for (int i = 0; i < algorithmNames.Count; ++i)
            {
                if (algorithmNames[i] == name)
                    result = i;
            }

            return result;
        }
        
        public ICiphers CreateInstance(int index, string key)
        {
            System.Runtime.Remoting.ObjectHandle handle;
            object[] args = { GetBytePassword(key)};
            handle = domain.CreateInstanceFrom(assemblyFileNames[index], allTypes[index], true, 0, null, args, null, null);
            object obj = handle.Unwrap();
            ICiphers algorithm = (ICiphers)obj;
            return algorithm;
        }


        public AppDomain CreateDomain(string path)
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = path;
            return AppDomain.CreateDomain("Temporary domain", null, setup);
        }

        public void UnloadDomain()
        {
            AppDomain.Unload(domain);
        }

        private byte[] GetBytePassword(string initial_string)
        {
            MD5 md5hash = MD5.Create();
            byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(initial_string));
            return data;
        }
    }
}
