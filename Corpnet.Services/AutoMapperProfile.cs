using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Corpnet.Entities;

namespace Corpnet.Services
{
   public class AutoMapperProfile: Profile
    {

		public AutoMapperProfile()
		{
			CreateMap<FavouriteDto, Favourite>();
			CreateMap<DirectoryDto, Directory>();
			CreateMap<DocumentDto, Document>();
			CreateMap<GenericPageDto, GenericPage>();
			CreateMap<RecentLinksDto, RecentLinks>();
			CreateMap<AdminUsersDto, AdminUsers>();
			CreateMap<AdminDirectoryAccessDto, AdminDirectoryAccess>();
		}
	}
}
