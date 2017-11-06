using System;
using System.Collections.ObjectModel;
using Random = UnityEngine.Random;

namespace Human_Enterprise_Project
{
    static class Manager
    {
        // type collections
        public static Collection<ResourceType> resourceTypes = new Collection<ResourceType>();
        public static Collection<JobType> jobtypes = new Collection<JobType>();


        // instance classes
        public static Collection<Home> homes = new Collection<Home>();
        public static Collection<Job> jobs = new Collection<Job>();
        
        // managing classes
        public static Human_Manager human_mng = new Human_Manager();

        // god storage and vars
        public static int god_money = 0;
        public static Collection<Resource> god_resources = new Collection<Resource>(); 

        public static void InitStemValues()
        {
            InitResourceTypes();
            InitJobTypes();

            //init dyn Collections

            InitHomes(10);
            InitJobs(25);
            
            InitPeople(4);
        }

        private static void InitResourceTypes()
        {
            resourceTypes.Add(new ResourceType("Oak Log"));
            resourceTypes.Add(new ResourceType("Rock"));
            resourceTypes.Add(new ResourceType("Stone"));
            resourceTypes.Add(new ResourceType("Stick"));
            resourceTypes.Add(new ResourceType("Plank"));
            resourceTypes.Add(new ResourceType("Mud"));
            resourceTypes.Add(new ResourceType("Brick"));
            resourceTypes.Add(new ResourceType("Sand"));
            resourceTypes.Add(new ResourceType("Fish"));
        }

        private static void InitJobTypes()
        {
            jobtypes.Add(new JobType("Lumberjack", 3, new Resource(GetResourceTypeByName("Oak Log"), 2), 5, 25));
            jobtypes.Add(new JobType("Miner", 3, new Resource(GetResourceTypeByName("Rock"), 2), 5, 30));
            jobtypes.Add(new JobType("Stick Collector", 3, new Resource(GetResourceTypeByName("Stick"), 2), 5, 10));
            jobtypes.Add(new JobType("Stone Collector", 3, new Resource(GetResourceTypeByName("Stone"), 2), 5, 15));
            jobtypes.Add(new JobType("Fisherman", 3, new Resource(GetResourceTypeByName("Fish"), 2), 5, 15));
        }
        
        private static void InitJobs(int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                jobs.Add(new Job(GetRandomJobType()));
            }
        }

        private static void InitHomes(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Manager.homes.Add(new Home(4, 25, 10));
            }
        }

        private static void InitPeople(int initPopulation)
        {
            for (int i = 0; i < initPopulation; i++)
            {
                Human.GENDER g = (Human.GENDER)Random.Range(0, 1);
                String name = Human.GetName(g);
                Human h = new Human(name, g, i, 18);
                //CommonFunctions.DisplayMessage(name + " just started to live here!");
                h.FindHome();
                human_mng.people.Add(h);
            }
        }
        
        public static ResourceType GetResourceTypeByName(String Name)
        {
            ResourceType result = null;

            foreach (ResourceType t in resourceTypes)
                if (t.Name == Name)
                    result = t;

            return result;
        }
        
        public static JobType GetRandomJobType()
        {
            return jobtypes[Random.Range(1, jobtypes.Count)- 1];
        }

        public static JobType GetJobTypeByName(String Name)
        {
            JobType result = null;

            foreach (JobType j in jobtypes)
                if (j.Name == Name)
                    result = j;

            return result;
        }

        public static Job GetRandomJob()
        {
            Job j = null;
            if(jobs.Count != 0)
                j = jobs[Random.Range(1, jobs.Count)];

            jobs.Remove(j);

            return j;
        }
    }
}