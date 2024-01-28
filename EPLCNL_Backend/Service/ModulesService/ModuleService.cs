using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ModulesService
{
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ModuleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ModuleResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Module>().GetAll()
                                            .ProjectTo<ModuleResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ModuleResponse> Get(Guid id)
        {
            try
            {
                Module module = await _unitOfWork.Repository<Module>()
                    .GetAll()
                    .Include(a => a.Lessons)
                    .Include(a => a.Quizzes)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (module == null)
                {
                    throw new Exception("Module not found");
                }

                // Additional checks or processing as needed

                return _mapper.Map<Module, ModuleResponse>(module);
            }
            catch (Exception e)
            {
                throw new Exception($"Error retrieving module: {e.Message}");
            }
        }


        public async Task<ModuleResponse> Create(ModuleRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var module = _mapper.Map<ModuleRequest, Module>(request);
                module.Id = Guid.NewGuid();
                module.CreatedDate = localTime;
                await _unitOfWork.Repository<Module>().InsertAsync(module);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Module, ModuleResponse>(module);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ModuleResponse> Delete(Guid id)
        {
            try
            {
                Module module = null;
                module = _unitOfWork.Repository<Module>()
                    .Find(p => p.Id == id);
                if (module == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Module>().HardDeleteGuid(module.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Module, ModuleResponse>(module);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModuleResponse> Update(Guid id, ModuleRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Module module = _unitOfWork.Repository<Module>()
                            .Find(x => x.Id == id);
                if (module == null)
                {
                    throw new Exception();
                }
                module = _mapper.Map(request, module);
                module.UpdatedDate = localTime;

                await _unitOfWork.Repository<Module>().UpdateDetached(module);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Module, ModuleResponse>(module);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
