using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace EmployeeManagementSystem.Repo
{
    
    public class Repository
    {
       public string connectionString = ConfigurationManager.ConnectionStrings["emp"].ConnectionString;
        
        public List<EmployeeModel> show(EmployeeModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                
                string sql = "select e.id,e.empname,e.empid,d.deptname,e.dob,e.gender,e.address,e.phone from EmpRecord e join Departments d on e.depid=d.depid";
                return con.Query<EmployeeModel>(sql).ToList();
            }
        }



        public void add(EmployeeModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
            string sql = "insert into EmpRecord (empname,empid,depid,dob,gender,address,phone) values(@empname,@empid,@depid,@dob,@gender,@address,@phone)";
                con.Execute(sql,model);

            }
        }
        
        public EmployeeModel details(int id)
        {
            using(var con=new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "select e.id,e.empname,e.empid,d.deptname,e.dob,e.gender,e.address,e.phone from EmpRecord e join Departments d on e.depid=d.depid where e.id=@id";
                return con.QueryFirstOrDefault<EmployeeModel>(sql, new { id });
            }
        }

        

        public void delete(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
               
                string sql = "delete from EmpRecord where id=@id";
                con.Execute(sql, new { id });

            }
        }

        public void update(EmployeeModel emp)
        {
            using(var con=new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "update EmpRecord set empname=@empname,empid=@empid,depid=@depid,dob=@dob,gender=@gender,address=@address,phone=@phone where id=@id";
                con.Execute(sql, emp);
            }
        }

        
        public List<BackupModel> showBackup(BackupModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                //string sql = "select b.empname,b.empid,d.deptname,b.dob,b.gender,b.address,b.phone from Del_emp b join Departments d on b.depid=d.depid";
                string sql = "select* from Del_emp b  join Departments d on b.depid=d.depid";
                return con.Query<BackupModel>(sql).ToList();
            }
        }
            
        public BackupModel deletedDetail(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "select b.empname,b.empid,d.deptname,b.dob,b.gender,b.address,b.phone from Del_emp b join Departments d on b.depid=d.depid where b.id=@id ";
                return con.QueryFirstOrDefault<BackupModel>(sql,new { id });
            }
        }
        public void restore(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "delete from Del_emp where id=@id";
                con.Execute(sql, new { id });
            }
        }

    }
}