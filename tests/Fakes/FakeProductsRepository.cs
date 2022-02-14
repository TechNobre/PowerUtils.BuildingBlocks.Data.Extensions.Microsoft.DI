using PowerUtils.BuildingBlocks.Data.Repositories;

namespace PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI.Tests.Fakes;

public interface IFakeProductsRepository : IRepositoryBase { }

internal class FakeProductsRepository : IFakeProductsRepository { }
