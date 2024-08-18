using System.ComponentModel.DataAnnotations;

namespace api
{
    public class UrlDto
    {
       
            [Key]
            public int Id { get; set; }
            public string? Url { get; set; }
            public int time { get; set; }
            public string? Img { get; set; }
            public int? Priority { get; set; }
        
    }
}
