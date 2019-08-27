using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider Collider;
    [HideInInspector]
    public LODGroup BrickMeshes;

    public void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        BrickMeshes = GetComponent<LODGroup>();
    }

    public void SetMaterial(Material mat)
    {
        var lods = BrickMeshes.GetLODs();
        for (var i = 0; i < lods.Length; i++)
        {
            for (var j = 0; j < lods[i].renderers.Length; j++)
            {
                lods[i].renderers[j].material = mat;
            }
        }
    }
}