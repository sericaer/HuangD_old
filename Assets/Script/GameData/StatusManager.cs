using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

public partial class MyGame
{
    [Serializable]
    public class TWStatus : HuangDAPI.Status
    {
        public TWStatus(string ID)
        {
            _ID = ID;
            _param = new HuangDAPI.StatusParam("");
        }

        public string ID
        {
            get
            {
                return _ID;
            }
        }

        public HuangDAPI.StatusParam param
        {
            get
            {
                return _param;
            }
            set
            {
                _param = value;
            }
        }

        public string name
        {
            get
            {
                return StreamManager.uiDesc.Get(ID);
            }
        }

        public string desc
        {
            get
            {
                string result = StreamManager.uiDesc.Get(ID + "_DESC");
                if (_param != null)
                {
                    result += "\n";
                    result += StreamManager.uiDesc.Get(_param.ToString());
                }

                return result;
            }
        }

        private string _ID;
        private HuangDAPI.StatusParam _param;
    }

    [Serializable]
    public class StatusManager
    {

        public void Listen(object obj, string cmd)
        {
            switch (cmd)
            {
                case "DIE":
                    {
                        for (int i = listStatus.Count - 1; i >= 0; i--)
                        {
                            PersonProcess process = listStatus[i].param as PersonProcess;
                            if (process == null)
                            {
                                continue;
                            }

                            if (process.opp == obj)
                            {
                                listStatus[i].param = null;
                                continue;
                            }

                            if (process.tag.Contains(obj))
                            {
                                process.tag.Remove(obj);
                                if (process.tag.Count == 0)
                                {
                                    listStatus[i].param = null;
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public List<HuangDAPI.Status> listStatus = new List<HuangDAPI.Status>();
    }
}
