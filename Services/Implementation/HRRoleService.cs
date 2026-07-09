using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTestMvc.Models.Setup;
using Web.API.Data;
using Web.API.DTOs;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class HRRoleService : IHRRoleService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HRRoleService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HRRoleDto>> GetAllRolesAsync()
        {
            var roles = await _context.HRRole.ToListAsync();

            return _mapper.Map<List<HRRoleDto>>(roles);
        }

        public async Task<HRRoleDto> GetRoleByIdAsync(int roleId)
        {
            var role = await _context.HRRole.FindAsync((long)roleId);

            if (role == null)
                return null!;

            return _mapper.Map<HRRoleDto>(role);
        }

        public async Task<HRRoleDto> CreateRoleAsync(HRRoleDto roleDto)
        {
            var role = _mapper.Map<HRRole>(roleDto);

            await _context.HRRole.AddAsync(role);
            await _context.SaveChangesAsync();

            return _mapper.Map<HRRoleDto>(role);
        }

        public async Task<HRRoleDto> UpdateRoleAsync(HRRoleDto roleDto)
        {
            var role = await _context.HRRole.FindAsync(roleDto.Id);

            if (role == null)
                return null!;

            _mapper.Map(roleDto, role);

            _context.HRRole.Update(role);
            await _context.SaveChangesAsync();

            return _mapper.Map<HRRoleDto>(role);
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            var role = await _context.HRRole.FindAsync((long)roleId);

            if (role == null)
                return false;

            _context.HRRole.Remove(role);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}