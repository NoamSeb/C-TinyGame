using System;
using System.Collections;
using UnityEngine;

public class DestroyMyself : MonoBehaviour
{
    [SerializeField] private float DestroyTime;

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
