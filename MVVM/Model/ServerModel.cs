using EmptyBank.Core.Service;
namespace EmptyBank.MVVM.Model
{

    class ServerModel
    {
        private static int id, cvc, receiverId;
        private static double balance, limit, nocommission, receiverbalance, bonuses;
        private static long cardnumber, receiverCard;
        private static string login, password;
        public static string receivernickname;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Login 
        { 
            get { return login; }
            set { login = value; }
        }

        public string Password 
        { 
            get { return password; }
            set { password = value; }
        }
        public double Balance 
        { 
            get { return balance; }
            set { balance = value; }
        }

        public double Bonuses 
        { 
            get { return bonuses; }
            set { bonuses = value; }
        }

        public double Limit 
        { 
            get { return limit; }
            set {  limit = value; }
        }

        public long CardNumber 
        { 
            get { return cardnumber; }
            set {  cardnumber = value; }
        }
        public int Cvc 
        { 
            get { return cvc; }
            set { cvc = value; }
        }

        public double NoCommission 
        { 
            get { return nocommission; }
            set { nocommission = value; } 
        }

        public string ReceiverNickname
        {
            get { return receivernickname; }
            set { receivernickname = value; }
        }

        public double ReceiverBalance
        {
            get { return receiverbalance; }
            set { receiverbalance = value; }
        }

        public long ReceiverCard
        {
            get { return receiverCard; }
            set {  receiverCard = value; }
        }

        public int ReceiverId
        {
            get { return receiverId; }
            set { receiverId = value; }
        }

        public void Server()
        {
            AuthService authService = new AuthService();

            authService.FindByID(Id);
        }
    }
}
