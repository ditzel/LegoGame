using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "RingElement", menuName = "RingMenu/Ring", order = 1)]
public class Ring : ScriptableObject
{
    public RingElement[] Elements;

}
