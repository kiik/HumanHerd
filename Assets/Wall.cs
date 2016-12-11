using Pathfinding;
using UnityEngine;

public class Wall : MonoBehaviour , IDestructible{
    int health = 100;

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destruct();
        }
    }

    public void Destruct()
    {
        // TODO add kaboom effect

        // Recalculate path
        Bounds b = GetComponent<BoxCollider2D>().bounds;

        GraphUpdateObject guo = new GraphUpdateObject(b);
        AstarPath.active.UpdateGraphs(guo);

        Destroy(gameObject.transform.parent.gameObject);
    }
}
