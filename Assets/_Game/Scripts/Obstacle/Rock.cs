using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private bool flag;
    private Vector3 curPos;

    private void OnEnable()
    {
        if (curPos != Vector3.zero)
        {
            transform.position = curPos;
        }
        rb.simulated = false;
        flag = true;
    }

    private void Start()
    {
        curPos = transform.position;
    }

    private void Update()
    {
        if (LevelManager.Ins.isDrawed && flag)
        {
            rb.simulated = true;
            flag = false;
        }
    }
}
