namespace Tavel_Tracker_API.Interfaces
{
    public interface IUntOfWork
    {
        IUserRepository UserRepository { get; }
        ILocationRepository LocationRepository { get; }
    }
}
