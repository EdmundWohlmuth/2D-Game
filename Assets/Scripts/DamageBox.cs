using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    //public float damageMuliplier // maybe??
    // damage may also be hard locked to type
    [Header("Damage Box")]
    public GameObject lDamageBox;
    public GameObject mDamageBox;
    public GameObject hDamageBox;
    public GameObject sDamageBox;
    private GameObject hitBox;  

    public enum damageManager
    {
        light,
        medium,
        heavy,
        special
    }
    [Header("Type of Damage")]
    public damageManager damageType;

    [Header("DO NOT TOUCH")]
    public bool isFacingRight;
    public float damageValue;
    public Vector3 damageKnockBack;

    // Start is called before the first frame update
    void Start()
    {
        DamageDetermination();
        Despawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // --------------------------- Damage Box Mechanics -------------------------
    void DamageDetermination()
    {
        if (damageType == damageManager.light)
        {
            hitBox = lDamageBox;
            damageKnockBack = new Vector2(75, 25);
            damageValue = 3f;

            if (!isFacingRight)
            {
                damageKnockBack = damageKnockBack * -1;
            }
        }
        else if (damageType == damageManager.medium)
        {
            hitBox = mDamageBox;
            damageKnockBack = new Vector2(100, 55);
            damageValue = 5f;

            if (!isFacingRight)
            {
                damageKnockBack = damageKnockBack * -1;
            }
        }
        else if (damageType == damageManager.heavy)
        {
            hitBox = hDamageBox;
            damageKnockBack = new Vector2(150, 80);
            damageValue = 8f;

            if (!isFacingRight)
            {
                damageKnockBack = damageKnockBack * -1;
            }
        }
        else if (damageType == damageManager.special)
        {
            hitBox = sDamageBox;
            damageKnockBack = new Vector2(80, 80);
            damageValue = 4f;
        }
    }

    void Despawn()
    {
        StartCoroutine(damageTime());
    }

    IEnumerator damageTime()
    {
        yield return new WaitForSeconds(0.5f);
        // despawn this
        hitBox.SetActive(false);
    }
}
