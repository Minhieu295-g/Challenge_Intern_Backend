using Common.Domains.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetTraining.Domains.Entities
{
    [Table("Permissions")]
    public class Permission : SystemLogEntity<int>
    {
        public string Name;

        public string Description;
        
    }
}
