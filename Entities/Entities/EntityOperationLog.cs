using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities.Abstract;
using Entities.Enums;

namespace Entities.Entities
{
    public class EntityOperationLog : IEntity
    {
        [Key] 
        public int LogID { get; set; }
        public string TableName { get; set; }
        public int LogType { get; set; }
        public DateTime OperationDate { get; set; }
        public string LogData { get; set; }
    }
}
