using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class DistanceUI : MonoBehaviour
{
    public Vector3 Distance;
    public Transform parent;
    public Transform current;

    private void Update()
    {
        if(current != null && parent != null)
            Distance = current.position  - parent.position;
    }
}
