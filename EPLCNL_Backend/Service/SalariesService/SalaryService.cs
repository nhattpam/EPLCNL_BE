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

namespace Service.SalariesService
{
    public class SalaryService : ISalaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public SalaryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SalaryResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Salary>().GetAll()
                                            .ProjectTo<SalaryResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<SalaryResponse> Get(Guid? id)
        {
            try
            {
                Salary salary = null;
                salary = await _unitOfWork.Repository<Salary>().GetAll()
                     .AsNoTracking()
                        .Include(a => a.Account)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (salary == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Salary, SalaryResponse>(salary);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SalaryResponse> Create(SalaryRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {

                var salary = _mapper.Map<SalaryRequest, Salary>(request);
                salary.Id = Guid.NewGuid();
                salary.PaymentDate = localTime;
                await _unitOfWork.Repository<Salary>().InsertAsync(salary);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Salary, SalaryResponse>(salary);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SalaryResponse> Delete(Guid id)
        {
            try
            {
                Salary salary = null;
                salary = _unitOfWork.Repository<Salary>()
                    .Find(p => p.Id == id);
                if (salary == null)
                {
                    throw new Exception("Id is not existed");
                }
                await _unitOfWork.Repository<Salary>().HardDeleteGuid(salary.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Salary, SalaryResponse>(salary);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SalaryResponse> Update(Guid id, SalaryRequest request)
        {
            try
            {
                Salary salary = _unitOfWork.Repository<Salary>()
                            .Find(x => x.Id == id);
                if (salary == null)
                {
                    throw new Exception();
                }
                salary = _mapper.Map(request, salary);

                await _unitOfWork.Repository<Salary>().UpdateDetached(salary);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Salary, SalaryResponse>(salary);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
