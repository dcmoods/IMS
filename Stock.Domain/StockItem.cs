﻿using SharedKernel.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Stock.Domain
{
    public class StockItem
    {

        private readonly List<ItemEntry> _itemEntries;

        public StockItem()
        {
            _itemEntries = new List<ItemEntry>();
        }

        [Key]
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MinimumLevel { get; set; }
        public double MaximumLevel { get; set; }
        public string LevelUnit { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ItemEntry> ItemEntries
        {
            get { return _itemEntries; }
        }


        public void AddItemEntries(List<ItemEntry> itemEntries)
        {
            foreach(ItemEntry item in itemEntries)
            {
                _itemEntries.Add(item);
            }
        }

        public double TotalOnHand
        {
            get
            {
                return _itemEntries.Sum(s => s.Quantity);
            }
        }

        public decimal TotalValuation
        {
            get
            {
                return _itemEntries.Sum(s => s.PricePerUnit * Convert.ToDecimal(s.Quantity));
            }
        }

        public void UseStockItem()
        {
            var soonestExpirationDate = _itemEntries.Select(ie => ie.ExpirationDate).Min();
            var itemEntry = _itemEntries.FirstOrDefault(ie => ie.ExpirationDate == soonestExpirationDate);
            itemEntry.UseSingleItem();
        }

        public void InsertNewItemEntry(double quantity, decimal pricePerUnit, DateTime expirationDate, string temperature = "")
        {
            var itemEntry = ItemEntry.Create(this.StockItemId, quantity, pricePerUnit, expirationDate, temperature = "");
            _itemEntries.Add(itemEntry);
        }
    }
}
