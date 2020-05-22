using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public GameObject targetParent;
    public List<GameObject> targets;

    public GameObject gun;
    public GameObject end, start; // The gun's start and end point
    public GameObject bulletHole;

    public GameObject shotSound;
    public GameObject muzzlePrefab;

    public GameObject righthand;
    public float health = 100;
    public bool isDead;
    int index = 0;
    float fov;

    //public List<GameObject> escapeTarget;

    private float waitTime = 0.02f;
    private float timer = 0.0f;

    private void Start()
    {
        for (int i = 0; i < targetParent.transform.childCount; i++)
            targets.Add(targetParent.transform.GetChild(i).gameObject);

        fov = 0.5f;
    }

    public void Being_shot(float damage) // getting hit from player
    {
        if (health > 0)
        {
            health -= damage;

        }

        if (health <= 0)
        {
            isDead = true;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<Animator>().SetBool("dead", true);

            gun.GetComponent<Rigidbody>().AddForce(2, 0, 1, ForceMode.Force);
            gun.GetComponent<BoxCollider>().isTrigger = false;
            gun.GetComponent<Rigidbody>().isKinematic = false;

        }

        if(!isDead)
        {
          Quaternion desiredRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up); // - transform.position
          transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime);
        }
        }
    

    void Update()
    {
        timer += Time.deltaTime;

        Vector3 player_direction = (player.transform.position - end.transform.position);
        float angle_of_player = Vector3.Dot(transform.forward, player_direction.normalized);
        float player_distance = player_direction.magnitude;

        if (angle_of_player >= fov && player_distance < 15)
        {
            if (player_distance > 10)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up); // - transform.position
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime);
                Debug.LogError("running towards player");
                GetComponent<Animator>().SetBool("run", true);
            }
            else
            {
                //Quaternion correction = Quaternion.FromToRotation(end.transform.position - transform.position, player.transform.position - transform.position);
                //Quaternion correction = Quaternion.LookRotation(transform.position - end.transform.forward, Vector3.up);
                float correction = transform.rotation.eulerAngles.y - end.transform.rotation.eulerAngles.y;
                Debug.LogError("Aiming at player");

                Quaternion desiredRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
                Vector3 targetRotation = desiredRotation.eulerAngles;
                targetRotation.y += correction;
                desiredRotation.eulerAngles = targetRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime);

                if (!player.GetComponent<CharacterMovement>().isDead)
                {
                    if (timer >= waitTime)
                    {
                        //addEffects();

                        GetComponent<Animator>().SetBool("run", false);
                        GetComponent<Animator>().SetTrigger("fire");
                        Debug.LogError("Shoot!");
                        timer = 0.0f;

                        RaycastHit rayHit;

                        float rand_y = Random.Range(-0.5f, 0.5f);
                        float rand_x = Random.Range(-0.5f, 0.5f);
                        Vector3 up_vector = end.transform.up * rand_y;
                        Vector3 right_vector = end.transform.right * rand_x;

                        //Debug.Log(end.transform.up);
                        //Debug.Log(up_vector);

                        // + up_vector + right_vector
                        if (Physics.Raycast(end.transform.position + up_vector + right_vector, (end.transform.position + up_vector + right_vector) - start.transform.position, out rayHit, 100.0f))
                        {
                            if (!player.GetComponent<CharacterMovement>().isDead)
                            {
                                Debug.Log(rayHit.transform.name);
                                if (rayHit.transform.tag == "Player")
                                {
                                    player.GetComponent<GunVR>().Being_shot(20.0f);
                                    Debug.LogError("Hit!!");

                                }
                                else
                                {
                                   // Instantiate(bulletHole, rayHit.point + rayHit.transform.up * 0.01f, rayHit.transform.rotation);
                                }
                            }
                        }
                    }
                }
            }

        }
        else
        {
            Vector3 tempPos = new Vector3(targets[index].transform.position.x, transform.position.y, targets[index].transform.position.z);
            Quaternion desiredRotation = Quaternion.LookRotation(tempPos - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime);

            index += 1;
            if (index >= targets.Count)
            {
                index = 0;
            }
        }

    }

    void addEffects() // Adding muzzle flash, shoot sound and bullet hole on the wall
    {
        Instantiate(shotSound, transform.position, transform.rotation);

        GameObject tempMuzzle = Instantiate(muzzlePrefab, end.transform.position, end.transform.rotation);
        tempMuzzle.GetComponent<ParticleSystem>().Play();
        Destroy(tempMuzzle, 2.0f);
    }
}
