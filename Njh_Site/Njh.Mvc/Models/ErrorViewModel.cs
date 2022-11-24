// Copyright (c) NJH. All rights reserved.

namespace Njh.Mvc.Models;

/// <summary>
/// Defines the error view model.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Gets or sets the request ID.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Gets a value indicating whether
    /// the request ID should be displayed.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
}