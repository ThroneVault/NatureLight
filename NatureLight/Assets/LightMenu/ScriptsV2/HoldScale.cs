using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldScale : MonoBehaviour {

 
    private Transform father;
    public bool LockY_X = true;

    float Factor = 1.0f;

    void Start()
    {

        father = transform.parent;

        if (LockY_X)
        {
            Factor = transform.localScale.x / father.localScale.x;
        }
        else
        {
            Factor = transform.localScale.y / father.localScale.z;
        }

    }

    void Update()
    {
        if (LockY_X)
        {
            transform.localScale = new Vector3(Factor * 1.0f / father.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Factor * 1.0f / father.localScale.y);
        }
    }

}
