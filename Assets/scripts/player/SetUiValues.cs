using UnityEngine;
using UnityEngine.UI;

namespace PlayerMechanics
{ 
public class SetUiValues : MonoBehaviour
{
    public static SetUiValues Instance {  get; private set; }

    [SerializeField] private Image hitmarker;
    private float hitmarkerTimer;
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
        }

    void Update()
    {
        if(hitmarkerTimer > 0)
        {
            hitmarkerTimer -=Time.deltaTime;
        }
        else if(hitmarker.enabled)
        {
            hitmarker.enabled = false;
        }
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
}
}
