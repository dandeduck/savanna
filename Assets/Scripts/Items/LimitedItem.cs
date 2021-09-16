public abstract class LimitedItem : Item
{
    public override void Use()
    {
        WhenUsed();

        ReduceAmount(1);
    }

    protected abstract void WhenUsed();
}
