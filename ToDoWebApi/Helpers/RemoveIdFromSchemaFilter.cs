using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using ToDoWebApi.Models;

namespace ToDoWebApi.Helpers
{
    public class RemoveIdFromSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(ToDoItem))
            {
                schema.Properties.Remove("id");
            }

            if (context.Type == typeof(ToDoItem))
            {
                schema.Properties.Remove("isDeleted");
            }
        }
    }
}
