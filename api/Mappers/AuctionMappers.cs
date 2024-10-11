using System;
using api.Dtos.Auctions;
using api.Dtos.Users;
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
            StartingBid = auctionsModel.StartingBid,
            AuctionCategory = auctionsModel.AuctionCategory,
            SellerId = auctionsModel.SellerId,
            StartTime = auctionsModel.StartTime,
            EndTime = auctionsModel.EndTime,
            Status = auctionsModel.Status,
            IsLive = auctionsModel.IsLive,
            CreatedAt = auctionsModel.CreatedAt,
            UpdatedAt = auctionsModel.UpdatedAt
        };
    }
}
