using UnityEngine;

public abstract class UnlockDayEvent : MonoBehaviour
{
    [Header("Insira abaixo o dia para ser destravado:")]
    [SerializeField] protected int unlockableDay;

    protected void Unlock()
    {
        DaysManager.instance.UnlockDay(unlockableDay);
    }
}
