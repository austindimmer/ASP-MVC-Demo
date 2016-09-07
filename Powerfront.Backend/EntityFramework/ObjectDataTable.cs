namespace Powerfront.Backend.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ObjectDataTable")]
    public partial class ObjectDataTable
    {
        [StringLength(255)]
        public string id { get; set; }

        [Required]
        [StringLength(255)]
        public string PropertiesId { get; set; }

        public virtual ObjectPropertyTable ObjectPropertyTable { get; set; }
    }
}
