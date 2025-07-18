using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Bomb : MonsterCtrl
{
   
    public override void SetDamage(float damage)              
    {
        if (damage > 0 && m_state != AIState.Attack)
        {
            // 맞은 후 밀리는 효과
            Vector3 from = transform.position; //현재 위치에서
            Vector3 dir = (transform.position - m_player.transform.position); //맞은 방향(player direction)
            dir.y = 0f;
            Vector3 to = from + dir.normalized * 0.2f;//맞은 거리
            float duration = 0.2f;
            m_moveTween.Play(from, to, duration, false);

            SetState(AIState.Attack);
            m_monAniCtrl.Play(MonsterAniCtrl.Motion.Attack);
        }
    }
    protected override void AnimEvent_AttackFinished()
    {
        SetDie();
    }
    protected override void SetDie()
    {
        gameObject.transform.position = Vector3.zero;
        m_monAniCtrl.Play(MonsterAniCtrl.Motion.Idle);
        MonsterManager.Instance.RemoveMonster(this, m_hudCtrl);
    }
    protected override void Start()
    {
        base.Start();
    }
}
