using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovemnet>())
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
