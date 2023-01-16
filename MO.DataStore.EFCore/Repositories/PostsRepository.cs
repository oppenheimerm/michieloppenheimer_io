﻿
using Microsoft.EntityFrameworkCore;
using MO.UseCases.DataStoreInterfaces;
using MO.UseCases.PostResponse;

namespace MO.DataStore.EFCore.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly MODbContext context;

        public PostsRepository(MODbContext context)
        {
            this.context = context;
        }

        public PostQueryResponse GetAllPosts()
        {
            PostQueryResponse postQueryResponse = new();

            try
            {
                postQueryResponse.PostsEntries = context.Posts.OrderBy(x => x.PubDate).AsQueryable();
                postQueryResponse.Success= true;
                return postQueryResponse;
            }catch(Exception ex)
            {
                postQueryResponse.Success= false;
                postQueryResponse.ErrorMessage= ex.Message;
                return postQueryResponse;
            }
        }

        public PostQueryResponse GetPostsByTag(string tagNameEncoded)
        {
			PostQueryResponse? postQueryResponse = new();

            try
            {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
				postQueryResponse.PostsEntries = context.PostTags.Include(x => x.Post)
                    .Where(t => t.TagNameEncoded == tagNameEncoded)
                    .OrderBy(d => d.Post.PubDate)
                    .Select(y => y.Post)
                    .AsQueryable();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
				postQueryResponse.Success= true;
                return postQueryResponse;
			}
            catch(Exception ex)
            {
                postQueryResponse.Success= false;
                postQueryResponse.ErrorMessage= ex.Message;
                return postQueryResponse;
            }
		}

        public async Task<PostEntryResponse> GetPostBySlug(string slug)
        {
			PostEntryResponse postEntryResponse = new();

            try
            {
                postEntryResponse.PostEntry = await context.Posts
                    .Include(t => t.Tags)
                    .AsNoTracking()
                    .FirstOrDefaultAsync( x => x.Slug == slug);
                postEntryResponse.Success= true;
                return postEntryResponse;
            }catch(Exception ex)
            {
                postEntryResponse.Success= false;
                postEntryResponse.ErrorMessage= ex.Message;
                return postEntryResponse;
            }
        }
    }
}
