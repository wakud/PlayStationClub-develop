using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using PlayStationClub.Infrastructure.ViewModels;

namespace PlayStationClub.Pages.Game
{
    public class GamesModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ICollection<CategoryViewModel> Categories { get; set; }
        public ICollection<GameViewModel> Games { get; set; }
        //add
        public IEnumerable<CategoryViewModel> Category { get; private set; }
        public IEnumerable<GameViewModel> GameWithCategory { get; set; }

        public GamesModel(IGameService gameService, ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _gameService = gameService;
            _categoryService = categoryService;
        }

        public async Task OnGet(int id)
        {
            Categories = _mapper.Map<ICollection<CategoryViewModel>>(await _categoryService.GetAllAsync());
            Categories.Add(new CategoryViewModel { Id = 0, Name = "Все" });
            Games = _mapper.Map<ICollection<GameViewModel>>(await _gameService.GetAllAsync());
            //add
            Category = _mapper.Map<ICollection<CategoryViewModel>>(await _categoryService.GetAllAsync()).Where(c => c.Id == id);
            GameWithCategory = _mapper.Map<ICollection<GameViewModel>>(await _gameService.GetAllAsync()).Where(g => g.Categories.Any(c => c.Id == id));
        }
        public async Task<PartialViewResult> OnGetGameDetails(int id)
        {
            var game = _mapper.Map<GameViewModel>(await _gameService.GetByIdAsync(id));
            return new PartialViewResult
            {
                ViewName = "_GameDetails",
                ViewData = new ViewDataDictionary<GameViewModel>(ViewData, game)
            };
        }
    }
}
