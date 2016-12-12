using Pathfinding;
using UnityEngine;
using System.Collections.Generic;

public class Wall : MonoBehaviour , IDestructible{

    [SerializeField]
    List<Sprite> wallSprites = new List<Sprite>();

    public int health = 100;

    public void Hit(int damage)
    {
        GameManager.instance.soundManager.WallHit(transform.position);
        health -= damage;
        if (health <= 0)
        {
            Destruct();
        }
    }

    public void Destruct()
    {
        // TODO add kaboom effect
        GameManager.instance.soundManager.WallDestruct(transform.position);

        // Recalculate path
        Bounds b = GetComponent<BoxCollider2D>().bounds;

        GraphUpdateObject guo = new GraphUpdateObject(b);
        AstarPath.active.UpdateGraphs(guo);

        Destroy(gameObject.transform.parent.gameObject);
    }
}
