namespace Human_Enterprise_Project
{
    class Job
    {
        JobType type;

        // variable encapsulation
        internal JobType Type { get { return type; }
            set
            {
                type = value;
            }
        }

        public Job(JobType initType)
        {
            Type = initType;
        }
        
        public Resource getBonus()
        {
            return Type.BonusResource;
        }
    }
}
