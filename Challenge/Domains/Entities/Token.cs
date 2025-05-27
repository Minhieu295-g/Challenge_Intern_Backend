using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetTraining.Domains.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Tokens")]
    public class Token : SystemLogEntity<Guid>
    {
        public string RefreshToken { get; set; }

        public Guid JwtId { get; set; }

        public Guid UserId { get; set; }

        [Write(false)]
        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime ExpiredAt { get; set; }


    }
}
