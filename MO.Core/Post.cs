﻿using MO.Core.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Xml.Linq;


namespace MO.Core
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(160, ErrorMessage = "Maximum 160 character limit")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(512, ErrorMessage = "Maximum 512 character limit")]
        public string Excerpt { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = false;

        public DateTime PubDate { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;        

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Slug { get; set; } = string.Empty;
        [MaxLength(256, ErrorMessage = "Maximum 256 character limit")]
        public string PostCoverPhoto { get; set; } = String.Empty;
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostTag> Tags { get; set; }
        public ICollection<PostMedia> Media { get; set; }

        public bool PostHasCoverPhoto()
        {
            return (!string.IsNullOrEmpty(PostCoverPhoto));
        }        
    }
}