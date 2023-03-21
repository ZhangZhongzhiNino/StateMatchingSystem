using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;

        transform.LookAt(targetPos);

    }
}
