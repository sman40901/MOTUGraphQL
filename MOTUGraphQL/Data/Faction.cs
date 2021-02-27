using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MOTUGraphQL.Data
{
   
    [Table("factionMain")]
    public class Faction
    {
        [Key]
        [Column("factionId")]
        public int FactionID { get; set; }

        [Column("baseFaction")]
        public int BaseFaction { get; set; }

        [Column("factionName")]
        public string FactionName { get; set; }

        [Column("updated")]
        public DateTime Updated { get; set; }

        [Column("inserted")]
        public DateTime Inserted { get; set; }
    }
}
