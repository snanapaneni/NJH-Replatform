// Copyright (c) NJH. All rights reserved.

namespace Njh.Mvc.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Njh.Mvc.Models;

/// <summary>
/// Implements the controller that page requests.
/// </summary>
public class HomeController
    : Controller
{
    private readonly ILogger<HomeController> logger;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    public HomeController(
        ILogger<HomeController> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Displays the home page.
    /// </summary>
    /// <returns>
    /// The action result.
    /// </returns>
    public IActionResult Index()
    {
        return this.View();
    }

    /// <summary>
    /// Displays the error page.
    /// </summary>
    /// <returns>
    /// The action result.
    /// </returns>
    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return this.View(
            new ErrorViewModel
            {
                RequestId =
                    Activity.Current?.Id ??
                    this.HttpContext.TraceIdentifier,
            });
    }
}