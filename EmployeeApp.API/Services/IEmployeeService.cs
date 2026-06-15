using EmployeeApp.API.Dto;

namespace EmployeeApp.API.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<EmployeeDto> AddAsync(EmployeeDto entity);
        Task<EmployeeDto> UpdateAsync(int id, EmployeeDto entity);
        Task<EmployeeDto> DeleteAsync(int id);
    }
}
