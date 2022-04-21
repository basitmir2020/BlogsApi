using AutoMapper;
using BlogsApi.Data.Enum;
using BlogsApi.Data.Response;
using BlogsApi.Dtos;
using BlogsApi.IRepository;
using BlogsApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryController> _logger;
        private IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork,ILogger<CategoryController> logger,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CreateCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTOS categoryDTOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(ResponseCode.Error, "Model Error", ModelState));
            }

            var category =  _mapper.Map<Category>(categoryDTOS);
            category.CategoryStatus = 0;
            category.CreatedOn = DateTime.Now;
            category.UpdatedOn = DateTime.Now;

            if ((await _unitOfWork.Categories.GetAsync(x=>x.Slug == category.Slug)) == null)
            {
                await _unitOfWork.Categories.InsertAsync(category);
                await _unitOfWork.Save();
            }
            else{
                return BadRequest(new ResponseModel(ResponseCode.Error, "Category Already Exist", null));
            }

            return Ok(new ResponseModel(ResponseCode.Ok, "Category Created", null));
        }

        [HttpGet]
        [Route("ShowCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var result = _mapper.Map<IList<ShowCategoryDTOS>>(categories);
            return Ok(new ResponseModel(ResponseCode.Ok, "Detail", result));
        }

        [HttpPut("{id:int}")]
        [Route("UpdateCategory/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategories(int id, [FromBody] CategoryDTOS categoryDTOS)
        {
            if(!ModelState.IsValid && id < 1)
            {
                return BadRequest(new ResponseModel(ResponseCode.Error, "Error", ModelState));
            }

            var categories = await _unitOfWork.Categories.GetAsync(c => c.Id == id);
            if(categories == null)
            {
                return BadRequest(new ResponseModel(ResponseCode.Error, "No Cateory Associated With This Id", null));
            }

            _mapper.Map(categoryDTOS,categories);
            _unitOfWork.Categories.UpdateAsync(categories);
            await _unitOfWork.Save();
            return Ok(new ResponseModel(ResponseCode.Ok, "Category Updated", null));
        }

        [HttpDelete("{id:int}")]
        [Route("DeleteCategory/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            if(id < 1)
            {
                return BadRequest(new ResponseModel(ResponseCode.Error, "Id Cannot Be Zero", null));
            }

            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == id);
            if(category == null)
            {
                return BadRequest(new ResponseModel(ResponseCode.Error, "Category Not Present With This Id", null));
            }

            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.Save();
            return Ok(new ResponseModel(ResponseCode.Ok, "Category Deleted!", null));
        }
    }
}
