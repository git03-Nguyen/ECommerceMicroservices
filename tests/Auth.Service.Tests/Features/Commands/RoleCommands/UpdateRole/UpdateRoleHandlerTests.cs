using Auth.Service.Features.Commands.RoleCommands.UpdateRole;

namespace Auth.Service.Tests.Features.Commands.RoleCommands.UpdateRole;

[TestFixture]
public class UpdateRoleHandlerTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private Mock<RoleManager<ApplicationRole>> _roleManagerMock;
    private UpdateRoleHandler _handler;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    [SetUp]
    public void Setup()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _identityServiceMock.Setup(x => x.IsUserAdmin()).Returns(true);

        _roleManagerMock = new Mock<RoleManager<ApplicationRole>>(
            Mock.Of<IRoleStore<ApplicationRole>>(),
            null, null, null, null);

        _cancellationToken = new CancellationToken();
        _fixture = new Fixture();


        _handler = new UpdateRoleHandler(new Mock<ILogger<UpdateRoleHandler>>().Object, _roleManagerMock.Object,
            _identityServiceMock.Object);
    }


}