namespace StudentAdminPortal.API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
