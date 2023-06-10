public interface ISavable
{
    public string pathToSavedFile { get; protected set; }
    public string pathToDefaultFile { get; protected set; }
    public List<(string, string, string)> savableItems { get; protected set; }
    
    protected void RemoveSavableItem(string name);

    public void Save();
}