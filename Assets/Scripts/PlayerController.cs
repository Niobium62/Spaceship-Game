using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 30.0f;
    public float turnSpeed = 90.0f;
    private float horizontalInput;
    private float verticalInput;
    public float fireRate = 0.1f;
    private float nextShot = 0;
    public int invertControl = -1;
    public int health = 50;
    public GameObject projectile;
    public Camera mainCam;
    public GameObject explosion;
    public bool destroyed = false;
    private HealthBarScript healthBarScript;
    public GameObject gameOverScreen;
    public AudioClip laserSound;

    // Start is called before the first frame update
    void Start()
    {
        //healthBar = GameObject.Find("Health Bar");
        healthBarScript = GameObject.Find("Health Bar").GetComponent<HealthBarScript>();
        healthBarScript.SetMaxHealth(health);
        healthBarScript.SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            ControlShip();
        } else if (destroyed == false) //when health is 0, destroy ship
        {
            DestroyShip();
        }

    }

    private void DestroyShip()
    {
        Debug.Log("Game Over");
        Camera deathCam = Instantiate(mainCam, transform.position, transform.rotation);
        deathCam.transform.Translate(0, 3, -16.5f);
        deathCam.enabled = true;
        mainCam.enabled = false;
        mainCam.GetComponent<AudioListener>().enabled = false;
        deathCam.GetComponent<AudioListener>().enabled = true;
        Instantiate(explosion, transform.position, transform.rotation);

        destroyed = true;
        Destroy(gameObject);
        gameOverScreen.gameObject.SetActive(true);
    }

    void ControlShip()
    {
        Debug.Log($"Health: {health}");
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // ship always moves forward, but turns left, right, up, or down according to WASD input
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        transform.Rotate(Vector3.right, Time.deltaTime * turnSpeed * verticalInput * invertControl);


        // fire projectile when space is pressed
        if (Input.GetKey(KeyCode.Space) && Time.time > nextShot)
        {
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            projectileInstance.transform.Rotate(0, 90, 90);
            projectileInstance.transform.Translate(0, 21, 0);
            AudioSource.PlayClipAtPoint(laserSound, transform.position);
            nextShot = Time.time + fireRate; 
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Projectile") && health > 0) //if collide with laser
        {
            
            health--;
            healthBarScript.SetHealth(health);
        }
    }
}
