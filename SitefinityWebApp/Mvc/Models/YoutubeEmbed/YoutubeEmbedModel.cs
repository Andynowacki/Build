using System;

namespace SitefinityWebApp.Mvc.Models.YoutubeEmbed
{
  public class YoutubeEmbedModel
  {
    public string ID { get; set; }

    public string GetVideoThumbnail()
    {
      return $"https://img.youtube.com/vi/{this.ID}/hqdefault.jpg";
    }
  }
}
