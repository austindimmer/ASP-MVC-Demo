namespace Powerfront.Backend.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ObjectTypeTable")]
    public partial class ObjectTypeTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObjectTypeTable()
        {
            ObjectPropertyTables = new HashSet<ObjectPropertyTable>();
        }

        [StringLength(255)]
        public string id { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectPropertyTable> ObjectPropertyTables { get; set; }
    }
}
