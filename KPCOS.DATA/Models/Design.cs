﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KPCOS.Data.Models;

public partial class Design
{
    public string Id { get; set; }

    public string DesignType { get; set; }

    public string Shape { get; set; }

    public string Location { get; set; }

    public decimal MinLength { get; set; }

    public decimal MinWidth { get; set; }

    public decimal Depth { get; set; }

    public decimal WaterLevel { get; set; }

    public string FiltrationSystem { get; set; }

    public string WaterQuality { get; set; }

    public string KoiType { get; set; }

    public string KoiCountRange { get; set; }

    public decimal Budget { get; set; }

    public string CustomerId { get; set; }

    public string TemplateId { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string ConsultantBy { get; set; }

    public string Note { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual Customer Customer { get; set; }

    public virtual ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();

    public virtual DesignTemplate Template { get; set; }
}