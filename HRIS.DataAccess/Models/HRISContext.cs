﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HRIS.DataAccess.Models
{
    public partial class HRISContext : DbContext
    {
        public HRISContext()
        {
        }

        public HRISContext(DbContextOptions<HRISContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Competency> Competencies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeShiftGroupDemo> EmployeeShiftGroupDemos { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
        public virtual DbSet<Interview> Interviews { get; set; }
        public virtual DbSet<PartTimeEmployee> PartTimeEmployees { get; set; }
        public virtual DbSet<PayrollReport> PayrollReports { get; set; }
        public virtual DbSet<ShiftGroup> ShiftGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-IHVN9CGH;Database=HRIS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.Property(e => e.ApplyDate).HasColumnType("date");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BirthPlace)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IdcardNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDCardNumber");

                entity.Property(e => e.JobApply)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SallaryRequest).HasColumnType("money");
            });

            modelBuilder.Entity<Competency>(entity =>
            {
                entity.ToTable("Competency");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.Competencies)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competency_Employee");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HeadDepartment)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Education");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EducationLevel)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.InstitutionName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.LastValue).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Education_Employee");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber);

                entity.ToTable("Employee");

                entity.Property(e => e.CandidateId).HasColumnName("CandidateID");

                entity.Property(e => e.HiredDate).HasColumnType("date");

                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Candidate");
            });

            modelBuilder.Entity<EmployeeShiftGroupDemo>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeNumber, e.ShiftGroupId })
                    .HasName("pk_myConstraint");

                entity.ToTable("EmployeeShiftGroupDemo");

                entity.Property(e => e.ShiftGroupId).HasColumnName("ShiftGroupID");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.EmployeeShiftGroupDemos)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeShiftGroupDemo_Employee1");

                entity.HasOne(d => d.ShiftGroup)
                    .WithMany(p => p.EmployeeShiftGroupDemos)
                    .HasForeignKey(d => d.ShiftGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeShiftGroupDemo_ShiftGroup");
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.ToTable("Experience");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.Experiences)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Experience_Employee");
            });

            modelBuilder.Entity<FullTimeEmployee>(entity =>
            {
                entity.ToTable("FullTimeEmployee");

                entity.Property(e => e.Allowance).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.FullTimeEmployees)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FullTimeEmployee_Employee");
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.ToTable("Interview");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CandidateId).HasColumnName("CandidateID");

                entity.Property(e => e.InterviewDate).HasColumnType("datetime");

                entity.Property(e => e.Pic)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PIC");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_Candidate");
            });

            modelBuilder.Entity<PartTimeEmployee>(entity =>
            {
                entity.ToTable("PartTimeEmployee");

                entity.Property(e => e.Wages).HasColumnType("money");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.PartTimeEmployees)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartTimeEmployee_Employee");
            });

            modelBuilder.Entity<PayrollReport>(entity =>
            {
                entity.ToTable("PayrollReport");

                entity.Property(e => e.MonthIncome).HasColumnType("money");

                entity.Property(e => e.PayrollGroup)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.PayrollReports)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PayrollReport_Employee");
            });

            modelBuilder.Entity<ShiftGroup>(entity =>
            {
                entity.ToTable("ShiftGroup");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
