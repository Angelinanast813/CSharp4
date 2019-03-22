using System;
using System.ComponentModel.DataAnnotations;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Models
{
    [Serializable]
    internal class User
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _login;
        private DateTime _birthdate;
        private bool _isAdult;
        private bool _isBirthday;
        private string _age;
        private string _chineseSign;
        private string _sunSign;
        #endregion

        #region Properties
        public string FirstName
        {
            get{ return _firstName; }
            private set{ _firstName = value;}
        }
        public string LastName
        {
            get{ return _lastName;}
            private set{ _lastName = value; }
        }
        public string Email
        {
            get{ return _email; }
            private set
            {
                if (new EmailAddressAttribute().IsValid(value))
                    _email = value;
                else
                    throw new InvalidEmail(value);
            }
        }
        public string Login{
            get{ return _login;}
            private set{ _login = value; }
        }
        
        public DateTime BirthDate
        {
            get { return _birthdate; }
            private set
            {
                      if (value.Year > DateTime.Now.Year)
                    throw new InvalidFutureDate();

                if (value.Year < (DateTime.Now.Year - 135))
                    throw new InvalidPastDate();
      
                _birthdate = value;
            }

        }
         public string BirthDateDateOnly
          {
              get { return _birthdate.ToString("yyyy-MM-dd"); }
          } 

       
        public string DateString
        {
            get { return _birthdate.ToShortDateString(); }
            
        }
        public bool IsAdult
        {
            get { return _isAdult; }
            private set { _isAdult = value; }
        }
        public bool IsBirthday
        {
            get { return _isBirthday; }
            private set { _isBirthday = value; }
        }
        public string Age
        {
            get { return _age; }
            private set { _age = value; }
        }
        public string ChineseSign
        {
            get { return _chineseSign; }
            private set { _chineseSign = value; }
        }
        public string SunSign
        {
            get { return _sunSign; }
            private set { _sunSign = value; }
        }
        #endregion


        public User(string name, string surname, string email, DateTime birthdate)
        {
            BirthDate = birthdate;
            FirstName = name;
            LastName = surname;
            IsAdult = isadult();
            IsBirthday = isBirthday();
            ChineseSign = chinesesign();
            SunSign = sunsign();
            Email = email;
          }


       

        bool isBirthday()
        {
            var today = DateTime.Today;
            if (_birthdate.Day == today.Day & _birthdate.Month == today.Month)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isadult()
        {
            DateTime today = DateTime.Today;
            var age = today.Year - _birthdate.Year;
            
            if (age >= 18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string chinesesign()
        {
            string[] zodiakCh = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Ram", "Monkey", "Rooster", "Dog", "Pig" };

            if (_birthdate.Year % 12 == 4)
            {
                return zodiakCh[0]; // Крыса
            }
            else
                if (_birthdate.Year % 12 == 5)
            {
                return zodiakCh[1]; // Бык
            }
            else
                if (_birthdate.Year % 12 == 6)
            {
                return zodiakCh[2]; // Тигр
            }
            else
                if (_birthdate.Year % 12 == 7)
            {
                return zodiakCh[3]; // Кролик
            }
            else
                if (_birthdate.Year % 12 == 8)
            {
                return zodiakCh[4]; // Дракон
            }
            else
                if (_birthdate.Year % 12 == 9)
            {
                return zodiakCh[5]; // Змея
            }
            else
                if (_birthdate.Year % 12 == 10)
            {
                return zodiakCh[6];// Лошадь
            }
            else
                if (_birthdate.Year % 12 == 11)
            {
                return zodiakCh[7];// Овца
            }
            else
                if (_birthdate.Year % 12 == 0)
            {
                return zodiakCh[8]; // Обезьяна
            }
            else
                if (_birthdate.Year % 12 == 1)
            {
                return zodiakCh[9]; // Петух
            }
            else
                if (_birthdate.Year % 12 == 2)
            {
                return zodiakCh[10];// Собака
            }
            else
                if (_birthdate.Year % 12 == 3)
            {
                return zodiakCh[11];// Свинья
            }
            return "";
        }
        private string sunsign()
        {
            string[] zodiak = { "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces" };


            if ((_birthdate.Month == 3 && _birthdate.Day >= 21) || (_birthdate.Month == 4 && _birthdate.Day <= 20))
            {
                return zodiak[0];   // Овен
            }
            else
                if ((_birthdate.Month == 4 && _birthdate.Day >= 21) || (_birthdate.Month == 5 && _birthdate.Day <= 20))
            {
                return zodiak[1];   // Телец
            }
            else
    if ((_birthdate.Month == 5 && _birthdate.Day >= 21) || (_birthdate.Month == 6 && _birthdate.Day <= 21))
            {
                return zodiak[2];   // Близнецы
            }
            else
    if ((_birthdate.Month == 6 && _birthdate.Day >= 22) || (_birthdate.Month == 7 && _birthdate.Day <= 22))
            {
                return zodiak[3];   // Рак
            }
            else
    if ((_birthdate.Month == 7 && _birthdate.Day >= 23) || (_birthdate.Month == 8 && _birthdate.Day <= 23))
            {
                return zodiak[4];   // Лев
            }
            else
    if ((_birthdate.Month == 8 && _birthdate.Day >= 24) || (_birthdate.Month == 9 && _birthdate.Day <= 23))
            {
                return zodiak[5];  // Дева
            }
            else
    if ((_birthdate.Month == 9 && _birthdate.Day >= 24) || (_birthdate.Month == 10 && _birthdate.Day <= 22))
            {
                return zodiak[6];   // Весы
            }
            else
    if ((_birthdate.Month == 10 && _birthdate.Day >= 23) || (_birthdate.Month == 11 && _birthdate.Day <= 22))
            {
                return zodiak[7];   // Скорпион
            }
            else
    if ((_birthdate.Month == 11 && _birthdate.Day >= 23) || (_birthdate.Month == 12 && _birthdate.Day <= 21))
            {
                return zodiak[8];   // Стрелец
            }
            else
    if ((_birthdate.Month == 12 && _birthdate.Day >= 22) || (_birthdate.Month == 1 && _birthdate.Day <= 20))
            {
                return zodiak[9];   // Козерог
            }
            else
    if ((_birthdate.Month == 1 && _birthdate.Day >= 21) || (_birthdate.Month == 2 && _birthdate.Day <= 19))
            {
                return zodiak[10];  // Водолей
            }
            else
    if ((_birthdate.Month == 2 && _birthdate.Day >= 20) || (_birthdate.Month == 3 && _birthdate.Day <= 20))
            {
                return zodiak[11];  // Рыбы
            }
            return "";
        }
    }

    internal class InvalidFutureDate : Exception
    {

        internal InvalidFutureDate() : base("You have not born yet!") { }
    }

    internal class InvalidPastDate : Exception
    {

        internal InvalidPastDate() : base("You are too old!") { }
    }

    internal class InvalidEmail : Exception
    {
        internal InvalidEmail(string email) : base($"Email is invalid: {email}") { }


        
    }
}
