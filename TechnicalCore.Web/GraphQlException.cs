using System;

namespace TechnicalCore.Web
{
    public class GraphQlException : ApplicationException
    {
        public GraphQlException(string message) : base(message)
        {
        }
    }
}
