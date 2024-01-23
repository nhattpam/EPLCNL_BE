using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AccountForumsService
{
    public class AccountForumService: IAccountForumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AccountForumService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AccountForumResponse>> GetAccountForums()
        {

            var list = await _unitOfWork.Repository<AccountForum>().GetAll()
                                            .ProjectTo<AccountForumResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<AccountForumResponse> Create(AccountForumRequest request)
        {
            try
            {
                var accountforum = _mapper.Map<AccountForumRequest, AccountForum>(request);
                accountforum.MessagedDate = DateTime.Now;
                await _unitOfWork.Repository<AccountForum>().InsertAsync(accountforum);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<AccountForum, AccountForumResponse>(accountforum);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AccountForumResponse> Delete(Guid learnerid, Guid tutorid)
        {
            try
            {
                AccountForum accountforumlearner = null;
                accountforumlearner = _unitOfWork.Repository<AccountForum>()
                    .Find(p => p.LearnerId == learnerid);
                AccountForum accountforumtutor = null;
                accountforumtutor = _unitOfWork.Repository<AccountForum>()
                    .Find(p => p.TutorId == tutorid);
                if (accountforumlearner != null)
                {
                    await _unitOfWork.Repository<AccountForum>().HardDeleteGuid((Guid)accountforumlearner.LearnerId);
                    await _unitOfWork.CommitAsync();
                    return _mapper.Map<AccountForum, AccountForumResponse>(accountforumlearner);
                }
                else if (accountforumtutor != null)
                {
                    await _unitOfWork.Repository<AccountForum>().HardDeleteGuid((Guid)accountforumtutor.TutorId);
                    await _unitOfWork.CommitAsync();
                    return _mapper.Map<AccountForum, AccountForumResponse>(accountforumtutor);
                }
                else
                {
                    throw new Exception("Id is not existed");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccountForumResponse> Update(Guid learnerid,Guid tutorid, AccountForumRequest request)
        {
            try
            {
                AccountForum accountforumlearner = _unitOfWork.Repository<AccountForum>()
                            .Find(x => x.LearnerId == learnerid);
                AccountForum accountforumtutor = _unitOfWork.Repository<AccountForum>()
                            .Find(x => x.TutorId == tutorid);
                if (accountforumlearner != null)
                {
                    accountforumlearner = _mapper.Map(request, accountforumlearner);
                    accountforumlearner.MessagedDate = DateTime.Now;

                    await _unitOfWork.Repository<AccountForum>().UpdateDetached(accountforumlearner);
                    await _unitOfWork.CommitAsync();

                    return _mapper.Map<AccountForum, AccountForumResponse>(accountforumlearner);
                }
                else if(accountforumtutor != null)
                {
                    accountforumtutor = _mapper.Map(request, accountforumtutor);
                    accountforumtutor.MessagedDate = DateTime.Now;

                    await _unitOfWork.Repository<AccountForum>().UpdateDetached(accountforumtutor);
                    await _unitOfWork.CommitAsync();

                    return _mapper.Map<AccountForum, AccountForumResponse>(accountforumtutor);
                }
                else
                {
                    throw new Exception();
                }
               
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
