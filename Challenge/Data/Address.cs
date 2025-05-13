using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge2.Data
{
    [Table("addresses")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50), Column("district")]
        public string District { get; set; }

        [Required]
        [StringLength(50), Column("ward")]
        public string Ward { get; set; }
        [Required]
        [StringLength(50), Column("province")]
        public string Province { get; set; }

        [Required]
        [StringLength(255), Column("details")]
        public string details;

        [ForeignKey("User")]
        [Column("user_id")]
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
