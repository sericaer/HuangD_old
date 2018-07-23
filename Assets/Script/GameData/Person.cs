using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HuangDAPI;

public partial class MyGame
{
    //[Serializable]
    //public class PersonProcess : HuangDAPI.StatusParam, HuangDAPI.PersonProcess 
    //{
    //    public PersonProcess(string name, Person opp, params object[] tag) : base(name)
    //    {
    //        this._opp = opp;

    //        this._tag = new List<object>(tag);
    //    }

    //    public HuangDAPI.Person opp 
    //    {
    //        get
    //        {
    //            return _opp;
    //        }
    //    }
    //    public List<object> tag 
    //    {
    //        get
    //        {
    //            return _tag;
    //        }
    //    }

    //    public override string ToString()
    //    {
    //        if (_tag.Count == 0)
    //        {
    //            return string.Format(StreamManager.uiDesc.Get(ID), _opp.ToString());
    //        }

    //        List<string> lstString = new List<string> { _opp.ToString() };
    //        for (int i = 0; i < _tag.Count; i++)
    //        {
    //            lstString.Add(_tag[i].ToString());
    //        }

    //        return string.Format(StreamManager.uiDesc.Get(ID), lstString.ToArray());

    //        //for(int i=0; i< tag.Count; i++)
    //        //string.Format(StreamManager.uiDesc.Get(name), opp.name, );
    //    }

    //    public Person _opp;
    //    public List<object> _tag;
    //}

    [Serializable]
	public class Person : HuangDAPI.Person
    {
        //public Person()
        //{
        //    throw new NotImplementedException();
        //}

        //public Person(string name) : this(true, Tools.Probability.GetRandomNum(10, 90))
        //{
        //    _name = name;
        //}

        public static void Initialize()
        {
            while(_Males.Count < MyGame.Office.All.Length)
            {
                Person person = new Person(true);
                if(_Males.Any(x=>x.name == person.name))
                {
                    continue;
                }

                _Males.Add(person);
            }

            _Males = _Males.OrderByDescending(x=>x.score).ToList();

            while (_Females.Count < MyGame.Hougong.All.Length)
            {
                Person person = new Person(false);
                if (_Males.Any(x => x.name == person.name))
                {
                    continue;
                }

                _Females.Add(person);
            }

            _Females = _Females.OrderByDescending(x => x.score).ToList();
        }

        public static Person[] Males
        {
            get
            {
                return _Males.ToArray();
            }
        }

        public static Person[] Females
        {
            get
            {
                return _Females.ToArray();
            }
        }

        public Faction faction
        {
            get
            {
                return (from x in MyGame.RelationManager.mapPerson2Faction
                        where x.person == this
                        select x.faction).Single();
            }
        }

        public Office office
        {
            get
            {
                return (from x in MyGame.RelationManager.mapOffice2Person
                        where x.person == this
                        select x.office).Single();
            }
        }

        //public static Person NewPerson(Boolean isMale)
        //{
        //    do
        //    {
        //        Person person = new Person(isMale);

        //        int Count = 0;
        //        if(isMale)
        //        {
        //            Count = (from x in MyGame.Inst.relationManager.GetOfficeMap()
        //                     where x.person != null
        //                         where x.person.name == person.name
        //                         select x.person).Count();
        //        }
        //        else
        //        {
        //            Count = (from x in MyGame.Inst.relationManager.GetHougongMap()
        //                     where x.person != null
        //                         where x.person.name == person.name
        //                         select x.person).Count();
        //        }


        //        if(Count != 0)
        //        {
        //            continue;
        //        }

        //        return person;

        //    } while (true);
        //}

        public Person(Boolean isMale) : this(isMale, Tools.Probability.GetRandomNum(10, 90))
        {
            
        }

        public Person(Boolean isMale, int score)
        {
            if (isMale)
            {
                _name = StreamManager.personName.GetRandomMale();
            }
            else
            {
                _name = StreamManager.personName.GetRandomFemale();
            }

            _score = score;
        }

        //public Person(PER_CREATE_PERSON p)
        //{
        //    _name = p.personName;
        //    _score = p.score;
        //}

        public override string name
        {
            get
            {
                return _name;
            }
        }

