public interface ISavable
{
    public string pathToSavedFile { get; protected set; }
    public string pathToDefaultFile { get; protected set; }
    public List<(string, string, string)> savableItems { get; protected set; }
    
    protected void AddSavableItem(string type, string name, string value);
    protected void RemoveSavableItem(string name);
    public (string, string, string) GetSavableItem(string name);
    
}