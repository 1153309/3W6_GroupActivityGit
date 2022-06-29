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

        [ForeignKey("CalendarEvent")]
        public int CalendarEvent_Id { get; set; }

        public CalendarEvent CalendarEvent { get; set; }
    }
}
