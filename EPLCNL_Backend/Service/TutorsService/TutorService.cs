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

namespace Service.TutorService
{
    public class TutorService: ITutorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public TutorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TutorResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Tutor>().GetAll()
                                            .ProjectTo<TutorResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<TutorResponse> Create(TutorRequest request)
        {
            try
            {
                var tutor = _mapper.Map<TutorRequest, Tutor>(request);
                tutor.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Tutor>().InsertAsync(tutor);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Tutor, TutorResponse>(tutor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TutorResponse> Delete(Guid id)
        {
            try
            {
                Tutor tutor = null;
                tutor = _unitOfWork.Repository<Tutor>()
                    .Find(p => p.Id == id);
                if (tutor == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Tutor>().HardDeleteGuid(tutor.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Tutor, TutorResponse>(tutor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TutorResponse> Update(Guid id, TutorRequest request)
        {
            try
            {
                Tutor tutor = _unitOfWork.Repository<Tutor>()
                            .Find(x => x.Id == id);
                if (tutor == null)
                {
                    throw new Exception();
                }
                tutor = _mapper.Map(request, tutor);

                await _unitOfWork.Repository<Tutor>().UpdateDetached(tutor);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Tutor, TutorResponse>(tutor);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
