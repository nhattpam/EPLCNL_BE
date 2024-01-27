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

namespace Service.ForumsService
{
    public class ForumService : IForumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ForumService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ForumResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Forum>().GetAll()
                                            .ProjectTo<ForumResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ForumResponse> Get(Guid id)
        {
            try
            {
                Forum forum = null;
                forum = await _unitOfWork.Repository<Forum>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (forum == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Forum, ForumResponse>(forum);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ForumResponse> Create(ForumRequest request)
        {
            try
            {
                var forum = _mapper.Map<ForumRequest, Forum>(request);
                forum.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Forum>().InsertAsync(forum);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Forum, ForumResponse>(forum);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ForumResponse> Delete(Guid id)
        {
            try
            {
                Forum forum = null;
                forum = _unitOfWork.Repository<Forum>()
                    .Find(p => p.Id == id);
                if (forum == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Forum>().HardDeleteGuid(forum.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Forum, ForumResponse>(forum);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ForumResponse> Update(Guid id, ForumRequest request)
        {
            try
            {
                Forum forum = _unitOfWork.Repository<Forum>()
                            .Find(x => x.Id == id);
                if (forum == null)
                {
                    throw new Exception();
                }
                forum = _mapper.Map(request, forum);

                await _unitOfWork.Repository<Forum>().UpdateDetached(forum);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Forum, ForumResponse>(forum);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
