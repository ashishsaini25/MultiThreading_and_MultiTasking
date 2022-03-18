using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmplyeePayrollService
{
    public class EmployeeOperation
    {
        private SqlConnection con;    
        public void Connection()
        {
            string constr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Payroll_service;Integrated Security=True;MultipleActiveResultSets=True";
            con = new SqlConnection(constr);

        }
        public void GetAllEmployees()
        {
            Connection();
            List<Employee> EmpList = new List<Employee>();
            SqlCommand com = new SqlCommand("spGetEmployee", con);
            com.CommandType = CommandType.StoredProcedure;
            // SqlCommand com = new SqlCommand("select * from Employe_Payroll", con);
            //com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(

                    new Employee
                    {

                        id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        Phonenumber = Convert.ToString(dr["Phonenumber"]),
                        Address = Convert.ToString(dr["Address"]),
                        BasicPay = Convert.ToInt32(dr["BasicPay"]),

                        Deduction = Convert.ToInt32(dr["Deduction"]),

                        TaxablePay = Convert.ToInt32(dr["TaxablePay"]),

                        Incometax = Convert.ToInt32(dr["Incometax"]),

                        Netpay = Convert.ToInt32(dr["Netpay"]),

                    }

                    );
            }
            Display(EmpList);


        }
        public bool Addemployee(Employee obj)
        {

            Connection();
            SqlCommand com = new SqlCommand("InsertEmployee", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@StartDate", obj.StartDate);
            com.Parameters.AddWithValue("@Gender", obj.Gender);
            com.Parameters.AddWithValue("@Phonenumber",obj.Phonenumber);
            com.Parameters.AddWithValue("@Address",obj.Address);
            com.Parameters.AddWithValue("@BasicPay",obj.BasicPay);
            com.Parameters.AddWithValue("@Deduction",obj.Deduction);
            com.Parameters.AddWithValue("@TaxablePay",obj.TaxablePay);
            com.Parameters.AddWithValue("@Incometax",obj.Incometax);
            com.Parameters.AddWithValue("@Netpay",obj.Netpay);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i != 0)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
        public void AddEmployeePayrollWithoutThread(List<Employee> list)
        {
            foreach (Employee obj in list)
            {
                this.Addemployee(obj);
            }
        }
        public void AddEmployeePayrollWithThread(List<Employee> list)
        {
            foreach (Employee obj in list)
            {
                Task task = new Task(() => {

                    this.Addemployee(obj);
                });
                task.Start();
            }
        }

        public void Multithreading()
        {
          //  Console.WriteLine("**");
            var task1 =  Task.Run(() =>
            {
               // Thread.Sleep(5000); 
               for(int i=0;i<5;i++)
                Console.WriteLine("First Task");

            });
            var task2 =  Task.Run(() =>
            {
                //Thread.Sleep(5000);
                for (int i = 0; i < 5; i++)
                    Console.WriteLine("Second Task");

            });
            Task.WaitAll(task1, task2);
        }
        public double UpdateEmployee(Employee obj)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("EmployeeUpdateSalery", con);

                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@name", obj.Name);
                com.Parameters.AddWithValue("@salery", obj.BasicPay);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {

                    return obj.BasicPay;
                }
                else
                {
                    return 0.0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            finally
            {
                con.Close();
            }
        }
        public void Display(List<Employee> employees)
        {
            foreach(var data in employees)
            {
                Console.WriteLine(data.Name + " " + data.StartDate);
            }
        }
        public bool DeleteEmployee(string name)
        {

            Connection();
            SqlCommand com = new SqlCommand("DeleteEmpByName", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@name", name);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i !=0)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
