
using System.Linq;
using System.Collections.Generic;
namespace Coding{
static class MyExtension{
     public static List<A> Slice<A>(this List<A> e, int from, int to )
        {
            return e.Take(to).Skip(from).ToList();
        }
}
}