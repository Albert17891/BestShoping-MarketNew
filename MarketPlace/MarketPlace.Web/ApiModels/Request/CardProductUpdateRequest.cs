﻿namespace MarketPlace.Web.ApiModels.Request;

public class CardProductUpdateRequest
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double SumPrice { get; set; }

}
