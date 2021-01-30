using Items;

namespace UIComponents
{
    public interface IItemStorage
    {
        bool Contains(Item item);
        bool TakeItem(Item item);
        bool GiveItem(Item item);
        bool SpaceAvailable();
        int CountItem(Item item);
    }
}
