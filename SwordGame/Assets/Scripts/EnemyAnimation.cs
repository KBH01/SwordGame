using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.right, Time.timeSinceLevelLoad * Time.deltaTime * 10);
    }
}
