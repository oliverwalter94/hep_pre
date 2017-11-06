using System.Collections.ObjectModel;
using System.Linq;

namespace Human_Enterprise_Project
{
    class Home
    {
        int humanCapacity;
        int resourceCapacity;
        public int money;
        private int food;

        Collection<Resource> recourceList = new Collection<Resource>();
        Collection<Human> residents = new Collection<Human>();

        public int Food
        {
            get
            {
                return food;
            }

            set
            {
                food = value;
            }
        }

        public Home (int initHumanCapacity, int initResourceCapacity, int initMoney = 0)
        {
            humanCapacity = initHumanCapacity;
            resourceCapacity = initResourceCapacity;
            money = initMoney;
            Food = 25;
        }

        public bool addHuman(Human resident)
        {
            if (residents.Count() < humanCapacity)
            {
                residents.Add(resident);
                return true;
            }
            else
                return false;
    
        }

        public void removeHuman(Human resident)
        {
            residents.Remove(resident);
        }

        void addResource(Resource r)
        {
            
        }

        bool hasResource(ResourceType type) {
            return false;
        }

        public bool isEmpty()
            {
                return residents.Count == 0;
            }

    }
}
