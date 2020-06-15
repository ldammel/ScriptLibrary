using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time;
    public GameObject objectToDestroy;
    private void Start()
    {
        Destroy(objectToDestroy, time);
    }
}
