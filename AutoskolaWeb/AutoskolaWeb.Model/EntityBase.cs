using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoskolaWeb.Model
{
    public abstract class EntityBase
    {
        [Key]
        public int ID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string CreatedByUser { get; set; }

        public string ModifiedByUser { get; set; }
    }
}
