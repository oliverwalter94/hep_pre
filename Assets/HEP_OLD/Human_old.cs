using System;
using System.Collections.ObjectModel;
using Random = UnityEngine.Random;

namespace Human_Enterprise_Project
{
    public class Human
    {
        public enum GENDER { Male, Female };
        const int AgeOfConsent = 18;
        public const int RelationshipAge = 14;
        const int Birthrate = 12;
        public const int AgeDiff = 6;
        
        //general params
        String Name;
        GENDER Gender;
        byte Age;
        bool Alive;
        int Id;

        //social params
        Human Partner;
        Human Mother;
        Human Father;

        //io params
        private int happiness;
        private byte hunger;
        private int health;
        int Energy;

        //skill params
        byte Charisma;
        byte Fertility;

        //life params
        Job job;
        Home home;

        public int Happiness
        {
            get
            {
                return happiness;
            }

            set
            {
                happiness = value;
                if(happiness > 100)
                    happiness = 100;
                else if(happiness < 0)
                    happiness = 0;
            }
        }
        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
                if (health > 100)
                    health = 100;
                else if (health < 0)
                    health = 0;
            }
        }
        public byte Hunger
        {
            get
            {
                return hunger;
            }

            set
            {
                hunger = value;
                if (hunger > 100)
                    hunger = 100;
                else if (hunger < 100)
                    hunger = 100;
            }
        }

        public Human(string initName, GENDER initGender, int initId, byte Age, Human initFather = null, Human initMother = null)
        {
            Name = initName;
            Gender = initGender;
            Age = 1;
            Alive = true;
            Id = initId;

            Father = initFather;
            Mother = initMother;

            Happiness = 75;
            Hunger = 75;
            Health = 100;
            Energy = 100;

            Charisma = (byte)Random.Range(1, 100);
            Fertility = (byte)Random.Range(1, 100);
            
        }

        public void HaveBaby()
        {
            int chance = Random.Range(1, 100);
            if (Partner.Alive && (Age >= AgeOfConsent) && (Partner.Age >= AgeOfConsent) && (Fertility >= chance) && (Birthrate >= chance) && (Age <= 45) && (Partner.Age <= 55))
            {

                GENDER gender = (GENDER)Random.Range(0, 1);
                Human baby = new Human(GetName(gender), gender, 1, 1, this, Partner)
                {
                    Charisma = (byte)Random.Range(Charisma, Partner.Charisma),
                    Fertility = (byte)Random.Range(Fertility, Partner.Fertility),

                    Mother = this,
                    Father = Partner
                };
                baby.home = home;
                home.addHuman(baby);

                Manager.human_mng.addPeople.Add(baby);

                // TODO: Notify CommonFunctions.DisplayMessage(baby.Name + " was just born!\n" + Name + " and " + Partner.Name + " are their parents.", 2);
                Happiness = 100;
                Partner.Happiness = 100;
            }
        }

        void Metabolize()
        {
            Hunger -= 25;
            
            if (home.Food > 0)
            {
                Eat(30);
                home.Food--;
            }
            
            if (Hunger == 0)
                Health -= 5;
            else if (Hunger <= 25)
                Health -= 10;

            if (Health == 0)
                Die("starvation");
            DoAge();
        }
        
        void DoAge()
        {
            if (Alive)
            {
                Age++;
                if(Age > 65 && Random.Range(1,1100) < Age)
                    {
                    Die("old age of " + Age);
                    }
            }
        }

        void Eat(byte nutrition)
        {
            Hunger += nutrition;
            if (Hunger > 100)
                Health += 5;
        }

        void Die(String reason)
        {
            Alive = false;
            if(Partner != null)
            {
                Partner.Partner = null;
                Partner.Happiness -= 50;
            }
            if(home != null)
                home.removeHuman(this);
            Manager.human_mng.Dead++;
            Manager.human_mng.deadPeople.Add(this);
            // TODO: Notify CommonFunctions.DisplayMessage(Name + " just died from " + reason, 1, false);
        }
        public override string ToString()
        {
            String result = "";
            result = "Name: " + Name + "\n Age: " + Age + "\n Gender: " + Gender + "\n Charisma: " + Charisma;
            return result;
        }

        public static string GetName(GENDER g)
        {
            String[] maleNames = new String[] { "Oliver", "Peter", "Benjamin", "Philip", "Klaus", "Herbert", "Alfred", "Hilbert", "Markus", "Stefan", "Wolfgang"};
            String[] femaleNames = new String[] { "Silvia", "Melanie", "Julia", "Sophie", "Lisa", "Lena", "Katrin", "Karin", "Anna", "Miriam", "Maggie", "Christina", "Claudia", "Emma"};
            return g == GENDER.Male ? maleNames[Random.Range(1, maleNames.Length) - 1] : femaleNames[Random.Range(1, femaleNames.Length) - 1];
        }

        public void FindPartner(Collection<Human> people)
        {
            int successrate = Random.Range(1, 100);

            if ((successrate < Charisma) && (Age >= Human.RelationshipAge))
            {
                foreach (Human p in people)
                {
                    if (p.Alive && (p.Gender != Gender) && (Age > p.Age - AgeDiff) && (Age < p.Age + AgeDiff) && (p.Age >= RelationshipAge) && (p.Partner == null))
                    {
                        p.Partner = this;
                        Partner = p;
                        // TODO: Notify CommonFunctions.DisplayMessage(p.Name + " is now in a Relationship with " + Name);
                        p.Happiness = 100;
                        Happiness = 100;
                        // move together
                        if(home != null && p.home == null)
                        {
                            p.home = home;
                            home.addHuman(p);
                            // TODO: Notify CommonFunctions.DisplayMessage(Name + " and " + p.Name + " just moved together.");
                        } else if (home == null && p.home != null)
                        {
                            home = p.home;
                            home.addHuman(this);
                            // TODO: Notify CommonFunctions.DisplayMessage(Name + " and " + p.Name + " just moved together.");
                        }else if(home != null && p.home != null)
                        {
                            if(Random.Range(0,1) == 0)
                            {
                                p.home.removeHuman(p);
                                p.home = home;
                                home.addHuman(p);

                            }else {
                                home.removeHuman(this);
                                home = p.home;
                                home.addHuman(this);
                            }
                            // TODO: Notify CommonFunctions.DisplayMessage(Name + " and " + p.Name + " just moved together.");
                        }
                        break;
                    }
                } 
            }
        }
        
        public void Socialize(Collection<Human> people)
        {
            if (Partner == null)
                FindPartner(people);
            else if (Gender == GENDER.Female)
                HaveBaby();

        }

        public void DoGameCycle(Collection<Human> people)
        {
            if (Alive)
            {
                if(home == null)
                {
                    if (FindHome())
                        home.addHuman(this);
                    if(home != null && Partner != null)
                    {
                        Partner.home = home;
                        home.addHuman(Partner);
                    }
                        
                }
                Metabolize();
                Socialize(people);
                Work();
            }
        }
        
        void Work()
        {
            if(Age >= 18 && job == null)
            {
                job = Manager.GetRandomJob();
                //if (job != null)
                    // TODO: Notify CommonFunctions.DisplayMessage(Name + " just started to work as a " + job.Type.Name);
            }

            if(job != null)
            {
                Energy += job.Type.Energy;
                //TODO : use individual payment;
                home.money += job.Type.DefaultPayment;
            }       
            
        }

        public bool FindHome()
        {
            //home = Program.findHome();
            if (home != null)
            {
                // TODO: Notify CommonFunctions.DisplayMessage(Name + " is just moved into a new house!");
                home.addHuman(this);
                return true;
            }
            else
                return false;


        }
    }
}
