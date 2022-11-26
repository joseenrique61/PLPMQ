using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubBlockClass : Object
{
    public List<Object> blocks = new();

    public SubBlockClass(Object[] blocks)
    {
        this.blocks = blocks.ToList();
    }
}
