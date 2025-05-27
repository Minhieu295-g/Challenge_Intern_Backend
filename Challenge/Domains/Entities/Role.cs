using Common.Domains.Entities;
using Org.BouncyCastle.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetTraining.Domains.Entities
{
    [Table("Roles")]
    public class Role : SystemLogEntity<int>
    {
        public string Name;

        public string Description;
    }
}
