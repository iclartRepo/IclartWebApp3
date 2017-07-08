using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.Common.Models
{
    public class MessageResult<TEntity> where TEntity : class
    {
        public bool isError { get; set; }
        public TEntity Result { get; set; }
        public List<TEntity> ResultList { get; set; }
        public string Message { get; set; }
    }
}
