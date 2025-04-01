using System.Collections.Generic;
using TestTemplate16.Application.Sorting.Models;

namespace TestTemplate16.Application.Tests.Helpers;

public class ResourceParameters1
    : BaseSortable<MappingSourceModel1>
{
    public override IEnumerable<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}
