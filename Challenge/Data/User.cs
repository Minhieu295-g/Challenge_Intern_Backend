using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge2.Data
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255), Column("username")]
        public string Username { get; set; }

        [Required, MaxLength(255), Column("password")]
        public string Password { get; set; }

        [Required, EmailAddress, Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
