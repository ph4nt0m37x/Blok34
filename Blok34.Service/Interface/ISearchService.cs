using Blok34.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Service.Interface
{
    public interface ISearchService
    {
        SearchDTO Search(string query);
    }
}
