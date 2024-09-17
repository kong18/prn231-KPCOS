﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KPCOS.Data.Models;

public partial class Project
{
    public string Id { get; set; }

    public string Status { get; set; }

    public string CustomerId { get; set; }

    public decimal EstimatedCost { get; set; }

    public decimal? ActualCost { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string DesignerId { get; set; }

    public string ConstructionStaffId { get; set; }

    public virtual Employee ConstructionStaff { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<DesignConcept> DesignConcepts { get; set; } = new List<DesignConcept>();

    public virtual Employee Designer { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}