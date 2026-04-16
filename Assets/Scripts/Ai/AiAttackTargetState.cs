using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackTargetState : AiState
{
    public AiStateId GetId() {
        return AiStateId.AttackTarget;
    }

    public void Update(AiAgent agent) {
        if (!agent.targeting.HasTarget) {
            agent.stateMachine.ChangeState(AiStateId.FindTarget);
            return;
        }

        agent.swords.SetTarget(agent.targeting.Target.transform);
        agent.weapons.SetTarget(agent.targeting.Target.transform);
        agent.navMeshAgent.destination = agent.targeting.TargetPosition;
        
        ReloadWeapon(agent);
        SelectWeapon(agent);
        UpdateFiring(agent);
        UpdateLowHealth(agent);
        UpdateLowAmmo(agent);
    }
    private void UpdateFiring(AiAgent agent)
    {
        // Проверяем, что цель в поле зрения
        if (!agent.targeting.TargetInSight)
        {
            agent.weapons.SetFiring(false);
            return;
        }

        var sword = agent.weapons.currentSword;
        var weapon = agent.weapons.currentWeapon;

        if (sword)
        {
            agent.swords.SetFiring(true);
            agent.GetComponent<Animator>().SetTrigger("equip");
            /*
            float distance = agent.targeting.TargetDistance;
            if (distance <= 2.5f)
            { // Дистанция удара
                sword.StartFiring();
                // Запускаем анимацию махания (убедись, что триггер такой есть)
                agent.GetComponent<Animator>().SetTrigger("equip");
            }*/
        }
        else if (weapon)
        {
            agent.weapons.SetFiring(true);
        }
    }

    public void Enter(AiAgent agent)
    {
        agent.weapons.ActivateWeapon();
        agent.swords.ActivateWeapon();

        if (agent.weapons.currentSword != null)
        {
            agent.navMeshAgent.stoppingDistance = 1.5f;
        }
        else if (agent.swords.currentSword != null)
        {
            agent.navMeshAgent.stoppingDistance = 0.5f;
        }
        else
        {
            agent.navMeshAgent.stoppingDistance = agent.config.attackStoppingDistance;
        }

        agent.navMeshAgent.speed = agent.config.attackSpeed;
    }

    public void Exit(AiAgent agent) {
        agent.weapons.DeactivateWeapon();
        agent.navMeshAgent.stoppingDistance = 0.0f;
    }

    void ReloadWeapon(AiAgent agent) {
        var weapon = agent.weapons.currentWeapon;
        if (weapon && weapon.ShouldReload()) {
            agent.weapons.ReloadWeapon();
        }
    }

    void SelectWeapon(AiAgent agent) {
        var bestWeapon = ChooseWeapon(agent);
        if (bestWeapon != agent.weapons.currentWeaponSlot) {
            agent.weapons.SwitchWeapon(bestWeapon);
        }
        if (bestWeapon != agent.swords.currentWeaponSlot)
        {
            agent.weapons.SwitchWeapon(bestWeapon);
        }
    }

    AiWeapons.WeaponSlot ChooseWeapon(AiAgent agent) {
        float distance = agent.targeting.TargetDistance;
        if (distance > agent.config.attackCloseRange) {
            return AiWeapons.WeaponSlot.Primary;
        } else {
            return AiWeapons.WeaponSlot.Secondary;
        }
    }

    void UpdateLowHealth(AiAgent agent) {
        if (agent.health.IsLowHealth()) {
            agent.stateMachine.ChangeState(AiStateId.FindHealth);
        }
    }

    void UpdateLowAmmo(AiAgent agent) {
        if (agent.weapons.IsLowAmmo()) {
            agent.stateMachine.ChangeState(AiStateId.FindAmmo);
        }
    }
}
