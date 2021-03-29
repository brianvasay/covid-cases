using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Entities;
using Application.Entities.Dtos;
using Application.Errors;
using Application.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CasesController : BaseApiController
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;

        public CasesController(ICaseRepository caseRepository,
            IMapper mapper)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListConfirmedCasesAsync()
        {
            var confirmedCases = await _caseRepository.GetCasesAsync();

            if (confirmedCases == null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<IReadOnlyList<Case>, IReadOnlyList<CaseToReturnDto>>(confirmedCases));
        }

        [HttpGet("top/confirmed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListConfirmedCasesAsync(
            [FromQuery] string observationDate,
            [FromQuery] int maxResults = 10)
        {
            var confirmedCases = await _caseRepository.GetCasesAsync(observationDate, maxResults);

            if (confirmedCases == null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<IReadOnlyList<Case>, IReadOnlyList<CaseToReturnDto>>(confirmedCases));
        }
    }
}
