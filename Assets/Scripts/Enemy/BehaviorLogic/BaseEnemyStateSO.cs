using System.Collections;
using UnityEngine;
public class BaseEnemyStateSO : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform player;

    protected float stateTimer;
    protected bool triggerCalled;

    public virtual void Init(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        player = PlayerManager.instance.player.transform;
    }

    public virtual void DoEnter()
    {
        triggerCalled = false;
    }

    public virtual void DoExit()
    {
        ResetValues();
    }

    public virtual void DoUpdate()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void DoAniamtionFinishTrigger()
    {
        triggerCalled = true;
    }

    public virtual void ResetValues()
    {
        triggerCalled = false;
    }

    public IEnumerator DestroyAfter(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        Destroy(gameObject);
    }
}