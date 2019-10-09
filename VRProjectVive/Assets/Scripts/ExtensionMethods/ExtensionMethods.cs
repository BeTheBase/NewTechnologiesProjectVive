using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Transform TotalTransformSetByIndex(this Transform transform, int index, List<Transform> list)
    {
        transform.position = list[index].position;
        transform.rotation = list[index].rotation;
        return transform;
    }
}
