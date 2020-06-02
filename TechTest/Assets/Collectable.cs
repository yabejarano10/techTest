using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public int value = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("PowerCollectable"))
            {
                ScoreManager.instance.ChangeSpecialScore(value);
            }
            else
            {
                ScoreManager.instance.ChangeScore(value);
            }

            Destroy(gameObject);
        }
    }
}
