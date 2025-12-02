using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public LayerMask obstacleMask;
    public int maxTries = 20;

    private Collider area;

    void Awake()
    {
        area = GetComponent<Collider>();
    }

    public Vector3 GetValidSpawnPoint()
    {
        for (int i = 0; i < maxTries; i++)
        {
            Vector3 randomPoint = GetRandomPointInside();

            // Check if this point overlaps any obstacle
            if (!Physics.CheckSphere(randomPoint, 0.5f, obstacleMask))
            {
                return randomPoint;
            }
        }

        // fallback if all fails (very rare)
        return transform.position;
    }

    private Vector3 GetRandomPointInside()
    {
        Vector3 min = area.bounds.min;
        Vector3 max = area.bounds.max;

        return new Vector3(
            Random.Range(min.x, max.x),
            transform.position.y,
            Random.Range(min.z, max.z)
        );
    }
}
