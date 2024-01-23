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

namespace Service.ClassTopicsService
{
    public class ClassTopicService : IClassTopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ClassTopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClassTopicResponse>> GetClassTopics()
        {

            var list = await _unitOfWork.Repository<ClassTopic>().GetAll()
                                            .ProjectTo<ClassTopicResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ClassTopicResponse> Create(ClassTopicRequest request)
        {
            try
            {
                var classTopic = _mapper.Map<ClassTopicRequest, ClassTopic>(request);
                classTopic.Id = Guid.NewGuid();
                await _unitOfWork.Repository<ClassTopic>().InsertAsync(classTopic);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassTopic, ClassTopicResponse>(classTopic);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassTopicResponse> Delete(Guid id)
        {
            try
            {
                ClassTopic classTopic = null;
                classTopic = _unitOfWork.Repository<ClassTopic>()
                    .Find(p => p.Id == id);
                if (classTopic == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ClassTopic>().HardDeleteGuid(classTopic.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ClassTopic, ClassTopicResponse>(classTopic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClassTopicResponse> Update(Guid id, ClassTopicRequest request)
        {
            try
            {
                ClassTopic classTopic = _unitOfWork.Repository<ClassTopic>()
                            .Find(x => x.Id == id);
                if (classTopic == null)
                {
                    throw new Exception();
                }
                classTopic = _mapper.Map(request, classTopic);

                await _unitOfWork.Repository<ClassTopic>().UpdateDetached(classTopic);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassTopic, ClassTopicResponse>(classTopic);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
