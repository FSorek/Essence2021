using System.Collections.Generic;
using System.Linq;

public class Attack : IState
{
    private readonly FireEssenceAttack attack;

    public Attack(FireEssenceAttack attack)
    {
        this.attack = attack;
    }
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        attack.enabled = true;
    }

    public void OnExit()
    {
        attack.enabled = false;
    }
}