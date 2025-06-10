using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Roles;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly PersonsDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public RolesService(
            RoleManager<RoleEntity> roleManager,
            PersonsDbContext context,
            IConfiguration configuration,
            IMapper mapper
        )
        {
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            PAGE_SIZE = _configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = _configuration.GetValue<int>("PageSizeLimit");

        }

        public async Task<ResponseDto<PaginationDto<List<RoleDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 10
        )
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;

            int startIndex= (page - 1) * pageSize;

            IQueryable<RoleEntity> rolesQuery = _context.Roles;

            if (!string.IsNullOrEmpty(searchTerm)) 
            {
                rolesQuery = rolesQuery
                    .Where(r => (r.Name + " " + r.Description)
                    .Contains(searchTerm));
            }

            int totalRows = await rolesQuery.CountAsync();



            throw new NotImplementedException();
        }
    }
}
