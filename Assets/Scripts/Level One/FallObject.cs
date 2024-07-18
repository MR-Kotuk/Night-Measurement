using UnityEngine;

public class FallObject : MonoBehaviour, IFallObject
{
    [HideInInspector] public float FallDelay { get; set; }
    [HideInInspector] public bool isFall { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() && isFall)
            Invoke("Fall", FallDelay);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider>() && isFall)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), true);
            Invoke("Fall", FallDelay);
        }
    }

    public void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
