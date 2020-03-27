using System.IO;
using System.Threading.Tasks;

namespace UploadImagesSample.Services
{
    public interface IBlobServices
    {
        Task<string> Upload(string filePath);
    }
}