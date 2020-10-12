using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Abstract;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Log
{
    public class DbLogger : ILogger
    {
        private readonly AppDbContext _context;

        public DbLogger(AppDbContext context)
        {
            _context = context;   
        }



        public List<EntityOperationLog> GetLogs()
        {
            return _context.EntityOperationLogs.ToList();
        }




        public void Log(EntityOperationLog entitiesEntityOperationLog)
        {
            _context.Set<EntityOperationLog>().Add(entitiesEntityOperationLog);
        }




        public string SerializeObject(object entity)
        {
            return JsonSerializer.Serialize(entity);
        }


        public string SerializeListOfObjects(List<IEntity> entities)
        {
            return JsonSerializer.Serialize(entities);
        }


    }
}
