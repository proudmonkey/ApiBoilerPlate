using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Data.Entity
{
    public class EntityBase
    {
        //Add common Properties here that will be used for all your entities
        public long RowCreatedById { get; set; }
        public long RowModifiedById { get; set; }
        public DateTime RowCreatedDateTimeUtc { get; set; }
        public DateTime RowModifiedDateTimeUtc { get; set; }
    }
}
