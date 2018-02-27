namespace Tasks.Repository.Thoughts
{
    public interface IThoughtRepository
    {
        void UpdateSortId(int thoughtId, int moveToId);
    }
}