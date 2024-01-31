using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace ToDoWebApi.Helpers
{
    public class RemoveIdFromCreateToDoItemSchemaFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.RelativePath == "/ToDo" && context.ApiDescription.HttpMethod == "POST")
            {
                var idParameter = operation.Parameters.FirstOrDefault(p => p.Name == "Id");
                if (idParameter != null)
                {
                    operation.Parameters.Remove(idParameter);
                }
            }
        }
    }
}
