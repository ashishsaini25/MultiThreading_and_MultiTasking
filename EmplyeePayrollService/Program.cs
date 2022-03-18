using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmplyeePayrollService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeOperation emp = new EmployeeOperation();
            //   emp.getallemployees();
            //bool result= emp.deleteemployee("ashish");
            //console.writeline(result);
            List<Employee> list = new List<Employee>()
            {
                new Employee()
                {
                    Name = "ashish",
                    StartDate = DateTime.Now,
                    Gender = "m",
                    Phonenumber = "7007231604",
                    Address = "113/234",
                    BasicPay = 5000,
                    Deduction = 2000,
                    TaxablePay = 4000,
                    Incometax = 5000,
                    Netpay = 3000
                },
                 new Employee()
                {
                    Name = "ajay",
                    StartDate = DateTime.Now,
                    Gender = "m",
                    Phonenumber = "7007231604",
                    Address = "113/234",
                    BasicPay = 50000,
                    Deduction = 2000,
                    TaxablePay = 4000,
                    Incometax = 5000,
                    Netpay = 3000
                },
                  new Employee()
                {
                    Name = "astha",
                    StartDate = DateTime.Now,
                    Gender = "f",
                    Phonenumber = "7007231604",
                    Address = "113/234",
                    BasicPay = 5000,
                    Deduction = 2000,
                    TaxablePay = 4000,
                    Incometax = 5000,
                    Netpay = 3000
                },
                   new Employee()
                {
                    Name = "ujjwal",
                    StartDate = DateTime.Now,
                    Gender = "m",
                    Phonenumber = "7007231604",
                    Address = "113/234",
                    BasicPay = 5000,
                    Deduction = 2000,
                    TaxablePay = 4000,
                    Incometax = 5000,
                    Netpay = 3000
                }
            };
            DateTime startdate = DateTime.Now;
            emp.AddEmployeePayrollWithoutThread(list);
            DateTime stopdate = DateTime.Now;
            Console.WriteLine("time duration without thread" + (stopdate - startdate));
            startdate = DateTime.Now;
            //  emp.addemployeepayrollwiththread(list);
            emp.Multithreading();
            stopdate = DateTime.Now;
            Console.WriteLine("time duration with thread" + (stopdate - startdate));



            string[] words = CreateWordArray(@"https://www.gutenberg.org/files/54700/54700-0.txt");
            #region ParallelTasks
            Parallel.Invoke( () =>
            {
                Console.WriteLine("Begin the first task.....");
                GetLongestWord(words);

            },
            ()=>
            {
                Console.WriteLine("Begin the second task....");
                GetMostCommonWords(words);
            },
             () =>
             {
                 Console.WriteLine("Begin the third task....");
                 GetCountForWord(words, "sleep");
             }
            );
        #endregion

        }
        static string[] CreateWordArray(string url)
        {
            string blog =new WebClient().DownloadString(url);
            return blog.Split(new char[] {' ','\u000A',',',';',':','-','_','/'}, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string GetLongestWord(string[] words)
        {
            var longestword= (from word in words orderby word.Length descending select word).First();
            Console.WriteLine("The longest word is " + longestword);

            return longestword;
        }
        private static void GetMostCommonWords(string[] words)
        {
            var frequencyoreder=from word in words where word.Length > 6 group word by word into g orderby g.Count() descending select g.Key;

            StringBuilder stringBuilder = new StringBuilder();
            var commonword = frequencyoreder.Take(10);
            foreach (var word in commonword)
            {
                stringBuilder.AppendLine(" " + word);
            }
            Console.WriteLine(stringBuilder.ToString());

        }
        private static void GetCountForWord(string[] words,string term)
        {
            var findword= from word in words where word.ToUpper().Contains(term.ToUpper()) select word;
            Console.WriteLine("Task 3--- the word " + term + "occurs" + findword.Count());
;        }
    }
}
