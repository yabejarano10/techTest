using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 10.4f;
    public float jumpFoce = 4f;

    public bool isGrounded = false;
    private bool pendulum = false;

    private Transform healthTransform;

    private HealthSystem healthSystem;
    private ScoreManager scoring;

    private HingeJoint2D joint;
    private GameObject pendulumArm;

    public Rigidbody2D body2d;

    public Transform healthbarPf;
    // Start is called before the first frame update
    void Start()
    {
        pendulumArm = gameObject.transform.GetChild(1).gameObject;
        pendulumArm.SetActive(false);

        scoring = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
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
        if (!pendulum)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * speed;
        }
        if (Input.GetKey("w") && isGrounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpFoce), ForceMode2D.Impulse);

        }

        healthTransform.position = new Vector3(transform.position.x, transform.position.y + 1.2f);
    }

    void Jump(){
   
        if (Input.GetKeyDown("w") && isGrounded == false && scoring.GetSpecialScore() > 0 && pendulum == false)
        {
            StartCoroutine(Pendulum());
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(10);
        }
    }

    IEnumerator Pendulum()
    {
        body2d.constraints = RigidbodyConstraints2D.None;
        pendulum = true;
        pendulumArm.SetActive(true);
        joint = gameObject.AddComponent<HingeJoint2D>();
        joint.anchor = new Vector2(-0.003f, 0.334f);
        transform.GetComponent<Pendulum>().SetPendulum();
        yield return new WaitForSeconds(3);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        pendulum = false;
        pendulumArm.SetActive(false);
        Destroy(joint);
        body2d.angularVelocity = 0;
        body2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
