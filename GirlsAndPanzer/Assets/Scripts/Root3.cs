using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Root3 : MonoBehaviour
{
    //Enumeration for states in the state machine.
    public enum STATE {Idle, Moving, Combat, Following};

    [Header("State")]
    public STATE currentState;
    public TextMesh myStateText;

    //Stats.
    [Header("Stats")]
    public int HP = 60;
    public int def = 5;
    public int atk = 10;
    public int atkVar = 2;
    public float atkSpeed = 2.5f;
    public float reach = 3;

    //List for detection.
    [Header("Detection")]
    public List<Root3> detected, enemies;

    [Header("Other")]
    public ParticleSystem myDamageParticle;
    public ParticleSystem myHealParticle;
    public Slider barraHP;
    public float fadeTime = 6;
    public GameObject FloatingText;

    //Use this for initialization.
    private void Start()
    {
        myStateText = GetComponentInChildren<TextMesh>();

        //Gets the particle system of the first child who has a particle system.
        myDamageParticle = GetComponentInChildren<ParticleSystem>();
        //Gets the particle system of the child names Heals.
        //myHealParticle = transform.Find("Heals").GetComponent<ParticleSystem>();

        ChangeState(STATE.Idle);

        barraHP.maxValue = HP;
        barraHP.value = HP;
        Invoke("FadeLifeBar", fadeTime);
    }

    public void ChangeState(STATE state)
    {
        currentState = state;

        if(myStateText != null)
        {
            myStateText.text = state.ToString();
        }
    }

    public void TakeDamage(int damage)
    {
        //myDamageParticle.Emit(5);
        int realDamage = damage - def <= 0 ? 1 : damage - def;
        HP = HP - realDamage <= 0 ? 0 : HP - realDamage;
        
        //HP Bar.
        barraHP.gameObject.SetActive(true);
        barraHP.value = HP;
        Invoke("FadeLifeBar", fadeTime);

        //Floating Text
        GameObject displayDmg = Instantiate(FloatingText, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;
        //displayDmg.GetComponent<FloatingText>().dano = realDamage.ToString();

        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetHeals(int heal)
    {
        myHealParticle.Emit(5);
        HP = HP + heal >= (int)barraHP.maxValue ? (int)barraHP.maxValue : HP + heal;

        //HP Bar.
        barraHP.gameObject.SetActive(true);
        barraHP.value = HP;
        Invoke("FadeLifeBar", fadeTime);

        //Floating Text.
        GameObject displayDmg = Instantiate(FloatingText, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;
        displayDmg.GetComponent<FloatingText>().dano = "+" + heal.ToString();
    }

    public void FadeLifeBar()
    {
        if(barraHP.value == barraHP.maxValue)
        {
            barraHP.gameObject.SetActive(false);
        }
    }

}