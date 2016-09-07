using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

namespace Sixeyed.SpecFlowItAll.AcceptanceTests
{
    public class StepBase
    {
        public T ExecuteScalar<T>(string sqlFormat, params object[] args)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopContext"].ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(sqlFormat, args);
                    var response = command.ExecuteScalar();
                    return (T)response;
                }
            }
        }

        public void RetryAssert(Action action, int timeoutMilliseconds, int retryIntervalMilliseconds = 50)
        {
            AssertFailedException ex = new AssertFailedException("Timeout exceeded before positive check");
            var timeout = DateTime.Now.AddMilliseconds(timeoutMilliseconds);
            while (timeout.Subtract(DateTime.Now).TotalMilliseconds > 0)
            {
                try
                {
                    action();
                    //no error - return successfully:
                    return;
                }
                catch (AssertFailedException aEx)
                {
                    ex = aEx;
                    Thread.Sleep(retryIntervalMilliseconds);
                }
            }
            throw ex;
        }
    }
}
