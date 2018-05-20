using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
   public class W_MeetingReportEntity:BaseEntity
    {
        public int MeetingID { get; set; }
        public string MeetingTitle { get; set; }

        public string MeetingBody { get; set; }

        public string MeetingType { get; set; }
    }
}
