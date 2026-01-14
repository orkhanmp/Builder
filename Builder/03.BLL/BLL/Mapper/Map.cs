using AutoMapper;
using Entities.DTOs.ContentDTO.AboutDTOs;
using Entities.DTOs.ContentDTO.BlogCategoryDTOs;
using Entities.DTOs.ContentDTO.BlogPostDTOs;
using Entities.DTOs.ContentDTO.ContactSubmissionDTOs;
using Entities.DTOs.ContentDTO.HomeSliderDTOs;
using Entities.DTOs.ContentDTO.PageBannerDTOs;
using Entities.DTOs.ContentDTO.ProjectCategoryDTOs;
using Entities.DTOs.ContentDTO.ProjectDTOs;
using Entities.DTOs.ContentDTO.ServiceCategoryDTOs;
using Entities.DTOs.ContentDTO.ServiceDTOs;
using Entities.DTOs.ContentDTO.SiteSettingsDTOs;
using Entities.DTOs.ContentDTO.TeamCategoryDTOs;
using Entities.DTOs.ContentDTO.TeamMamberDTOs;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<About, AboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();

            CreateMap<HomeSlider, HomeSliderDto>().ReverseMap();
            CreateMap<HomeSlider, CreateHomeSliderDto>().ReverseMap();
            CreateMap<HomeSlider, UpdateHomeSliderDto>().ReverseMap();

            CreateMap<ServiceCategory, ServiceCategoryDto>()
                .ForMember(dest => dest.ServiceCount, opt => opt.MapFrom(src => src.Services != null ? src.Services.Count : 0))
                .ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryDetailDto>().ReverseMap();
            CreateMap<ServiceCategory, CreateServiceCategoryDto>().ReverseMap();
            CreateMap<ServiceCategory, UpdateServiceCategoryDto>().ReverseMap();

            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.ServiceCategoryName, opt => opt.MapFrom(src => src.ServiceCategory != null ? src.ServiceCategory.Name : null))
                .ReverseMap();
            CreateMap<Service, ServiceDetailDto>()
                .ForMember(dest => dest.ServiceCategoryName, opt => opt.MapFrom(src => src.ServiceCategory != null ? src.ServiceCategory.Name : null))
                .ReverseMap();
            CreateMap<Service, CreateServiceDto>().ReverseMap();
            CreateMap<Service, UpdateServiceDto>().ReverseMap();

            CreateMap<TeamCategory, TeamCategoryDto>()
                .ForMember(dest => dest.MemberCount, opt => opt.MapFrom(src => src.TeamMembers != null ? src.TeamMembers.Count : 0))
                .ReverseMap();
            CreateMap<TeamCategory, TeamCategoryDetailDto>().ReverseMap();
            CreateMap<TeamCategory, CreateTeamCategoryDto>().ReverseMap();
            CreateMap<TeamCategory, UpdateTeamCategoryDto>().ReverseMap();

            CreateMap<TeamMember, TeamMemberDto>()
                .ForMember(dest => dest.TeamCategoryName, opt => opt.MapFrom(src => src.TeamCategory != null ? src.TeamCategory.Name : null))
                .ReverseMap();
            CreateMap<TeamMember, TeamMemberDetailDto>()
                .ForMember(dest => dest.TeamCategoryName, opt => opt.MapFrom(src => src.TeamCategory != null ? src.TeamCategory.Name : null))
                .ReverseMap();
            CreateMap<TeamMember, CreateTeamMemberDto>().ReverseMap();
            CreateMap<TeamMember, UpdateTeamMemberDto>().ReverseMap();

            CreateMap<ProjectCategory, ProjectCategoryDto>()
                .ForMember(dest => dest.ProjectCount, opt => opt.MapFrom(src => src.Projects != null ? src.Projects.Count : 0))
                .ReverseMap();
            CreateMap<ProjectCategory, ProjectCategoryDetailDto>().ReverseMap();
            CreateMap<ProjectCategory, CreateProjectCategoryDto>().ReverseMap();
            CreateMap<ProjectCategory, UpdateProjectCategoryDto>().ReverseMap();

            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.ProjectCategoryName, opt => opt.MapFrom(src => src.ProjectCategory != null ? src.ProjectCategory.Name : null))
                .ReverseMap();

            CreateMap<Project, ProjectDetailDto>()
                .ForMember(dest => dest.ProjectCategoryName, opt => opt.MapFrom(src => src.ProjectCategory.Name))
                .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.GalleryImagesJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(src.GalleryImagesJson, (JsonSerializerOptions)null))).ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();

            CreateMap<BlogCategory, BlogCategoryDto>()
                .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.BlogPosts != null ? src.BlogPosts.Count : 0))
                .ReverseMap();
            CreateMap<BlogCategory, BlogCategoryDetailDto>().ReverseMap();
            CreateMap<BlogCategory, CreateBlogCategoryDto>().ReverseMap();
            CreateMap<BlogCategory, UpdateBlogCategoryDto>().ReverseMap();

            CreateMap<BlogPost, BlogPostDto>()
                .ForMember(dest => dest.BlogCategoryName, opt => opt.MapFrom(src => src.BlogCategory != null ? src.BlogCategory.Name : null))
                .ForMember(dest => dest.TagList, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.Tags)
                        ? src.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList()
                        : new List<string>()))
                .ReverseMap();
            CreateMap<BlogPost, BlogPostDetailDto>()
                .ForMember(dest => dest.BlogCategoryName, opt => opt.MapFrom(src => src.BlogCategory != null ? src.BlogCategory.Name : null))
                .ForMember(dest => dest.TagList, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.Tags)
                        ? src.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList()
                        : new List<string>()))
                .ReverseMap();
            CreateMap<BlogPost, CreateBlogPostDto>().ReverseMap();
            CreateMap<BlogPost, UpdateBlogPostDto>().ReverseMap();

            CreateMap<ContactSubmission, ContactSubmissionDto>().ReverseMap();
            CreateMap<ContactSubmission, CreateContactSubmissionDto>().ReverseMap();
            CreateMap<ContactSubmission, UpdateContactSubmissionDto>().ReverseMap();

            CreateMap<SiteSettings, SiteSettingsDto>().ReverseMap();
            CreateMap<SiteSettings, UpdateSiteSettingsDto>().ReverseMap();

            CreateMap<PageBanner, PageBannerDto>().ReverseMap();
            CreateMap<PageBanner, CreatePageBannerDto>().ReverseMap();
            CreateMap<PageBanner, UpdatePageBannerDto>().ReverseMap();
        }
    }
}
