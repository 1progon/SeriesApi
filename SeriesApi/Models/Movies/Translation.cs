using SeriesApi.Enums.Movies;

namespace SeriesApi.Models.Movies;

public class Translation : BaseModel
{
    public int KodikTranslationId { get; set; }
    public TranslationType Type { get; set; }
}