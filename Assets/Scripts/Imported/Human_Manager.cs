using System;
using System.Collections.ObjectModel;

namespace Human_Enterprise_Project
{

    public class Human_Manager
    {
        public Collection<Human> people = new Collection<Human>();
        public Collection<Human> addPeople = new Collection<Human>();
        public Collection<Human> deadPeople = new Collection<Human>();
        public int overallHappiness = 0;

        private int dead = 0;

        public int Dead
        {
            get
            {
                return dead;
            }

            set
            {
                dead = value;
            }
        }

        public void HumanGameCycle()
        {
            foreach (Human h in people)
                h.DoGameCycle(people);
            
            addPeopleFromCache();
            removeDeadPeople();
            CalcOverallHappiness();
        }

        void addPeopleFromCache()
        {
            if(addPeople.Count > 0)
            {
                foreach (Human h in addPeople)people.Add(h);
                addPeople.Clear();
            }
        }

        void removeDeadPeople()
        {
            foreach (Human h in deadPeople)
            {
                people.Remove(h);
            }
            deadPeople.Clear();
        }

        void CalcOverallHappiness()
        {
            int sum = 0;
            foreach(Human h in people)
                sum += h.Happiness;
            if (people.Count > 0)
                overallHappiness = sum / people.Count;
            else
                overallHappiness = 0;
        }

        public void PrintPeople()
        {
            foreach (Human h in people)
                Console.WriteLine(h.ToString());
        }

    }
    

}
