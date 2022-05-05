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
        //add new colections
        public IEnumerable<CategoryViewModel> Category { get; private set; }
        public IEnumerable<GameViewModel> GameWithCategory { get; set; }
        public GameViewModel SelectedGame { get; set; } = new();
        //end add
        public GamesModel(IGameService gameService, ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _gameService = gameService;
            _categoryService = categoryService;
        }

        public async Task OnGet(int categoryId, int gameId)
        {
            Categories = _mapper.Map<ICollection<CategoryViewModel>>(await _categoryService.GetAllAsync());
            Categories.Add(new CategoryViewModel { Id = 0, Name = "Все" }); //add new category "All"
            Games = _mapper.Map<ICollection<GameViewModel>>(await _gameService.GetAllAsync());
            //add
            Category = _mapper.Map<ICollection<CategoryViewModel>>(await _categoryService.GetAllAsync())
                .Where(c => c.Id == categoryId);    //one selected category
            GameWithCategory = _mapper.Map<ICollection<GameViewModel>>(await _gameService.GetAllAsync())
                .Where(g => g.Categories.Any(c => c.Id == categoryId)); //games of the selected category
        }

        public async Task<PartialViewResult> OnGetGameDetails(int gameId)
        {
            SelectedGame = _mapper.Map<GameViewModel>(await _gameService.GetByIdAsync(gameId));
            return new PartialViewResult
            {
                ViewName = "GameDetails",
                ViewData = new ViewDataDictionary<GameViewModel>(ViewData, SelectedGame)
            };
        }
    }
}
