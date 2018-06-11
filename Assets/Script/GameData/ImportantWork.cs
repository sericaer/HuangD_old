using System;
using System.Linq;
using System.Collections.Generic;

public partial class MyGame
{
    public class ImpWork : HuangDAPI.ImpWork
    {
        public ImpWork(string name, string detail, object src) : this(name, detail, src, null)
        {
        }

        public ImpWork(string name, string detail, object src, object dest)
        {
            _name = name;
            _defail = detail;
            _src = src;
            _dest = dest;
        }

        public string name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string detail
        {
            get
            {
                return _defail;
            }
            set
            {
                _defail = value;
            }
        }

        public object src
        {
            get
            {
                return _src;
            }
            set
            {
                _src = value;
            }
        }

        public object dest
        {
            get
            {
                return _dest;
            }
            set
            {
                _dest = value;
            }
        }

        public string _name;
        public string _defail;
        public object _src;
        public object _dest;
    }

    public class ImpWorkManager
    {
        public void Add(string name, string detail, object src)
        {
            listImpWork.Add(new ImpWork(name, detail, src));
        }

        public void Add(string name, string detail, object src, object dest)
        {
            listImpWork.Add(new ImpWork(name, detail, src, dest));
        }

        public ImpWork Find(string name)
        {
            return listImpWork.Find(x => x.name == name);  
        }

        public bool Contains(string name)
        {
            return listImpWork.Any(x=>x.name == name);
        }

        List<ImpWork> listImpWork = new List<ImpWork>();
    }
}
