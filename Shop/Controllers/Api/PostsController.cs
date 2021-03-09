using AutoMapper;
using Shop.Dtos;
using Shop.Models;
using Shop.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class PostsController : ApiController
    {
        private ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        // Get /api/posts
        public IEnumerable<PostsDto> GetPosts()
        {

            return _context.Posts.ToList().Select(Mapper.Map<Post, PostsDto>);
        }


        public IHttpActionResult GetProducts(int id)
        {
            var post = _context.Posts.SingleOrDefault(b => b.Id == id);

            if (post == null)
                return NotFound();


            return Ok(Mapper.Map<Post, PostsDto>(post));
        }


        [HttpPost]
        public IHttpActionResult CreateProducts(PostsDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var post = Mapper.Map<PostsDto, Post>(postDto);


            _context.Posts.Add(post);
            _context.SaveChanges();

            postDto.Id = post.Id;

            return Created(new Uri(Request.RequestUri + "/" + post.Id), postDto);
        }
        //PUT /api/Products/1
        [HttpPut]

        public IHttpActionResult UpdateProducts(int id, PostsDto postsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var postsInDb = _context.Posts.SingleOrDefault(p => p.Id == id);

            if (postsInDb == null)
                return NotFound();

            Mapper.Map(postsDto, postsInDb);

            _context.SaveChanges();

            return Ok();
        }
        //DELETE /api/Products/1
        [HttpDelete]
        public IHttpActionResult DeletePosts(int id)
        {
            var productInDb = _context.Posts.SingleOrDefault(p => p.Id == id);
            if (productInDb == null)
                return NotFound();

            _context.Posts.Remove(productInDb);
            _context.SaveChanges();

            return Ok();
        }


    }
}
