using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 10.4f;
    public float jumpFoce = 4f;
    public bool isGrounded = false;
    private Transform healthTransform;
    private HealthSystem healthSystem;
    private HingeJoint2D joint;

    public Rigidbody2D body2d;

    public Transform healthbarPf;
    // Start is called before the first frame update
    void Start()
    {

        healthSystem = new HealthSystem(100);
        body2d = GetComponent<Rigidbody2D>();

        healthTransform = Instantiate(healthbarPf, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1.2f), Quaternion.identity);

        Healthbar healthbar = healthTransform.GetComponent<Healthbar>();
       
        healthbar.Setup(healthSystem);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0f,0f);
        transform.position += movement * Time.deltaTime * speed;
        healthTransform.position = new Vector3(transform.position.x, transform.position.y + 1.2f);
    }

    void Jump(){
        if(Input.GetKey("w") && isGrounded == true){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,jumpFoce),ForceMode2D.Impulse);
        }else if(Input.GetKeyDown("w") && isGrounded == false)
        {
            joint = gameObject.AddComponent<HingeJoint2D>();
            joint.anchor = new Vector2(-0.003f, 0.334f);
            transform.GetComponent<Pendulum>().SetPendulum();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(10);
        }
    }
}
