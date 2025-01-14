using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using System.Security.Claims;

namespace Restaurants.Application.Tests.Users
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            DateOnly dateOfBirth = new(1990, 1, 1);

            Mock<IHttpContextAccessor>? httpContextAccessorMock = new();

            List<Claim> claims =
            [
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, "test@test.com"),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User),
                new("Nationality", "German"),
                new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
            ];

            ClaimsPrincipal user = new(new ClaimsIdentity(claims, "Test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            UserContext userContext = new(httpContextAccessorMock.Object);

            // act
            CurrentUser? currentUser = userContext.GetCurrentUser();

            currentUser.Should().NotBeNull();
            currentUser!.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("German");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);
        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            Mock<IHttpContextAccessor>? httpContextAccessorMock = new();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)default!);

            UserContext? userContext = new(httpContextAccessorMock.Object);

            // act
            Action action = () => userContext.GetCurrentUser();

            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }
    }
}