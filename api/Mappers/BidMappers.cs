using System;
using api.Dtos.Bids;
using api.Models;

namespace api.Mappers;

public static class BidMappers
{
    public static BidDto ToBidDtoGet(this Bid bidsModel)
    {
        return new BidDto
        {
            BidId = bidsModel.BidId,
            AuctionId = bidsModel.AuctionId,
            BidderId = bidsModel.BidderId,
            BidderName = bidsModel.BidderName,
            BidAmount = bidsModel.BidAmount,
            CreatedAt = bidsModel.CreatedAt,
            UpdatedAt = bidsModel.UpdatedAt
        };
    }

}
