public class EnemyDownEvent : UnlockDayEvent
{
    Combatant combatant;
    
    void Awake()
    {
        combatant = this.GetComponent<Combatant>();
        
    }

    void OnEnable()
    {
        combatant.enemyDefeated += UnlockNewEnemy;
    }

    private void UnlockNewEnemy()
    {
        Unlock();
        combatant.enemyDefeated += UnlockNewEnemy; //need to unsubscribe here;
    }
    
}
