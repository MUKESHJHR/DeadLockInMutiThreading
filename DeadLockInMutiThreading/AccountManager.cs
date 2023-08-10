using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeadLockInMutiThreading
{
    public class AccountManager
    {
        #region Example - 1 - Example to understand Deadlock in C#
        //private Account FromAccount;
        //private Account ToAccount;
        //private double TransferAmount;

        //public AccountManager(Account fromAccount, Account toAccount, double amountTransfer)
        //{
        //    FromAccount = fromAccount;
        //    ToAccount = toAccount;
        //    TransferAmount= amountTransfer;
        //}
        //public void FundTransfer()
        //{
        //    Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {FromAccount.ID}");
        //    lock(FromAccount)
        //    {
        //        Console.WriteLine($"{Thread.CurrentThread.Name} acquire lock on {FromAccount.ID}");
        //        Thread.Sleep(1000);
        //        Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {ToAccount.ID}");
        //        lock(ToAccount)
        //        {
        //            FromAccount.WithdrawMoney(TransferAmount);
        //            ToAccount.DepositMoney(TransferAmount);
        //        }
        //    }
        //}
        #endregion

        #region Example - 2 (Avoiding Deadlock by using Monitor.TryEnter method)
        //private Account FromAccount;
        //private Account ToAccount;
        //private double TransferAmount;

        //public AccountManager(Account fromAccount, Account toAccount, double amountTransfer)
        //{
        //    this.FromAccount = fromAccount;
        //    this.ToAccount = toAccount;
        //    this.TransferAmount = amountTransfer;
        //}
        //public void FundTransfer()
        //{
        //    Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {FromAccount.ID}");
        //    lock (FromAccount)
        //    {
        //        Console.WriteLine($"{Thread.CurrentThread.Name} acquire lock on {FromAccount.ID}");

        //        Console.WriteLine($"{Thread.CurrentThread.Name} Doing Some Work.");
        //        Thread.Sleep(3000);
        //        Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {ToAccount.ID}");
        //        if (Monitor.TryEnter(ToAccount, 3000))
        //        {
        //            Console.WriteLine($"{Thread.CurrentThread.Name} acquire lock on {ToAccount.ID}");
        //            try
        //            {
        //                FromAccount.WithdrawMoney(TransferAmount);
        //                ToAccount.DepositMoney(TransferAmount);
        //            }
        //            finally
        //            {
        //                Monitor.Exit(ToAccount);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine($"{Thread.CurrentThread.Name} Unable to acquire lock on {ToAccount.ID}, So existing.");
        //        }
        //    }
        //}
        #endregion

        #region Example - 3 (Avoid Deadlock in C# by acquiring locks in a specific order)
        private Account FromAccount;
        private Account ToAccount;
        private readonly double TransferAmount;
        private static readonly Mutex mutex = new Mutex();

        public AccountManager(Account fromAccount, Account toAccount, double amountTransfer)
        {
            this.FromAccount = fromAccount;
            this.ToAccount = toAccount;
            this.TransferAmount = amountTransfer;
        }
        public void FundTransfer()
        {
            object _lock1, _lock2;
            if (FromAccount.ID < ToAccount.ID)
            {
                _lock1 = FromAccount;
                _lock2 = ToAccount;
            }
            else
            {
                _lock1 = ToAccount;
                _lock2 = FromAccount;
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {((Account)_lock1).ID}");

            lock (_lock1)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} acquired lock on {((Account)_lock1).ID}");
                Console.WriteLine($"{Thread.CurrentThread.Name} Doing Some work");
                Thread.Sleep(3000);
                Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {((Account)_lock2).ID}");
                lock (_lock2)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} acquired lock on {((Account)_lock2).ID}");
                    FromAccount.WithdrawMoney(TransferAmount);
                    ToAccount.DepositMoney(TransferAmount);
                }
            }

        }
        #endregion

    }
}
