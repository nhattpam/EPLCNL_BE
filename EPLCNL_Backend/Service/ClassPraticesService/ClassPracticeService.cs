using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.ClassPraticesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ClassPracticesService
{
    public class ClassPracticeService : IClassPracticeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ClassPracticeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClassPracticeResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<ClassPractice>().GetAll()
                                            .ProjectTo<ClassPracticeResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ClassPracticeResponse> Get(Guid id)
        {
            try
            {
                ClassPractice classPractice = null;
                classPractice = await _unitOfWork.Repository<ClassPractice>().GetAll()
                     .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (classPractice == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<ClassPractice, ClassPracticeResponse>(classPractice);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassPracticeResponse> Create(ClassPracticeRequest request)
        {
            try
            {
                var classPractice = _mapper.Map<ClassPracticeRequest, ClassPractice>(request);
                classPractice.Id = Guid.NewGuid();
                await _unitOfWork.Repository<ClassPractice>().InsertAsync(classPractice);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassPractice, ClassPracticeResponse>(classPractice);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassPracticeResponse> Delete(Guid id)
        {
            try
            {
                ClassPractice classPractice = null;
                classPractice = _unitOfWork.Repository<ClassPractice>()
                    .Find(p => p.Id == id);
                if (classPractice == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ClassPractice>().HardDeleteGuid(classPractice.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ClassPractice, ClassPracticeResponse>(classPractice);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClassPracticeResponse> Update(Guid id, ClassPracticeRequest request)
        {
            try
            {
                ClassPractice classPractice = _unitOfWork.Repository<ClassPractice>()
                            .Find(x => x.Id == id);
                if (classPractice == null)
                {
                    throw new Exception();
                }
                classPractice = _mapper.Map(request, classPractice);

                await _unitOfWork.Repository<ClassPractice>().UpdateDetached(classPractice);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassPractice, ClassPracticeResponse>(classPractice);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
