using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Service.Interface
{
    
        public interface IServiceFactory
        {
            T GetServices<T>() where T : class;
        }  
}
