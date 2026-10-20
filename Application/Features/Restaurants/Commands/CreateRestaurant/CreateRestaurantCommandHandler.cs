using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities;
using MediatR;

namespace DeliveryApp.Application.Features.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateRestaurantCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = new Restaurant
            {
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync(cancellationToken);

            return restaurant.Id;
        }
    }
}