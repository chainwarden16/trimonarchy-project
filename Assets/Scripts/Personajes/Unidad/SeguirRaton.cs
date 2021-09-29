using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirRaton : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gameObject.transform.position, Vector2.one);
    }
}
