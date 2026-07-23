using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ProblemZone : MonoBehaviour
{
    public int weight = 1; // pondération pour le spawner (zones plus/moins actives)

    private Collider2D zoneCollider;

    private void Awake()
    {
        zoneCollider = GetComponent<Collider2D>();

        if (zoneCollider == null)
            Debug.LogError($"ProblemZone sur {gameObject.name} : aucun Collider2D trouvé ! Ajoute un BoxCollider2D, CircleCollider2D ou PolygonCollider2D.");
    }

    public Vector3 GetRandomPoint()
    {
        Bounds bounds = zoneCollider.bounds;
        Vector3 point;
        int safety = 0;

        // Rejection sampling : on tire un point dans le rectangle englobant,
        // et on vérifie qu'il est bien DANS la forme réelle du collider
        // (utile si c'est un cercle ou une forme irrégulière, pas juste un rectangle)
        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            point = new Vector3(x, y, transform.position.z);
            safety++;
        }
        while (!zoneCollider.OverlapPoint(point) && safety < 30);

        return point;
    }
}