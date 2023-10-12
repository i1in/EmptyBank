using EmptyBank.Core;
using EmptyBank.Core.Service;
using EmptyBank.MVVM.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.IO;

namespace EmptyBank.MVVM.ViewModel
{
    internal class BankBillViewModel : ObservableObject
    {
        public RelayCommand RefreshDataLimit {  get; set; }
        public RelayCommand RefreshDataCommission {  get; set; }
        public RelayCommand BillApprove { get; set; }
        public RelayCommand Approve { get; set; }
        public RelayCommand ShowCardTransactions { get; set; }
        public RelayCommand HideTransactionMenuButton { get; set; }
        public RelayCommand HideApproveMenuButton { get; set; }
        public RelayCommand ShowNicknameForm { get; set; }
        public RelayCommand ShowTransactionForm { get; set; }

        private string _leftLimit;
        public string LeftLimit
        {
            get { return _leftLimit; }
            set { _leftLimit = value; OnPropertyChanged(); }
        }

        private string _leftCommission;

        public string LeftCommission
        {
            get { return _leftCommission; }
            set { _leftCommission = value; OnPropertyChanged(); }
        }

        private string _billCard = string.Empty;
        public string BillViewCard
        {
            get { return _billCard; }
            set { _billCard = value; OnPropertyChanged(); }
        }

        private string _billBalance = string.Empty;
        public string BillBalance
        {
            get { return _billBalance; }
            set { _billBalance = value; OnPropertyChanged(); }
        }

        private string _billCardBrush = Brushes.White.ToString();
        public string BillCardBrush
        {
            get { return _billCardBrush; }
            set { _billCardBrush = value; OnPropertyChanged(); }
        }

        private string _billBalanceBrush = Brushes.White.ToString();
        public string BillBalanceBrush
        {
            get { return _billBalanceBrush; }
            set { _billBalanceBrush = value; OnPropertyChanged(); }
        }

        private string _billCardWarning;
        public string BillCardWarning
        {
            get { return _billCardWarning; }
            set { _billCardWarning = value; OnPropertyChanged(); }
        }

        private string _billBalanceWarning;
        public string BillBalanceWarning
        {
            get { return _billBalanceWarning; }
            set { _billBalanceWarning = value; OnPropertyChanged(); }
        }

        private string _billCardTransaction;
        public string BillCardTransaction
        {
            get { return _billCardTransaction; }
            set { _billCardTransaction = value; OnPropertyChanged(); }
        }

        private string _billBalanceTransaction;
        public string BillBalanceTransaction
        {
            get { return _billBalanceTransaction; }
            set { _billBalanceTransaction = value; OnPropertyChanged(); }
        }

        private Visibility _approveCardVisibility = Visibility.Collapsed;
        public Visibility ApproveCardVisibility
        {
            get { return _approveCardVisibility; }
            set { _approveCardVisibility = value; OnPropertyChanged(); }
        }

        public Visibility _showCardTransactions = Visibility.Collapsed;
        public Visibility CardTransaction
        {
            get { return _showCardTransactions; }
            set { _showCardTransactions = value; OnPropertyChanged(); }
        }

        private string _receiverData;
        public string ReceiverData
        {
            get { return _receiverData; }
            set { _receiverData = value; OnPropertyChanged(); }
        }

        public double resultBill, commission = 0, totalCommission = 0;
        double cashback = 0;

        private string _resultBill;
        public string ResultBill
        {
            get { return _resultBill; }
            set { _resultBill = value; OnPropertyChanged(); }
        }

        private string _resultBillCommission;
        public string ResultBillCommission
        {
            get { return _resultBillCommission; }
            set { _resultBillCommission = value; OnPropertyChanged(); }
        }

        private string _termRefresh;
        public string TermRefresh
        {
            get { return _termRefresh; }
            set { _termRefresh = value; OnPropertyChanged(); }
        }

        public BankBillViewModel() 
        { 
            ServerModel serverModel = new ServerModel();
            RefreshDataLimit = new RelayCommand(sender => RefreshLimit(sender));
            RefreshDataCommission = new RelayCommand(sender => RefreshLimit(sender));
            BillApprove = new RelayCommand(sender => ApproveButton(sender));
            Approve = new RelayCommand(sender => TransactionApprove(sender));
            ShowCardTransactions = new RelayCommand(sender => TransactionMenu(sender));
            HideTransactionMenuButton = new RelayCommand(sender => HideTransactionMenu(sender));
            HideApproveMenuButton = new RelayCommand(sender => HideApproveMenu(sender));

            LeftLimit = $"{serverModel.Limit} ₽ до конца месяца";
            LeftCommission = $"Осталось {serverModel.NoCommission} ₽ из 50000 ₽";
        }

        private void RefreshLimit(object sender)
        {
            ServerModel serverModel = new ServerModel();
            AuthService authService = new AuthService();
            serverModel.Limit = float.Parse(authService.GetLimit(), CultureInfo.InvariantCulture.NumberFormat);
            serverModel.NoCommission = float.Parse(authService.GetCommission(), CultureInfo.InvariantCulture.NumberFormat);

            LeftLimit = $"{authService.GetLimit()} ₽ до конца месяца";
            LeftCommission = $"Осталось {authService.GetCommission()} ₽ из 50000 ₽";
            TermRefresh = "";
        }

