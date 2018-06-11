using System;
using UnityEngine;

public partial class MyGame
{
    public class CountryFlag 
    {
        public CountryFlag()
        {
        }
    }

    public class PersonFlag : HuangDAPI.PersonFlag
    {
        public PersonFlag(string name, string effect)
        {
            _name = name;
            _effect = effect;
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        string _name;
        string _effect;
    }
}
