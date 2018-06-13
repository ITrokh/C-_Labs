using System;

namespace University.Entities.Objective
{
    public class Student : Abstract.Person
    {
        private int _id;
        private string _university;
        private int _term;


        public Student(string name, string surname, DateTime birthday,
            int id, string university, int term) : base(name, surname, birthday)
        {
            if (university == null)
                throw new ArgumentException("Невозможно создать экземпляр Student через параметры");

            this._id = id;
            this._university = university;
            this._term = term;
        }


        public int Id
        {
            get
            {
                return this._id;
            }
        }

        public string University
        {
            get
            {
                return this._university;
            }
        }

        public int Term
        {
            get
            {
                return this._term;
            }
        }

        public override void Show()
        {
            base.Show();

            System.Console.WriteLine("Тип : {0}" + this.GetType());
            System.Console.WriteLine("Id : {0}", _id);
            System.Console.WriteLine("Университет : {0}", _university);
            System.Console.WriteLine("Срок : {0}", _term);
        }

        public override object DeepCopy()
        {
            Student student = new Student(Name, Surname, Birthday, _id, _university, _term);


            return student;
        }

    }
}
