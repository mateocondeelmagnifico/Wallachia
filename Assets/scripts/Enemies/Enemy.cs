using PlayerMechanics;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMechanics
{
    public class Enemy : MonoBehaviour
    {
        //THis Script controls the life and status effects of the enemy, it goes on all enemies

        protected Animator animador;
        protected SetUiValues setUIPlayer;
        [HideInInspector] public Groupmanager groupManager;
        protected Scaryness scaryness;

        protected Sonido sonido;
        protected AudioSource[] sources = new AudioSource[2];
        public GameObject[] damager;
        protected GameObject particlesystem;
        [HideInInspector] public GameObject player;

        protected ParticleSystem particles;

        protected NavMeshAgent navegador;

        #region Life variables
        public string enemytype;

        public float life, maxLife, regeneration, minStunAmount, staggerNeeded, stun;
        protected float idletimer, damageTimer, stunResistance, stunTimer, dmgResistance, stagger;

        protected bool invulnerable, isplaying;

        //Awaiting must be turned on from the scene
        public bool isStaggered, awaiting;
        #endregion

        #region Movement and Attacking variables
        public bool angry, playerDetected, slowCondition, canDamage;
        protected bool isattacking;
        private bool isdamaging, hasreached, isinplace;

        protected float wanderTimer;
        //must be modified from enemy scripts
        public float attackingrange, lungeSpeed, speed;

        [HideInInspector] public Vector3 destination;
        private Vector3 attackposition;
        #endregion

        private void Start()
        {
            for (int i = 0; i < 2; i++)
            {
                sources[i] = GetComponents<AudioSource>()[i];
            }

            SetStartVariables();
        }
        private void Update()
        {
            ApplyRegeneration();

            if (!awaiting)
            {
                #region idilingSounds
                //this is for idling sounds
                if (idletimer > 0 && stagger <= 0)
                {
                    idletimer -= Time.deltaTime;
                }
                else
                {
                    ChoseRandomSound();
                }
                #endregion

                #region Movement and empty Update

                ModifySpeed();

                if (playerDetected == true)
                {
                    destination = player.transform.position;
                }
                else
                {
                    Wander();
                }

                if (navegador.velocity != Vector3.zero)
                {
                    animador.SetFloat("Speed", 1);
                }
                else
                {
                    animador.SetFloat("Speed", 0);
                }

                EmptyUpdate();

                if (navegador.isActiveAndEnabled) navegador.SetDestination(destination);
                #endregion

                #region Attacking
                CheckDistance();
                CheckStun();
                CheckAttack();
                #endregion

                #region Damage and stun timer
                if (damageTimer > 0)
                {
                    damageTimer -= Time.deltaTime;
                }

                if (stunTimer <= 0)
                {
                    stunResistance = 0;
                }
                else
                {
                    stunTimer -= Time.deltaTime;
                }
                #endregion
            }
        }

        #region Protected voids
        protected void SetStartVariables()
        {
            sonido = Sonido.instance;
            particles = GetComponent<ParticleSystem>();
            animador = GetComponent<Animator>();
            idletimer = 4;
            life = maxLife;
            setUIPlayer = SetUiValues.Instance;
            destination = this.transform.position;
            navegador = GetComponent<NavMeshAgent>();
            scaryness = Scaryness.Instance;
        }
        protected void ChoseRandomSound()
        {
            string Whichsound;
            int random = Random.Range(2, 6);
            Whichsound = random.ToString();
            sonido.playaudio(enemytype + " Idle " + Whichsound, sources[0]);
            idletimer = Random.Range(3, 8);
        }
        protected void ChangeLife(float damage)
        {
            if (damage > 0)
            {
                awaiting = false;
                SetHitmarker(damage - dmgResistance);
                playerDetected = true;
                angry = true;
                scaryness.IncreaseScaryness(damage/5);
            }
            life -= (damage - dmgResistance);

            #region CheckDead
            if (life <= 0) Die(0);
            #endregion
        }
        protected void SetHitmarker(float damage)
        {
            float sizemultiplier;
            bool doesItKill;

            #region Set color
            if (damage >= life)
            {
                doesItKill = true;
            }
            else
            {
                doesItKill = false;
            }
            #endregion

            #region Set Size
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
            #endregion

            setUIPlayer.SetHitmarker(sizemultiplier, doesItKill);
        }
        protected void DecideStun(float stunamount)
        {
            if(stunamount > minStunAmount)
            {
                stagger = stunamount;
                stun = stunamount;
                attackposition = transform.position;
                //stunResistance += 0.2f;

                if(stagger < staggerNeeded) animador.SetInteger("Stun", 1);
                else GetStagger();
            }
        }
        private void ApplyRegeneration()
        {
            if (life <= maxLife) ChangeLife(-regeneration * Time.deltaTime);
        }
        private void CheckDistance()
        {
            //Detect player
            if (Vector3.Distance(player.transform.position, transform.position) < 8)
            {
                playerDetected = true;
                groupManager.InformEnemies();

                if (speed < 6)
                {
                    speed += Time.deltaTime;
                }
            }

            //If the player is too close, it attacks
            if (Vector3.Distance(player.transform.position, transform.position) < attackingrange && isattacking == false && stagger <= 0 && canDamage == false)
            {
                attackposition = transform.position;
                animador.SetBool("Attacking", true);
                angry = true;
                navegador.isStopped = true;
                isattacking = true;
            }

            //warn other enemies
            if(playerDetected && !groupManager.activated)
            {
                groupManager.InformEnemies();
            }
        }
        private void GetStagger() 
        {
            animador.SetInteger("Stun", 2);
            isStaggered = true;
            stunTimer = 3;
            stagger = 3;
            CheckStun();
        }
        public virtual void ModifySpeed()
        {
            if (playerDetected)
            {
                speed = 5;
            }
            if (stagger > 0 || isattacking == true)
            {
                speed = 3;
            }
            if (slowCondition == true)
            {
                speed /= 2;
            }

            SpeedChanges();

            navegador.speed = speed;
        }
        private void CheckStun()
        {
            if (stagger > 0)
            {
                //transform.position = attackposition;
                stagger -= Time.deltaTime;
                if(navegador.enabled) navegador.isStopped = true;
                animador.SetFloat("Speed", 0);
                canDamage = false;
                navegador.velocity = new Vector3(0, 0, 0);
            }
            else
            {
                animador.SetInteger("Stun", 0);
                if (isattacking == false && navegador.isActiveAndEnabled)
                {
                    navegador.isStopped = false;
                }
            }
        }
        private void CheckAttack()
        {
            if (isattacking == true)
            {
                transform.LookAt(player.transform.position);
                if (canDamage == false && isdamaging == true)
                {
                    navegador.velocity = new Vector3(0, 0, 0);
                }
                navegador.isStopped = true;
                if (isinplace == false)
                {
                    transform.position = attackposition;
                    if (canDamage == true)
                    {
                        isinplace = true;
                    }
                }
            }


            if (canDamage == true)
            {
                if (Vector3.Distance(transform.position, player.transform.position) > 1 && stagger <= 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, lungeSpeed * Time.deltaTime);
                    hasreached = false;
                }
                else
                {
                    if (hasreached == false)
                    {
                        attackposition = transform.position;
                        hasreached = true;
                    }

                    transform.position = attackposition;
                }
            }
        }
        void Wander()
        {
            wanderTimer -= Time.deltaTime;

            if (wanderTimer <= 0)
            {
                int decidestomove = Random.Range(0, 6);
                if (decidestomove >= 3)
                {
                    //move
                    destination = new Vector3(transform.position.x + Random.Range(-6, 5), transform.position.y, transform.position.z + Random.Range(-6, 5));
                    wanderTimer = Random.Range(3, 5);
                }
                else
                {
                    //don't move
                    wanderTimer = Random.Range(3, 5);
                }
            }
        }
        public void returnHome(Vector3 Destination)
        {
            //This script is so the enemy doesn't stray from their destined location
            //It is accesed by the groupmanager
            wanderTimer = 4;
            destination = Destination;
        }
        public void Die(int type)
        {
            //Type 0 is normal death, 1 is for death on respwan
            DyingEffects();
            groupManager.EnemyDead();
            if(type == 0)scaryness.IncreaseScaryness(0.6f);
            setUIPlayer.UpdateEnemyCounter();
            destination = transform.position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            sonido.enabled = false;
            damager[0].SetActive(false);
            damager[1].SetActive(false);
            particles.Stop();
            GetComponent<MeshCollider>().enabled = false;
            animador.SetBool("Dead", true);
            if (type == 1) animador.SetFloat("Death Speed", 1);
            navegador.enabled = false;
            this.enabled = false;
        }

        //These three scripts are called by animation events
        public void Damagestart()
        {
            sonido.playaudio(enemytype + " Attack", sources[0]);
            canDamage = true;
            isdamaging = true;
        }
        public void Endattack()
        {
            animador.SetBool("Attacking", false);
            isattacking = false;
            isinplace = false;
        }
        public void Enddamage()
        {
            canDamage = false;
            isdamaging = false;
        }

        public void CallSoundManager(string soundname, int wantedSource)
        {
            //Needed for anim events basically
            sonido.playaudio(enemytype + " " + soundname, sources[wantedSource]);
        }
        #endregion

        #region Virtual Voids
        public virtual void TakeDamage(float damage, string hitype, bool playsound, float staggerAmount) { }
        public virtual void StatusEffect(string type) { }
        protected virtual void DyingEffects() { }
        protected virtual void EmptyUpdate() { }
        protected virtual void SpeedChanges() { }
        #endregion
    }
}