        private void ApproveButton(object sender)
        {
            AuthService authService = new AuthService();
            ServerModel serverModel = new ServerModel();
            if(BillViewCard.Length < 16)
            {
                BillCardWarning = "Введите полную длину карты";
                BillCardBrush = Brushes.Red.ToString();
                return;
            } else { BillCardWarning = ""; BillCardBrush = Brushes.White.ToString(); }

            if (BillViewCard == serverModel.CardNumber.ToString())
            {
                BillCardWarning = "Вы ввели свою же карту. Так нельзя :(";
                BillCardBrush = Brushes.Red.ToString();
                return;
            }
            else { BillCardWarning = ""; BillCardBrush = Brushes.White.ToString(); }

            if (!authService.CardExists(BillViewCard)) 
            {
                BillCardWarning = "Карта не найдена. Проверьте, нет ли ошибки";
                BillCardBrush = Brushes.Red.ToString();
                return;
            } else { BillCardWarning = ""; BillCardBrush = Brushes.White.ToString(); }

            try
            {
                double bill = Convert.ToDouble(BillBalance);
                double result = serverModel.Balance - bill;
                double newLimit = serverModel.Limit - bill;
                if ((serverModel.NoCommission - bill) >= 0)
                {
                    commission = serverModel.NoCommission - bill;
                    MessageBox.Show(commission.ToString(), "комиссия");
                    resultBill = bill;
                    cashback = bill * 0.01;
                    MessageBox.Show(cashback.ToString(), "кэщбэк");
                }
                else
                {
                    resultBill = bill + (bill - serverModel.NoCommission) / 10;
                    cashback = bill * 0.01;
                }

                if (commission > 0)
                {
                    totalCommission = 0;
                } else totalCommission = (bill - serverModel.NoCommission) / 10;
                MessageBox.Show(totalCommission.ToString());

                if (resultBill > serverModel.Balance)
                {
                    BillBalanceWarning = "Недостаточно средств на Вашем счёте";
                    BillBalanceBrush = Brushes.Red.ToString();
                    return;
                } else { BillBalanceWarning = ""; BillBalanceBrush = Brushes.White.ToString(); }

                if (resultBill > serverModel.Limit)
                {
                    BillBalanceWarning = "Лимит переводов на этот месяц исчерпан.";
                    BillBalanceBrush = Brushes.Red.ToString();
                    return;
                }
                else { BillBalanceWarning = ""; BillBalanceBrush = Brushes.White.ToString(); }
            } catch {
                BillBalanceWarning = "Неправильно введена сумма";
                BillBalanceBrush = Brushes.Red.ToString();
                return;
            }
            string CardNum = serverModel.CardNumber.ToString();
            BillCardTransaction = $"• • {CardNum.Substring(CardNum.Length - 4)}";
            BillBalanceTransaction = serverModel.Balance.ToString() + " ₽";
            ApproveCardVisibility = Visibility.Visible;

            authService.CardReceiver(BillViewCard);
            ReceiverData = $"{serverModel.ReceiverNickname} // • • {BillViewCard.Substring(BillViewCard.Length - 4)}";

            ResultBill = resultBill.ToString();
            ResultBillCommission = totalCommission.ToString();
        }

        private void TransactionApprove(object sender)
        {
            ServerModel serverModel = new ServerModel();

            MessageBox.Show(serverModel.Balance.ToString(), "было");
            double senderResult = serverModel.Balance - resultBill;
            MessageBox.Show(senderResult.ToString(), "стало");

            MessageBox.Show(serverModel.Balance.ToString(), "было");
            MessageBox.Show(resultBill.ToString());
            double receiverResult = serverModel.ReceiverBalance + resultBill;
            MessageBox.Show(receiverResult.ToString(), "стало");

            MessageBox.Show(serverModel.Limit.ToString(), "было");
            double newLimit = serverModel.Limit - resultBill;
            MessageBox.Show(newLimit.ToString(), "стало");

            MessageBox.Show(serverModel.Bonuses.ToString(), "было");
            double totalCashback = serverModel.Bonuses + cashback;
            MessageBox.Show(totalCashback.ToString(), "стало");
            AuthService authService = new AuthService();
            

            authService.UpdateData(serverModel.CardNumber.ToString(), BillViewCard, senderResult.ToString(), receiverResult.ToString(), newLimit.ToString(), totalCashback.ToString(), commission.ToString());
            DateTime dateTime = DateTime.Now;
            string format = "dd.MM.yyyy HH:mm";

            string path = "C:\\Users\\amira\\source\\repos\\Bank\\UserLog.txt";

            FileStream fs;
            fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine($"[{dateTime.ToString(format)}] Клиент {serverModel.Login} перевёл клиенту {serverModel.ReceiverNickname} {resultBill}₽. Комиссия составила {totalCommission}₽.\n");
            writer.Close();
            fs.Close();
            TermRefresh = "Данные Ваших условий обновлены.\nПерезагрузите страницу.";
            BillViewCard = string.Empty;
            BillBalance = string.Empty;
            ApproveCardVisibility = Visibility.Collapsed;
        }

        private void TransactionMenu(object sender)
        {
            CardTransaction = Visibility.Visible;
        }

        private void HideTransactionMenu(object sender)
        {
            CardTransaction = Visibility.Collapsed;
            BillViewCard = string.Empty;
            BillBalance = string.Empty;
        }

        private void HideApproveMenu(object sender)
        {
            ApproveCardVisibility = Visibility.Collapsed;
        }
    }
}
