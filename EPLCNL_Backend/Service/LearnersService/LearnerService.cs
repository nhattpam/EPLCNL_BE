﻿using AutoMapper;
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

namespace Service.LearnersService
{
    public class LearnerService : ILearnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public LearnerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LearnerResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Learner>().GetAll()
                                            .ProjectTo<LearnerResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<LearnerResponse> Get(Guid? id)
        {
            try
            {
                Learner learner = null;
                learner = await _unitOfWork.Repository<Learner>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Account)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (learner == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Learner, LearnerResponse>(learner);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LearnerResponse> Create(LearnerRequest request)
        {
            try
            {
                var learner = _mapper.Map<LearnerRequest, Learner>(request);
                learner.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Learner>().InsertAsync(learner);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Learner, LearnerResponse>(learner);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LearnerResponse> Delete(Guid id)
        {
            try
            {
                Learner learner = null;
                learner = _unitOfWork.Repository<Learner>()
                    .Find(p => p.Id == id);
                if (learner == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Learner>().HardDeleteGuid(learner.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Learner, LearnerResponse>(learner);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LearnerResponse> Update(Guid id, LearnerRequest request)
        {
            try
            {
                Learner learner = _unitOfWork.Repository<Learner>()
                            .Find(x => x.Id == id);
                if (learner == null)
                {
                    throw new Exception();
                }
                learner = _mapper.Map(request, learner);

                await _unitOfWork.Repository<Learner>().UpdateDetached(learner);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Learner, LearnerResponse>(learner);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<ForumResponse>> GetAllForumsByLearner(Guid id)
        {
            // Retrieve the learner
            var learner = await _unitOfWork.Repository<Learner>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (learner == null)
            {
                // Handle the case where the learner with the specified id is not found
                return null;
            }

            // Retrieve enrollments for the learner
            var enrollments = _unitOfWork.Repository<Enrollment>().GetAll()
                .Where(t => t.LearnerId == id)
                .ToList();

            // Retrieve forums related to the learner's enrollments
            var forumResponses = new List<ForumResponse>();
            foreach (var enrollment in enrollments)
            {
                var forums = _unitOfWork.Repository<Forum>().GetAll()
                    .Where(forum => forum.CourseId == enrollment.CourseId)
                    .ProjectTo<ForumResponse>(_mapper.ConfigurationProvider)
                    .ToList();

                forumResponses.AddRange(forums);
            }

            return forumResponses;
        }


        public async Task<List<EnrollmentResponse>> GetAllEnrollmentsByLearner(Guid id)
        {
            // Retrieve the learner
            var learner = await _unitOfWork.Repository<Learner>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (learner == null)
            {
                // Handle the case where the learner with the specified id is not found
                return null;
            }

            // Retrieve enrollments for the learner
            var enrollments = await _unitOfWork.Repository<Enrollment>().GetAll()
                .Where(t => t.LearnerId == id)
                .ProjectTo<EnrollmentResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();

            return enrollments;
        }

        public async Task<List<TransactionResponse>> GetAllTransactionsByLearner(Guid id)
        {
            try
            {
                var transactions = await _unitOfWork.Repository<Transaction>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.PaymentMethod)
                     .Include(x => x.Learner)
                     .ThenInclude(x => x.Account)
                     .Include(x => x.Course)
                    .Where(x => x.LearnerId == id)
                    .ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (transactions.Count == 0)
                {
                    throw new Exception("khong tim thay");
                }

                return transactions;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
