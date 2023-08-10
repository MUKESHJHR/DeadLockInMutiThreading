namespace DeadLockInMutiThreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Example - 1 - Example to understand Deadlock in C#
            //Console.WriteLine("Main Thread Started...");
            //Account account1001 = new Account(1001, 5000);
            //Account account1002 = new Account(1002, 3000);

            //AccountManager accountManager1=new AccountManager(account1001 , account1002,5000);

            //Thread t1 = new Thread(accountManager1.FundTransfer)
            //{
            //    Name="Thread 1"
            //};

            //AccountManager accountManager2 = new AccountManager(account1002, account1001, 6000);

            //Thread t2 = new Thread(accountManager2.FundTransfer)
            //{
            //    Name = "Thread 2"
            //};

            //t1.Start();
            //t2.Start();

            //t1.Join();
            //t2.Join();

            //Console.WriteLine("Main Thread Completed.");
            #endregion

            #region Example - 2 (Avoiding Deadlock by using Monitor.TryEnter method)
            Console.WriteLine("Main Thread Started...");
            Account account1001 = new Account(1001, 5000);
            Account account1002 = new Account(1002, 3000);

            AccountManager accountManager1 = new AccountManager(account1001, account1002, 5000);

            Thread t1 = new Thread(accountManager1.FundTransfer)
            {
                Name = "Thread 1"
            };

            AccountManager accountManager2 = new AccountManager(account1002, account1001, 6000);

            Thread t2 = new Thread(accountManager2.FundTransfer)
            {
                Name = "Thread 2"
            };

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine("Main Thread Completed.");
            #endregion

            Console.ReadKey();
        }
    }
}