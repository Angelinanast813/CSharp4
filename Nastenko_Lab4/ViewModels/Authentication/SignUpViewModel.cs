using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels.Authentication
{
    internal class SignUpViewModel:BaseViewModel
    {


        private DateTime _birthdate;
        private string _firstName;
        private string _lastName;
        private string _email;

        internal bool IsAdult { get; }
        internal bool IsBirthday { get; }
        internal int Age { get; }
        internal string ChineseSign { get; }
        internal string SunSign { get; }

        #region Commands
        private ICommand _signUpCommand;
        private ICommand _toSignInCommand;
        private ICommand _closeCommand;
        #endregion

        public DateTime BirthDate
        {
            get {
                if(_birthdate.Year == 1 && _birthdate.Month == 1 && _birthdate.Day == 1)
        {
                    _birthdate = DateTime.Now;
                }
                return _birthdate;
            }
            private set
            {
                
                if (value.Year > DateTime.Now.Year)

                    throw new InvalidFutureDate();

                if (value.Year < (DateTime.Now.Year - 135))
                {
                    throw new InvalidPastDate();
                }
                
                _birthdate = value;
                OnPropertyChanged();
            }
        }


        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        #region Commands

        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand =
                           new RelayCommand<object>(SignUpImplementation, CanSignUpExecute));
            }
        }

        public ICommand ToSignInCommand
        {
            get
            {
                return _toSignInCommand ?? (_toSignInCommand = new RelayCommand<object>(ToSignInImplementation));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseImplementation));
            }
        }

        #endregion

        private bool CanSignUpExecute(object obj)
        {
            return !String.IsNullOrEmpty(_firstName) &&
                   !String.IsNullOrEmpty(_lastName) &&
                   !String.IsNullOrEmpty(_email);
        }

        private async void SignUpImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(1000);
                    if (!new EmailAddressAttribute().IsValid(_email))
                    {
                        MessageBox.Show($"Sign Up failed fo user {_email}. Reason:{Environment.NewLine} Email {_email} is not valid.");
                        return false;
                    }
                    if (StationManager.DataStorage.UserExists(_email))
                    {
                        MessageBox.Show($"Sign Up failed fo user {_email}. Reason:{Environment.NewLine} User with such login already exists.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed fo user {_email}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                try
                {
                    var user = new User(_firstName, _lastName, _email, _birthdate);
                    StationManager.DataStorage.AddUser(user);
                    StationManager.CurrentUser = user;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed fo user {_firstName} {_lastName}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                //MessageBox.Show($"User {_firstName} {_lastName} was successfully created.");
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
                NavigationManager.Instance.Navigate(ViewType.Main);
        }

        private void ToSignInImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.SignIn);
        }

        private void CloseImplementation(object obj)
        {
            StationManager.CloseApp();
        }

    }

}
