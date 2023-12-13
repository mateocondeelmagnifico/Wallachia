
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemy : MonoBehaviour
{
    //THis Script controls the life and status effects of the enemy, it goes on all enemies

    public Animator animador;
    public BasicEnemyMovement othersript;
    public Image hitmarker;
    public Sonido sonido;
    public GameObject[] damager;
    public GameObject particlesystem;
    public GameObject hitmarkerObject;

    public ParticleSystem particles;

    public string enemytype;

    public float life;
    public float maxlife;
    public float transforming;
    public float regeneration;
    public float hitmarkertimer;
    public float idletimer;
    public float damageTimer;
    public float vulnerableTimer;

    public bool vulnerable;
    public bool invulnerable;
    public bool isplaying;
    public virtual void Start()
    {
        sonido = GetComponent<Sonido>();
        particles = GetComponent<ParticleSystem>();
        idletimer = 4;
        life = maxlife;
        othersript = GetComponent<BasicEnemyMovement>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        #region enable hitmarker
        //this is to enable the hitmarker when you hit an enemy
        if (hitmarker.enabled == false)
        {
            hitmarkertimer = 0;
        }
        if (hitmarkertimer > 0 && hitmarker.enabled == true)
        {
            hitmarkertimer -= Time.deltaTime;
            if (hitmarkertimer <= 0)
            {
                hitmarker.enabled = false;
            }
        }
        #endregion

        //The more an enemy gets hit, the more stunresistance he builds up
        checkdead();

        #region applyTransforming
        //this applies the transforming status effect, it affects zombies and werewolves differently
        if (enemytype == "Zombie")
        {
            life -= transforming * Time.deltaTime;
            if (transforming > 0 && isplaying == false)
            {
                particles.Play();
                isplaying = true;
            }
        }
        if (enemytype == "Werewolf")
        {
            if (transforming > 0.2)
            {
                regeneration = 0.30f;
                if (isplaying == false)
                {
                    particles.Play();
                    isplaying = true;
                }
            }
            else
            {
                regeneration = 0.7f;
            }
            if (transforming < 0.4 && life < maxlife && life > 0)
            {
                life += Time.deltaTime * regeneration;
            }
        }
        #endregion

        #region idilingSounds
        //this is for idling sounds
        if (idletimer > 0 && othersript.staggered <= 0)
        {
            idletimer -= Time.deltaTime;
        }
        else
        {
            choserandomsound();
        }
        #endregion
    }
    public void choserandomsound()
    {
        string Whichsound;
        int random = Random.Range(2, 6);
        Whichsound = random.ToString();
        sonido.playaudio("Idle " + Whichsound);
        idletimer = Random.Range(3, 8);
    }
    public virtual void takedamage(float damage, string hitype, bool playsound)
    {
        sonido.playaudio("Hurt");
        if (life > 0)
        {
            SetHitmarker(damage);
            //This breaks invulnerability for zombie enemies
            if (damage >= maxlife / 4 && enemytype == "Zombie")
            {
                invulnerable = false;
            }
            if (invulnerable == false)
            {
                life -= damage;
            }

            if (enemytype == "Werewolf" && hitype == "Light")
            {

            }
            else
            {
                animador.SetBool("Hurt", true);
                othersript.isattacking = false;
            }
            othersript.playerdetected = true;

            statuseffect(hitype);
            decidestun(hitype);
        }
    }
    public virtual void statuseffect(string type)
    {
        //Silver, Iron, Garlic and Holy

        //Currently iron does nothing

        if (invulnerable != true)
        {
            if (type == "Garlic")
            {
                if (enemytype == "Zombie")
                {
                    {
                        life -= 1f;
                    }
                }
                if (enemytype == "Werewolf")
                {
                    if (vulnerable == true)
                    {
                        life -= 2;
                    }
                }
            }
            if (type == "Silver")
            {
                transforming += 0.2f;
                if (enemytype == "Werewolf")
                {
                    decidestun("Heavy");
                }
            }
        }
    }
    public virtual void decidestun(string hitype)
    {
        float stunamount;
        if (life > 0)
        {
            //Decide stun duration based on if its a heavy or light attack
            if (hitype == "Light")
            {
                if (enemytype == "Zombie")
                {
                    stunamount = 2;
                    if (stunamount > 0.4f)
                    {
                        othersript.staggered = stunamount;
                        othersript.attackposition = transform.position;
                    }
                }
            }
            if (hitype == "Heavy")
            {
                if (enemytype == "Zombie")
                {
                    stunamount = 3;
                    othersript.staggered = stunamount;
                }
                if (enemytype == "Werewolf")
                {

                        stunamount = 3;
                        if (stunamount > 0.4f)
                        {
                            othersript.staggered = stunamount;
                        }
                        vulnerable = true;
                    
                }
                othersript.attackposition = transform.position;
            }
        }
    }
    public void checkdead()
    {
        if (life <= 0)
        {
            othersript.navegador.velocity = new Vector3(0, 0, 0);
            sonido.enabled = false;
            if (enemytype == "Werewolf")
            {
                GetComponent<Werewolf>().myball.GetComponent<circlefollow>().istaken = false;
                GetComponent<Werewolf>().myball.SetActive(false);
                transform.parent.GetComponent<Groupmanager>().isattacking = false;
            }
            damager[0].SetActive(false);
            damager[1].SetActive(false);
            particles.Stop();
            GetComponent<BoxCollider>().enabled = false;
            othersript.navegador.isStopped = true;
            othersript.enabled = false;
            animador.SetBool("Dead", true);
        }
    }
    public void SetHitmarker(float damage)
    {
        float sizemultiplier;
        //this is to check if the attack kills
        if (damage >= life)
        {
            hitmarker.color = new Color(255, 0, 0);
        }
        else
        {
            hitmarker.color = new Color(255, 255, 255);
        }

        if (damage > 1.2f)
        {
            sizemultiplier = 1.2f;
        }
        else
        {
            if (damage > 0.8f)
            {
                sizemultiplier = 0.9f;
            }
            else
            {
                sizemultiplier = 0.6f;
            }
        }
        hitmarkerObject.transform.localScale = new Vector3(1, 1, 1) * sizemultiplier;
        hitmarker.enabled = true;
        hitmarkertimer = 0.4f;
    }

}
