namespace Powerfront.Backend.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ObjectPropertyTable")]
    public partial class ObjectPropertyTable
    {
        [StringLength(255)]
        public string id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Age { get; set; }

        [Required]
        [StringLength(255)]
        public string TypeId { get; set; }

        public virtual ObjectDataTable ObjectDataTable { get; set; }

        public virtual ObjectTypeTable ObjectTypeTable { get; set; }
    }
}
