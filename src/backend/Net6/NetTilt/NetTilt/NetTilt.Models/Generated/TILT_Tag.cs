using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class TILT_Tag
    {
        public TILT_Tag()
        {
            TILT_Tag_Note = new HashSet<TILT_Tag_Note>();
        }

        [Key]
        public long ID { get; set; }
        [StringLength(10)]
        public string Name { get; set; } = null!;

        [InverseProperty("IDTagNavigation")]
        public virtual ICollection<TILT_Tag_Note> TILT_Tag_Note { get; set; }
    }
}
