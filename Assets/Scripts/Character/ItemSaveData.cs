namespace Character
{
    [System.Serializable]
    public class ItemSaveData : SaveDataBase
    {
        public int Amount;

        public ItemSaveData(ItemScriptables item)
        {
            Name = item.Name;
            Amount = item.Amount;
        }
    }
}
