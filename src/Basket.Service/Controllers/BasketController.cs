using Basket.Service.Data.Models;
using Basket.Service.Models.Dtos;
using Basket.Service.Models.Requests;
using Basket.Service.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Service.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public BasketController(ILogger<BasketController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var baskets = _unitOfWork.BasketRepository.GetAll();
        return Ok(baskets);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var basket = await _unitOfWork.BasketRepository.GetByIdAsync(id);
        if (basket == null)
        {
            return NotFound();
        }
        
        // get all basket items
        var basketItems = _unitOfWork.BasketItemRepository.GetByCondition(x => x.BasketId == id);
        basket.BasketItems = basketItems.ToList();
        
        return Ok(basket);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddBasketRequest request)
    {
        var basket = new Data.Models.Basket
        {
            BuyerId = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11),
            BasketItems = new List<BasketItem>()
        };
        
        await _unitOfWork.BasketRepository.AddAsync(basket);
        await _unitOfWork.SaveChangesAsync();
        
        return Ok(basket);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Check if basket exists
        var basket = await _unitOfWork.BasketRepository.GetByIdAsync(id);
        if (basket == null)
        {
            return NotFound();
        }

        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            // Delete basket items
            for (int i = 0; i < basket.BasketItems.Count; i++)
            {
                _unitOfWork.BasketItemRepository.RemoveRange(basket.BasketItems);
            }
            
            // Delete basket
            _unitOfWork.BasketRepository.Remove(basket);
            
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "An error occurred while deleting the basket.");
            return StatusCode(500);
        }
        
        return Ok();
    }
    
    // Add a new item to the basket
    [HttpPost("{id}")]
    public async Task<IActionResult> AddItem(int id, [FromBody] AddBasketItemRequest request)
    {
        // Check if basket exists
        var basket = await _unitOfWork.BasketRepository.GetByIdAsync(id);
        if (basket == null)
        {
            return NotFound();
        }
        
        // CHeck if the same item already exists in the basket
        var existingItem = _unitOfWork.BasketItemRepository.GetByCondition(x => x.BasketId == id && x.ProductId == request.ProductId).FirstOrDefault();
        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
            _unitOfWork.BasketItemRepository.Update(existingItem);
            await _unitOfWork.SaveChangesAsync();
            return Ok(existingItem);
        }
        
        var item = new BasketItem
        {
            BasketId = id,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };
        
        
        
        await _unitOfWork.BasketItemRepository.AddAsync(item);
        await _unitOfWork.SaveChangesAsync();
        
        return Ok(item);
    }
    
    // Remove an item from the basket
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveItem(int id)
    {
        // Check if basket item exists
        var item = await _unitOfWork.BasketItemRepository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        
        _unitOfWork.BasketItemRepository.Remove(item);
        await _unitOfWork.SaveChangesAsync();
        
        return Ok();
    }
    
    // Update an item in the basket
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateBasketItemRequest request)
    {
        // Check if basket item exists
        var item = await _unitOfWork.BasketItemRepository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        
        item.Quantity = request.Quantity;
        _unitOfWork.BasketItemRepository.Update(item);
        await _unitOfWork.SaveChangesAsync();
        
        return Ok(item);
    }
    
    
}