﻿using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;

using System.Linq;

public partial class MyGame
{
    [Serializable]
    public class Province : HuangDAPI.Province
    {
        public static STATUS[] GetDebuffStatus()
        {
            List<STATUS> result = new List<STATUS>();
            foreach (var status in Enum.GetValues(typeof(Province.ENUM_PROV_STATUS)))
            {

                FieldInfo field = status.GetType().GetField(status.ToString());
                Province.ProvStatusAttribute attribute = Attribute.GetCustomAttribute(field, typeof(Province.ProvStatusAttribute)) as Province.ProvStatusAttribute;

                if (!attribute.isBuff)
                {
                    result.Add(new STATUS(status.ToString(), false));
                }
            }

            return result.ToArray();
        }

        public class ProvinceAttribute : Attribute
        {
            public ENUM_ECONOMY economy;
        }

        public enum ENUM_PROV
        {
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ1,
            [ProvinceAttribute(economy = ENUM_ECONOMY.LOW)]
            ZHOUJ2,
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ3,
            [ProvinceAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ4,
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ5,
            [ProvinceAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ6,
            [ProvinceAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ7,
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ8,
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ9
        }

        public enum ENUM_BUFF_TYPE
        {
            NORMAL,
            BUFF,
            DEBUFF
        }

        public class ProvStatusAttribute : Attribute
        {
            public bool isBuff;
        }

        public enum ENUM_PROV_STATUS
        {
            [ProvStatusAttribute(isBuff = false)]
            HONG,
            [ProvStatusAttribute(isBuff = false)]
            HAN,
            [ProvStatusAttribute(isBuff = false)]
            HUANG,
            [ProvStatusAttribute(isBuff = false)]
            WEN,
            [ProvStatusAttribute(isBuff = false)]
            ZHEN,
            [ProvStatusAttribute(isBuff = false)]
            KOU,
            [ProvStatusAttribute(isBuff = false)]
            FAN,
     
            [ProvStatusAttribute(isBuff = false)]
            FENG
        }

        public class STATUS
        {
            public STATUS(string name, bool isbuff)
            {
                this.name = name;
                this.isbuff = isbuff;

                days = 0;
                cover = 0.0;
                corrput = false;
                recover = false;
            }

            public string name;
            public int days;
            public double cover;
            public bool corrput;
            public bool isbuff;
            public bool recover;
        }

        public enum ENUM_ECONOMY
        {
            HIGH,
            MID,
            LOW,

        }

//        public static STATUS[] GetStatusArray(bool isBufff)
//        {
//            List<string> result = new List<string>();
//            foreach (ENUM_PROV_STATUS status in listStatus)
//            {
//                FieldInfo field = status.GetType().GetField(status.ToString());
//                ProvStatusAttribute attribute = Attribute.GetCustomAttribute(field, typeof(ProvStatusAttribute)) as ProvStatusAttribute;
//                if (!attribute.isBuff)
//                {
//                    result.Add(status.ToString());
//                }
//            }
//
//            return result.ToArray();
//        }

        public Province(string name, ENUM_ECONOMY economy)
        {
            _name = name;
            _economy = economy;
            listStatus = new List<STATUS>();
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public string economy
        {
            get
            {
                return _economy.ToString();
            }
        }

        public STATUS[] status
        {
            get
            {
                return listStatus.ToArray();
            }
        }

        public override string ToString()
        {
            return name;
        }
//
//        public void SetBuff(string buff)
//        {
//            ENUM_PROV_STATUS e = (ENUM_PROV_STATUS)Enum.Parse(typeof(ENUM_PROV_STATUS), buff);
//            if(!listStatus.Contains(e))
//            {
//                listStatus.Add(e);
//            }
//        }
//
//        public void ClearBuff(string buff)
//        {
//            ENUM_PROV_STATUS e = (ENUM_PROV_STATUS)Enum.Parse(typeof(ENUM_PROV_STATUS), buff);
//            listStatus.Remove(e);
//        }
//
//        public bool HasBuff(string buff)
//        {
//            if(buff == null)
//            {
//                foreach (ENUM_PROV_STATUS status in listStatus)
//                {
//                    FieldInfo field = status.GetType().GetField(status.ToString());
//                    ProvStatusAttribute attribute = Attribute.GetCustomAttribute(field, typeof(ProvStatusAttribute)) as ProvStatusAttribute;
//                    if(attribute.buffType == ENUM_BUFF_TYPE.BUFF)
//                    {
//                        return true;
//                    }
//                }
//
//                return false;
//            }
//
//            ENUM_PROV_STATUS e = (ENUM_PROV_STATUS)Enum.Parse(typeof(ENUM_PROV_STATUS), buff);
//            return listStatus.Contains(e);
//        }
//
//        public bool HasDebuff(string buff)
//        {
//            if (buff == null)
//            {
//                foreach (ENUM_PROV_STATUS status in listStatus)
//                {
//                    FieldInfo field = status.GetType().GetField(status.ToString());
//                    ProvStatusAttribute attribute = Attribute.GetCustomAttribute(field, typeof(ProvStatusAttribute)) as ProvStatusAttribute;
//                    if (attribute.buffType == ENUM_BUFF_TYPE.DEBUFF)
//                    {
//                        return true;
//                    }
//                }
//
//                return false;
//            }
//
//            ENUM_PROV_STATUS e = (ENUM_PROV_STATUS)Enum.Parse(typeof(ENUM_PROV_STATUS), buff);
//            return listStatus.Contains(e);
//        }
//
        public STATUS[] GetBuffArray(bool isBuff)
        {
            List<STATUS> result = new List<STATUS>();
            //foreach (STATUS status in listStatus)
            //{
            //    if (status.isbuff == isBuff)
            //        result.Add(status);
            //}

            return result.ToArray();
        }
            

        private string _name;
        public List<STATUS> listStatus;
        private ENUM_ECONOMY _economy;

    }

    [Serializable]
    public class ProvinceManager : IEnumerable
    {
        public ProvinceManager()
        {
            foreach (var zj in Enum.GetValues(typeof(Province.ENUM_PROV)))
            {

                FieldInfo field = zj.GetType().GetField(zj.ToString());
                Province.ProvinceAttribute attribute = Attribute.GetCustomAttribute(field, typeof(Province.ProvinceAttribute)) as Province.ProvinceAttribute;

                Province ZhoujObj = new Province(zj.ToString(), attribute.economy);
                lstProvince.Add(ZhoujObj);

            }
        }

        public Province[] provinces
        {
            get
            {
                return lstProvince.ToArray();
            }
        }

        delegate bool delCompare(MyGame.Province.STATUS a);

        //public List<Province> GetProvinceBySelector(SelectElem selectElem)
        //{
        //    List<Province> lstResult = new List<Province>();

        //    foreach(Province  prov in lstProvince)
        //    {
        //        List<Province.STATUS> lst = prov.listStatus.Where(selectElem.Complie<Province.STATUS>()).ToList();
        //        if (lst.Count != 0)
        //        {
        //            lstResult.Add(prov);
        //        }
        //    }

        //    return lstResult;
        //}

        public Province GetByName(string name)
        {
            return lstProvince.Find(x => x.name == name);
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < lstProvince.Count; i++)
            {
                yield return lstProvince[i];
            }
        }

        public Province[] Array()
        {
             return lstProvince.ToArray();
        }

        private List<Province> lstProvince = new List<Province>();
    }

}

