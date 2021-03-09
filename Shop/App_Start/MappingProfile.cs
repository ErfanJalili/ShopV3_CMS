using AutoMapper;
using Shop.Dtos;
using Shop.Models.Order;
using Shop.Models.Page;
using Shop.Models.Post;
using Shop.Models.Product;
using Shop.Models.Slider;
using Shop.Persistence.Dtos;
using Shop.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Tag, TagDto>();
            Mapper.CreateMap<TagDto, Tag>();

            Mapper.CreateMap<Product, ProductsDto>();
            Mapper.CreateMap<ProductsDto, Product>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<Order, OrdersDto>();
            Mapper.CreateMap<OrdersDto, Order>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<OrderDetail, OrderDetailsDto>();
            Mapper.CreateMap<OrderDetailsDto, OrderDetail>();

            Mapper.CreateMap<OrderCopon, OrderCoponsDto>();
            Mapper.CreateMap<OrderCoponsDto, OrderCopon>();

            Mapper.CreateMap<OrderStatus, OrderStatusesDto>();
            Mapper.CreateMap<OrderStatusesDto, OrderStatus>();

            Mapper.CreateMap<Brand, BrandsDto>();
            Mapper.CreateMap<BrandsDto, Brand>();

            Mapper.CreateMap<Category, CategoriesDto>();
            Mapper.CreateMap<CategoriesDto, Category>();

            Mapper.CreateMap<SKU, SKUsDto>();
            Mapper.CreateMap<SKUsDto,SKU>();

            Mapper.CreateMap<Status, StatusesDto>();
            Mapper.CreateMap<StatusesDto, Status>();

            Mapper.CreateMap<Post, PostsDto>();
            Mapper.CreateMap<PostsDto, Post>();

            Mapper.CreateMap<Page, PagesDto>();
            Mapper.CreateMap<PagesDto, Page>();

            Mapper.CreateMap<Product, HomeIndexDto>();

            Mapper.CreateMap<Slider, SliderDto>();

            Mapper.CreateMap<Customer, CustomerResource>();
            Mapper.CreateMap<CustomerResource, Customer>();

        }
    }
}