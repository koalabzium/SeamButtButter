using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationManager.SeamButBetter
{
    public class DBContext : ValueObject
    {


        [Key]
        private int Id;

        [Column("Driver")]
        private string Driver;

        [Column("Values")]
        public string Values;

        [Column("CreationTime")]
        public DateTime CreationTime;

        [Column("TimeOut")]
        public int TimeOut { get; set; }
        

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Driver;
        }
    }
}
