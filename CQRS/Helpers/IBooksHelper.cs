using Model.DTOs;
using Model.Models;

namespace CQRS.Helpers
{
    public interface IBooksHelper
    {
        GetBookDTO ExtractBookDTO(Book book);
    }
}
