using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;

namespace MarketPlace.Infastructure.Data.Repository;

public class CardRepository:BaseRepository<UserProductCard>,ICardRepository
{
	public CardRepository(AppDbContext context):base(context)
	{

	}
}
