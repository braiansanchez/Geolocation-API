namespace GeolocationApp.Services
{
    public interface IGeolocationService<T> where T : class
    {
        public T Get(int id);

        public IEnumerable<T> GetAll();

        public T Add(T entity);

        public T Update(T entity);

        public void Delete(T entity);
    }
}
