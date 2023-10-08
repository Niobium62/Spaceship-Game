using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    public float speed = 30.0f;
    public float rotateSpeed = 5.0f;
    public float attackRange;
    public float outOfRange;
    private bool playerInAttackRange;
    private bool playerOutOfRange;
    public GameObject projectile;
    public GameObject explosion;
    private PlayerController playerControllerScript;
    private GameManager gameManager;
    private bool destroyed = false;
    public AudioClip laserSound;

    public LayerMask whatIsPlayer;
    private float nextShot = 0;
    public float fireRate;

    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerControllerScript = player.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {


        //rotate towards player
        if (player != null)
        {
            Quaternion relativeRot = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, relativeRot, rotateSpeed * Time.deltaTime);
        }

        //move towards player
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        //fire projectile when player in attackRange
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if(playerInAttackRange && Time.time > nextShot)
        {
            // controls fireRate
            nextShot = Time.time + fireRate;
            
            //shoots laser
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            projectileInstance.transform.Rotate(0, 90, 90);
            projectileInstance.transform.Translate(0, 21, 0);
            AudioSource.PlayClipAtPoint(laserSound, transform.position);
        }

        playerOutOfRange = !(Physics.CheckSphere(transform.position, outOfRange, whatIsPlayer));
        if (playerOutOfRange && player != null) //if out of range
        {
            Destroy(gameObject);
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile")) //if collide with laser
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            if (destroyed == false)
            {
                gameManager.UpdateScore(100);
                destroyed = true;
            }
            
        }
    }
}
