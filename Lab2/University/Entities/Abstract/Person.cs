namespace University.Entities.Abstract
{
    public abstract class Person
    {
        private string _name;
        private string _surname;
        private System.DateTime _birthday;

        public abstract object DeepCopy();

        

        public Person(string name, string surname, System.DateTime birthday)
        {
            //  check for correct parametrs
            if ((name == null) || (surname == null) ||
                (birthday == null))
                throw new System.ArgumentException("Невозможно инициализировать пользователя через параметры");


            this._name = name;
            this._surname = surname;
            this._birthday = birthday;

        }

        #region Properties

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public string Surname
        {
            get
            {
                return this._surname;
            }
        }

        public System.DateTime Birthday
        {
            get
            {
                return this._birthday;
            }
        }

        public int Age
        {
            get
            {
                System.TimeSpan delta = System.DateTime.Now - _birthday;

                return delta.Days / 365;
            }
        }

        #endregion

        public virtual void Show()
        {
            System.Console.WriteLine("Имя : {0}", _name);
            System.Console.WriteLine("Фамилия : {0}", _surname);
            System.Console.WriteLine("День Рождение : {0}", _birthday);
            System.Console.WriteLine("Возраст : {0}", Age);
        }

        public static bool operator ==(Person obj1, object obj2) => obj1.Equals(obj2);

        public static bool operator !=(Person obj1, object obj2) => !obj1.Equals(obj2);

        #region Override

        public override bool Equals(object obj)
        {
            if(obj is Person)
            {
                Person person = obj as Person;

                return person._name == this._name &&
                    person._surname == this._surname &&
                    person._birthday == this._birthday;

            }

            return false;
        }

        public override int GetHashCode()
        {
            
            int hash = 0;
            hash += _name.GetHashCode();
            hash += _surname.GetHashCode();
         
            return hash;
        }

        public override string ToString()
        {
            string general = "Имя : {0}\nФаимилия : {1}\nДень Рождение : {2}";

            var res = System.String.Format(general, _name, _surname, System.Convert.ToString(_birthday));

            return res;
        }
        #endregion Override
    }
}
