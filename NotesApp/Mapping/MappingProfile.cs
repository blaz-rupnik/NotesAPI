using AutoMapper;
using NotesApp.Controllers.Resources;
using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {          
            CreateMap<Folder, FolderResource>();
            CreateMap<FolderResource, Folder>();

            CreateMap<Note, NoteResource>();
            CreateMap<NoteResource, Note>();

            CreateMap<User, UserResource>();
            CreateMap<UserAuthenticateResource, User>();
        }
    }
}
