using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeMug.Models;
using CoffeeMug.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMug.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;   
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_productRepository.GetProductsList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                if (Guid.Empty.Equals(id))
                    return NotFound("The guide couldn't be empty.");

                var product = _productRepository.GetProductById(id);

                if (product == null)
                    return NotFound("The product record couldn't be found.");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        public IActionResult Post([FromBody][Bind("Name, Price")]Product product)
        {
            try
            {
                if (product == null)
                    return NotFound("Product is null.");

                if (!ModelState.IsValid)
                    return BadRequest($"Errors: {ErrorMessageManager.ShowErrors(ModelState)}");

                var result = _productRepository.Add(product);

                return Created(nameof(Get), result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Product product)
        {
            try
            {
                if (product == null)
                    return NotFound("Product is null.");

                if (Guid.Empty.Equals(id))
                    return NotFound("The guide couldn't be empty.");

                if (!ModelState.IsValid)
                    return BadRequest($"Errors: {ErrorMessageManager.ShowErrors(ModelState)}");

                var productToUpdate = _productRepository.GetProductById(id);

                if (productToUpdate == null)
                    return NotFound("Product about given id doesn't exist.");

                _productRepository.Update(productToUpdate, product);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (Guid.Empty.Equals(id))
                    return BadRequest("The guide couldn't be empty.");

                _productRepository.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
