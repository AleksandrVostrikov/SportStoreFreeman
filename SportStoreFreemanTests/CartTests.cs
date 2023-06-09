﻿using Microsoft.Build.Execution;
using SportStoreFreeman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreFreemanTests
{
    public class CartTests
    {
        [Fact]
        public void CanAddNewLine()
        {
            Product p1 = new() { ProductId = Guid.NewGuid(), Name = "P1" };
            Product p2 = new() { ProductId = Guid.NewGuid(), Name = "P2" };

            Cart target = new();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines
                .OrderBy(p => p.Product.Name)
                .ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void CadAddQuantityForExistingLines()
        {
            Product p1 = new() { ProductId = Guid.NewGuid(), Name = "P1" };
            Product p2 = new() { ProductId = Guid.NewGuid(), Name = "P2" };

            Cart target = new();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines
                .OrderBy(c => c.Product.Name)
                .ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void CanRemoveLine()
        {
            Product p1 = new() { ProductId = Guid.NewGuid(), Name = "P1" };
            Product p2 = new() { ProductId = Guid.NewGuid(), Name = "P2" };
            Product p3 = new() { ProductId = Guid.NewGuid(), Name = "P3" };

            Cart target = new();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            target.RemoveLine(p2);

            Assert.Empty(target.Lines.Where(p => p.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void CanCalculateTotal()
        {
            Product p1 = new() { ProductId = Guid.NewGuid(), Name = "P1" , Price = 100M};
            Product p2 = new() { ProductId = Guid.NewGuid(), Name = "P2" , Price = 50M};

            Cart target = new();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            decimal result = target.ComputeTotalValue();

            Assert.Equal(450M, result);
        }

        [Fact]
        public void CanClearContents()
        {
            Product p1 = new() { ProductId = Guid.NewGuid(), Name = "P1", Price = 100M };
            Product p2 = new() { ProductId = Guid.NewGuid(), Name = "P2", Price = 50M };

            Cart target = new();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            target.Clear();

            Assert.Empty(target.Lines);
        }

       
    }
}
