using System;

namespace Human_Enterprise_Project
{
    class JobType
    {
        private String name;
        private int avaliablePositions;
        private int defaultPayment;
        private int energy;
        private Resource resource;
        private Resource bonusResource;


        //variable encapsulation
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public int AvaliablePositions
        {
            get
            {
                return avaliablePositions;
            }
            set
            {
                avaliablePositions = value;
            }
        }
        public int DefaultPayment
        {
            get
            { 
                return defaultPayment;
            }
                set
            {
                defaultPayment = value;
            }
        }
        public int Energy
        {
            get 
            { 
                return energy;
            }
            set
            {
                energy = value;
            }
        }
        internal Resource Resource
        {
            get 
            { 
                return resource;
            }
            set
            {
                resource = value;
            }
        }
        internal Resource BonusResource
        {
            get
            {
                return bonusResource;
            }

            set
            {
                bonusResource = value;
            }
        }

        public JobType(String initName, int initAvaliablePositions, Resource initResource, int initDefaultPayment, int initEnergy)
        {
            Name = initName;
            AvaliablePositions = initAvaliablePositions;
            Resource = initResource;
            DefaultPayment = initDefaultPayment;
            energy = initEnergy;
        }
    }
}

