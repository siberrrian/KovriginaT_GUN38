namespace SaveData
{
    public interface ISaveDataService<T>
    {
        void Load(T player);
        void Save(T player);
    }

}

