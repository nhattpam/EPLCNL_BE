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

namespace Service.PaymentMethodsService
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public PaymentMethodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PaymentMethodResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<PaymentMethod>().GetAll()
                                            .ProjectTo<PaymentMethodResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<PaymentMethodResponse> Get(Guid id)
        {
            try
            {
                PaymentMethod paymentMethod = null;
                paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetAll()
                     .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (paymentMethod == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<PaymentMethod, PaymentMethodResponse>(paymentMethod);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PaymentMethodResponse> Create(PaymentMethodRequest request)
        {
            try
            {
                var paymentMethod = _mapper.Map<PaymentMethodRequest, PaymentMethod>(request);
                paymentMethod.Id = Guid.NewGuid();
                await _unitOfWork.Repository<PaymentMethod>().InsertAsync(paymentMethod);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<PaymentMethod, PaymentMethodResponse>(paymentMethod);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PaymentMethodResponse> Delete(Guid id)
        {
            try
            {
                PaymentMethod paymentMethod = null;
                paymentMethod = _unitOfWork.Repository<PaymentMethod>()
                    .Find(p => p.Id == id);
                if (paymentMethod == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<PaymentMethod>().HardDeleteGuid(paymentMethod.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<PaymentMethod, PaymentMethodResponse>(paymentMethod);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaymentMethodResponse> Update(Guid id, PaymentMethodRequest request)
        {
            try
            {
                PaymentMethod paymentMethod = _unitOfWork.Repository<PaymentMethod>()
                            .Find(x => x.Id == id);
                if (paymentMethod == null)
                {
                    throw new Exception();
                }
                paymentMethod = _mapper.Map(request, paymentMethod);

                await _unitOfWork.Repository<PaymentMethod>().UpdateDetached(paymentMethod);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<PaymentMethod, PaymentMethodResponse>(paymentMethod);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
