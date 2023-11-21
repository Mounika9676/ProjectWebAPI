namespace ProjectWebAPI.Repo
{
    public interface IRepo<T>
    {
        bool Add(T item);
        List<T> GetAll();
        bool Update(int id, T updatedItem);
        bool Delete(int id);
    }
}