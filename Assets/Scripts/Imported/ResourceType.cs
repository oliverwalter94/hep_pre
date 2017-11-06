using System;

namespace Human_Enterprise_Project
{
    class ResourceType
    {
        String name;

        public ResourceType (String initName)
        {
            Name = initName;
        }

        public string Name { get 
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }
}
