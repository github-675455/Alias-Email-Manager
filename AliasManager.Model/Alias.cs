using System;
using System.ComponentModel.DataAnnotations;

namespace AliasManager.Model
{
    public class Aliases : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String Alias { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        public Boolean Enabled { get; set; }
    }

}