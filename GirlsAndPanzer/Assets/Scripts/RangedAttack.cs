using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Contains NavMesh classes.

public class RangedAttack : MonoBehaviour
{
    //Variables.
    public Root3 myRoot, targetRoot;
    public List<Root3> enemies;
    public bool atkReady;
    public NavMeshAgent myAgent;

    public GameObject Bullet;
    public float range;
    public float coolDown;
    private float coolDownTimer;

    public float rotateSpeedWhenAtk = 8;
    float velocityRoto;

    //Use this for initialization.
    void Start()
    {
        myRoot = GetComponent<Root3>();
        myAgent = GetComponent<NavMeshAgent>();
        Invoke("ReadyAction", myRoot.atkSpeed);
        coolDownTimer = coolDown;
    }

    void Update()
    {
        //1) Look for enemies in the detected objects.
        enemies.Clear();

        foreach(Root3 detectedObject in myRoot.detected)
        {
            if(detectedObject != null)
            {
                if(detectedObject.tag == "Enemy") enemies.Add(detectedObject);
            }
        }

        //2) If there are enemies, target the nearest one.
        if(enemies.Count > 0)
        {
            targetRoot = enemies[0];
            foreach(var enemy in enemies)
            {
                //Distance to current target.
                float dist1 = Vector3.Distance(transform.position, targetRoot.transform.position);

                //Distance to next target.
                float dist2 = Vector3.Distance(transform.position, enemy.transform.position);

                //Pick the closer.
                if(dist1 > dist2)
                {
                    targetRoot = enemy;
                }
            }

            //3) With a target picked, engage if not moving.
            if(myRoot.currentState != Root3.STATE.Moving)
            {
                myRoot.ChangeState(Root3.STATE.Combat);

                //4) Chase the target. Now based on range, not reach.
                if(Vector3.Distance(transform.position, targetRoot.transform.position) > range)
                {
                    myAgent.SetDestination(targetRoot.transform.position);
                    myAgent.isStopped = false;
                }
                else //5) When close enough, stop and keep looking at target.
                {
                    print("Shoot");
                    myAgent.isStopped = true;
                    //Rotating with SmoothDampAngle.
                    Quaternion rotationToLookAt = Quaternion.LookRotation(targetRoot.transform.position - transform.eulerAngles);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref velocityRoto, rotateSpeedWhenAtk);
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                    if(coolDownTimer <= 0)
                    {
                        int dmg = myRoot.atk + Random.Range(-myRoot.atkVar, myRoot.atkVar + 1);
                        GameObject missileClone = Instantiate(Bullet, transform.position, Quaternion.identity);
                        missileClone.GetComponent<Bullet>().damage = dmg;
                        missileClone.GetComponent<Bullet>().target = targetRoot;
                        coolDownTimer = coolDown;
                    }
                    coolDownTimer -= Time.deltaTime;
                    coolDownTimer = Mathf.Clamp(coolDownTimer, 0, coolDown);

                    //6) Attack the target when ready.
                    /*if(atkReady)
                    {
                        atkReady = false;
                        Invoke("ReadyAction", myRoot.atkSpeed);

                        //7) Calculate damage to make.
                        int dmg = myRoot.atk + Random.Range(-myRoot.atkVar, myRoot.atkVar + 1);

                        //8) Instantiate a projectile that will chase the target.
                        GameObject missileClone = Instantiate(Bullet, transform.position, Quaternion.identity);
                        missileClone.GetComponent<Bullet>().damage = dmg;
                        missileClone.GetComponent<Bullet>().target = targetRoot;
                    }*/
                }
            }
        }
        else if(myRoot.currentState == Root3.STATE.Combat)
        {
            if(coolDownTimer <= 0)
            {
                GameObject missileClone = Instantiate(Bullet, transform.position, Quaternion.identity);
                missileClone.GetComponent<Bullet>().target = targetRoot;
                coolDownTimer = coolDown;
            }

            coolDownTimer = Mathf.Clamp(coolDownTimer, 0, coolDown);
        }
    }
}