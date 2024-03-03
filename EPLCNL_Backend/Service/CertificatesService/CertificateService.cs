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

namespace Service.CertificatesService
{
    public class CertificateService: ICertificateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CertificateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CertificateResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Certificate>().GetAll()
                                            .ProjectTo<CertificateResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<CertificateResponse> Get(Guid id)
        {
            try
            {
                Certificate certificate = null;
                certificate = await _unitOfWork.Repository<Certificate>().GetAll()
                     .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (certificate == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Certificate, CertificateResponse>(certificate);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CertificateResponse> Create(CertificateRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var certificate = _mapper.Map<CertificateRequest, Certificate>(request);
                certificate.Id = Guid.NewGuid();
                certificate.CreatedDate = localTime;
                await _unitOfWork.Repository<Certificate>().InsertAsync(certificate);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Certificate, CertificateResponse>(certificate);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CertificateResponse> Delete(Guid id)
        {
            try
            {
                Certificate certificate = null;
                certificate = _unitOfWork.Repository<Certificate>()
                    .Find(p => p.Id == id);
                if (certificate == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Certificate>().HardDeleteGuid(certificate.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Certificate, CertificateResponse>(certificate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CertificateResponse> Update(Guid id, CertificateRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Certificate certificate = _unitOfWork.Repository<Certificate>()
                            .Find(x => x.Id == id);
                if (certificate == null)
                {
                    throw new Exception();
                }
                certificate.UpdatedDate = localTime;
                certificate = _mapper.Map(request, certificate);

                await _unitOfWork.Repository<Certificate>().UpdateDetached(certificate);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Certificate, CertificateResponse>(certificate);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
