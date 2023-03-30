using System;
using heroesBackend.models;

namespace heroesBackend.Repositories
{

	public interface IHeroesRepository
	{
        Task<Guid> NewHero(NewHeroModel newHeroModel);
        Task<List<HeroModel>> GetAllAvailableHeroes();
        Task<bool> AddHeroToTrainer(Guid heroId, string userName);
        Task<List<HeroModel>> GetAllHeroesByUserName(string userName);
        Task<decimal?> TrainHero(Guid heroId, string userName);

    }

}

