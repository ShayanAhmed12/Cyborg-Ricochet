using UnityEngine;

public class ReflectiveSurface : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calculate reflection vector
            Vector3 incomingVector = rb.velocity;
            Vector3 normalVector = collision.contacts[0].normal;
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector);

            // Apply the reflected velocity
            rb.velocity = reflectVector;
        }
    }
}