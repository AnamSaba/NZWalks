﻿namespace NZWalks.API.Models.DTO
{
    public class AddRegionDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}