using System;
using System.ComponentModel.DataAnnotations;

namespace AliasManager.Models
{
    public class Aliases : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public String Alias { get; set; }

        [Required]
        public String Email { get; set; }

        public Boolean Enabled { get; set; }
    }

}