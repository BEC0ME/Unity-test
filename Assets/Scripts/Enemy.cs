using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp = 3;
    public Transform hudPos;
    public GameObject hudDamgeText;
    public void TakeDamage(int damage)
    {
        Hp = Hp - damage;
        GameObject hudText = Instantiate(hudDamgeText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damage;
        Debug.Log(damage);
    }
}
