using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllor : MonoBehaviour
{
    [SerializeField] float speed;
    GameObject player;
    Animator anime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anime = GetComponentInChildren<Animator>();


    }

    private void Update()
    {
        if(player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            anime.SetTrigger("Dead");
        }
    }

}
