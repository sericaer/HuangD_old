using HuangDAPI;
using System.Linq;

namespace native
{
    class EVENT_SG_EMPTY : EVENT_HD
    {
        bool Precondition()
        {
            emptyOffice = GMData.GetOffice(Selector.ByOffice("SGX").ByPerson(null));
            if (emptyOffice != null)
            {
                Faction[] factions = GMData.GetFactions();
                foreach(Faction faction in factions)
                {
                    Person[] persons = GMData.GetPersons(Selector.ByOffice("JQX")).ByFaction(faction.name));
                    if (persons.Length == 0)
                    {
                        continue;
                    }

                    Person p = persons.OrderByDescending(p => p.score).ToArray().First();
                    listPerson.Add(p);
                }

                if(listPerson.Count != 0)
                    return true;
            }
                
            return false;         
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.Emp.Heath--;
            }
        }
        class OPTION2 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.Emp.Heath--;
            }
        }
        class OPTION3 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.Emp.Heath--;
            }
        }

        private Office emptyOffice;
        private List<Person> listPerson;
    }
}