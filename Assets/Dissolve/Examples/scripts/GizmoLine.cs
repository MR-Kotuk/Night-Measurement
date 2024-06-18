using UnityEngine;
using System.Collections;

public class GizmoLine : MonoBehaviour 
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.right);
    }
}
