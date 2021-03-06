﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class CatalogController : ControllerBase
  {
    private readonly IProductRepository _repository;

    public CatalogController(IProductRepository repository)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      var products = await _repository.GetProducts();
      return Ok(products);
    }
  }
}
