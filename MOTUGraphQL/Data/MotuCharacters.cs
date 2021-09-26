using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MOTUGraphQL.Data
{
   
    [Table("motuMain")]
    public class MotuCharacter
    {
      //  [name]
      //,[faction]
      //,[id]
      //,[inserted]
      //,[updated]
      //,[leader]
      //,[deletedRecord]

        [Key]
        [Column("id")]
        public int ID { get; set; }

        [Column("faction")]
        public int Faction { get; set; }

        [Column("leader")]
        public string Leader { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("updated")]
        public DateTime Updated { get; set; }

        [Column("inserted")]
        public DateTime Inserted { get; set; }

        [Column("deletedRecord")]
        public short deletedRecord { get; set; }
    }
}
