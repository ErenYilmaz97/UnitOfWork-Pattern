using System;
using System.Collections.Generic;
using System.Text;
using Entities.Abstract;
using Entities.Entities;

namespace Core.Log
{
   public interface ILogger
   {
       void Log(EntityOperationLog entitiesEntityOperationLog);
       List<EntityOperationLog> GetLogs();
       string SerializeObject(object entity);
       string SerializeListOfObjects(List<IEntity> entities);
   }
}
