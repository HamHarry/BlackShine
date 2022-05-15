using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    GameObject player;
    Animator anime;
    bool isAlive = true;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            anime.SetTrigger("Dead");
            isAlive = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
