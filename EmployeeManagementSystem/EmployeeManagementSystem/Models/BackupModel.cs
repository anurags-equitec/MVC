using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class BackupModel
    {
        public int id { get; set; }
        public string empname { get; set; }
        public int empid { get; set; }
        public int depid { get; set; }
        public string deptname { get; set; }
        public DateTime dob { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
    }
}