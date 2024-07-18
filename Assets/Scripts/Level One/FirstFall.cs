using UnityEngine;

public class FirstFall : MonoBehaviour, IFallObject
{
    [HideInInspector] public float FallDelay { get; set; }
    [HideInInspector] public bool isFall { get; set; }

    private void Update()
    {
        if (isFall)
            Fall();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider>())
            Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), true);
    }

    public void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
