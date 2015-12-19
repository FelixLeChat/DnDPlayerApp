using System.Collections.Generic;
using System.Linq;
using DnDAppAndroid.Utility.Class.Classes;

namespace DnDAppAndroid.Utility.Class
{
    class ClassManager
    {
        private static ClassManager _ins;
        public static ClassManager Instance
        {
            get { return _ins ?? (_ins = new ClassManager()); }
        }


        public List<AbstractClass> Classes { get; } = new List<AbstractClass>();
        private ClassManager()
        {
            Classes.Add(new WarlockClass());
        }

        public AbstractClass GetClass(string className)
        {
            return Classes.FirstOrDefault(x => x.Name == className);
        }
    }
}