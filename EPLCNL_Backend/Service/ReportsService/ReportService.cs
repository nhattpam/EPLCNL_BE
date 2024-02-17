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

namespace Service.ReportsService
{
    public class ReportService: IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ReportResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Report>().GetAll()
                                            .ProjectTo<ReportResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ReportResponse> Get(Guid id)
        {
            try
            {
                Report report = null;
                report = await _unitOfWork.Repository<Report>().GetAll()
                     .AsNoTracking()
                     .Include(a => a.Course)
                     .Include(a => a.Learner)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (report == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Report, ReportResponse>(report);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<ReportResponse> Create(ReportRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var report = _mapper.Map<ReportRequest, Report>(request);
                report.Id = Guid.NewGuid();
                report.ReportedDate = localTime;
                await _unitOfWork.Repository<Report>().InsertAsync(report);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Report, ReportResponse>(report);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ReportResponse> Delete(Guid id)
        {
            try
            {
                Report report = null;
                report = _unitOfWork.Repository<Report>()
                    .Find(p => p.Id == id);
                if (report == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Report>().HardDeleteGuid(report.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Report, ReportResponse>(report);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReportResponse> Update(Guid id, ReportRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Report report = _unitOfWork.Repository<Report>()
                            .Find(x => x.Id == id);
                if (report == null)
                {
                    throw new Exception();
                }
                report = _mapper.Map(request, report);
                report.ReportedDate = localTime;
                await _unitOfWork.Repository<Report>().UpdateDetached(report);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Report, ReportResponse>(report);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
