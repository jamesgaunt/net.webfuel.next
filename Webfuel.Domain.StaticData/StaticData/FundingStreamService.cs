using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.StaticData
{
    internal class FundingStreamService
    {
        private readonly FundingStreamRepository _fundingStreamRepository;

        public FundingStreamService(
            FundingStreamRepository fundingStreamRepository)
        {
            _fundingStreamRepository = fundingStreamRepository;
        }

        public async Task<FundingStream> InsertFundingStream(FundingStream fundingStream)
        {
            await _fundingStreamRepository.InsertFundingStream(fundingStream);
            return fundingStream;
        }

        public async Task<FundingStream> UpdateFundingStream(FundingStream fundingStream)
        {
            await _fundingStreamRepository.UpdateFundingStream(fundingStream);
            return fundingStream;
        }

        public Task DeleteFundingStream(Guid id)
        {
            return _fundingStreamRepository.DeleteFundingStream(id);
        }

        public Task<QueryResult<FundingStream>> QueryFundingStream(Query query)
        {
            return _fundingStreamRepository.QueryFundingStream(query);
        }
    }
}
