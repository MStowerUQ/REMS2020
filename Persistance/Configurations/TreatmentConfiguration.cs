﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> builder)
        {
            builder.HasKey(e => e.TreatmentId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.TreatmentId)
                .HasName("TreatmentId")
                .IsUnique();

            builder.Property(e => e.ExperimentId)
                .HasDefaultValueSql("0");

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.HasOne(d => d.Experiment)
                .WithMany(p => p.Treatments)
                .HasForeignKey(d => d.ExperimentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TreatmentExperimentId");
        }
    }
}
