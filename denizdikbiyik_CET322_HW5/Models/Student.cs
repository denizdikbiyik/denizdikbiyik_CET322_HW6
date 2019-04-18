using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace denizdikbiyik_CET322_HW5.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Öğrenci Numarası Zorunlu!")]
        [Range(1900000000, 2020999999)]
        public int StudentNo { get; set; }
        [Required(ErrorMessage = "Öğrenci Adı Zorunlu!")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Öğrenci Soyadı Zorunlu!")]
        public string StudentSurname { get; set; }
        public string StudentEmail { get; set; }
        public string StudentTelNo { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [NotMapped]
        public virtual IEnumerable<SelectListItem> departments { get; set; }
        public string CetUserId { get; set; }
        public virtual CetUser CetUser { get; set; }
    }
    public class CetUser : IdentityUser
    {
        [Required]
        [StringLength(10)]
        public string SchoolNo { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(100)]
        public string Department { get; set; }
    }
}
