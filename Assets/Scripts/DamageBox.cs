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

    // Start is called before the first frame update
    void Start()
    {
        DamageDetermination();
        DealDamage();
        Despawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // --------------------------- Damage Box Mechanics -------------------------
    // Desapwns the Damage Box
    void DamageDetermination()
    {
        if (damageType == damageManager.light)
        {

        }
        else if (damageType == damageManager.medium)
        {
            hitBox = mDamageBox;
        }
        else if (damageType == damageManager.heavy)
        {

        }
        else if (damageType == damageManager.special)
        {

        }
    }

    void Despawn()
    {
        StartCoroutine(damageTime());
    }

    IEnumerator damageTime()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Times Up");
        // despawn this
        hitBox.SetActive(false);
    }

    // Deal Damage
    void DealDamage()
    {
        // code will go here
    }
}
