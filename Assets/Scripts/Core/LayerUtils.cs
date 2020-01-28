using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AptGames
{
    public static class LayerUtils
    {
        public static void MoveToLayerWithChildren(this GameObject root, int layer)
        {
            root.layer = layer;
            for(int i = 0; i < root.transform.childCount; i++)
            {
                root.transform.GetChild(i).gameObject.MoveToLayerWithChildren(layer);
            }
        }
    }
}
