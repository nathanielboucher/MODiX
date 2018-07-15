﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modix.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Modix.Data.Migrations
{
    [DbContext(typeof(ModixContext))]
    [Migration("20180715020007_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Modix.Data.Models.Core.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTimeOffset>("FirstSeen");

                    b.Property<DateTimeOffset>("LastSeen");

                    b.Property<string>("Nickname");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Modix.Data.Models.Moderation.InfractionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreateActionId");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<TimeSpan?>("Duration");

                    b.Property<long?>("RescindActionId");

                    b.Property<long>("SubjectId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CreateActionId")
                        .IsUnique();

                    b.HasIndex("RescindActionId")
                        .IsUnique();

                    b.HasIndex("SubjectId");

                    b.ToTable("Infractions");
                });

            modelBuilder.Entity("Modix.Data.Models.Moderation.ModerationActionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<long>("CreatedById");

                    b.Property<long?>("InfractionId");

                    b.Property<string>("Reason")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("InfractionId");

                    b.ToTable("ModerationActions");
                });

            modelBuilder.Entity("Modix.Data.Models.Moderation.ModerationConfigEntity", b =>
                {
                    b.Property<long>("GuildId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<long>("MuteRoleId");

                    b.HasKey("GuildId");

                    b.ToTable("ModerationConfigs");
                });

            modelBuilder.Entity("Modix.Data.Models.Promotion.PromotionCampaignEntity", b =>
                {
                    b.Property<long>("PromotionCampaignId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("PromotionForId");

                    b.Property<DateTimeOffset>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("PromotionCampaignId");

                    b.HasIndex("PromotionForId");

                    b.ToTable("PromotionCampaigns");
                });

            modelBuilder.Entity("Modix.Data.Models.Promotion.PromotionCommentEntity", b =>
                {
                    b.Property<long>("PromotionCommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTimeOffset>("PostedDate");

                    b.Property<long?>("PromotionCampaignId");

                    b.Property<int>("Sentiment");

                    b.HasKey("PromotionCommentId");

                    b.HasIndex("PromotionCampaignId");

                    b.ToTable("PromotionComments");
                });

            modelBuilder.Entity("Modix.Data.Models.Moderation.InfractionEntity", b =>
                {
                    b.HasOne("Modix.Data.Models.Moderation.ModerationActionEntity", "CreateAction")
                        .WithOne("CreatedInfraction")
                        .HasForeignKey("Modix.Data.Models.Moderation.InfractionEntity", "CreateActionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Modix.Data.Models.Moderation.ModerationActionEntity", "RescindAction")
                        .WithOne("RescindedInfraction")
                        .HasForeignKey("Modix.Data.Models.Moderation.InfractionEntity", "RescindActionId");

                    b.HasOne("Modix.Data.Models.Core.UserEntity", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Modix.Data.Models.Moderation.ModerationActionEntity", b =>
                {
                    b.HasOne("Modix.Data.Models.Core.UserEntity", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Modix.Data.Models.Moderation.InfractionEntity", "Infraction")
                        .WithMany()
                        .HasForeignKey("InfractionId");
                });

            modelBuilder.Entity("Modix.Data.Models.Promotion.PromotionCampaignEntity", b =>
                {
                    b.HasOne("Modix.Data.Models.Core.UserEntity", "PromotionFor")
                        .WithMany()
                        .HasForeignKey("PromotionForId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Modix.Data.Models.Promotion.PromotionCommentEntity", b =>
                {
                    b.HasOne("Modix.Data.Models.Promotion.PromotionCampaignEntity", "PromotionCampaign")
                        .WithMany("Comments")
                        .HasForeignKey("PromotionCampaignId");
                });
#pragma warning restore 612, 618
        }
    }
}
