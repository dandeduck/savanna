using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int size;

    private Item[] items;
    
    private void Awake()
    {
        items = new Item[size];

        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }

    public int Size()
    {
        return size;
    }

    public Item[] Items()
    {
        return items;
    }

    public Item GetItem(string type)
    {
        int index = ItemIndex(type);

        if (index != -1)
            return items[index];
        
        return null;
    }

    public bool Pickup(Item item)
    {
        int index = ItemIndex(item);

        if (index != -1)
        {
            items[index].Merge(item);

            return true;
        }

        else
        {
            index = FirstOpenIndex();

            if (index != -1)
            {
                items[index] = item.Pickup();

                return true;
            }
        }

        return false;
    }

    public Item Drop(Item item)
    {
        return Drop(item, item.Amount());
    }

    public Item Drop(Item item, int amountDropped)
    {
        return Drop(item.Type(), amountDropped);
    }

    public Item Drop(string type, int amountDropped)
    {
        int index = ItemIndex(type);

        if (index == -1)
            return null;

        Item item = items[index];
        Item dropped = item.Drop(amountDropped, transform.position);

        return dropped;
    }

    public bool Consume(Item item)
    {
        return Consume(item, item.Amount());
    }

    public bool Consume(Item item, int amount)
    {
        return Consume(item.Type(), amount);
    }

    public bool Consume(string type, int amount)
    {
        int index = ItemIndex(type);

        if (index == -1)
            return false;

        Item item = items[index];

        if (amount > item.Amount())
            return false;

        item.ReduceAmount(amount);
        
        return true;
    }

    public bool ContainsItem(Item item)
    {
        return ContainsItem(item.Type());
    }

    public bool ContainsItem(string type)
    {
        return ItemIndex(type) != -1;
    }


    public bool ContainsAtLeast(Item item)
    {
        return ContainsAtLeast(item, item.Amount());
    }

    public bool ContainsAtLeast(Item item, int amount)
    {
        return ContainsAtLeast(item.Type(), amount);
    }

    public bool ContainsAtLeast(string type, int amount)
    {
        int index = ItemIndex(type);

        if (index != -1)
            return items[index].Amount() >= amount;

        return false;
    }

    public bool Craft(Recipe recipe, int amount)
    {
        Item crafted = recipe.Craft(this, amount);

        if (crafted == null)
            return false;

        if (!Pickup(crafted))
            Drop(crafted);
        
        return true;
    }

    protected virtual void OnAwake()  {}
    protected virtual void OnStart() {}
    protected virtual void OnUpdate() {}

    private int ItemIndex(Item item)
    {
        return ItemIndex(item.Type());
    }

    private int ItemIndex(string type)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] != null && items[i].Type() == type)
                return i;
        }

        return -1;
    }

    private bool IsFull()
    {
        return FirstOpenIndex() != -1;
    }

    private int FirstOpenIndex()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                return i;
        }

        return -1;
    }
}
