using UnityEngine;

public class PlanetaryMotion : MonoBehaviour
{
    // Rotation speeds for each axis (degrees per second)
    [SerializeField] public float rotationSpeedX = 0f; // Rotation around X-axis
    [SerializeField] public float rotationSpeedY = 10f; // Rotation around Y-axis (typical for planets)
    [SerializeField] public float rotationSpeedZ = 0f; // Rotation around Z-axis

    // Orbital parameters
    [SerializeField] public float orbitalRadius = 5f;    // Distance from the center of orbit
    [SerializeField] public float orbitalSpeed = 20f;    // Speed of orbit (degrees per second)
    [SerializeField] public Transform orbitCenter;       // The point to orbit around (e.g., a planet)

    private float orbitalAngle = 0f; // Current angle in the orbit

    void Update()
    {
        // Handle self-rotation on all three axes
        transform.Rotate(Vector3.right * rotationSpeedX * Time.deltaTime, Space.Self); // X-axis
        transform.Rotate(Vector3.up * rotationSpeedY * Time.deltaTime, Space.Self);    // Y-axis
        transform.Rotate(Vector3.forward * rotationSpeedZ * Time.deltaTime, Space.Self); // Z-axis

        // Handle orbital movement if an orbit center is assigned
        if (orbitCenter != null)
        {
            // Increment the orbital angle
            orbitalAngle += orbitalSpeed * Time.deltaTime;

            // Calculate new position in the orbit (on the XZ plane for simplicity)
            float x = Mathf.Cos(Mathf.Deg2Rad * orbitalAngle) * orbitalRadius;
            float z = Mathf.Sin(Mathf.Deg2Rad * orbitalAngle) * orbitalRadius;

            // Update position relative to the orbit center
            transform.position = orbitCenter.position + new Vector3(x, 0, z);
        }
    }
}