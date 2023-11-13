using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    //can access through inspector but not other scripts
    [SerializeField] private string robotType;
    [SerializeField] GameObject missileprefab;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip weakHitSound;
    
    public Animator robot;

    public int health;
    public int range;
    public float fireRate;

    public Transform missileFireSpot;
    UnityEngine.AI.NavMeshAgent agent;  //reference to NavMesh component

    private Transform player;
    private float timeLastFired;

    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        //1
        isDead = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //2
        if (isDead)
        {
            return;
        }
        
        //3
        transform.LookAt(player);
        //4
        agent.SetDestination(player.position);
            
        //check if player is within fire range
        //& if robot has enough time to shoot between shots
        if(Vector3.Distance(transform.position, player.position) < range 
                                    && Time.time - timeLastFired > fireRate)
        {
            //6
            timeLastFired = Time.time;
            fire();
        }

    }
    private void fire()
    {
        GameObject missile = Instantiate(missileprefab);
        
        missile.transform.position = missileFireSpot.transform.position;
        missile.transform.rotation = missileFireSpot.transform.rotation;
        
        robot.Play("Fire");
        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }
        health -= amount;

        if(health <= 0)
        {
            isDead = true;
            robot.Play("Die");
            StartCoroutine("DestroyRobot");
            GetComponent<AudioSource>().PlayOneShot(deathSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(weakHitSound);
        }
    }

    //Adds a delay to allow RobotDeath animation to play
    IEnumerator DestroyRobot()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
