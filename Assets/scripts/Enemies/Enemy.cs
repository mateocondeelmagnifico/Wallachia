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
        protected BasicEnemyMovement othersript;
        protected SetUiValues setUIPlayer;

        protected Sonido sonido;
        protected GameObject[] damager;
        protected GameObject particlesystem;
        public GameObject player;

        protected ParticleSystem particles;

        protected NavMeshAgent navegador;

        #region Life variables
        protected string enemytype;

        protected float life, maxlife, idletimer, damageTimer, stunResistance, stunTimer, regeneration, dmgResistance, minStunAmount;

        protected bool invulnerable, isplaying;
        #endregion

        #region Movement and Attacking variables
        public bool angry, playerdetected;
        protected bool candamage, isattacking;
        private bool isdamaging, hasreached, isinplace;

        protected float staggered;
        //must be modified from enemy scripts
        protected float attackingrange, lungeSpeed;

        private Vector3 destination, attackposition;
        #endregion

        private void Update()
        {
            #region idilingSounds
            //this is for idling sounds
            if (idletimer > 0 && othersript.staggered <= 0)
            {
                idletimer -= Time.deltaTime;
            }
            else
            {
                ChoseRandomSound();
            }
            #endregion

            ApplyRegeneration();

            #region Movement
            if (playerdetected == true)
            {
                destination = player.transform.position;
            }

            if (navegador.velocity != Vector3.zero)
            {
                animador.SetBool("Moving", true);
            }
            else
            {
                animador.SetBool("Moving", false);
            }

            navegador.SetDestination(destination);
            #endregion

            #region Attacking
            CheckDistance();
            CheckStun();
            CheckAttack();
            #endregion
        }
        #region Protected voids
        protected void SetStartVariables()
        {
            sonido = GetComponent<Sonido>();
            particles = GetComponent<ParticleSystem>();
            idletimer = 4;
            life = maxlife;
            othersript = GetComponent<BasicEnemyMovement>();
            setUIPlayer = SetUiValues.Instance;
            destination = this.transform.position;
            navegador = GetComponent<NavMeshAgent>();
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
            SetHitmarker(damage - dmgResistance);
            othersript.playerdetected = true;
            life -= (damage - dmgResistance);

            #region CheckDead
            if (life <= 0)
            {
                DyingEffects();
                PlayerMechanics.Scaryness.Instance.IncreaseScaryness(1);
                othersript.navegador.velocity = new Vector3(0, 0, 0);
                sonido.enabled = false;
                damager[0].SetActive(false);
                damager[1].SetActive(false);
                particles.Stop();
                GetComponent<BoxCollider>().enabled = false;
                othersript.navegador.isStopped = true;
                othersript.enabled = false;
                animador.SetBool("Dead", true);
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
                othersript.staggered = stunamount - stunResistance;
                othersript.attackposition = transform.position;
                stunResistance += 0.5f;
            }
        }
        private void ApplyRegeneration()
        {
            life -= regeneration * Time.deltaTime;
        }
        private void CheckDistance()
        {
            //If the player is too close, it attacks
            if (Vector3.Distance(player.transform.position, transform.position) < attackingrange && isattacking == false && staggered <= 0)
            {
                attackposition = transform.position;
                animador.SetTrigger("Attack");
                navegador.isStopped = true;
                isattacking = true;
            }
        }
        private void CheckStun()
        {
            if (staggered > 0)
            {
                //transform.position = attackposition;
                staggered -= Time.deltaTime;
                navegador.isStopped = true;
                animador.SetBool("Moving", false);
                candamage = false;
                navegador.velocity = new Vector3(0, 0, 0);
            }
            else
            {
                animador.SetBool("Hurt", false);
                if (isattacking == false)
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
                if (candamage == false && isdamaging == true)
                {
                    navegador.velocity = new Vector3(0, 0, 0);
                }
                navegador.isStopped = true;
                if (isinplace == false)
                {
                    transform.position = attackposition;
                    if (candamage == true)
                    {
                        isinplace = true;
                    }
                }
            }

            if (candamage == true)
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

        //These three scripts are called by animation events
        public void Damagestart()
        {
            sonido.playaudio("Attack");
            candamage = true;
            isdamaging = true;
        }
        public void Endattack()
        {
            isattacking = false;
            isinplace = false;
        }
        public void Enddamage()
        {
            candamage = false;
            isdamaging = false;
        }
        #endregion

        #region Virtual Voids
        public virtual void TakeDamage(float damage, string hitype, bool playsound) { }
        protected virtual void StatusEffect(string type) { }
        protected virtual void DyingEffects() { }
        #endregion
    }
}
