namespace Save
{
    public class SaveComposite: ISave
    {
        private readonly ISave[] _saves;

        public SaveComposite(params ISave[] saves)
        {
            _saves = saves;
        }

        public void Save()
        {
            foreach (var save in _saves)
            {
                save.Save();
            }
        }

        public void Load()
        {
            foreach (var save in _saves)
            {
                save.Load();
            }
        }
    }
}