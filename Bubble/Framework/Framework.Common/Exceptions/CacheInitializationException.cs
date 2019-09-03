using System;
using System.Runtime.Serialization;

namespace Framework.Common.Exceptions
{
    [Serializable]
    public class CacheInitializationException : Exception
    {
       
        public CacheInitializationException()
        {
        }
        
        public CacheInitializationException(string msg) : base(msg)
        {
        }
      
        public CacheInitializationException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
        
        protected CacheInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
