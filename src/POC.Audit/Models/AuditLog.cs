using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Audit.Models
{
    public class AuditLog
    {
        public int AuditId { get; set; }        
        public string TablePk { get; set; }        
        public string AuditAction { get; set; }
        public string AuditUser { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditData { get; set; }
        public string EntityType { get; set; }
    }
}
