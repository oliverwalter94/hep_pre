using System.Collections.ObjectModel;

namespace Human_Enterprise_Project
{
    class Resource
    {
        private ResourceType type;
        private int amount;

        // get/set methods for all vars
        internal ResourceType Type { get { return type; }
            set
            {
                type = value;
            }
        }
        public int Amount { get { return amount; }
            set
            {
                amount = value;
            }
        }

        public Resource(ResourceType initType, int initAmount)
        {
            Type = initType;
            Amount = initAmount;
        }
        
        public Resource FindResourceInStorage(Collection<Resource> storage)
        {
            Resource result = null;

            foreach(Resource r in storage)
            {
                if (r.type == type)
                    result = r;
            }

            return result;
        }

        public void AddResourceToStorage(Collection<Resource> storage )
        {
            Resource r = FindResourceInStorage(storage);

            if (r != null)
                r.Amount += Amount;
            else
                storage.Add(this);
        }

    }
}
