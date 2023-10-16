using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum ResourceType
    {
        Stone,
        Wood,
        Metal,
        coins
    }

    public ResourceType type;
}
