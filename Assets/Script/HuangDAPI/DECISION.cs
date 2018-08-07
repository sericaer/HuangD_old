using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HuangDAPI
{
    public abstract class DECISION : ReflectBase
    {
        public DECISION()
        {
            title = StreamManager.uiDesc.Get(this.GetType().Name + "_TITLE");

            _funcTitle = GetDelegateInSubEvent<Func<string>>("Title",
                                                 () =>
                                                 {
                                                     FieldInfo field = _subFields.Where(x => x.Name == "title").First();
                                                     return (string)field.GetValue(this);
                                                 });
            _funcDesc = GetDelegateInSubEvent<Func<string>>("Desc",
                                                 () =>
                                                 {
                                                     return this.GetType().Name + "_DESC";
                                                 });
            _funcVisable = GetDelegateInSubEvent<Func<bool>>("Visable",
                                                  () =>
                                                  {
                                                      return true;
                                                  });
            _funcEnable = GetDelegateInSubEvent<Func<bool>>("Enable",
                                                  () =>
                                                  {
                                                      return true;
                                                  });
            _funcProcFinish = GetDelegateInSubEvent<Func<bool>>("ProcFinish", null);

            _funcEnableEvent = GetDelegateInSubEvent<DelegateEnableEvent>("EnableEvent",
                                                  (ref dynamic param) =>
                                                  {
                                                      return "";
                                                  });
            _funcDisableEvent = GetDelegateInSubEvent<Func<string>>("DisableEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });
            _funcFinishEvent = GetDelegateInSubEvent<Func<string>>("FinishEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });
            _funcStartEvent = GetDelegateInSubEvent<Func<string>>("StartEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });
            _funcProcEvent = GetDelegateInSubEvent<Func<string>>("ProcEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });

            var fieldInfo = _subFields.Where(x => x.Name == "CostDay").SingleOrDefault();
            if (fieldInfo != null)
            {
                _CostDay = (int)fieldInfo.GetValue(this);
            }

            fieldInfo = _subFields.Where(x => x.Name == "Responsible").SingleOrDefault();
            if (fieldInfo != null)
            {
                _Responsible = (string)fieldInfo.GetValue(this);
            }
        }


        public Func<bool> _funcVisable;
        public Func<bool> _funcEnable;
        public Func<bool> _funcProcFinish;

        public Func<string> _funcTitle;
        public Func<string> _funcDesc;

        public delegate string DelegateEnableEvent(ref dynamic param);

        public DelegateEnableEvent _funcEnableEvent;
        public Func<string> _funcDisableEvent;

        public Func<string> _funcStartEvent;
        public Func<string> _funcFinishEvent;
        public Func<string> _funcProcEvent;

        public string _funcFinishEventParam;

        public int _CostDay = 0;
        public string _Responsible = "";
        protected string title;
        public IList<string> Flags;

        public dynamic param = new ExpandoObject();
    }

    public class Decision
    {
        public Decision(string name)
        {
            this._name = name;
        }

        //public Person ResponsiblePerson
        //{
        //    get
        //    {
        //        if(MyGame.DecisionManager.Procs.ContainsKey(_name))
        //        {
        //            return MyGame.DecisionManager.Procs[_name].ResponsiblePerson;
        //        }

        //        return MyGame.DecisionManager.Plans[_name].ResponsibleOffice.person;

        //    }
        //}

        public void process()
        {
            if(MyGame.DecisionManager.Procs.Exists((obj)=>obj.name == _name))
            {
                throw new Exception("decision already process! name:" + _name);
            }

            MyGame.DecisionManager.Plans.Find((obj) => obj.name == _name).process();
        }

        public IList<string> Flags
        {
            get
            {
                if (MyGame.DecisionManager.Procs.Exists((obj) => obj.name == _name))
                {
                    return MyGame.DecisionManager.Procs.Find((obj) => obj.name == _name).Flags;
                }

                throw new Exception("decision not process! name:" + _name);
            }

        }

        string _name;
    }

    public interface DecisionProc
    {
        Person ResponsiblePerson { get; }
        List<string> Flags { get; }
    }

    public interface DecisionPlan
    {
        void process();
        Office ResponsibleOffice { get; }
    }
}
