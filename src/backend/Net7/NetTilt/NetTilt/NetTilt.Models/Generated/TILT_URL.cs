using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class TILT_URL
    {
        public TILT_URL()
        {
            TILT_Note = new HashSet<TILT_Note>();
        }

        [Key]
        public long ID { get; set; }
        [StringLength(10)]
        public string URLPart { get; set; } = null!;
        [StringLength(50)]
        public string Secret { get; set; } = null!;

        [InverseProperty("IDURLNavigation")]
        public virtual ICollection<TILT_Note> TILT_Note { get; set; }
    }
}
