using UnityEngine;
using UnityEngine.UI;

namespace PlayerMechanics
{ 
    public class SetUiValues : MonoBehaviour
    {
        public static SetUiValues Instance {  get; private set; }

        [SerializeField] private Image hitmarker, bloodyScreen;
        [SerializeField] private TMPro.TextMeshProUGUI enemyCounter;

        private float hitmarkerTimer, enemiesLeft, enemiesTimer, enemiesTimer2, bloodTimer, hurtValue;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            enemiesLeft = 23;
            enemyCounter.text = enemiesLeft.ToString();
            bloodyScreen.color = new Color(1, 1, 1, 0);
        }

        void Update()
        {
            #region Hitmarker timer
            if (hitmarkerTimer > 0)
            {
                hitmarkerTimer -=Time.deltaTime;
            }
            else if(hitmarker.enabled)
            {
                hitmarker.enabled = false;
            }
            #endregion

            #region Enemies left Timer
            if (enemiesTimer > 0)
            {
                enemiesTimer -= Time.deltaTime;
                enemyCounter.fontSize += Time.deltaTime * 500;
            }
            else if(enemiesTimer2 > 0)
            {
                enemiesTimer2 -= Time.deltaTime;
                enemyCounter.fontSize -= Time.deltaTime * 250;
            }
            #endregion

            #region Bloodyscreen Timer
            if (bloodTimer > 0)
            {
                bloodTimer -= Time.deltaTime;
                bloodyScreen.color = new Color(1, 1, 1, bloodTimer + hurtValue);
            }
            #endregion
        }

        public void SetHitmarker(float damage, bool kills)
    {

        if (kills)
        {
            hitmarker.color = new Color(255, 0, 0);
        }
        else
        {

            hitmarker.color = new Color(255, 255, 255);
        }

        hitmarker.transform.localScale = new Vector3(1, 1, 1) * damage;
        hitmarker.enabled = true;
        hitmarkerTimer = 0.4f;
    }
        public void UpdateEnemyCounter()
        {
            enemiesLeft -= 1;
            enemyCounter.text = enemiesLeft.ToString();
            enemiesTimer = 0.15f;
            enemiesTimer2 = 0.3f;
        }
        public void UpdateBloodyScreen(float damage)
        {
            hurtValue -= damage/6;
            bloodTimer = 0.2f + Mathf.Abs(damage);
        }
    }
}
