using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class EmployeeModel
    {
       public int id {  get; set; }
        [Required(ErrorMessage ="Name is required.")]
       public string empname { get; set; }
        [Required(ErrorMessage = "Employee Id is required.")]
        [RegularExpression(@"^\d+$",ErrorMessage ="Enter numbers only")]

        public int empid { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public int depid {  get; set; }
        [Required(ErrorMessage = "Department Name is required.")]
        public string deptname { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime dob { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string address { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(\d{10})$",ErrorMessage ="Phone number must be of 10 digits")]
        public string phone { get; set; }
       
    }
}