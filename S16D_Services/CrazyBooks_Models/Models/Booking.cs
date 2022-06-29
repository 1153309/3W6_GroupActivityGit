using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyBooks_Models.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string Details { get; set; }

        [ForeignKey("CalendarEventId")]
        public int CalendarEventId { get; set; }
        public CalendarEvent Evenment { get; set; }
    }
}
