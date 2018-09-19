using RunIt.Api;
using System;

namespace RunIt.Models.Singleton
{
    public class Singleton
    {
        private static Singleton instance;
        public User User { get; set; }
        public int time { get; set; }
        public string distance { get; set; }
        public double distance_meter { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }

        public double begin_latitude { get; set; }
        public double begin_longitude{ get; set; }


        public string[] MenuItems = new string[8] { "Run", "Profile", "News", "History", "Friends", "Event", "Group" , "Sign out" };

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
