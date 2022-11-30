﻿using Domain.FlowerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.SQLite;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class FlowerTypeEntityConfiguration : IEntityTypeConfiguration<Flower>
{
    public void Configure(EntityTypeBuilder<Flower> builder)
    {
        builder
            .Property(flower => flower.Name)
            .UseCollation(CollationTypeEnum.NoCase.ToString())
            .IsRequired();

        builder
            .HasIndex(flower => flower.Name)
            .IsUnique();

        builder
            .Property(flower => flower.Type)
            .IsRequired();
    }
}
