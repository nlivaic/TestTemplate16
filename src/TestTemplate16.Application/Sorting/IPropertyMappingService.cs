using System.Collections.Generic;
using TestTemplate16.Application.Sorting.Models;

namespace TestTemplate16.Application.Sorting;

public interface IPropertyMappingService
{
    IEnumerable<SortCriteria> Resolve(BaseSortable sortableSource, BaseSortable sortableTarget);
}
