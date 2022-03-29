using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 3;
    Vector2 rightAttackOffset;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            // Deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null) {
                enemy.Health -= damage;
            }
        }
    }
}
