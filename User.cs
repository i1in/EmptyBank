using EmptyBank.Core;

namespace EmptyBank
{
    class User : ObservableObject
    {
        public int id { get; set; }

        private string login, pass, remember;

        public string Login 
        { 
            get { return login; }
            set { 
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Pass
        {
            get { return pass; }
            set { 
                pass = value;
                OnPropertyChanged("Pass");
            }
        }

        public string Remember
        {
            get { return remember; }
            set {
                remember = value;
                OnPropertyChanged("Remember");
            }
        }

        public User() { }

        public User(string login, string pass)
        {
            this.login = login;
            this.pass = pass;
        }
    }
}
