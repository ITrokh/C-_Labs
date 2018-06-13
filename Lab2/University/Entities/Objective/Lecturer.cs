using System;
using System.Collections.Generic;

namespace University.Entities.Objective
{
    public class Lecturer : Abstract.Person
    {
        private List<string> _subjects = null;
        private string _university;

        public Lecturer(string name, string surname, DateTime birthday,
            string subject, string university) : base(name, surname, birthday)
        {
            if ((subject == null) || (university == null))
                throw new ArgumentException("Невозможно создать экземпляр Lecturer через параметры");

            this._subjects = new List<string>();

            this._subjects.Add(subject);
            this._university = university;
        }


        public string University
        {
            get
            {
                return this._university;
            }
        }

        public List<string> Subjects
        {
            get
            {
                return this._subjects;
            }
        }

        public string AddSubject
        {
            set
            {
                if(value != null)
                    this._subjects.Add(value);
            }
        }

        public override void Show()
        {
            base.Show();

            System.Console.WriteLine("Тип : {0}", this.GetType());
            System.Console.WriteLine("Университет : {0}", _university);
            foreach(var subject in _subjects)
            {
                System.Console.WriteLine("Предмет : {0}", subject);
            }
        }

        public override object DeepCopy()
        {
            Lecturer lecturer = new Lecturer(Name, Surname, Birthday, _subjects[0], _university);
            
            foreach(var subject in _subjects)
            {
                lecturer.AddSubject = subject;
            }


            return lecturer;
        }

    }
}
