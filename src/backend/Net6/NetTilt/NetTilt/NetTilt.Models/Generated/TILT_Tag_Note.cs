using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class TILT_Tag_Note
    {
        [Key]
        public long ID { get; set; }
        public long IDTag { get; set; }
        public long IDNote { get; set; }

        [ForeignKey("IDNote")]
        [InverseProperty("TILT_Tag_Note")]
        public virtual TILT_Note IDNoteNavigation { get; set; } = null!;
        [ForeignKey("IDTag")]
        [InverseProperty("TILT_Tag_Note")]
        public virtual TILT_Tag IDTagNavigation { get; set; } = null!;
    }
}
