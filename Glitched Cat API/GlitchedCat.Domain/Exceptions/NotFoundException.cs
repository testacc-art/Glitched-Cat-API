using System;

namespace GlitchedCat.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, Guid entityId)
            : base($"Entity {entityName} with ID {entityId} was not found.")
        {
        }
    }
}