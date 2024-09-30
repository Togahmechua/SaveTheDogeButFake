using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] private GameObject model;

    private void Update()
    {
        FaceToDog();
    }

    private void FaceToDog()
    {
        Vector2 playerPos = transform.position;
        Vector2 beePos = transform.position;
    }
}
