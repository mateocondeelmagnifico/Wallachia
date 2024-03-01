using EnemyMechanics;
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
        public GameObject[] damager;
        protected GameObject particlesystem;
        [HideInInspector] public GameObject player;

        protected ParticleSystem particles;

        protected NavMeshAgent navegador;

        #region Life variables
        protected string enemytype;

        public float life, maxLife, regeneration, minStunAmount;
        protected float idletimer, damageTimer, stunResistance, stunTimer, dmgResistance;

        protected bool invulnerable, isplaying;
        #endregion

        #region Movement and Attacking variables
        public bool angry, playerDetected, slowCondition, canDamage;
        protected bool isattacking;
        private bool isdamaging, hasreached, isinplace;

        protected float staggered, wanderTimer;
        //must be modified from enemy scripts
        public float attackingrange, lungeSpeed, speed;

        [HideInInspector] public Vector3 destination;
        private Vector3 attackposition;
        #endregion

        private void Start()
        {
            SetStartVariables();
        }
        private void Update()
        {
            #region idilingSounds
            //this is for idling sounds
            if (idletimer > 0 && staggered <= 0)
            {
                idletimer -= Time.deltaTime;
            }
            else
            {
                ChoseRandomSound();
            }
            #endregion

            ApplyRegeneration();

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
                animador.SetBool("Moving", true);
            }
            else
            {
                animador.SetBool("Moving", false);
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

        #region Protected voids
        protected void SetStartVariables()
        {
            sonido = GetComponent<Sonido>();
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
            sonido.playaudio("Idle " + Whichsound);
            idletimer = Random.Range(3, 8);
        }
        protected void ChangeLife(float damage)
        {
            if (damage > 0)
            {
                SetHitmarker(damage - dmgResistance);
                playerDetected = true;
                angry = true;
                scaryness.IncreaseScaryness(damage/5);
            }
            life -= (damage - dmgResistance);

            #region CheckDead
            if (life <= 0)
            {
                DyingEffects();
                scaryness.IncreaseScaryness(0.6f);
                setUIPlayer.UpdateEnemyCounter();
                destination = transform.position;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                sonido.enabled = false;
                damager[0].SetActive(false);
                damager[1].SetActive(false);
                particles.Stop();
                GetComponent<BoxCollider>().enabled = false;
                enabled = false;
                animador.SetBool("Dead", true);
                navegador.enabled = false;
                this.enabled = false;
            }
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
                staggered = stunamount - stunResistance;
                attackposition = transform.position;
                stunResistance += 0.5f;
                animador.SetBool("Hurt", true);
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
            if (Vector3.Distance(player.transform.position, transform.position) < attackingrange && isattacking == false && staggered <= 0)
            {
                attackposition = transform.position;
                animador.SetTrigger("Attack");
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
        public virtual void ModifySpeed()
        {
            if (playerDetected)
            {
                speed = 5;
            }
            if (staggered > 0 || isattacking == true)
            {
                speed = 3;
            }
            if (slowCondition == true)
            {
                speed /= 2;
            }

            navegador.speed = speed;
        }
        private void CheckStun()
        {
            if (staggered > 0)
            {
                //transform.position = attackposition;
                staggered -= Time.deltaTime;
                if(navegador.enabled) navegador.isStopped = true;
                animador.SetBool("Moving", false);
                canDamage = false;
                navegador.velocity = new Vector3(0, 0, 0);
            }
            else
            {
                animador.SetBool("Hurt", false);
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
                if (Vector3.Distance(transform.position, player.transform.position) > 1 && staggered <= 0)
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

        //These three scripts are called by animation events
        public void Damagestart()
        {
            sonido.playaudio("Attack");
            canDamage = true;
            isdamaging = true;
        }
        public void Endattack()
        {
            isattacking = false;
            isinplace = false;
        }
        public void Enddamage()
        {
            canDamage = false;
            isdamaging = false;
        }
        #endregion

        #region Virtual Voids
        public virtual void TakeDamage(float damage, string hitype, bool playsound) { }
        public virtual void StatusEffect(string type) { }
        protected virtual void DyingEffects() { }
        protected virtual void EmptyUpdate() { }
        #endregion
    }
}
