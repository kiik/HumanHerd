using Pathfinding;
using UnityEngine;
using System.Collections.Generic;

public class Wall : MonoBehaviour , IDestructible{

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    List<Sprite> wallSprites = new List<Sprite>();

    int maxHealth;
    public int health = 100;

    void Start()
    {
        maxHealth = health;
    }

    public void Hit(int damage)
    {
        GameManager.instance.soundManager.WallHit(transform.position);
        health -= damage;
        if (health <= maxHealth*0.6f && health > maxHealth*0.3f)
        {
            spriteRenderer.sprite = wallSprites[1];
        }
        else if (health <= maxHealth*0.3f && health > 0)
        {
            spriteRenderer.sprite = wallSprites[2];
        } 
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
