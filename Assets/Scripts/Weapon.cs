using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float attackRate = 0.25f;
    [SerializeField] private float baseAttackTime = 0.5f;
    [SerializeField] private int maxCombo = 4;
    [SerializeField] private Collider weaponCollider;
    [SerializeField, Range(0, 1)] private float delayActiveWeapon = 0.2f;
    
    public bool IsAttacking { get; private set; }
    public bool IsCooldown { get; private set; }

    private float resetComboRemainTime;
    private int currentCombo;

    private void Awake()
    {
        weaponCollider.enabled = false;
        StartCoroutine(ResetComboCoroutine());
    }

    private void OnDestroy()
    {
        StopCoroutine(ResetComboCoroutine());
    }

    public void Attack()
    {
        if (IsAttacking || IsCooldown)
            return;
        
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        IsAttacking = true;
        resetComboRemainTime = 1;
        animator.SetFloat("Combo", currentCombo);
        animator.SetTrigger("Attack");

        var animSpeed = 1f;
        var attackTime = baseAttackTime;
        var cooldownTime = attackRate - attackTime;
        if (attackRate < baseAttackTime)
        {
            attackTime = attackRate;
            animSpeed =  baseAttackTime / attackRate;
        }
        
        animator.SetFloat("AttackSpeed", animSpeed);

        yield return new WaitForSeconds(attackTime * delayActiveWeapon);
        weaponCollider.enabled = true;
        
        yield return new WaitForSeconds(attackTime * (1 - delayActiveWeapon));
        IsAttacking = false;
        weaponCollider.enabled = false;

        currentCombo = (currentCombo + 1) % maxCombo;

        if (cooldownTime > 0)
        {
            IsCooldown = true;
            yield return new WaitForSeconds(cooldownTime);
            IsCooldown = false;
        }
    }

    private IEnumerator ResetComboCoroutine()
    {
        while (true)
        {
            if (IsAttacking || IsCooldown)
                yield return new WaitForEndOfFrame();;

            resetComboRemainTime -= Time.deltaTime;
            if (resetComboRemainTime <= 0)
            {
                currentCombo = 0;
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
}

// https://github.com/Thinhltltlt/demo
