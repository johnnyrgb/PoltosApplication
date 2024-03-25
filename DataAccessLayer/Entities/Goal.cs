using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public DateTime DateToSaveUp { get; set; }
        public DateTime DateOfCreation { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [InverseProperty("Goals")]
        public virtual User User { get; set; }

    }
}
