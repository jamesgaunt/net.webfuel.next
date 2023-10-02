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
            return await _fundingStreamRepository.InsertFundingStream(fundingStream);
        }

        public async Task<FundingStream> UpdateFundingStream(FundingStream fundingStream)
        {
            return await _fundingStreamRepository.UpdateFundingStream(fundingStream);
        }

        public async Task DeleteFundingStream(Guid id)
        {
            await _fundingStreamRepository.DeleteFundingStream(id);
        }

        public async Task<QueryResult<FundingStream>> QueryFundingStream(Query query)
        {
            return await _fundingStreamRepository.QueryFundingStream(query);
        }
    }
}
