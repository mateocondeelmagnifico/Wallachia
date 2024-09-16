using UnityEngine;
using UnityEngine.UI;

namespace PlayerMechanics
{ 
    public class SetUiValues : MonoBehaviour
    {
        public static SetUiValues Instance {  get; private set; }

        [SerializeField] private Image hitmarker, bloodyScreen;
        public Image crossImage;
        [SerializeField] private TMPro.TextMeshProUGUI enemyCounter, bulletName;

        public float enemiesLeft;
        private float hitmarkerTimer, enemiesTimer, enemiesTimer2, bloodTimer, texTimer, opacityTimer;
        [SerializeField] private float hurtValue;

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
                
                if(enemyCounter.fontSize < 200)enemyCounter.fontSize += Time.deltaTime * 500;
            }
            else if(enemiesTimer2 > 0 || enemyCounter.fontSize > 70)
            {
                enemiesTimer2 -= Time.deltaTime;
                enemyCounter.fontSize -= Time.deltaTime * 250;
            }
            #endregion

            #region Bloodyscreen Timer
            if (bloodTimer > 0)
            {
                bloodTimer -= Time.deltaTime;
                bloodyScreen.color = new Color(1, 1, 1, bloodTimer + hurtValue/ 6);
            }
            #endregion

            #region Bullet name timer
            if(texTimer > 0)
            {
                texTimer -= Time.deltaTime;
                if (opacityTimer < 1 && texTimer > 0.5f) opacityTimer += Time.deltaTime * 2;

                if (opacityTimer > 0 && texTimer < 0.5f) opacityTimer -= Time.deltaTime * 2.5f;

                bulletName.color = new Color(1, 1, 1, opacityTimer);
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
            hurtValue -= damage;

            float time = 0.3f + Mathf.Abs(damage / 6);
            if (time < 0.6f) time = 0.6f;

            bloodTimer = time;
        }
        public void SetBulletName(string name)
        {
            texTimer = 1.5f;
            bulletName.text = name;
            opacityTimer = 0;
        }

        public void SetCrossCooldown(float amount)
        {
            crossImage.fillAmount = 1 - amount/9;

            if(crossImage.fillAmount >= 1) crossImage.color = new Color(1,1,1,1);
            else crossImage.color = new Color(1, 1, 1, 0.3f);
        }
    }
}
