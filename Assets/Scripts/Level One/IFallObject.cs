using UnityEngine;

public class IFallObject : MonoBehaviour
{
    [HideInInspector] public float FallDelay;
    [HideInInspector] public bool isFall;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() && isFall)
            Invoke("Fall", FallDelay);

        if (collision.gameObject.GetComponent<MeshCollider>())
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), gameObject.GetComponent<MeshCollider>(), true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() && isFall)
            Invoke("Fall", FallDelay);
    }

    private void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