        //public override int press
        //{
        //    get
        //    {
        //        int value = 0;
        //        foreach(var elem in Flags.Values)
        //        {
        //            string[] array = elem.Split(':');
        //            if(array[0] == "press")
        //            {
        //                value += Convert.ToInt32(array[1]);
        //            }
        //        }
        //        return value;
        //    }
        //}

        public override int score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        //public override int heath
        //{
        //    get
        //    {
        //        return _heath;
        //    }
        //    set
        //    {
        //        _heath = value;
        //    }
        //}

        //public override int age
        //{
        //    get
        //    {
        //        return _age;
        //    }
        //    set
        //    {
        //        _age = value;
        //    }
        //}

        //public override Dictionary<string, string> Flags
        //{
        //    get
        //    {
        //        return _Flags;
        //    }

        //    set
        //    {
        //        _Flags = value;
        //    }
        //}

        //public override string ToString()
        //{
        //    //HuangDAPI.Office office = (from x in MyGame.Inst.relationManager.GetOfficeMap()
        //    //                 where x.person == this
        //    //                 select x.office).FirstOrDefault();
        //    //if(office != null)
        //    //{
        //    //    return StreamManager.uiDesc.Get(office.name) + name;
        //    //}

        //    return name;
        //}

        //public PersonProcess Process(string name, params object[] param)
        //{
        //    return  new PersonProcess(name, this, param);
        //}

        public override void Die()
        {
            //foreach (Action<object, string> listener in ListListener)
            //{
            //    listener(this, "DIE");
            //}
        }

        //public int ScoreAdd(int value)
        //{
        //    return _score += value;
        //}

        //public static List<Action<object, string>> ListListener = new List<Action<object, string>>();
        //public static List<PersonProcess> ListProcess = new List<PersonProcess>();

        [SerializeField]
        string _name;

        [SerializeField]
        int _score;

        //[SerializeField]
        //int _press;

        //[SerializeField]
        //int _heath;

        //[SerializeField]
        //int _age;

        //Dictionary<string, string> _Flags = new Dictionary<string, string>();

        static List<Person> _Males = new List<Person>();
        static List<Person> _Females = new List<Person>();
    }

    //public class PersonManager
    //{
    //    public PersonManager(int count, Boolean isMale)
    //    {
    //        while (lstPerson.Count < count)
    //        {
    //            Person p = new Person(isMale);
    //            if (lstPerson.Find(x => x.name.Equals(p.name)) != null)
    //            {
    //                continue;
    //            }

    //            lstPerson.Add(p);
    //        }

    //    }

    //    public Person[] GetRange(int start, int end)
    //    {
    //        if (start > lstPerson.Count || start >= end)
    //        {
    //            Person[] ps = { };
    //            return ps;
    //        }

    //        if (end > lstPerson.Count)
    //        {
    //            end = lstPerson.Count;
    //        }

    //        return lstPerson.GetRange(start, end - start).ToArray();
    //    }

    //    //public List<Person> GetPersonBySelector(SelectElem Selector)
    //    //{
    //    //    List<Person> lstResult = lstPerson.Where(Selector.Complie<Person>()).ToList();

    //    //    return lstResult;
    //    //}

    //    public Person GetByName(string name)
    //    {
    //        return lstPerson.Find(x => x.name == name);
    //    }

    //    public Person[] GetByName(string[] names)
    //    {
    //        List<Person> lstResult = new List<Person>();
    //        foreach (string name in names)
    //        {
    //            Person o = GetByName(name);
    //            if (o == null)
    //            {
    //                continue;
    //            }

    //            lstResult.Add(o);
    //        }

    //        return lstResult.ToArray();
    //    }

    //    public int Count
    //    {
    //        get
    //        {
    //            return lstPerson.Count;
    //        }
    //    }

    //    public void Sort(Comparison<Person> comparison)
    //    {
    //        lstPerson.Sort(comparison);
    //    }

    //    public void Listen(object obj, string cmd)
    //    {
    //        switch (cmd)
    //        {
    //            case "DIE":
    //                lstPerson.Remove((Person)obj);
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    public void Add(Person p)
    //    {
    //        lstPerson.Add(p);
    //    }

    //    [SerializeField]
    //    private List<Person> lstPerson = new List<Person>();
    //}
}
