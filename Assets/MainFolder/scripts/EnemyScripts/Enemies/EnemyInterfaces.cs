public interface IAttackable
{
    void MeleeAttack();
}

public interface IRangeAttackable
{
    void RangeAttack();
}

public interface IMagicCaster
{
    void CastSpell();
}
public interface IDropable
{
    void DropLoot();
}