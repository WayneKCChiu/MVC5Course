using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
   public class MemberViewModel
   {
      public int Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Required]
      public DateTime Birthday { get; set; }
   }
}