using System;

namespace ControlPaper2
{
    public class User
    {
        private String name;
        
        private String surname;

        public User(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string Name => name;

        public string Surname => surname;
    }
}