using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public float speed = 10.4f;
    private Transform healthTransform;
    private HealthSystem healthSystem;

    public Transform healthbarPf;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthTransform = Instantiate(healthbarPf, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.6f), Quaternion.identity);
        Healthbar healthbar = healthTransform.GetComponent<Healthbar>();
        healthbar.Setup(healthSystem);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyUp("f"))
        {
            healthSystem.Damage(10);
        }
    }
}
