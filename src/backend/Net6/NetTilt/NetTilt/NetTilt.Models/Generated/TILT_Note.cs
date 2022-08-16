using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    public partial class TILT_Note
    {
        public TILT_Note()
        {
            TILT_Tag_Note = new HashSet<TILT_Tag_Note>();
            TimeZoneString = "";
        }

        [Key]
        public long ID { get; set; }
        public long IDURL { get; set; }
        [StringLength(150)]
        public string Text { get; set; } = null!;
        [StringLength(150)]
        public string? Link { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ForDate { get; set; }

        [ForeignKey("IDURL")]
        [InverseProperty("TILT_Note")]
        public virtual TILT_URL IDURLNavigation { get; set; } = null!;
        [InverseProperty("IDNoteNavigation")]
        public virtual ICollection<TILT_Tag_Note> TILT_Tag_Note { get; set; }
        
        [StringLength(250)]
        public string TimeZoneString { get; set; }
    }
}
