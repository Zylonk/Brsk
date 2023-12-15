using Apis.Model;

namespace Apis.Class
{
    public class GetrContext
    {
        public static TodolistContext Context { get; } = new TodolistContext();
    }
}
