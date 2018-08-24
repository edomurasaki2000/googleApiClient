using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace google.youtube
{
   
    public class SearchListResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string regionCode { get; set; }

        public PageInfo pageInfo;
        public class PageInfo
        {
            public int totalResults { get; set; }
            public int resultsPerPage { get; set; }
        }
        public SearchResult[] items;

        public class SearchResult
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public Id id { get; set; }
            public class Id
            {
                public string kind { get; set; }
                public string videoId { get; set; }
            };

            public Snippet snippet { get; set; }
            public class Snippet
            {
                public string publishedAt { get; set; }
                public string channelId { get; set; }
                public string title { get; set; }
                public string description { get; set; }

                public string channelTitle { get; set; }
                public string liveBroadcastContent { get; set; }
            }
        }
    }

    public class SubscriptionListResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public PageInfo pageInfo { get; set; }
        public class PageInfo
        {
            public string stringtotalResults { get; set; }
            public string stringresultsPerPage { get; set; }
        }
        public Subscription[] items { get; set; }
        public class Subscription
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public Snippet snippet { get; set; }
            public class Snippet
            {
                public string publishedAt { get; set; }
                public string title { get; set; }
                public string description { get; set; }
                public ResourceId resourceId { get; set; }
                public class ResourceId
                {
                    public string kind { get; set; }
                    public string channelId { get; set; }
                }
                public string channelId { get; set; }
            }
        }
    }

    public class PlaylistListResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }

        public PageInfo pageInfo { get; set; }
        public class PageInfo
        {
            public string totalResults { get; set; }
            public string resultsPerPage { get; set; }
        }
        public Playlist[] items { get; set; }
        public class Playlist
        {
            public override string ToString()
            {
                return snippet.title;
            }
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public Snippet snippet { get; set; }
            public class Snippet
            {
                public string publishedAt { get; set; }
                public string channelId { get; set; }
                public string title { get; set; }
                public string description { get; set; }

                public string channelTitle { get; set; }
            }
        }
    }
    public class Playlist
    {
       
 public string kind{get; set;}
  public string etag{get; set;}
  public string id{get; set;}
  public Snippet snippet { get; set; }
        public class Snippet {
   public string publishedAt{get; set;}
   public string channelId{get; set;}
   public string title{get; set;}
   public string description{get; set;}
  
   public string channelTitle{get; set;}
 }
    public Status status{get; set;}
    public class Status{
   public string  privacyStatus{get; set;}
 }
}
    public class PlaylistItem
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public Snippet snippet { get; set; }
        public class Snippet
        {
            public string publishedAt { get; set; }
            public string channelId { get; set; }
            public string title { get; set; }
            public string description { get; set; }

            public string channelTitle { get; set; }
            public string playlistId { get; set; }
            public ResourceId resourceId { get; set; }
            public class ResourceId
            {
                public string kind { get; set; }
                public string videoId { get; set; }
            }
        }
    }
    public class PlaylistItemListResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public PageInfo pageInfo { get; set; }
        public class PageInfo
        {
            public int totalResults { get; set; }
            public int resultsPerPage { get; set; }
        }
        public PlaylistItem[] items { get; set; }
        public class PlaylistItem
        {
            public override string ToString()
            {
                return snippet.title;
            }
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public Snippet snippet { get; set; }
            public class Snippet
            {
                public string publishedAt { get; set; }
                public string channelId { get; set; }
                public string title { get; set; }
                public string description { get; set; }

                public string channelTitle { get; set; }
                public string playlistId { get; set; }
                public string position { get; set; }
                public ResourceId resourceId { get; set; }
                public class ResourceId
                {
                    public string kind { get; set; }
                    public string videoId { get; set; }
                }
            }
        }
    }
    /*
    public class PlaylistItemListResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public PageInfo pageInfo { get; set; }
        public class PageInfo
        {
            public string totalResults { get; set; }
            public string resultsPerPage { get; set; }
        }
        public PlaylistItem items { get; set; }

        public class PlaylistItem
        {
            public string kind { get; set; }
            public string etag { get; set; }

            public string id { get; set; }
            public Snippet snippet { get; set; }
            public class Snippet
            {
                public string publishedAt { get; set; }
                public string channelId { get; set; }
                public string title { get; set; }
                public string description { get; set; }
                public Thumbnails thumbnails { get; set; }
                public class Thumbnails
                {
                    [Newtonsoft.Json.JsonProperty("default")]
                    public Thumbnail _default { get; set; }
                    public class Thumbnail
                    {
                        public string url { get; set; }
                        public string width { get; set; }
                        public string height { get; set; }
                    }
                    public Thumbnail medium { get; set; }
                    public Thumbnail high { get; set; }
                }
                public string channelTitle { get; set; }
                public string playlistId { get; set; }
                public string position { get; set; }
                public ResourceId resourceId { get; set; }
                public class ResourceId
                {
                    public string kind { get; set; }
                    public string videoId { get; set; }
                }
            }
        }
    }
   */
    public class Video
        {
 public string kind{get; set;}
  public string  etag{get; set;}
    public string id{get; set;}
 public Snippet snippet{get; set;}
        public class Snippet {
   public string publishedAt{get; set;}
   public string channelId{get; set;}
   public string title{get; set;}
   public string description{get; set;}
        public Thumbnails thumbnails{get; set;}
   public class Thumbnails{
        
       [Newtonsoft.Json.JsonProperty("default")]
  public Thumbnail _default{get; set;} 
       public class Thumbnail{
     public string url{get; set;}
     public string width{get; set;}
     public string height{get; set;}
   }
  public Thumbnail medium {get; set;}
   
  public Thumbnail high{get; set;}
    
   }
   public string  channelTitle{get; set;}
   public string  categoryId{get; set;}
   public string liveBroadcastContent{get; set;}
 public Localized localized{get; set;} 
            public class Localized {
    public string title{get; set;}
    public string description { get; set; }
  }
 }
public Status status{get; set;}
public class Status{
   public string uploadStatus{get; set;}
   public string privacyStatus{get; set;}
   public string license{get; set;}
   public bool embeddable{get; set;}
   public bool publicStatsViewable{get; set;}
 }
}

    public class ChannelListResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public PageInfo pageInfo { get; set; }
        public class PageInfo
        {
            public string totalResults { get; set; }
            public string resultsPerPage { get; set; }
        }
        public Channel[] items { get; set; }
        public class Channel
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public ContentDetails contentDetails { get; set; }
            public class ContentDetails
            {
                public RelatedPlaylists relatedPlaylists { get; set; }
                public class RelatedPlaylists
                {
                    public string likes { get; set; }
                    public string favorites { get; set; }
                    public string uploads { get; set; }
                    public string watchHistory { get; set; }
                    public string watchLater { get; set; }
                }
            }
        }
    }
}