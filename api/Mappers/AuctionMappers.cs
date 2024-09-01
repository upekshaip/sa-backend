using System;
using api.Dtos.Auctions;
using api.Models;

namespace api.Mappers;

public static class AuctionMappers
{
    public static AuctionDto ToAuctionsDtoGet(this Auction auctionsModel)
    {
        return new AuctionDto
        {
            AuctionId = auctionsModel.AuctionId,
            Title = auctionsModel.Title,
            Description = auctionsModel.Description,
            AuctionImage = auctionsModel.AuctionImage,
            AuctionCategory = auctionsModel.AuctionCategory,
            SellerId = auctionsModel.SellerId,
            StartTime = auctionsModel.StartTime,
            EndTime = auctionsModel.EndTime,
            Status = auctionsModel.Status,
            CreatedAt = auctionsModel.CreatedAt,
            UpdatedAt = auctionsModel.UpdatedAt
        };
    }
}
