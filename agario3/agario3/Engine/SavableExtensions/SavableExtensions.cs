public static class SavableExtensions
{
    public static (string, string, string) GetSavableItem(this ISavable savable,string name)
    {
        foreach (var item in savable.savableItems)
        {
            if (item.Item2 == name)
            {
                return item;
            }
        }
        return (null, null, null);
    }

    
}