using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeDummy : MonoBehaviour
{
    public GameObject damageBox = null;
    public DamageBox damageBoxScript = null;

    public Rigidbody2D characterController;

    private float stanima;

    // ------------------------------ Knockback ---------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageBox") == true)
        {
            damageBox = collision.gameObject;
            damageBoxScript = damageBox.GetComponent<DamageBox>();

            //TEMP - will actually pull information from damageBox
            Vector2 knockBacktaken = damageBox.GetComponent<DamageBox>().damageKnockBack;
            characterController.AddForce(knockBacktaken);
            Debug.Log(knockBacktaken);

            TakeDamage();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageBox") == true)
        {
            damageBox = null;
            damageBoxScript = null;
        }
    }

    // -------------------------------- Damage --------------------------------------

    private void TakeDamage()
    {
        stanima = stanima + damageBox.GetComponent<DamageBox>().damageValue;
        Debug.Log("Current Stanima at: " + stanima);
    }


}
