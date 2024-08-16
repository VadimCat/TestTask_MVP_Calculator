namespace Save
{
    public interface IAppStateSave
    {
        public void SaveValue(string key, object save);
        public TType GetValue<TType>(string key, TType defaultValue = default);
        public void Load();
    }
}